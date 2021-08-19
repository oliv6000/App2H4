using App2H4.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace App2H4.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}