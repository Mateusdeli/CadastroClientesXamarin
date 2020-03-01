using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Plugin.LocalNotifications;
using XF_CadastroClientes.DAL;
using MvvmHelpers;
using XF_CadastroClientes.Views;

namespace XF_CadastroClientes.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        private INavigation _navigation;
        private ClienteDAL _clienteDAL = 
            DependencyService.Get<ClienteDAL>();

        public LoginViewModel(INavigation navigation)
        {
            _navigation = navigation;
        }

        #region Propriedades
        private string _loginEntry;

        public string LoginEntry
        {
            get { return _loginEntry; }
            set
            {
                _loginEntry = value;
                OnPropertyChanged();
            }
        }

        private string _senhaEntry;

        public string SenhaEntry
        {
            get { return _senhaEntry; }
            set
            {
                _senhaEntry = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new Command<string>(async (string senha) =>
                {
                    var cliente = await _clienteDAL.GetUsuarioByPredicate(x => x.Senha == senha);
                    if (cliente != null)
                    {
                        await _navigation.PushAsync(new ClientesPage());
                    }
                    else
                    {
                        _clienteDAL.InsertFirst(new Models.Usuario { Login = "m", Senha = "1"});
                        await App.Current.MainPage.DisplayAlert("Login", "Login Inválido", "OK");
                    }
                });
            }
        }
        #endregion
    }
}
