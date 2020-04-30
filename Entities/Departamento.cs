using System;
using System.Collections.Generic;

namespace Prj_GestionPDC_OASG.Entities
{
    public partial class Departamento
    {
        public Departamento()
        {
            Ciudad = new HashSet<Ciudad>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public int PaisId { get; set; }
        public bool Active { get; set; }

        public virtual Pais Pais { get; set; }
        public virtual ICollection<Ciudad> Ciudad { get; set; }
    }
}
