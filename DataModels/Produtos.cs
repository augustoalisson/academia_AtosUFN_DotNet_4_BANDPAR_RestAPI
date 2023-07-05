using System.Data.SqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BANDPAR_RestAPI.DataModels
{
    public class Produtos
    {
        [Key]
        public int Id { get; set; }
        public int codigo { get; set; }
        public string? descricao { get; set; }
        public string? marca { get; set; }
        public string? fornecedor { get; set; }
        public float? valor { get; set; }
        public float? quantidade { get; set; }
    }
}
