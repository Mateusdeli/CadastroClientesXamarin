using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XF_CadastroClientes.DAL;
using XF_CadastroClientes.Models;
using XF_CadastroClientes.Validations;

namespace XF_CadastroClientes.ViewModels
{
    public class RegisterViewModel : BaseViewModel
	{
		private UsuarioValidation _usuarioValidation = 
			DependencyService.Get<UsuarioValidation>();

		private UsuarioDAL _usuarioDAL =
			DependencyService.Get<UsuarioDAL>();

		private Usuario _usuario = new Usuario();

		#region Propriedades

		private string _loginEntry;

		public string LoginEntry
		{
			get { return _loginEntry; }
			set
			{
				_loginEntry = value;
				OnPropertyChanged();
				if (value != null && _usuario.Login != _loginEntry)
					_usuario.Login = _loginEntry;
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
				if (value != null && _usuario.Senha != _senhaEntry)
					_usuario.Senha = _senhaEntry;
			}
		}

		private string _confrimarSenhaEntry;
		public string ConfirmarSenha
		{
			get { return _confrimarSenhaEntry; }
			set
			{
				_confrimarSenhaEntry = value;
				OnPropertyChanged();
			}
		}

		#endregion

		#region Commands

		public ICommand RegistrarCommand =>
			new Command(async () =>
			{

				var validarUsuario = await _usuarioValidation.ValidateAsync(_usuario);

				if (!validarUsuario.IsValid)
				{
					await App.Current.MainPage.DisplayAlert("Usuario inválido", validarUsuario.Errors[0].ToString(), "Ok");
					return;
				}

				if (_usuario.Senha != ConfirmarSenha)
				{
					await App.Current.MainPage.DisplayAlert("Registrar", "As senhas digitadas não correspondem.", "Ok");
					return;
				}

				_usuarioDAL.Create(_usuario);
				await App.Current.MainPage.DisplayAlert("Registrar", "Usuario registrado com sucesso!", "Ok");

			});

		#endregion



	}
}
