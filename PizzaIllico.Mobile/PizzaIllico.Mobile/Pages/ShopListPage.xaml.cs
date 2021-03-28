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
    public partial class ShopListPage
    {
        private ObservableCollection<ShopItem> Shops;
        public ShopListPage()
        {
            //BindingContext = new ShopListViewModel();
            BindingContext = new ProfilViewModel();
            InitializeComponent();
        }
        async void GoToCommandes(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new OrderListPage());
        }
        async void GoToProfil(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new ProfilPage());
        }

        async void CheckShop(object sender, System.EventArgs e)
        {
            Button btn = (Button)sender;
            int idShop = Convert.ToInt32(btn.ClassId.ToString());

            Device.BeginInvokeOnMainThread(async () =>
            {
                IPizzaApiService service = DependencyService.Get<IPizzaApiService>();

                Response<List<ShopItem>> response = await service.ListShops();

                Console.WriteLine($"Appel HTTP : {response.IsSuccess}");
                if (response.IsSuccess)
                {
                    Console.WriteLine($"Appel HTTP : {response.Data.Count}");
                    Shops = new ObservableCollection<ShopItem>(response.Data);
                }

                foreach (ShopItem s in Shops)
                {
                    if (s.Id == idShop)
                    {
                        await Navigation.PushAsync(new PizzaListPage(s.Name, s.Address, idShop));
                    }
                }
            });
        }
    }
}