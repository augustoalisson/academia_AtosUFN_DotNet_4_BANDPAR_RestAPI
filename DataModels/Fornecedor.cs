using System.ComponentModel.DataAnnotations;

namespace BANDPAR_RestAPI.DataModels
{
    public class Fornecedor
    {
        [Key]
        public int Id { get; set; }
        public string? nome { get; set; }
    }
}
