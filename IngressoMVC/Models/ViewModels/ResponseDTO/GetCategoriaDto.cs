using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IngressoMVC.Models.ViewModels.ResponseDTO
{
    public class GetCategoriaDto
    {
        [Display(Name = "Nome")]
        public string Nome { get; set; }
    }
}