using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dcode.Caetering.Application.Dto
{
    public class LocalDto
    {
        public int LocalID { get; set; }

        public string Nombre { get; set; }

        public string Direccion { get; set; }

        public int Capacidad { get; set; }

        public string Fotos { get; set; }

        public string TerminosCondiciones { get; set; }

        public virtual ICollection<Evento> Evento { get; set; } = new List<Evento>();

    }

}
