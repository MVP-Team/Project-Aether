using System;
using System.Reactive;
using Avalonia.FreeDesktop.DBusIme;
using Microsoft.CodeAnalysis.Operations;
using ReactiveUI;
using Todo.Models;
using Todo.Services;

namespace Todo.ViewModels
{
    class AddItemViewModel : ViewModelBase
    {
        string description;
        bool isChecked;
        int id;

        readonly Database dbs = new();

        public AddItemViewModel()
        {
            var okEnabled = this.WhenAnyValue(
                x => x.Description,
                x => !string.IsNullOrWhiteSpace(x) && x.Length < 18);
                

            Ok = ReactiveCommand.Create(
                () => AddItem(), 
                okEnabled);
            Cancel = ReactiveCommand.Create(() => { });
        }

        public string Description
        {
            get => description;
            set => this.RaiseAndSetIfChanged(ref description, value);
        }

        public bool IsChecked
        {
            get => isChecked;
            set => this.RaiseAndSetIfChanged(ref isChecked, value);
            
        }

        public int Id
        {
            get => id;
            set => this.RaiseAndSetIfChanged(ref id, value);
        }

        public TodoItem AddItem()
        {
            TodoItem todo = new() { Description = Description, IsChecked = IsChecked };
            int IsCheckedForDb = (todo.IsChecked) ? 1 : 0;
            dbs.AddItem(todo.Description, IsCheckedForDb);

            return todo; 
        }


        public ReactiveCommand<Unit, TodoItem> Ok { get; }
        public ReactiveCommand<Unit, Unit> Cancel { get; }
    }
}
