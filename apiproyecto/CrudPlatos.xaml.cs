using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace apiproyecto
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CrudPlatos : ContentPage
    {
        string apiUrl = "https://ajbenavidespapi.azurewebsites.net/api/Platos";

        public CrudPlatos()
        {
            InitializeComponent();
        }

        private void cmdInsert_Clicked(object sender, EventArgs e)
        {
            using (var webClient = new HttpClient())
            {
                webClient
                    .DefaultRequestHeaders
                    .Accept
                    .Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(new Plato
                {
                    Id = int.Parse(txtId.Text),
                    Nombre = txtNombre.Text,
                    Precio = double.Parse(txtPrecio.Text),
                    Disponible = bool.Parse(txtDisponible.Text),
                    RestauranteId = int.Parse(txtRestauranteId.Text)
                });

                var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");

                var resp = webClient.SendAsync(request);
                resp.Wait();

                json = resp.Result.Content.ReadAsStringAsync().Result;
                var plato = JsonConvert.DeserializeObject<Plato>(json);

                txtId.Text = plato.Id.ToString();
            }
        }

        private void cmdUpdate_Clicked(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                var url = $"{apiUrl}/{txtId.Text}";
                client.BaseAddress = new Uri(url);
                client
                    .DefaultRequestHeaders
                    .Accept
                    .Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                var json = JsonConvert.SerializeObject(new Plato
                {
                    Id = int.Parse(txtId.Text),
                    Nombre = txtNombre.Text,
                    Precio = double.Parse(txtPrecio.Text),
                    Disponible = bool.Parse(txtDisponible.Text),
                    RestauranteId = int.Parse(txtRestauranteId.Text)
                });
                var rqst = new HttpRequestMessage(HttpMethod.Put, apiUrl);
                rqst.Content = new StringContent(json, Encoding.UTF8, "application/json");

                var resp = client.SendAsync(rqst);
                resp.Wait();
            }
        }

        private void cmdReadOne_Clicked(object sender, EventArgs e)
        {
            using (var webClient = new HttpClient())
            {
                var resp = webClient.GetStringAsync(apiUrl + "/" + txtId.Text);
                resp.Wait();

                var json = resp.Result;
                var plato = Newtonsoft.Json.JsonConvert.DeserializeObject<Plato>(json);

                txtId.Text = plato.Id.ToString();
                txtNombre.Text = plato.Nombre;
                txtPrecio.Text = plato.Precio.ToString();
                txtDisponible.Text = plato.Disponible.ToString();
                txtRestauranteId.Text = plato.RestauranteId.ToString();
            }
        }

        private void cmdDelete_Clicked(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                var url = $"{apiUrl}/{txtId.Text}";
                client.BaseAddress = new Uri(url);
                client
                    .DefaultRequestHeaders
                    .Accept
                    .Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                var resp = client.DeleteAsync(url);
                resp.Wait();

                txtId.Text = "";
                txtNombre.Text = "";
                txtPrecio.Text = string.Empty;
                txtDisponible.Text = string.Empty;
                txtRestauranteId.Text = string.Empty;
            }
        }

        private async void cmdRegresar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

    }
}