using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF_CadastroClientes.DAL;
using XF_CadastroClientes.Models;
using XF_CadastroClientes.ViewModels;

namespace XF_CadastroClientes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientesPage : ContentPage
    {
        private ListaClientesViewModel _listaClientesViewModel;
        public ClientesPage()
        {
            InitializeComponent();
            _listaClientesViewModel = new ListaClientesViewModel(Navigation);
            BindingContext = _listaClientesViewModel;
        }

        private async void MenuItemExcluir_Clicked(object sender, EventArgs e)
        {

            var menuItem = sender as MenuItem;
            var clienteSelecionado = menuItem.CommandParameter as Cliente;
            var result = await DisplayAlert("Excluir Cliente", "Deseja excluir este cliente?", "SIM", "NÃO");

            if (result)
                _listaClientesViewModel.ExcluirCliente(clienteSelecionado);
        }

        private void MenuItemAlterar_Clicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var clienteSelecionado = menuItem.CommandParameter as Cliente;
            _listaClientesViewModel.AlterarCliente(clienteSelecionado);
        }

        private void listaClientes_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var cliente = e.Item as Cliente;

            if (cliente != null)
                _listaClientesViewModel.ClienteDetalhesNavigation(cliente);
        }
    }
}