using Microsoft.EntityFrameworkCore;

namespace ToDoList.Models
{
    public class ToDoListContext : DbContext
    {
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<CategoryItem> CategoryItem { get; set; }

        public ToDoListContext(DbContextOptions options) : base(options) { }
    }
}