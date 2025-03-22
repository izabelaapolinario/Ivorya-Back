using System.ComponentModel.DataAnnotations;

namespace ivorya_back.Models
{
    public class Contato
    {
        [Key]
        public int IdContato { get; set;  }
        public string Nome { get; set; }
        public string Assunto { get; set; }
        public string Email { get; set; }
        public string Mensagem { get; set; }
        public DateTime DataEnvio { get; set; }

    }
}
