using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using PizzaIllico.Mobile.Dtos;
using PizzaIllico.Mobile.Dtos.Pizzas;
using PizzaIllico.Mobile.Services;
using Storm.Mvvm;
using Xamarin.Forms;

namespace PizzaIllico.Mobile.ViewModels
{
    class RegisterViewModel : INotifyPropertyChanged 
    {
        ApiService _apiService = new ApiService();

        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var eventHandler = PropertyChanged;
            eventHandler?.Invoke(this, e);
        }

        public ICommand RegisterCommand
        {
            
            get
            {
                return new Command(async() =>
                {                                 
                   
                    var isSuccess = await _apiService.RegisterAsync(Email, Password, UserFirstName, UserLastName, PhoneNumber);         
                    
                    if (isSuccess.IsSuccess) 
                    {
                        Message = "Register successfully";
                        OnPropertyChanged(new PropertyChangedEventArgs(nameof(Message)));
                    }
                    
                    
                    else
                    {
                        Message = "Register failed, Retry please";
                        OnPropertyChanged(new PropertyChangedEventArgs(nameof(Message)));
                    }
                
                }); 
            }

        }

       
    }
}
