using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace BANDPAR_RestAPI.DataModels
{
    public class Vendas
    {
        [Key]
        public int Id { get; set; }
        public int? codigoVenda { get; set; }
        public DateTime? dataVenda { get; set; }
        public string? nomeProduto { get; set; }
        public float? valorProduto { get; set; }
        public float? quantidadeProduto { get; set; }
        public float? valorTotal { get; set; }

        //public Produtos? Produto { get; set; } // Propriedade de navegação
    }
}
