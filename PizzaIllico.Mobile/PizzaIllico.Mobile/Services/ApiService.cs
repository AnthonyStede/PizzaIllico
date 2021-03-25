using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using PizzaIllico.Mobile.Dtos.Accounts;

namespace PizzaIllico.Mobile.Services
{
    public interface IApiService
    {
        Task<TResponse> Get<TResponse>(string url);
    }
    public interface ILoginCommandParameter
    {
        string ClientId { get; }
        string ClientSecret { get; }
    }

    public class ApiService : IApiService
    {
	    private const string HOST = "https://pizza.julienmialon.ovh/";
        private readonly HttpClient _client = new HttpClient();
        
        public async Task<TResponse> Get<TResponse>(string url)
        {
	        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, HOST + url);

	        HttpResponseMessage response = await _client.SendAsync(request);

	        string content = await response.Content.ReadAsStringAsync();

	        return JsonConvert.DeserializeObject<TResponse>(content);
        }

        public async Task <bool> RegisterAsync(string email, string password, string userFirstName, string userLastName, string phoneNumber)
        {
            var client = new HttpClient();

            var model = new CreateUserRequest { 
                Email= email,
                Password = password,
                FirstName = userFirstName,
                LastName = userLastName,
                PhoneNumber= phoneNumber
            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);

            var response = await client.PostAsync("https://pizza.julienmialon.ovh/api/v1/accounts/register",content);

            return response.IsSuccessStatusCode;
        }
    }
}