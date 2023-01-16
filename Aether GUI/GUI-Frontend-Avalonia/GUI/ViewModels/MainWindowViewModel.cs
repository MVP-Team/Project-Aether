using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using GUI.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GUI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        //Bindings
        public string Greeting => "Welcome to Aether, how can I help you?";
        //public Window mainWindow
        private bool _txtBoxVisibility = false;
        public bool TxtBoxVisibility
        {
            get => _txtBoxVisibility;
            set => this.RaiseAndSetIfChanged(ref _txtBoxVisibility, value);
        }


        //Eigene var
        private bool btnState = false;
        public void EnableTxtInput()
        {
            btnState = !btnState;
            TxtBoxVisibility = btnState;
        }
    }
}
