using ApiOAuthVideoJuegos.Models;
using ApiOAuthVideoJuegos.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace ApiOAuthVideoJuegos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoJuegosController : ControllerBase
    {
        private RepositoryVideoJuegos repo;
        public VideoJuegosController(RepositoryVideoJuegos repo)
        {
            this.repo = repo;
        }
        [Authorize]
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<UsuarioGaming>> PerfilUsuarioGaming()
        {
            Claim claim = HttpContext.User.Claims.SingleOrDefault(x => x.Type == "UserData");
            string jsonUsuarioGaming =
                claim.Value;
            UsuarioGaming usuario = JsonConvert.DeserializeObject<UsuarioGaming>(jsonUsuarioGaming);
            return usuario;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<VideoJuego>>> GetVideoJuegos()
        {
            List<VideoJuego> videoJuegos = await this.repo.GetVideoJuegosAsync();
            return videoJuegos;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<VideoJuego>> FindVideoJuego(int id)
        {
            VideoJuego videoJuego = await this.repo.FindVideoJuegoAsync(id);
            return videoJuego;
        }   
        [HttpPost]
        public async Task<ActionResult<VideoJuego>> NewVideoJuego(VideoJuego videojuego)
        {
            await this.repo.NewVideoJuegoAsync(videojuego.IdVideojuego, videojuego.Nombre, videojuego.Descripcion, videojuego.Precio, videojuego.Imagen);
            return Ok();
        }  
        [HttpPut]
        public async Task<ActionResult<VideoJuego>> UpdateVideoJuego(VideoJuego videojuego)
        {
            await this.repo.UpdateVideoJuegoAsync(videojuego.IdVideojuego, videojuego.Nombre, videojuego.Descripcion, videojuego.Precio, videojuego.Imagen);
            return Ok();
        }  
        [HttpDelete("{id}")]
        public async Task<ActionResult<VideoJuego>> DeleteVideoJuego(int id)
        {
             await this.repo.DeleteVideoJuegoAsync(id);
            return Ok();
        }
    }
}
