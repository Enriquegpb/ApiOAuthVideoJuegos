using ApiOAuthVideoJuegos.Data;
using ApiOAuthVideoJuegos.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiOAuthVideoJuegos.Repositories
{
    public class RepositoryVideoJuegos
    {
        private VideoJuegosContext context;
        public RepositoryVideoJuegos(VideoJuegosContext context)
        {
            this.context = context;
        }

        public async Task<List<VideoJuego>> GetVideoJuegosAsync()
        {
            return await this.context.VideoJuegos.ToListAsync();
        }

        public async Task<VideoJuego> FindVideoJuegoAsync(int idvideojuego)
        {
            return await this.context.VideoJuegos.FirstOrDefaultAsync(v => v.IdVideojuego == idvideojuego);
        }

        private int GetMaxIdVideoJuego()
        {
            return this.context.VideoJuegos.Max(x => x.IdVideojuego) + 1;
        }

        public async Task NewVideoJuegoAsync(int idvideojuego, string nombre, string descripcion, int precio, string imagen)
        {
            VideoJuego videoJuego = new VideoJuego
            {
                IdVideojuego = this.GetMaxIdVideoJuego(),
                Nombre = nombre,
                Descripcion = descripcion,
                Precio = precio,
                Imagen = imagen
            };

            this.context.VideoJuegos.Add(videoJuego);
            await this.context.SaveChangesAsync();
        }
        public async Task UpdateVideoJuegoAsync(int idvideojuego, string nombre, string descripcion, int precio, string imagen)
        {
            VideoJuego videoJuego = await this.FindVideoJuegoAsync(idvideojuego);

            videoJuego.Nombre = nombre;
            videoJuego.Descripcion = descripcion;
            videoJuego.Precio = precio;
            videoJuego.Imagen = imagen;
            await this.context.SaveChangesAsync();
        } 
        public async Task DeleteVideoJuegoAsync(int idvideojuego)
        {
            VideoJuego videoJuego = await this.FindVideoJuegoAsync(idvideojuego);
            this.context.VideoJuegos.Remove(videoJuego);
            await this.context.SaveChangesAsync();
        }
    }
}
