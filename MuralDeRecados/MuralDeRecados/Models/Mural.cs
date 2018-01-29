using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MuralDeRecados.Models
{
    public class Mural
    {
        public int MuralId { get; set; }

        [StringLength(200, MinimumLength = 3, ErrorMessage = "Favor informe um mínimo de 3 caracteres e máximo de 200 caracteres")]
        public string Texto { get; set; }

        public DateTime DataCriacao { get; set; }

        public int UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }

        public Mural()
        {

        }
    }
}