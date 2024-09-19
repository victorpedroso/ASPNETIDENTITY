using System.ComponentModel.DataAnnotations;

namespace SecurityWEB.Entities
{
    public class Aluno
    {
        public int Id { get; set; }
        [Required, MaxLength(80, ErrorMessage = "Nome não pode exceder 80 caracteres")]
        public string? Nome { get; set; }
        public int SophiaId { get; set; }
        public int SophiaRm {  get; set; }
        public DateTime DataCadastro { get; set; }
        public string? PathFoto { get; set; }
        public string? UrlFoto { get; set; }
    }
}
 