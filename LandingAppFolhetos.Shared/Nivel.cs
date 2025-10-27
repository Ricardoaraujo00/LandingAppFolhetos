using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingAppFolhetos.Shared
{
    public class Nivel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nome { get; set; } = "";
        public bool Activo { get; set; } = false;
        [NotMapped]
        public Local LocalAssociado { get; set; } = new Local();

     
        public List<Local> Locais { get; set; } = new List<Local>();
    }
}
