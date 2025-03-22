using System.ComponentModel.DataAnnotations;

namespace ivorya_back.Models
{
    public class Contato
    {
        [Key]
        public int IdContato { get; set;  }
        public required  string Nome { get; set; }
        public required  string Assunto { get; set; }
        public required  string Email { get; set; }
        public required  string Mensagem { get; set; }
        public DateTime DataEnvio { get; set; } = DateTime.Now;
    }
}
