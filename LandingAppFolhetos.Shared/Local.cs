using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LandingAppFolhetos.Shared
{
    public class Local
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int NivelId { get; set; }
        public string Codigo { get; set; } = "";
        public string Nome { get; set; } = "";
        public int DependeDeId { get; set; } = 0;
        [NotMapped]
        public bool Selecionado { get; set; } = false;

        public Nivel? Nivel { get; set; }
    }
}
