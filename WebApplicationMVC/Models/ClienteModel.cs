using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationMVC.Models
{
    public class ClienteModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo nome obrigatório!")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo Telefone obrigatório!")]
        public string Telefone { get; set; }
        [Required(ErrorMessage = "Campo Cpf obrigatório!")]
        public string Cpf { get; set; }        
    }
}
