using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prj_GestionPDC_OASG.Data;
using Prj_GestionPDC_OASG.Entities;

namespace Prj_GestionPDC_OASG.Pages.CiudadPage
{
    public class EditModel : PageModel
    {
        private readonly Prj_GestionPDC_OASG.Data.Prj_GestionPDC_OASGContext _context;

        public EditModel(Prj_GestionPDC_OASG.Data.Prj_GestionPDC_OASGContext context)
        {
            _context = context;
        }

        public JsonResult OnGetObtenerDepartamentos(int IdentificadorPais)
        {
            var query = (from pc in _context.Departamento
                         join c in _context.Pais on pc.PaisId equals c.Id
                         where c.Id == IdentificadorPais
                         select new SelectListItem()
                         {
                             Value = pc.Id.ToString(),
                             Text = pc.Nombre
                         });
            return new JsonResult(query.ToList());
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
                .Include(c => c.Departamento).FirstOrDefaultAsync(m => m.Id == id);

            if (Ciudad == null)
            {
                return NotFound();
            }

            /** mostrar los departamentos asignados al pais **/
            var lstpais = _context.Pais.Where(c => c.Departamento.Count > 0);
            ViewData["Pais_Id"] = new SelectList(lstpais, "Id", "Nombre");

            if (lstpais.ToList().Count > 0)
            {

                int tmpPaisId = lstpais.ToList<Pais>()[0].Id;

                var lstdepto = _context.Departamento.Where(c => c.PaisId == tmpPaisId);
                ViewData["Departamento_Id"] = new SelectList(lstdepto, "Id", "Nombre");
            }

            return Page();

        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            //  if (!ModelState.IsValid)
            //  {
            //   return Page();
            //    }

            var Ciudadtmp = await _context.Ciudad
                .FirstOrDefaultAsync(m => m.Id == Ciudad.Id);

            Ciudadtmp.Id = Ciudad.Id;
            Ciudadtmp.Nombre = Ciudad.Nombre;
            Ciudadtmp.DepartamentoId = Ciudad.DepartamentoId;
            Ciudadtmp.Active = Ciudad.Active;

            _context.Attach(Ciudadtmp).State = EntityState.Modified;

            try
            {
                //_context.Ciudad.Add(Ciudadtmp);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CiudadExists(Ciudad.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");

        }

        private bool CiudadExists(int id)
        {
            return _context.Ciudad.Any(e => e.Id == id);
        }
    }
}
