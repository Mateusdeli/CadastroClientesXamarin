using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace XF_CadastroClientes.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int UsuarioId { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }

    }
}
