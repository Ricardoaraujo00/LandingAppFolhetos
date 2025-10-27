using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingAppFolhetos.Shared
{
    public class RegistoLeituraFolheto
    {
        public int Id { get; set; }
        public int IdLocal { get; set; }
        public string IdUtilizador { get; set; } = default!;
        public int Leituras { get; set; }

    }
}
