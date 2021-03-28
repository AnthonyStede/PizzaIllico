using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PizzaIllico.Mobile.Dtos;
using PizzaIllico.Mobile.Dtos.Pizzas;
using PizzaIllico.Mobile.Services;
using PizzaIllico.Mobile.ViewModels;
using Storm.Mvvm.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PizzaIllico.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PizzaListPage
    {
        private List<long> panierpizzId;
        private double prix;
        private ObservableCollection<PizzaItem> Pizzas;
        private int _idShop;
        private string _nomResto;
        private string _addresseResto;
        public PizzaListPage(string nomResto, string addresseResto, int idShop)
        {
            _nomResto = nomResto;
            _addresseResto = addresseResto;
            _idShop = idShop;
            panierpizzId = new();
            prix = 0;
            BindingContext = new PizzaListViewModel(_idShop);
            InitializeComponent();
            nomRestaurantLabel.Text = nomResto;
            addresseRestaurantLabel.Text = addresseResto;
        }
        async void GoToPanier(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new PanierPage(_nomResto, _addresseResto, panierpizzId, prix, _idShop));
        }
        void AddPizzaPanier(object sender, System.EventArgs e)
        {
            Button btn = (Button)sender;
            long idPizza = Convert.ToInt64(btn.ClassId.ToString());
            Device.BeginInvokeOnMainThread(async () =>
            {
                IPizzaApiService service = DependencyService.Get<IPizzaApiService>();

                Response<List<PizzaItem>> response = await service.ListPizzas(_idShop);

                Console.WriteLine($"Appel HTTP : {response.IsSuccess}");
                if (response.IsSuccess)
                {
                    Console.WriteLine($"Appel HTTP : {response.Data.Count}");
                    Pizzas = new ObservableCollection<PizzaItem>(response.Data);
                }

                foreach (PizzaItem p in Pizzas)
                {
                    if (p.Id == idPizza) {
                        if (p.OutOfStock)
                        {
                            await this.DisplayAlert("Pas ok", "La pizza n'est plus en stock", "Ok");
                        }
                        else
                        {
                            panierpizzId.Add(idPizza);
                            await this.DisplayAlert("Ok", "La pizza a été ajoutée au panier", "Ok");
                            prix += p.Price;
                        }
                    }
                }
            });
        }
    }
}