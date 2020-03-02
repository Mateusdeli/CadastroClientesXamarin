using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XF_CadastroClientes.Behaviors
{
    public class ConfirmarSenhaBehavior : Behavior<Entry>
    {

        public static readonly BindableProperty SenhaProperty =
            BindableProperty.Create(
                "Senha", 
                typeof(string), 
                typeof(ConfirmarSenhaBehavior), null);

        public string Senha
        {
            get { return (string) GetValue(SenhaProperty); }
            set { SetValue(SenhaProperty, value); }
        }


        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += Bindable_TextChanged;
        }

        private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;

            if (Senha == e.NewTextValue)
                entry.BackgroundColor = Color.Default;
            else
                entry.BackgroundColor = Color.Salmon;

            if (String.IsNullOrEmpty(e.NewTextValue))
                entry.BackgroundColor = Color.Default;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= Bindable_TextChanged;
        }

    }
}
