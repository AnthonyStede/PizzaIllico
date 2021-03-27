using PizzaIllico.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PizzaIllico.Mobile.ViewModels
{
    class LoginViewModel
    {

        private ApiService _apiService = new ApiService();
        public string Login { get; set; }

        public string Password { get; set; }

        public ICommand LoginCommand
        {
            get
            {
                return new Command(async() =>
                {
                    var isSuccess = await _apiService.LoginAsync(Login, Password);
                    if (isSuccess.IsSuccess)
                    {

                    }
                    else
                    {

                    }
                });
            }
        }
    }
}
