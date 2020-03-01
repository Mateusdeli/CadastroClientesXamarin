using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using Xamarin.Forms;
using XF_CadastroClientes.Droid.DAL;
using XF_CadastroClientes.Infra;

[assembly: Dependency(typeof(SQLiteDBConnection))]
namespace XF_CadastroClientes.Droid.DAL
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