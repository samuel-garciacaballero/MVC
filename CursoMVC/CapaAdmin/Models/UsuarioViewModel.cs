namespace CapaAdmin.Models
{
    public class UsuarioViewModel
    {
        public CapaAdmin.Models.Usuario? Usuario { get; set; }
        public IEnumerable<CapaAdmin.Models.Usuario>? Usuarios { get; set; }


        public Usuario OnSelectedUser(Usuario user)
        {
            Usuario = user;
            return user;
        }
    }
}
