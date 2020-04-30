using System;
using System.Collections.Generic;

namespace Prj_GestionPDC_OASG.Entities
{
    public partial class Usuario
    {
        public Usuario()
        {
            RolUsuario = new HashSet<RolUsuario>();
        }

        public int Id { get; set; }
        public string Usuario1 { get; set; }
        public string Password { get; set; }

        public virtual ICollection<RolUsuario> RolUsuario { get; set; }
    }
}
