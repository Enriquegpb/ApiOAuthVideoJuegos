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

        // GET: api/VideoJuegos/PerfilUsuario
        /// <summary>
        /// Devuelve El perfil de usuario , tabla Usuarios.
        /// </summary>
        /// <remarks>
        /// Método para devolver el perfil de usuario de la BBDD
        /// Dicho método está protegido por TOKEN
        /// </remarks>
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="401">Unathorized. No autorizado. </response>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]

        [Route("[action]")]
        public async Task<ActionResult<UsuarioGaming>> PerfilUsuarioGaming()
        {
            Claim claim = HttpContext.User.Claims.SingleOrDefault(x => x.Type == "UserData");
            string jsonUsuarioGaming =
                claim.Value;
            UsuarioGaming usuario = JsonConvert.DeserializeObject<UsuarioGaming>(jsonUsuarioGaming);
            return usuario;
        }

        // GET: api/VideoJuegos
        /// <summary>
        /// Devuelve los videojeugos , tabla VideoJuegos.
        /// </summary>
        /// <remarks>
        /// Método para devolver todas los videojuegos de la BBDD
        /// Dicho método está protegido por TOKEN
        /// </remarks>
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="401">Unathorized. No autorizado. </response>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<ActionResult<List<VideoJuego>>> GetVideoJuegos()
        {
            List<VideoJuego> videoJuegos = await this.repo.GetVideoJuegosAsync();
            return videoJuegos;
        }
        // GET: api/VideoJuegos/id
        /// <summary>
        /// Obtiene un Videojuego por su Id, tabla VideoJuegos.
        /// </summary>
        /// <remarks>
        /// Permite buscar un empleado por su ID de empresa
        /// </remarks>
        /// <param name="id">Id (GUID) del objeto.</param>
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<ActionResult<VideoJuego>> FindVideoJuego(int id)
        {
            VideoJuego videoJuego = await this.repo.FindVideoJuegoAsync(id);
            return videoJuego;
        }
        
        
        // POST: api/empresa
        /// <summary>
        /// Crea una nuevo videojuegi en la BD, tabla VIDEOJUEGOS..
        /// </summary>
        /// <remarks>
        /// Este método crea un nuevo videojuego por URL
        /// </remarks>
        /// <param name="videojuego">String con el nombre de la videojeugo.</param>
        /// <response code="201">Created. Objeto correctamente creado en la BD.</response>        
        /// <response code="500">BBDD. No se ha creado el objeto en la BD. Error en la BBDD.</response>/// 
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
