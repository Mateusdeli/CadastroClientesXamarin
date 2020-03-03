using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using Plugin.ExternalMaps;
using XF_CadastroClientes.DAL;
using XF_CadastroClientes.Models;
using XF_CadastroClientes.Validations;
using Plugin.Permissions.Abstractions;
using Plugin.Permissions;
using Plugin.Media;
using Plugin.Messaging;

namespace XF_CadastroClientes.ViewModels
{
    public class IncluirClienteViewModel : BaseViewModel
    {
        private INavigation _navigation;
        private Cliente _cliente = new Cliente();
        private ClienteValidation _clienteValidation =
            DependencyService.Get<ClienteValidation>();

        private ClienteDAL _clienteDAL = 
            DependencyService.Get<ClienteDAL>();
        public IncluirClienteViewModel(INavigation navigation)
        {
            ButtonMapAtivo = false;
            _navigation = navigation;
            FotoEntry = FotoEntry == null ? "nofoto.png" : FotoEntry;
        }

        #region Propriedades
        private string _nomeEntry;

        public string NomeEntry
        {
            get { return _nomeEntry; }
            set 
            { 
                _nomeEntry = value;
                OnPropertyChanged();

                if (_cliente.Nome != _nomeEntry)
                _cliente.Nome = _nomeEntry;
                
            }
        }

        private string _enderecoEntry;

        public string EnderecoEntry
        {
            get { return _enderecoEntry; }
            set 
            { 
                _enderecoEntry = value; 
                OnPropertyChanged();
                if (_enderecoEntry != _cliente.Endereco)
                    _cliente.Endereco = _enderecoEntry;

            }
        }

        private string _emailEntry;

        public string EmailEntry
        {
            get { return _emailEntry; }
            set 
            { 
                _emailEntry = value; 
                OnPropertyChanged();
                if (_emailEntry != _cliente.Email)
                    _cliente.Email = _emailEntry;
            }
        }

        private string _cidadeEntry;

        public string CidadeEntry
        {
            get { return _cidadeEntry; }
            set 
            { 
                _cidadeEntry = value;
                OnPropertyChanged();
                if (_cidadeEntry != _cliente.Cidade)
                    _cliente.Cidade = _cidadeEntry;
            }
        }

        private string _estadoEntry;

        public string EstadoEntry
        {
            get { return _estadoEntry; }
            set 
            { 
                _estadoEntry = value; 
                OnPropertyChanged();
                if (_estadoEntry != _cliente.Estado)
                    _cliente.Estado = _estadoEntry;
            }
        }

        private string _cepEntry;

        public string CepEntry
        {
            get { return _cepEntry; }
            set 
            { 
                _cepEntry = value; 
                OnPropertyChanged();
                if (_cepEntry != _cliente.Cep)
                    _cliente.Cep = _cepEntry;
            }
        }

        private string _telefoneEntry;

        public string TelefoneEntry
        {
            get { return _telefoneEntry; }
            set 
            { 
                _telefoneEntry = value;
                OnPropertyChanged();
                if (_telefoneEntry != _cliente.Telefone)
                    _cliente.Telefone = _telefoneEntry;
            }
        }

        private string _fotoEntry;

        public string FotoEntry
        {
            get { return _fotoEntry; }
            set 
            { 
                _fotoEntry = value; 
                OnPropertyChanged();
                if (_fotoEntry != _cliente.Foto)
                    _cliente.Foto = _fotoEntry;
            }
        }
        private bool _buttonMapAtivo;
        public bool ButtonMapAtivo
        {
            get { return _buttonMapAtivo; }
            set
            {
                _buttonMapAtivo = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        public ICommand IncluirClienteCommand
            => new Command(async () =>
            {

                var validate = _clienteValidation.Validate(_cliente);

                if (validate.IsValid)
                {
                    var result = await App.Current.MainPage.DisplayAlert("Cadastrar Cliente", "Deseja cadastrar um novo cliente?", "Sim", "Não");
                    
                    if (result)
                    {
                        _clienteDAL.Create(_cliente);
                        await App.Current.MainPage.Navigation.PopAsync();
                    }
                }
                else
                {
                    foreach (var erro in validate.Errors)
                    {
                        await App.Current.MainPage.DisplayAlert("Cadastrar Cliente", erro.ToString(), "Ok");
                    }
                }

            });

        public ICommand VerMapaCommand
            => new Command(async () =>
            {

                if (!String.IsNullOrEmpty(_cliente.Endereco) &&
                    !String.IsNullOrEmpty(_cliente.Cidade) &&
                    !String.IsNullOrEmpty(_cliente.Estado) &&
                    !String.IsNullOrEmpty(_cliente.Cep))
                {
                    var navigateMap = await CrossExternalMaps.Current.NavigateTo(null, _cliente.Endereco, _cliente.Cidade, _cliente.Estado, _cliente.Cep, null, null);

                    if (!navigateMap)
                    {
                        await App.Current.MainPage.DisplayAlert("Ver Mapa", "Não foi possivel exibir o mapa.", "Ok");
                        return;
                    }

                    ButtonMapAtivo = !ButtonMapAtivo;
                }
            });

        public ICommand CameraCommand
          => new Command<string>(async (string name) =>
          {

              if (String.IsNullOrEmpty(_nomeEntry))
              {
                  await App.Current.MainPage.DisplayAlert("Camera Error", "Informe o nome do cliente!", "OK");
                  return;
              }

              var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
              var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

              if (cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
              {
                  var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera, Permission.Storage });
                  cameraStatus = results[Permission.Camera];
                  storageStatus = results[Permission.Storage];
              }

              if (cameraStatus == PermissionStatus.Granted && storageStatus == PermissionStatus.Granted)
              {
                  await CrossMedia.Current.Initialize();

                  if (CrossMedia.IsSupported)
                  {

                      if (CrossMedia.Current.IsCameraAvailable || CrossMedia.Current.IsTakePhotoSupported)

                          try
                          {
                              var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                              {
                                  SaveToAlbum = true,
                                  Name = _cliente.Nome + ".jpg",

                              });

                              if (file == null)
                                  return;

                          }
                          catch (Exception ex)
                          {
                              await App.Current.MainPage.DisplayAlert("Camera Error", ex.Message, "OK");
                          }
                  }
                  else
                  {
                      await App.Current.MainPage.DisplayAlert("Camera Error", "Seu aparelho não suporta esta ação!", "OK");
                  }
              }
          });


        public ICommand AlbumCommand
            => new Command(async () =>
            {

                var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });

                if (file == null)
                    return;

                FotoEntry = file.Path;



            });

        public ICommand SmsCommand
            => new Command(async () =>
            {

                if (CrossMessaging.IsSupported)
                {
                    if (CrossMessaging.Current.SmsMessenger.CanSendSms)
                        CrossMessaging.Current.SmsMessenger.SendSms(_cliente.Telefone, "Uma messagem padrão apenas.");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Camera Error", "Seu aparelho não suporta esta ação!", "OK");
                }
            });

        #endregion

    }
}
