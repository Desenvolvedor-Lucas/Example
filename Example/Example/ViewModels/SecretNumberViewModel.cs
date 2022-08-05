using Example.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Example.ViewModels
{
    public class SecretNumberViewModel : ObservableProperty
    {
        private int _entryNumber;
        public int EntryNumber 
        { 
            get { return _entryNumber; }
            set 
            { 
                _entryNumber = value;
                OnPropertyChanged();
            }
        }

        private string _sourceImage;

        public string SourceImage
        {
            get { return _sourceImage; }
            set 
            { 
                _sourceImage = value;
                OnPropertyChanged();
            }
        }

        public ICommand CheckNumberCommand { get; set; }
        public ICommand ResetSecretNumber { get; set; }

        public SecretNumberViewModel()
        {
            EntryNumber = 1;
            int secretNumber = new Random().Next(1, 10);
            SourceImage = "Detective.jpg";

            CheckNumberCommand = new Command(async () => 
            {
                var isSecretNumber = await CheckAsync(secretNumber);
                if (isSecretNumber)
                {
                    secretNumber = new Random().Next(1, 10);
                    SourceImage = "VeryGood.jpg";
                }
                else
                    SourceImage = "IMSorry.png";
                    
            });

            ResetSecretNumber = new Command(() => 
            {
                secretNumber = new Random().Next(1, 10);
                SourceImage = "Detective.jpg";
            });
        }

        private async Task<bool> CheckAsync(int secretNumber)
        {
            if (EntryNumber == secretNumber)
            {
                await App.Current.MainPage.DisplayAlert($"Is {secretNumber}, very good",
                    "You got the secret number right, but can you get it right again? Let's find out...",
                    "Ok");
                return true;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("I'm sorry",
                    "That's not the secret number but you can try again.",
                    "Ok");
                return false;
            } 
        }
    }
}
