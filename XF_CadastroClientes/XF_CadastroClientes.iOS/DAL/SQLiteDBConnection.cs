using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Foundation;
using SQLite;
using UIKit;
using Xamarin.Forms;
using XF_CadastroClientes.Infra;
using XF_CadastroClientes.iOS.DAL;

[assembly:Dependency(typeof(SQLiteDBConnection))]
namespace XF_CadastroClientes.iOS.DAL
{
    public class SQLiteDBConnection : ISQLiteDBConnection
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var fileDbName = "CadastroDB.db";
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(path, fileDbName);
            return new SQLiteAsyncConnection(filePath);
        }
    }
}