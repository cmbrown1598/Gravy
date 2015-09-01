using System;
using System.Collections.Generic;

namespace Gravy
{
    public class TodoListItem
    {
        private List<TodoListItem> children = new List<TodoListItem>(); 

        public static List<TodoListItem> GetTodoList()
        {
            var retValue = new List<TodoListItem>
            {
                new TodoListItem
                {
                    Category = "Home Repair",
                    Completed = false,
                    Cost = 199.99m,
                    Description = "Replace the screen door.",
                    Identifier = 24,
                    Priority = 3
                },
                new TodoListItem
                {
                    Category = "Home Repair",
                    Completed = false,
                    Cost = 0.0m,
                    Description = "Fix the clogged sink",
                    Identifier = 468,
                    Priority = 0
                },
                new TodoListItem
                {
                    Category = "Home Repair",
                    Completed = true,
                    Cost = 2596.78m,
                    Description = "Put the flooring down",
                    Identifier = 721,
                    Priority = -1
                },
                new TodoListItem
                {
                    Category = "Purchases",
                    Completed = false,
                    Cost = 200.0m,
                    Description = "School supplies for the kids",
                    Identifier = 112,
                    Priority = 2
                },
                new TodoListItem
                {
                    Category = "Purchases",
                    Completed = true,
                    Cost = 39.95m,
                    Description = "Pet food",
                    Identifier = 89,
                    Priority = 0
                },


            };

            return retValue;
        }


        public int Identifier { get; set; }
        public double Priority { get; set; }
        public bool Completed { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }

        public List<TodoListItem> Sublist {
            get { return children; }
        }

        public string NewYork = "New York";
    }
}