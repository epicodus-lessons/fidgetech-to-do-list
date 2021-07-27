using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class CategoriesController : Controller
    {
        [HttpGet("/categories")]
        public ActionResult Index()
        {
            List<Category> allCategories = Category.GetAll();
            return View(allCategories);
        }

        [HttpGet("/categories/new")]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost("/categories")]
        public ActionResult Create(string categoryName)
        {
            Category newCategory = new Category(categoryName);
            return RedirectToAction("Index");
        }

        [HttpGet("/categories/{id}")]
        public ActionResult Show(int id)
        {
            Category selectedCategory = Category.Find(id);
            return View(selectedCategory);
        }

        // This one creates new Items within a given Category, not new Categories:
        [HttpPost("/categories/{categoryId}/items")]
        public ActionResult Create(int categoryId, string itemDescription)
        {
            Category foundCategory = Category.Find(categoryId);
            Item newItem = new Item(itemDescription);
            foundCategory.AddItem(newItem);
            return View("Show", foundCategory);
        }
    }
}