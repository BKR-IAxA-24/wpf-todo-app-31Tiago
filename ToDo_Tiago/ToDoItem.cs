using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo_Tiago
{
    internal class ToDoItem
    {
        public string Titel { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime EntryDate { get; set; }

        public ToDoItem(string titel)
        {
            Titel = titel;
            IsCompleted = false; 
            EntryDate = DateTime.Now; 
        }

        public override string ToString()
        {
            return $"{Titel} (Hinzugefügt am {EntryDate.ToShortDateString()}) - {(IsCompleted ? "Erledigt" : "Offen")}";
        }
    }
}
