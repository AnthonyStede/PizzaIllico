using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using PizzaIllico.Mobile.Dtos;
using PizzaIllico.Mobile.Dtos.Pizzas;
using PizzaIllico.Mobile.Services;
using Storm.Mvvm;
using Xamarin.Forms;

namespace PizzaIllico.Mobile.ViewModels
{
	public class PanierViewModel : ViewModelBase
	{
		private ObservableCollection<PizzaItem> _pizzas;
		private ObservableCollection<PizzaItem> _pizzasFinal;
		private ObservableCollection<ShopItem> _shops;
		private List<long> _pizzaPanierId;
		private int _idShop;
		private double _prix;
		ShopItem _shop;
		PizzaApiService _apiService = new PizzaApiService();

		public ObservableCollection<PizzaItem> Pizzas
		{
			get => _pizzas;
			set => SetProperty(ref _pizzas, value);
		}
		public ObservableCollection<PizzaItem> PizzasFinal
		{
			get => _pizzasFinal;
			set => SetProperty(ref _pizzasFinal, value);
		}

		public ObservableCollection<ShopItem> Shops
		{
			get => _shops;
			set => SetProperty(ref _shops, value);
		}
		public ICommand SelectedCommand { get; }

		public PanierViewModel(int idShop, List<long> pizzaPanierId, double prix)
		{
			_idShop = idShop;
			_prix = prix;
			_pizzaPanierId = pizzaPanierId;
			SelectedCommand = new Command<PizzaItem>(SelectedAction);
		}

		private void SelectedAction(PizzaItem obj)
		{

		}

		public override async Task OnResume()
		{
			await base.OnResume();

			IPizzaApiService service = DependencyService.Get<IPizzaApiService>();

			Response<List<PizzaItem>> response = await service.ListPizzas(_idShop);

			Console.WriteLine($"Appel HTTP : {response.IsSuccess}");
			if (response.IsSuccess)
			{
				Console.WriteLine($"Appel HTTP : {response.Data.Count}");
				Pizzas = new ObservableCollection<PizzaItem>(response.Data);
				PizzasFinal = new ObservableCollection<PizzaItem>();
				foreach(PizzaItem p in Pizzas)
                {
					foreach(int i in _pizzaPanierId)
                    {
						if(i == p.Id)
                        {
							PizzasFinal.Add(p);
                        }
                    }
                }
			}
		}

		public ICommand CommanderCommand
		{

			get
			{
				return new Command(async () =>
				{
					IPizzaApiService service = DependencyService.Get<IPizzaApiService>();
					Response<List<ShopItem>> response = await service.ListShops();
					Shops = new ObservableCollection<ShopItem>(response.Data);
					foreach (ShopItem s in Shops)
					{
						if (_idShop == s.Id)
						{
							_shop = s;
						}
					}

					var isSuccess = await _apiService.CommanderAsync(_idShop, _prix, _shop);
					Console.WriteLine($"Appel HTTP : {isSuccess.IsSuccess}");
					Console.WriteLine($"Error message : {isSuccess.ErrorMessage}");
					Console.WriteLine($"code : {isSuccess.ErrorCode}");
				});
			}

		}
	}
}