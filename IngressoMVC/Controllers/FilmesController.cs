using IngressoMVC.Data;
using IngressoMVC.Models;
using IngressoMVC.Models.ViewModels.RequestDTO;
using IngressoMVC.Models.ViewModels.ResponseDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IngressoMVC.Controllers
{
    public class FilmesController : Controller
    {
        private IngressoDbContext _context;

        public FilmesController(IngressoDbContext context)
        {
            _context = context;
        }

        public IActionResult Index() => View(_context.Filmes);

        public IActionResult Criar() => View();

        [HttpPost]
        IActionResult Criar(PostFilmeDTO filmeDto)
        {
            Filme filme = new Filme
                (
                    filmeDto.Titulo,
                    filmeDto.Descricao,
                    filmeDto.Preco,
                    filmeDto.ImageURL,
                    _context.Produtores
                        .FirstOrDefault(x => x.Id == filmeDto.ProdutorId).Id
                );

            _context.Add(filme);
            _context.SaveChanges();

            foreach (var categoria in filmeDto.CategoriasId)
            {
                int? categoriaId = _context.Categorias.Where(c => c.Id == categoria).FirstOrDefault().Id;

                if (categoriaId != null)
                {
                    var novaCategoria = new FilmeCategoria(filme.Id, categoriaId.Value);
                    _context.FilmesCategorias.Add(novaCategoria);
                    _context.SaveChanges();
                }
            }

            foreach (var atorId in filmeDto.AtoresId)
            {
                var novoAtor = new AtorFilme(atorId, filme.Id);
                _context.AtoresFilmes.Add(novoAtor);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detalhes(int id)
        {
            var resultado = _context.Filmes
                .Include(af => af.AtoresFilmes)
                .ThenInclude(f => f.Ator)
                .FirstOrDefault(ator => ator.Id == id);

            if (resultado == null)
                return View("NotFound");

            GetFilmeDto atorDTO = new GetFilmeDto()
            {
                Titulo = resultado.Titulo,
                Descricao = resultado.Descricao,
                ImageURL = resultado.ImageURL,
                FotoURLAtores = resultado.AtoresFilmes
                    .Select(af => af.Ator.FotoPerfilURL).ToList(),
                NomeAtores = resultado.AtoresFilmes
                    .Select(af => af.Ator.Nome).ToList()
            };

            return View(atorDTO);
        }

        public IActionResult Atualizar(int? id)
        {
            var result = _context.Filmes.FirstOrDefault(x => x.Id == id);

            if (result == null)
                return View("NotFound");

            return View(result);
        }

        [HttpPost]
        public IActionResult Atualizar(int id, PostFilmeDTO filmeDto)
        {
            var result = _context.Filmes.FirstOrDefault(x => x.Id == id);

            if (!ModelState.IsValid)
                return View(result);

            result.AlterarDados(
                filmeDto.Titulo,
                filmeDto.Descricao,
                filmeDto.Preco,
                filmeDto.ImageURL);

            _context.Update(result);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Deletar(int id)
        {
            //buscar o filme no banco
            var result = _context.Filmes.FirstOrDefault(x => x.Id == id);

            if (result == null)
                return View("NotFound");
            //passar o filme na view
            return View(result);
        }

        [HttpPost, ActionName("Deletar")]
        public IActionResult ConfirmarDeletar(int id)
        {
            var result = _context.Filmes.FirstOrDefault(x => x.Id == id);

            _context.Remove(result);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
