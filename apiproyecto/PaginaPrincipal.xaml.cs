using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace apiproyecto
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaPrincipal : ContentPage
    {
        public PaginaPrincipal()
        {
            InitializeComponent();
        }

        private async void cmdCrudPlatos_Clicked(object sender, EventArgs e)
        {
            var crudPlatos = new CrudPlatos();
            await Navigation.PushAsync(crudPlatos);
        }
    }
}