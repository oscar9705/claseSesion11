using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Prj_GestionPDC_OASG.Code;

namespace Prj_GestionPDC_OASG.Pages
{
    public class LoginModel : PageModel
    {
        Login lg_login;
        [BindProperty]
        public Login Lg_login { get => lg_login; set => lg_login = value; }
        private readonly Prj_GestionPDC_OASG.Data.Prj_GestionPDC_OASGContext _context;

        public LoginModel(Prj_GestionPDC_OASG.Data.Prj_GestionPDC_OASGContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var logininfo = _context.RolUsuario
                    .Include(S => S.Rol)
                    .Include(s => s.Usuario)
                    .Where(s => s.Usuario.Usuario1.ToUpper() == Lg_login.Username.ToUpper().Trim() && s.Usuario.Password == Lg_login.Password);
                if(logininfo!= null && logininfo.Count() > 0)
                {
                    var logindetails = logininfo.First();
                    int rolid = logindetails.Rol.Id;
                    var lstpermissions = _context.PermisosRol.Where(s => s.RolId == rolid).ToList();
                    string strpermissions = "";
                    foreach (var perm in lstpermissions)
                    {
                        if (perm.Active == 1)
                        {
                            strpermissions += perm.CodigoFuncion + ",";
                        }
                    }
                         //logueo
                    await UserAuthentication(Lg_login.Username, false);
                    HttpContext.Session.SetString("UserName", logindetails.Usuario.Usuario1);
                    HttpContext.Session.SetString("RolName", logindetails.Rol.Nombre);
                    HttpContext.Session.SetString("Permissions", strpermissions);
                    //_session.SetString("Permissions", strpermissions);
                    return this.RedirectToPage("/IndexPage");


                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Username o Password Incorrectos");
                }
            }
            return this.Redirect("");

        }
        private async Task UserAuthentication(string username, bool isPersistent)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, username));
            var claimIdenties = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimPrincipal = new ClaimsPrincipal(claimIdenties);
            var authenticationManager = Request.HttpContext;
            await authenticationManager.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, new AuthenticationProperties() { IsPersistent = isPersistent });
        }

    }
}