using PizzaIllico.Mobile.Pages;
using PizzaIllico.Mobile.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PizzaIllico.Mobile.ViewModels
{
    class ProfilViewModel 
    {

        private ApiService _apiService = new ApiService();
        public string Email { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public string AccessToken { get; set; }

        public ICommand UserProfilCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var accessToken = AccessToken;
                    var isSuccess = await _apiService.UserProfilAsync(accessToken);
                    
                    if (isSuccess.IsSuccess)
                    {
                        Email = "Your email is: "+ isSuccess.Data.Email;
                        UserFirstName = "Your First name is: " + isSuccess.Data.FirstName;
                        UserLastName = "Your Last name is: " + isSuccess.Data.LastName;
                        PhoneNumber = "Your phone number is: " + isSuccess.Data.PhoneNumber;
                        await Application.Current.MainPage.Navigation.PushAsync(new ProfilPage());
                    }
                    else
                    {
                        await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
                    }
                });

            }

        }
    }
}
