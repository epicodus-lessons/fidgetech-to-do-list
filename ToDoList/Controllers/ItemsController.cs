using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ToDoListContext _db;

        public ItemsController(ToDoListContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            return View(_db.Items.ToList());
        }

        public ActionResult Details(int id)
        {
            Item thisItem = _db.Items
                .Include(item => item.JoinEntities)
                    .ThenInclude(join => join.Category)
                .FirstOrDefault(item => item.ItemId == id);
            return View(thisItem);
        }

        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Item item, int categoryId)
        {
            _db.Items.Add(item);
            _db.SaveChanges();

            if (categoryId != 0)
            {
                _db.CategoriesItems.Add(new CategoryItem() { CategoryId = categoryId, ItemId = item.ItemId });
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            Item thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
            ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
            return View(thisItem);
        }

        [HttpPost]
        public ActionResult Edit(Item item, int categoryId)
        {
            bool duplicate = _db.CategoriesItems.Any(catItem =>
                catItem.CategoryId == categoryId && catItem.ItemId == item.ItemId);

            if (categoryId != 0 && !duplicate)
            {
                _db.CategoriesItems.Add(new CategoryItem() { CategoryId = categoryId, ItemId = item.ItemId });
            }
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddCategory(int id)
        {
            Item thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
            ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
            return View(thisItem);
        }

        [HttpPost]
        public ActionResult AddCategory(Item item, int categoryId)
        {
            bool duplicate = _db.CategoriesItems.Any(join =>
              join.CategoryId == categoryId && join.ItemId == item.ItemId);

            if (categoryId != 0 && !duplicate)
            {
                _db.CategoriesItems.Add(new CategoryItem() { CategoryId = categoryId, ItemId = item.ItemId });
            }

            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //         public ActionResult Delete(int id)
        //         {
        //             Item thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
        //             return View(thisItem);
        //         }

        //         [HttpPost, ActionName("Delete")]
        //         public ActionResult DeleteConfirmed(int id)
        //         {
        //             Item thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
        //             _db.Items.Remove(thisItem);
        //             _db.SaveChanges();
        //             return RedirectToAction("Index");
        //         }
    }
}