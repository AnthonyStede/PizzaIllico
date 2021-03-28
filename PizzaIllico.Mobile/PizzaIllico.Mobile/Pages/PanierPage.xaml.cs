using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaIllico.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PizzaIllico.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PanierPage
    {
        public PanierPage(string nomResto, string addresseResto, List<long> panierpizzId, double prix, int idShop)
        {
            BindingContext = new PanierViewModel(idShop, panierpizzId, prix);
            InitializeComponent();
            nomRestaurantLabel.Text = nomResto;
            addresseRestaurantLabel.Text = addresseResto;
            prixLabel.Text = "Prix total : " + prix + "€";
        }
        async void ValiderCommande(object sender, System.EventArgs e)
        {
            await this.DisplayAlert("Confirmé", "La commande a été passée !", "Ok");
            for (var counter = 1; counter < 2; counter++)
            {
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            }
            await Navigation.PopAsync();
        }
    }
}