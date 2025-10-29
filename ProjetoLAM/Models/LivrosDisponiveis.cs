using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoLAM.Models
{
    public class LivrosDisponiveis
    {
        [Key]
        public Guid LivrosDisponiveisId { get; set; } = Guid.NewGuid();

        [Required]
        public int Estoque { get; set; }

        [ForeignKey("Livros")]
        public Guid LivroId { get; set; }
        public Livros? Livros { get; set; }

        [ForeignKey("Genero")]
        public Guid GeneroId { get; set; }
        public Genero? Genero { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
