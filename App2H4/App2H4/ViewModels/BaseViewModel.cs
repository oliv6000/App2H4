using App2H4.Models;
using App2H4.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace App2H4.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string day1;
        public string Day1
        {
            get { return day1; }
            set { SetProperty(ref day1, value); }
        }
        string day2;
        public string Day2
        {
            get { return day2; }
            set { SetProperty(ref day2, value); }
        }
        string day3;
        public string Day3
        {
            get { return day3; }
            set { SetProperty(ref day3, value); }
        }
        string day4;
        public string Day4
        {
            get { return day4; }
            set { SetProperty(ref day4, value); }
        }
        string day5;
        public string Day5
        {
            get { return day5; }
            set { SetProperty(ref day5, value); }
        }


        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        string temp;
        public string Temp
        {
            get { return temp; }
            set { SetProperty(ref temp, value); }
        }

        string city;
        public string City
        {
            get { return city; }
            set { SetProperty(ref city, value); }
        }

        string icon;
        public string Icon
        {
            get { return icon; }
            set { SetProperty(ref icon, value); }
        }

        StackLayout stacklayout;
        public StackLayout Stacklayout
        {
            get { return stacklayout; }
            set { SetProperty(ref stacklayout, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
