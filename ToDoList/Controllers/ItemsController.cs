using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    [Authorize]
    public class ItemsController : Controller
    {
        private readonly ToDoListContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public ItemsController(UserManager<ApplicationUser> userManager, ToDoListContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public async Task<ActionResult> Index()
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
            List<Item> userItems = _db.Items.Where(entry => entry.User.Id == currentUser.Id).ToList();
            return View(userItems);
        }

        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Item item, int CategoryId)
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
            item.User = currentUser;
            _db.Items.Add(item);
            if (CategoryId != 0)
            {
                _db.CategoryItem.Add(new CategoryItem() { CategoryId = CategoryId, ItemId = item.ItemId });
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public ActionResult Details(int id)
        {
            Item thisItem = _db.Items
                .Include(item => item.Categories)
                    .ThenInclude(join => join.Category)
                .FirstOrDefault(item => item.ItemId == id);
            return View(thisItem);
        }

        public ActionResult Edit(int id)
        {
            Item thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
            ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
            return View(thisItem);
        }

        [HttpPost]
        public ActionResult Edit(Item item, int CategoryId)
        {
            if (CategoryId != 0)
            {
                _db.CategoryItem.Add(new CategoryItem() { CategoryId = CategoryId, ItemId = item.ItemId });
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
        public ActionResult AddCategory(Item item, int CategoryId)
        {
            if (CategoryId != 0)
            {
                _db.CategoryItem.Add(new CategoryItem() { CategoryId = CategoryId, ItemId = item.ItemId });
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Item thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
            return View(thisItem);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Item thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
            _db.Items.Remove(thisItem);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteCategory(int joinId)
        {
            CategoryItem joinEntry = _db.CategoryItem.FirstOrDefault(entry => entry.CategoryItemId == joinId);
            _db.CategoryItem.Remove(joinEntry);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}