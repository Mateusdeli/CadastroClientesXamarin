using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF_CadastroClientes.Infra;
using XF_CadastroClientes.Models;

namespace XF_CadastroClientes.DAL
{
    public class ClienteDAL
    {
        private ISQLiteDBConnection _sqliteConnection =
            DependencyService.Get<ISQLiteDBConnection>();

        private SQLite.SQLiteAsyncConnection _connection;

        public ClienteDAL()
        {
            _connection = _sqliteConnection.GetConnection();
            CreateTableCliente();
        }

        private async void CreateTableCliente()
        {
            await _connection.CreateTableAsync<Cliente>();
        }
        public async Task<IEnumerable<Cliente>> GetClientes()
        {
            return await _connection.Table<Cliente>().ToListAsync();
        }

        public async Task<Cliente> GetClienteById(long id)
        {
            return await _connection.Table<Cliente>().FirstOrDefaultAsync(x => x.ClienteId == id);
        }

        public async void Create(Cliente cliente)
        {
            await _connection.InsertAsync(cliente);
        }
        public async void Update(Cliente cliente)
        {
            await _connection.UpdateAsync(cliente);
        }

        public async void Remove(Cliente cliente)
        {
            await _connection.DeleteAsync(cliente);
        }

        public async Task<Cliente> GetClienteByPredicate(Expression<Func<Cliente, bool>> expression)
        {
            return await _connection.Table<Cliente>().Where(expression).FirstOrDefaultAsync();
        }

    }
}
