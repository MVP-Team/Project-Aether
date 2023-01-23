using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using Todo.ViewModels;
using Todo.Services;

namespace Todo.Views
{
    public class TodoListView : UserControl
    {
        public TodoListView()
        {
            this.InitializeComponent();
        }
        

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
