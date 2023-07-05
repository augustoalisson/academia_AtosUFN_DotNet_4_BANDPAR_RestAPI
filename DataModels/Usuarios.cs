using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BANDPAR_RestAPI.DataModels
{
    public class Usuarios
    {
        [Key]
        public int Id { get; set; }
        public string? usuario { get; set; }
        public string? senha { get; set; }
    }
}
