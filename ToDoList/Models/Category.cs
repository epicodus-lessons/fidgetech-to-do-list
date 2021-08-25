using System.Collections.Generic;

namespace ToDoList.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<CategoryItem> JoinEntities { get; set; }

        public Category()
        {
            this.JoinEntities = new HashSet<CategoryItem>();
        }
    }
}