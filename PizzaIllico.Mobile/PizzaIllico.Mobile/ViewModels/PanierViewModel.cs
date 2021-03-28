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
		private List<long> _pizzaPanierId;
		private int _idShop;

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

		public ICommand SelectedCommand { get; }

		public PanierViewModel(int idShop, List<long> pizzaPanierId)
		{
			_idShop = idShop;
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
							Console.WriteLine($"Id_liste : " + i + " / id_base = " + p.Id);
							PizzasFinal.Add(p);
                        }
                    }
                }
			}
		}
	}
}