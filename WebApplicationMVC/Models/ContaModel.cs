using System.ComponentModel.DataAnnotations;

namespace WebApplicationMVC.Models
{
    public class ContaModel
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo Agencia obrigatório!")]     
        public int Agencia { get; set; }
        [Required(ErrorMessage = "Campo Agencia obrigatório!")]
        public int Numero { get; set; }
        [Required(ErrorMessage = "Campo Saldo obrigatório!")]
        public double Saldo { get; set; }
        
    }
}
