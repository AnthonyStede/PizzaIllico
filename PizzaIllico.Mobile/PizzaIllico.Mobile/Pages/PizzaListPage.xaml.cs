using PizzaIllico.Mobile.ViewModels;
using Storm.Mvvm.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PizzaIllico.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PizzaListPage
    {
        public PizzaListPage()
        {
            BindingContext = new PizzaListViewModel();
            InitializeComponent();
        }
        void AddPizzaPanier(object sender, System.EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await this.DisplayAlert("Ok", "La pizza a été ajoutée au panier", "Ok");
            });
        }
    }
}