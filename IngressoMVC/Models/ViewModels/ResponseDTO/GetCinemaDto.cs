using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IngressoMVC.Models.ViewModels.ResponseDTO
{
    public class GetCinemaDto
    {
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Logo")]
        public string LogoURL { get; set; }

        public List<string> NomeFilmes { get; set; }
        public List<string> FotoURLFilmes { get; set; }
    }
}