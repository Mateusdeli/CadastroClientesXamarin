using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF_CadastroClientes.DAL;
using XF_CadastroClientes.Validations;
using XF_CadastroClientes.Views;

namespace XF_CadastroClientes
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DependecyInjection();
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void DependecyInjection()
        {
            DependencyService.Register<ClienteDAL>();
            DependencyService.Register<ClienteValidation>();
        }
    }
}
