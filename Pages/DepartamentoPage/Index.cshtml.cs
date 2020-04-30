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

namespace Prj_GestionPDC_OASG.Pages.DepartamentoPage
{
    public class IndexModel : PageModel
    {
        private readonly Prj_GestionPDC_OASG.Data.Prj_GestionPDC_OASGContext _context;
        public string NameSort { get; set; }

        public string CodeSort { get; set; }
        public string PaisSort { get; set; }

        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public ListaPaginada<Departamento> Departamento { get; set; }
        public IndexModel(Prj_GestionPDC_OASG.Data.Prj_GestionPDC_OASGContext context)
        {
            _context = context;
        }

        //public IList<Departamento> Departamento { get;set; }
        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            NameSort = (sortOrder == "Nombre") ? "Nombre_desc" : "Nombre";
            CodeSort = (sortOrder == "Codigo") ? "Codigo_desc" : "Codigo";
            PaisSort = (sortOrder == "Pais") ? "Pais_desc" : "Pais";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            CurrentFilter = searchString;
            IQueryable<Departamento> departamentosIQ = _context.Departamento
                .Include(c => c.Pais)
                .AsQueryable();
            if (!String.IsNullOrEmpty(searchString))
            {
                departamentosIQ = departamentosIQ.Where(s => s.Nombre.Contains(searchString)
                                        || s.Codigo.Contains(searchString)
                                        || s.Pais.Nombre.Contains(searchString)
                                        );
            }
            switch (sortOrder)
            {
                case "Nombre":
                    departamentosIQ = departamentosIQ.OrderBy(s => s.Nombre);
                    break;
                case "Nombre_desc":
                    departamentosIQ = departamentosIQ.OrderByDescending(s => s.Nombre);
                    break;
                case "Codigo":
                    departamentosIQ = departamentosIQ.OrderBy(s => s.Codigo);
                    break;
                case "Pais":
                    departamentosIQ = departamentosIQ.OrderBy(s => s.Pais.Nombre);
                    break;
                case "Pais_desc":
                    departamentosIQ = departamentosIQ.OrderByDescending(s => s.Pais.Nombre);
                    break;
                default:
                    departamentosIQ = departamentosIQ.OrderBy(s => s.Id);
                    break;
            }
            int pageSize = 4;
            Departamento = await ListaPaginada<Departamento>.CreateAsync(
                departamentosIQ.AsNoTracking(), pageIndex ?? 1, pageSize);

        }
               
        //    Departamento = await _context.Departamento
          //      .Include(d => d.Pais).ToListAsync();
        
    }
}
