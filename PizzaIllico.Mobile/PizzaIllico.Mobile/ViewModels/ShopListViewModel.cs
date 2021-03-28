using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using MvvmCross.ViewModels;
using PizzaIllico.Mobile.Dtos;
using PizzaIllico.Mobile.Dtos.Pizzas;
using PizzaIllico.Mobile.Services;
using Storm.Mvvm;
using Xamarin.Forms;

namespace PizzaIllico.Mobile.ViewModels
{
	public class ShopListViewModel : ViewModelBase, INotifyPropertyChanged
	{
		private ObservableCollection<ShopItem> _shops;

		public ObservableCollection<ShopItem> Shops
		{
			get => _shops;
			set => SetProperty(ref _shops, value);
		}
        public string AccessToken { get; set; }

        public ICommand SelectedCommand { get; }

		public event PropertyChangedEventHandler PropertyChanged;

		public ShopListViewModel()
		{
			SelectedCommand = new Command<ShopItem>(SelectedAction);
		}

		private void SelectedAction(ShopItem obj)
		{

		}
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			var eventHandler = PropertyChanged;
			eventHandler?.Invoke(this, e);

		}
		public override async Task OnResume()
		{
			await base.OnResume();

			IPizzaApiService service = DependencyService.Get<IPizzaApiService>();

			Response<List<ShopItem>> response = await service.ListShops();

            Messenger.Default.Register<string>(this, AccessTokenRecive);


			OnPropertyChanged(new PropertyChangedEventArgs(nameof(AccessToken)));
                  
			Console.WriteLine($"Appel HTTP : {response.IsSuccess}");
			if (response.IsSuccess)
			{
				Console.WriteLine($"Appel HTTP : {response.Data.Count}");
				Shops = new ObservableCollection<ShopItem>(response.Data);
			}
		}

		private void AccessTokenRecive(string accesstoken)
        {
			AccessToken = accesstoken;
        }

	}
}