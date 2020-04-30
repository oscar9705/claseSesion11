using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Prj_GestionPDC_OASG.Data;
using Prj_GestionPDC_OASG.Entities;

namespace Prj_GestionPDC_OASG.Pages.CiudadPage
{
    public class DeleteModel : PageModel
    {
        private readonly Prj_GestionPDC_OASG.Data.Prj_GestionPDC_OASGContext _context;

        public DeleteModel(Prj_GestionPDC_OASG.Data.Prj_GestionPDC_OASGContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Ciudad Ciudad { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ciudad = await _context.Ciudad
                .Include(c => c.Departamento)
                .Include(c => c.Departamento.Pais)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Ciudad == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ciudad = await _context.Ciudad.FindAsync(id);

            if (Ciudad != null)
            {
                _context.Ciudad.Remove(Ciudad);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
