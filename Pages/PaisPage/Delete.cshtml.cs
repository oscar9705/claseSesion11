using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Prj_GestionPDC_OASG.Data;
using Prj_GestionPDC_OASG.Entities;

namespace Prj_GestionPDC_OASG.Pages.PaisPage
{
    public class DeleteModel : PageModel
    {
        private readonly Prj_GestionPDC_OASG.Data.Prj_GestionPDC_OASGContext _context;

        public DeleteModel(Prj_GestionPDC_OASG.Data.Prj_GestionPDC_OASGContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Pais Pais { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Pais = await _context.Pais.FirstOrDefaultAsync(m => m.Id == id);

            if (Pais == null)
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

            Pais = await _context.Pais.FindAsync(id);

            if (Pais != null)
            {
                _context.Pais.Remove(Pais);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
