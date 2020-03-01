using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace XF_CadastroClientes.Infra
{
    public interface ISQLiteDBConnection
    {
        SQLiteAsyncConnection GetConnection();
    }
}
