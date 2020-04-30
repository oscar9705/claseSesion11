using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Prj_GestionPDC_OASG.Code;
using Prj_GestionPDC_OASG.Data;
using Prj_GestionPDC_OASG.Entities;

namespace Prj_GestionPDC_OASG.Pages.CiudadPage
{
    public class IndexModel : PageModel
    {
        private readonly Prj_GestionPDC_OASG.Data.Prj_GestionPDC_OASGContext _context;
        public string NameSort { get; set; }

        public string CodeSort { get; set; }

        public string DepartamentoSort { get; set; }
        public string PaisSort { get; set; }

        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public ListaPaginada<Ciudad> Ciudad { get; set; }
        public IndexModel(Prj_GestionPDC_OASG.Data.Prj_GestionPDC_OASGContext context)
        {
            _context = context;
        }

        //public IList<Ciudad> Ciudad { get;set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            NameSort = (sortOrder == "Nombre") ? "Nombre_desc" : "Nombre";
            CodeSort = (sortOrder == "Codigo") ? "Codigo_desc" : "Codigo";
            DepartamentoSort = (sortOrder == "Departamento") ? "Departamento_desc" : "Departamento";
            PaisSort = (sortOrder == "Pais") ? "Pais_desc" : "Pais";
            if(searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            CurrentFilter = searchString;
            IQueryable<Ciudad> ciudadesIQ = _context.Ciudad
                .Include(c => c.Departamento)
                .Include(c => c.Departamento.Pais).AsQueryable();
            if (!String.IsNullOrEmpty(searchString))
            {
                ciudadesIQ = ciudadesIQ.Where(s => s.Nombre.Contains(searchString)
                                        || s.Codigo.Contains(searchString)
                                        || s.Departamento.Nombre.Contains(searchString)
                                        || s.Departamento.Pais.Nombre.Contains(searchString)
                                        );
            }
            switch (sortOrder)
            {
                case "Nombre":
                    ciudadesIQ = ciudadesIQ.OrderBy(s => s.Nombre);
                    break;
                case "Nombre_desc":
                    ciudadesIQ = ciudadesIQ.OrderByDescending(s => s.Nombre);
                    break;
                case "Codigo":
                    ciudadesIQ = ciudadesIQ.OrderBy(s => s.Codigo);
                    break;
                case "Pais":
                    ciudadesIQ = ciudadesIQ.OrderBy(s => s.Departamento.Pais.Nombre);
                    break;
                case "Pais_desc":
                    ciudadesIQ = ciudadesIQ.OrderByDescending(s => s.Departamento.Pais.Nombre);
                    break;
                case "Departamento":
                    ciudadesIQ = ciudadesIQ.OrderBy(s => s.Departamento.Nombre);
                    break;
                case "Departamento_desc":
                    ciudadesIQ = ciudadesIQ.OrderByDescending(s => s.Departamento.Nombre);
                    break;
                default:
                    ciudadesIQ = ciudadesIQ.OrderBy(s => s.Id);
                    break;
            }
            int pageSize = 10;
            Ciudad = await ListaPaginada<Ciudad>.CreateAsync(
                ciudadesIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
    
        }
       
    }
}
