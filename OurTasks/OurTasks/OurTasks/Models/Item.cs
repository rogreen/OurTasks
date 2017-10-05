using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurTasks
{
    public class Item
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string AssignedTo { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DueDate { get; set; }
        public bool Completed { get; set; }
    }
}
