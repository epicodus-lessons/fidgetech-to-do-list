using System.Collections.Generic;

namespace ToDoList.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Description { get; set; }
        public virtual ICollection<CategoryItem> JoinEntities { get; }

        public Item()
        {
            this.JoinEntities = new HashSet<CategoryItem>();
        }
    }
}