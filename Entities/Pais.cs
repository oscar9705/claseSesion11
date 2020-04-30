using System;
using System.Collections.Generic;

namespace Prj_GestionPDC_OASG.Entities
{
    public partial class Pais
    {
        public Pais()
        {
            Departamento = new HashSet<Departamento>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<Departamento> Departamento { get; set; }
    }
}
