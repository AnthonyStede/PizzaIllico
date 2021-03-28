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
    class LoginViewModel : INotifyPropertyChanged
    {

        private ApiService _apiService = new ApiService();
        public string Login { get; set; }

        public string Password { get; set; }
        public string AccessToken { get; set; }
        public string Message { get; set; }
        
        public event PropertyChangedEventHandler PropertyChanged;

     

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var eventHandler = PropertyChanged;
            eventHandler?.Invoke(this, e);
            
        }

        public ICommand LoginCommand
        {
            get
            {
                return new Command(async() =>
                {
                    var isSuccess = await _apiService.LoginAsync(Login, Password);                   
                    
                    if (isSuccess.IsSuccess)
                    {
                        Message = "Login Success!";
                        OnPropertyChanged(new PropertyChangedEventArgs(nameof(Message)));
                        await Application.Current.MainPage.Navigation.PushAsync(new ShopListPage());
                        AccessToken = isSuccess.Data.AccessToken;
                        OnPropertyChanged(new PropertyChangedEventArgs(nameof(AccessToken)));
                    }
                    else
                    {
                        Message = "Login failed, Retry please";
                        OnPropertyChanged(new PropertyChangedEventArgs(nameof(Message)));
                    }
                });
            }
           
        }

       
    }
}
