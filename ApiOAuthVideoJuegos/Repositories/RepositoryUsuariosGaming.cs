using ApiOAuthVideoJuegos.Data;
using ApiOAuthVideoJuegos.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiOAuthVideoJuegos.Repositories
{
    public class RepositoryUsuariosGaming
    {
        private VideoJuegosContext context;
        public RepositoryUsuariosGaming(VideoJuegosContext context)
        {
            this.context = context;
        }

        public async Task<List<UsuarioGaming>> GetUsuarioGamingsAsync()
        {
            return await this.context.Usuarios.ToListAsync();
        }

        public async Task<UsuarioGaming> FindUsuarioGamingAsync(int idusuario)
        {
            return await this.context.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == idusuario);

        }
        public async Task<UsuarioGaming> ExisteUsuarioGamingAsync(string username, string password)
        {
            return await
                this.context.Usuarios.FirstOrDefaultAsync(x => x.UserName == username && x.Password == password);
        }
    }
}
