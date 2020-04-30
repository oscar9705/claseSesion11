using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Prj_GestionPDC_OASG.Data;
using Prj_GestionPDC_OASG.Entities;

namespace Prj_GestionPDC_OASG.Pages.DepartamentoPage
{
    public class DeleteModel : PageModel
    {
        private readonly Prj_GestionPDC_OASG.Data.Prj_GestionPDC_OASGContext _context;

        public DeleteModel(Prj_GestionPDC_OASG.Data.Prj_GestionPDC_OASGContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Departamento Departamento { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Departamento = await _context.Departamento
                .Include(d => d.Pais).FirstOrDefaultAsync(m => m.Id == id);

            if (Departamento == null)
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

            Departamento = await _context.Departamento.FindAsync(id);

            if (Departamento != null)
            {
                _context.Departamento.Remove(Departamento);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
