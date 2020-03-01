using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XF_CadastroClientes.DAL;
using XF_CadastroClientes.Models;
using XF_CadastroClientes.Views;

namespace XF_CadastroClientes.ViewModels
{
    public class ListaClientesViewModel : BaseViewModel
    {

        private INavigation _navigation;
        private ClienteDAL _clienteDAL =
           DependencyService.Get<ClienteDAL>();

        public ListaClientesViewModel(INavigation navigation)
        {
            _navigation = navigation;
            SetListaClientes();
        }

        private async void SetListaClientes()
        {
            var listaClientes = await _clienteDAL.GetClientes();
            Clientes = new ObservableCollection<Cliente>(listaClientes);
        }

        private ObservableCollection<Cliente> _clientes;

        public ObservableCollection<Cliente> Clientes
        {
            get { return _clientes; }
            set 
            { 
                if (_clientes != value)
                {
                    _clientes = value;
                    OnPropertyChanged(nameof(Clientes));
                }
            }
        }

        #region Propriedades

        private bool _refreshAtivo;

        public bool RefreshAtivo
        {
            get { return _refreshAtivo; }
            set 
            {
                _refreshAtivo = value;
                OnPropertyChanged();
            }
        }

        private Cliente _clienteSelecionado;

        public Cliente ClienteSelecionado
        {
            get { return _clienteSelecionado; }
            set 
            { 
                if (_clienteSelecionado != null)
                _clienteSelecionado = value;

                OnPropertyChanged();

            }
        }


        #endregion

        #region Commands

        public ICommand IncluirClienteCommand
            => new Command(async () =>
            {
                await _navigation.PushAsync(new IncluirClientePage());
            });

        public ICommand RefreshClientesCommand
            => new Command(() =>
            {
                SetListaClientes();
                RefreshAtivo = !RefreshAtivo;
            });

        #endregion

        public async void ExcluirCliente(Cliente cliente)
        {
            if (cliente != null)
            {
                _clienteDAL.Remove(cliente);
                await App.Current.MainPage.DisplayAlert("Cliente Excluido", $"O cliente {cliente.Nome}, foi excluido com sucesso!", "OK");
                SetListaClientes();
            }

        }

        public async void AlterarCliente(Cliente cliente)
        {
            await _navigation.PushAsync(new AlterarClientePage(cliente));
        }


        public async void ClienteDetalhesNavigation(Cliente cliente)
        {
            await App.Current.MainPage.Navigation.PushAsync(new ClienteDetalhesPage(cliente));
        }


    }
}
