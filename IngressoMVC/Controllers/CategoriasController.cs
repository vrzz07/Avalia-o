using IngressoMVC.Data;
using IngressoMVC.Models;
using IngressoMVC.Models.ViewModels.RequestDTO;
using IngressoMVC.Models.ViewModels.ResponseDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IngressoMVC.Controllers
{
    public class CategoriasController : Controller
    {
        private IngressoDbContext _context;

        public CategoriasController(IngressoDbContext context)
        {
            _context = context;
        }

        public IActionResult Index() => View(_context.Categorias);

        public IActionResult Criar() => View();

        [HttpPost]
        public IActionResult Criar(PostCategoriaDTO categoriaDto)
        {
            if(!ModelState.IsValid) return View(categoriaDto);
            Categoria categoria = new Categoria(categoriaDto.Nome);
            _context.Add(categoria);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detalhes(int id)
        {
            var resultado = _context.Categorias
                .FirstOrDefault(categoria => categoria.Id == id);

            if (resultado == null)
                return View("NotFound");

            GetCategoriaDto categoriaDTO = new GetCategoriaDto()
            {
                Nome = resultado.Nome
            };

            return View(categoriaDTO);
        }

        public IActionResult Atualizar(int? id)
        {
            if (id == null) return View("NotFound");

            var result = _context.Categorias.FirstOrDefault(a => a.Id == id);

            if (result == null) return View("NotFound");          
            
            return View(result);
        }

        [HttpPost]
        public IActionResult Atualizar(int id, PostCategoriaDTO categoriaDto)
        {
            var result = _context.Categorias.FirstOrDefault(a => a.Id == id);

            if (!ModelState.IsValid) return View(result);

            result.AtualizarDados(categoriaDto.Nome);
            _context.Categorias.Update(result);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Deletar(int id)
        {
            var result = _context.Categorias.FirstOrDefault(a => a.Id == id);
            if (result == null) return View();
            return View(result);
        }

        [HttpPost]
        public IActionResult ConfirmarDeletar(int id)
        {
            var result = _context.Categorias.FirstOrDefault(a => a.Id == id);
            _context.Categorias.Remove(result);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
