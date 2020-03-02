using SQLite;


namespace XF_CadastroClientes.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public long? UsuarioId { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }

    }
}
