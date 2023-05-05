using ApiOAuthVideoJuegos.Helpers;
using ApiOAuthVideoJuegos.Models;
using ApiOAuthVideoJuegos.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiOAuthVideoJuegos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private HelperOAuthToken helper;
        private RepositoryUsuariosGaming repo;
        public AuthController(HelperOAuthToken helper, RepositoryUsuariosGaming repo)
        {
            this.helper = helper;
            this.repo = repo;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> Login(LoginModel model)
        {
            UsuarioGaming usuario =
                await this.repo.ExisteUsuarioGamingAsync(model.UserName, model.Password);

            if (usuario == null)
            {
                return Unauthorized();
            }
            else
            {
                SigningCredentials credentials =
                    new SigningCredentials(this.helper.GetKeyToken(), SecurityAlgorithms.HmacSha256);
                //GENERACION DEL JWT TOKEN CON SUS CORRESPONDIENTES DATOS
                string jsonUsuarioVideojuego = JsonConvert.SerializeObject(usuario);
                Claim[] informacion = new[]
                {
                    new Claim("UserData", jsonUsuarioVideojuego)
                };

                JwtSecurityToken token =
                    new JwtSecurityToken(
                        claims: informacion,
                        issuer: this.helper.Issuer,
                        audience: this.helper.Audience,
                        signingCredentials: credentials,
                        expires: DateTime.UtcNow.AddMinutes(15),
                        notBefore: DateTime.UtcNow
                        );
                return Ok(new
                {
                    response =
                    new JwtSecurityTokenHandler().WriteToken(token)
                });


            }
        }
    }
}
