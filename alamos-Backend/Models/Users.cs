using System.ComponentModel.DataAnnotations;

namespace alamos_Backend.Models
{
    public class Users
    {

        [Key]
        public int idUsuarioSistema { get; set; }

        public string nombre { get; set; }

        public string apellidos { get; set; }

        public string? registroHuellaDigital { get; set; } = string.Empty;

        public string password { get; set; }
    }
}
