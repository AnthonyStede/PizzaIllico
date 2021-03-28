using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PizzaIllico.Mobile.Dtos;
using PizzaIllico.Mobile.Dtos.Pizzas;
using Xamarin.Forms;

namespace PizzaIllico.Mobile.Services
{
    public interface IPizzaApiService
    {
        Task<Response<List<ShopItem>>> ListShops();

        Task<Response<List<PizzaItem>>> ListPizzas(int shopId);

        Task<Response<List<OrderItem>>> ListOrders();
    }
    
    public class PizzaApiService : IPizzaApiService
    {
        private readonly IApiService _apiService;

        public PizzaApiService()
        {
            _apiService = DependencyService.Get<IApiService>();
        }

        public async Task<Response<List<ShopItem>>> ListShops()
        {
	        return await _apiService.Get<Response<List<ShopItem>>>(Urls.LIST_SHOPS);
        }

        public async Task<Response<List<PizzaItem>>> ListPizzas(int shopId)
        {
            return await _apiService.Get<Response<List<PizzaItem>>>("api/v1/shops/" + shopId + "/pizzas");
        }

        public async Task<Response<List<OrderItem>>> ListOrders()
        {
            return await _apiService.Get<Response<List<OrderItem>>>(Urls.LIST_ORDERS);
        }

        public async Task<Response> CommanderAsync(int idShop, double prix, ShopItem shop)
        {
            var client = new HttpClient();
            var model = new OrderItem
            {
                Shop = shop,
                Date = DateTime.Now,
                Amount = prix

            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync("https://pizza.julienmialon.ovh/api/v1/shops/" + idShop, content);

            string jsonResponse = await response.Content.ReadAsStringAsync();

            Response responseBody = JsonConvert.DeserializeObject<Response>(jsonResponse);

            return responseBody;
        }
    }
}