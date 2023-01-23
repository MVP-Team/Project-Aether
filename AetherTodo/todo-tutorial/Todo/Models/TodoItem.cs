using System;

namespace Todo.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsChecked { get; set; }

        /*public TodoItem()
        {

        }

        public TodoItem(int id, string description, bool isChecked)
        {
            Id = id;
            Description = description;
            IsChecked = isChecked;
        }*/
    }
}