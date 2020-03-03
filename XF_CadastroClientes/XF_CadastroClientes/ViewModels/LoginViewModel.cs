using System.Windows.Input;
using Xamarin.Forms;
using XF_CadastroClientes.DAL;
using MvvmHelpers;
using XF_CadastroClientes.Views;

namespace XF_CadastroClientes.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        private INavigation _navigation;
        private UsuarioDAL _usuarioDAL = 
            DependencyService.Get<UsuarioDAL>();

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
                return new Command<string>(async (string login) =>
                {
                    var cliente = await _usuarioDAL.GetByPredicate(x => x.Login == login);

                    if (cliente == null)
                    {
                        await App.Current.MainPage.DisplayAlert("Login", "Usuario não encontrado.", "OK");
                        return;
                    }   

                    if (cliente.Login != LoginEntry)
                    {
                        await App.Current.MainPage.DisplayAlert("Login", "Login digitado é Inválido", "OK");
                        return;
                    }

                    if (cliente.Senha != SenhaEntry)
                    {
                        await App.Current.MainPage.DisplayAlert("Login", "Senha digitada é Inválida", "OK");
                        return;
                    }
                        

                    if (cliente.Login == LoginEntry && cliente.Senha == SenhaEntry)
                    {
                        await _navigation.PushAsync(new ClientesPage());
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Login", "Login Inválido", "OK");
                    }

                });
            }
        }

        public ICommand RegistrarCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await _navigation.PushAsync(new RegisterPage());
                });
            }
        }
        #endregion
    }
}
