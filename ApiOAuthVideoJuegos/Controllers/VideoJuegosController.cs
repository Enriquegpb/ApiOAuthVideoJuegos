using ApiOAuthVideoJuegos.Models;
using ApiOAuthVideoJuegos.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet]
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
