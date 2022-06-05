using System;

namespace WebApplicationMVC.Models
{
    public class TransacaoModel
    {
        
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string Nome { get; set; }        
        public string TipoOperacao { get; set; }
        public DateTime Data { get; set; }
        public double Valor { get; set; }

    }
}
