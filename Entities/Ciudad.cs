using System;
using System.Collections.Generic;

namespace Prj_GestionPDC_OASG.Entities
{
    public partial class Ciudad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public int DepartamentoId { get; set; }
        public bool Active { get; set; }

        public virtual Departamento Departamento { get; set; }
    }
}
