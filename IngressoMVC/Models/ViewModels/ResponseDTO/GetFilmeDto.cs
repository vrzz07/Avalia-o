using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IngressoMVC.Models.ViewModels.ResponseDTO
{
    public class GetFilmeDto
    {
            [Display(Name = "Título")]
            public string Titulo { get; set; }

            [Display(Name = "Descrição")]
            public string Descricao { get; set; }

            [Display(Name = "Imagem")]
            public string ImageURL { get; set; }

            public List<string> NomeAtores { get; set; }
            public List<string> FotoURLAtores { get; set; }
    }
}