using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Prj_GestionPDC_OASG.Data;
using Prj_GestionPDC_OASG.Entities;

namespace Prj_GestionPDC_OASG.Pages.CiudadPage
{
    public class CreateModel : PageModel
    {
        private readonly Prj_GestionPDC_OASG.Data.Prj_GestionPDC_OASGContext _context;

        public CreateModel(Prj_GestionPDC_OASG.Data.Prj_GestionPDC_OASGContext context)
        {
            _context = context;
        }

        public JsonResult OnGetObtenerDepartamentos(int IdentificadorPais)
        {
            //OnGet(0);
            var query = (from pc in _context.Departamento
                         join c in _context.Pais on pc.PaisId equals c.Id
                         where c.Id == IdentificadorPais
                         select new SelectListItem()
                         {
                             Value = pc.Id.ToString(),
                             Text = pc.Nombre
                         });
            //Console.WriteLine(query);
           // OnGet((IdentificadorPais-1));
            return new JsonResult(query.ToList());
            
        }
        public IActionResult OnGet()
        {           
            var lstpais = _context.Pais.Where(c => c.Departamento.Count > 0);
            ViewData["Pais_Id"] = new SelectList(lstpais, "Id", "Nombre");

            if(lstpais.ToList().Count>0)
            {
                int tmpPaisId = lstpais.ToList<Pais>()[0].Id;
                Console.WriteLine("Id pais seleccionado "+tmpPaisId);
                Console.WriteLine("nombre pais seleccionado " + (lstpais.ToList<Pais>()[0].Nombre));
                var lstdepto = _context.Departamento.Where(c => c.PaisId == tmpPaisId);
                ViewData["Departamento_Id"] = new SelectList(lstdepto, "Id", "Nombre");
            }
            return Page();
        }

        [BindProperty]
        public Ciudad Ciudad { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Ciudad.Add(Ciudad);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
