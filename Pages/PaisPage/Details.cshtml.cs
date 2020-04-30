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
    public class DetailsModel : PageModel
    {
        private readonly Prj_GestionPDC_OASG.Data.Prj_GestionPDC_OASGContext _context;

        public DetailsModel(Prj_GestionPDC_OASG.Data.Prj_GestionPDC_OASGContext context)
        {
            _context = context;
        }

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
    }
}
