using System;
using System.ComponentModel.DataAnnotations;

namespace ProjetoLAM.Models
{
    public class Genero
    {
        [Key]
        public Guid GeneroId { get; set; } = Guid.NewGuid();

        [Required]
        public string Nome { get; set; }
    }
}
