namespace RpgMvc.Models
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordString { get; set; }
        public byte[] Foto { get; set; }
        public DateTime? DataAcesso { get; set; }
        public string Email { get; set; }
        public string Perfil { get; set; }
    }
}