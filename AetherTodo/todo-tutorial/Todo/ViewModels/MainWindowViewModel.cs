using System;
using System.Reactive.Linq;
using Avalonia.Interactivity;
using ReactiveUI;
using Todo.Models;
using Todo.Services;


namespace Todo.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        ViewModelBase content;
        Database db = new();
        
        public MainWindowViewModel(Database db)
        {
            Content = List = new TodoListViewModel(db.GetItems());
        }

        public ViewModelBase Content
        {
            get => content;
            private set => this.RaiseAndSetIfChanged(ref content, value);
        }

        public TodoListViewModel List { get; set; }

        public void RemoveItem(int id)
        {
            db.DeleteItem(id);
            Content = List = new TodoListViewModel(db.GetItems());

        }

        public void AddItem()
        {
            var vm = new AddItemViewModel();

            Observable.Merge(
                vm.Ok,
                vm.Cancel.Select(_ => (TodoItem)null))
                .Take(1)
                .Subscribe(model =>
                {
                    if (model != null)
                    {
                        List.Items.Add(model);
                    }

                    Content = List;
                });

            Content = vm;
        }

        
    }
}
