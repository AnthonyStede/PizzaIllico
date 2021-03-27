using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using PizzaIllico.Mobile.Dtos.Accounts;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Diagnostics;
using PizzaIllico.Mobile.Dtos.Authentications.Credentials;

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

    public class CreateUserCommandParameter : ILoginCommandParameter
    {
        public CreateUserRequest Data { get; set; }
        public string ClientId => Data.ClientId;
        public string ClientSecret => Data.ClientSecret;
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

        

        public async Task <ResponseBody> RegisterAsync(string email, string password, string userFirstName, string userLastName, string phoneNumber)
        {
            var client = new HttpClient();

            /*IUserService userService = Services.GetService<IUserService>();
            User existingAccount = await userService.GetUser(parameter.Data.Email);

            if (existingAccount is { })
            {
                throw new DomainException(Errors.EMAIL_ALREADY_EXISTS, "This email is already used by another account");
            }*/


            var model = new CreateUserRequest {
                ClientId = "MOBILE",
                ClientSecret = "UNIV",
                Email = email,
                Password = password,
                FirstName = userFirstName,
                LastName = userLastName,
                PhoneNumber= phoneNumber
            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync("https://pizza.julienmialon.ovh/api/v1/accounts/register", content);

            string jsonResponse = await response.Content.ReadAsStringAsync();

            ResponseBody responseBody = JsonConvert.DeserializeObject<ResponseBody>(jsonResponse);

            return responseBody;
        }

        public async Task<ResponseBody> LoginAsync (string login, string password)
        {

            var client = new HttpClient();

            var LoginModel = new LoginWithCredentialsRequest
            {
                Login = login,
                Password = password,
                ClientId = "MOBILE",
                ClientSecret = "UNIV"
            };
            var json = JsonConvert.SerializeObject(LoginModel);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync("https://pizza.julienmialon.ovh/api/v1/authentication/credentials", content);

            var jsonResponse = await response.Content.ReadAsStringAsync();

            Debug.WriteLine(content);

            ResponseBody responseBody = JsonConvert.DeserializeObject<ResponseBody>(jsonResponse);

            return responseBody;
        }
    }
}