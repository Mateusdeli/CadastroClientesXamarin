using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Plugin.Messaging;
using Plugin.ExternalMaps;
using Xamarin.Forms;
using XF_CadastroClientes.Models;

namespace XF_CadastroClientes.ViewModels
{
    public class DetalheClienteViewModel : Cliente
    {

        public DetalheClienteViewModel(Cliente cliente)
        {
            Nome = cliente.Nome;
            Endereco = cliente.Endereco;
            Telefone = cliente.Telefone;
            Email = cliente.Email;
            Cidade = cliente.Cidade;
            Foto = cliente.Foto;
        }

        #region Commands

        public ICommand EmailCommand =>
            new Command(() =>
            {
                if (!CrossMessaging.IsSupported)
                {
                    App.Current.MainPage.DisplayAlert("Enviar Email", "Seu aparelho não suporta este tipo de ação!", "Ok");
                    return;
                }

                if (!CrossMessaging.Current.EmailMessenger.CanSendEmail)
                    App.Current.MainPage.DisplayAlert("Enviar Email", "Não é possivel enviar o email!", "Ok");

                CrossMessaging.Current.EmailMessenger.SendEmail(Email, "Email Teste", "Email TEster");
            });

        public ICommand TelefoneCommand =>
            new Command(() =>
            {
                if (!CrossMessaging.IsSupported)
                {
                    App.Current.MainPage.DisplayAlert("Fazer Ligação", "Seu aparelho não suporta este tipo de ação!", "Ok");
                    return;
                }

                if (!CrossMessaging.Current.PhoneDialer.CanMakePhoneCall)
                    App.Current.MainPage.DisplayAlert("Fazer Ligação", "Não é possivel fazer a ligação!", "Ok");

                CrossMessaging.Current.PhoneDialer.MakePhoneCall(Telefone);
            });

        public ICommand EnderecoCommand =>
            new Command(() =>
            {
                if (!CrossExternalMaps.IsSupported)
                {
                    App.Current.MainPage.DisplayAlert("Mapa", "Seu aparelho não suporta este tipo de ação!", "Ok");
                    return;
                }

                CrossExternalMaps.Current.NavigateTo("Sao Paulo", Endereco, Cidade, Estado, Cep, null, null);
            });

        #endregion

    }
}
