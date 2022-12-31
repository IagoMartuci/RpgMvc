using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RpgMvc.Models
{
    public class DisputaViewModel
    {
        public int Id { get; set; }
        public DateTime? DataDisputa { get; set; }
        public int AtacanteId { get; set; }
        public int OponenteId { get; set; }
        public string Narracao { get; set; }
        public int HabilidadeId { get; set; }        
        public List<int> ListaIdPersonagens { get; set; } = new List<int>();        
        public List<string> Resultados { get; set; } = new List<string>();
    }
}