// You should be updating this controller and views on your own as practice:

// see lesson 3.3.2.5 - Relationships with Entity Framework
// https://www.learnhowtoprogram.com/fidgetech-3-c-and-net/3-3-database-basics/3-3-2-5-relationships-with-entity-framework :

// Adding a Controller and Views

/* Next, we need to update our CategoriesController and replace the CRUD actions with
our new Entity-backed ones. This controller will look like the ItemsController we
completed in the last lesson. Because we've already covered this functionality, take
the opportunity to practice building out this controller and its corresponding views
on your own. You'll get a chance to do this on your next project. Use the
Categories/Index view to display a list of categories. In order to see the CRUD
functionality in action, let's go ahead and add a link in the homepage 
(Home/Index.cshtml) to go to our categories index view. */

// using Microsoft.AspNetCore.Mvc;
// using System.Collections.Generic;
// using ToDoList.Models;

// namespace ToDoList.Controllers
// {
//     public class CategoriesController : Controller
//     {
//         [HttpGet("/categories")]
//         public ActionResult Index()
//         {
//             List<Category> allCategories = Category.GetAll();
//             return View(allCategories);
//         }

//         [HttpGet("/categories/new")]
//         public ActionResult New()
//         {
//             return View();
//         }

//         [HttpPost("/categories")]
//         public ActionResult Create(string categoryName)
//         {
//             Category newCategory = new Category(categoryName);
//             return RedirectToAction("Index");
//         }

//         [HttpGet("/categories/{id}")]
//         public ActionResult Show(int id)
//         {
//             Dictionary<string, object> model = new Dictionary<string, object>();
//             Category selectedCategory = Category.Find(id);
//             List<Item> categoryItems = selectedCategory.Items;
//             model.Add("category", selectedCategory);
//             model.Add("items", categoryItems);
//             return View(model);
//         }

//         // This one creates new Items within a given Category, not new Categories:
//         [HttpPost("/categories/{categoryId}/items")]
//         public ActionResult Create(int categoryId, string itemDescription)
//         {
//             Dictionary<string, object> model = new Dictionary<string, object>();
//             Category foundCategory = Category.Find(categoryId);
//             Item newItem = new Item(itemDescription);
//             newItem.Save();
//             foundCategory.AddItem(newItem);
//             List<Item> categoryItems = foundCategory.Items;
//             model.Add("items", categoryItems);
//             model.Add("category", foundCategory);
//             return View("Show", model);
//         }
//     }
// }