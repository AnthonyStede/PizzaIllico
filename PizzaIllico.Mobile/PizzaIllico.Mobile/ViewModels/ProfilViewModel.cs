using PizzaIllico.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Text;

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



    }
}
