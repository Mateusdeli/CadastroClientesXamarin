using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF_CadastroClientes.ViewModels;

namespace XF_CadastroClientes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IncluirClientePage : ContentPage
    {
        public IncluirClientePage()
        {
            InitializeComponent();
            BindingContext = new IncluirClienteViewModel(Navigation);
        }
    }
}