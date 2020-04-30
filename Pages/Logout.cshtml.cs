using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Prj_GestionPDC_OASG.Pages
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            var authenticationManager = Request.HttpContext;
            await authenticationManager.SignOutAsync();
            return LocalRedirect("/Login");

        }
    }
}