using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiOAuthVideoJuegos.Models
{
    [Table("USUARIOSGAMING")]
    public class UsuarioGaming
    {
        [Key]
        [Column("IDUSUARIO")]
        public int IdUsuario { get; set; }
        [Column("USERNAME")]
        public string UserName { get; set; }
        [Column("PASSWORD")]
        public string Password { get; set; }
        [Column("IMAGEN")]
        public string Imagen { get; set; }
    }
}
