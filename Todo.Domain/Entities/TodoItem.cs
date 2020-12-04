using System;

namespace Todo.Domain.Entities
{
    public class TodoItem : Entity
    {
        public TodoItem(string title, bool done, DateTime date, string user)
        {
            Title = title;
            Done = done;
            Date = date;
            User = user;
        }

        public string Title { get; private set; }
        public bool Done { get; private set; }
        public DateTime Date { get; private set; }
        public string User { get; private set; } // RefUser

        public void MarkAsDone()
        {
            Done = true;
        }
        public void MarkAsUndone()
        {
            Done = false;
        }

        public void UpdateTitle(string title)
        {
            Title = title;
        }

    }
}