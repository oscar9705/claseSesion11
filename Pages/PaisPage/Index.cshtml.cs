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

namespace Prj_GestionPDC_OASG.Pages.PaisPage
{
    public class IndexModel : PageModel
    {
        private readonly Prj_GestionPDC_OASG.Data.Prj_GestionPDC_OASGContext _context;

        public string NameSort { get; set; }

        public string CodeSort { get; set; }

        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public ListaPaginada<Pais> Pais { get; set; }
        public IndexModel(Prj_GestionPDC_OASG.Data.Prj_GestionPDC_OASGContext context)
        {
            _context = context;
        }

        // public IList<Pais> Pais { get;set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            NameSort = (sortOrder == "Nombre") ? "Nombre_desc" : "Nombre";
            CodeSort = (sortOrder == "Codigo") ? "Codigo_desc" : "Codigo";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            CurrentFilter = searchString;
            IQueryable<Pais> paisesIQ = _context.Pais
               .AsQueryable();
            if (!String.IsNullOrEmpty(searchString))
            {
                paisesIQ = paisesIQ.Where(s => s.Nombre.Contains(searchString)
                                        || s.Codigo.Contains(searchString)
                                        
                                        );
            }
            switch (sortOrder)
            {
                case "Nombre":
                    paisesIQ = paisesIQ.OrderBy(s => s.Nombre);
                    break;
                case "Nombre_desc":
                    paisesIQ = paisesIQ.OrderByDescending(s => s.Nombre);
                    break;
                case "Codigo":
                    paisesIQ = paisesIQ.OrderBy(s => s.Codigo);
                    break;
                default:
                    paisesIQ = paisesIQ.OrderBy(s => s.Id);
                    break;
            }
            int pageSize = 5;
            Pais = await ListaPaginada<Pais>.CreateAsync(
                paisesIQ.AsNoTracking(), pageIndex ?? 1, pageSize);

        }
       
    }
}
