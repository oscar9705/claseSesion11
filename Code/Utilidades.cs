using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prj_GestionPDC_OASG.Code
{
    public class Utilidades
    {
        public static bool HasRolPermissions(string permisos, string codfun)
        {
            bool result = false;

            if (permisos.Split(',').Contains(codfun))
            {
                result = true;
            }
            return result;
        }
    }
}
