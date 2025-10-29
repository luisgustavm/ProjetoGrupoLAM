using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoLAM.Models
{
    public class Livros
    {
        [Key]
        public Guid LivroId { get; set; } = Guid.NewGuid();

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Autor { get; set; }

        [Required]
        public DateTime AnoPublicacao { get; set; }

        public string ISBN { get; set; }

        [ForeignKey("Genero")]
        public Guid GeneroId { get; set; }
        public Genero? Genero { get; set; }
    }
}
