using App2H4.Services;
using App2H4.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2H4
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
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
