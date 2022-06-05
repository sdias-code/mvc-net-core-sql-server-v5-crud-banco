using System;

namespace WebApplicationMVC.Models
{
    public class TransacaoPoco
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string Nome { get; set; }        
        public string DescricaoTransacao { get; set; }
        public DateTime Data { get; set; }
        public double Valor { get; set; }
    }
}
