using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF_CadastroClientes.Infra;
using XF_CadastroClientes.Models;

namespace XF_CadastroClientes.DAL
{
    public class UsuarioDAL
    {

        private ISQLiteDBConnection _sqliteConnection =
            DependencyService.Get<ISQLiteDBConnection>();

        private SQLite.SQLiteAsyncConnection _connection;

        public UsuarioDAL()
        {
            _connection = _sqliteConnection.GetConnection();
            CreateUsuarioTable();
        }

        async void CreateUsuarioTable()
        {
            await _connection.CreateTableAsync<Usuario>();
        }

        public async void Create(Usuario usuario)
        {
            await _connection.InsertAsync(usuario);
        }

        public async Task<Usuario> GetByPredicate(Expression<Func<Usuario, bool>> predicate)
        {
            return await _connection.Table<Usuario>().FirstOrDefaultAsync(predicate);
        }


    }
}
