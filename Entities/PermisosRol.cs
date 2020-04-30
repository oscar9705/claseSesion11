using System;
using System.Collections.Generic;

namespace Prj_GestionPDC_OASG.Entities
{
    public partial class PermisosRol
    {
        public int Id { get; set; }
        public int RolId { get; set; }
        public string CodigoFuncion { get; set; }
        public int Active { get; set; }

        public virtual Rol Rol { get; set; }
    }
}
