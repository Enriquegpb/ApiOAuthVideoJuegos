using ApiOAuthVideoJuegos.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiOAuthVideoJuegos.Data
{
    public class VideoJuegosContext: DbContext
    {
        public VideoJuegosContext(DbContextOptions<VideoJuegosContext> options) : base(options) { }
        public DbSet<VideoJuego> VideoJuegos { get; set; }
        public DbSet<UsuarioGaming> Usuarios { get; set; }
    }
}
