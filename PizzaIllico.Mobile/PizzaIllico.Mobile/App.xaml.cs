using System.Diagnostics;
using PizzaIllico.Mobile.Pages;
using PizzaIllico.Mobile.Services;
using Storm.Mvvm;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace PizzaIllico.Mobile
{
    public partial class App : MvvmApplication
    {
        public App() : base(() => new NavigationPage(new LoginPage()),RegisterServices)
        {
/*#if DEBUG
            Log.Listeners.Add(new DelegateLogListener((arg1, arg2) => Debug.WriteLine($"{arg1} : {arg2}")));
#endif*/
            InitializeComponent();
        }

       private static void RegisterServices()
        {
            DependencyService.RegisterSingleton<IApiService>(new ApiService());

            DependencyService.RegisterSingleton<IPizzaApiService>(new PizzaApiService());
        }
       

     }

    
}