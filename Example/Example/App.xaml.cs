using Example.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Example
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new SecretNumberView());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
