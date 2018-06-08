using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookList_CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookList_CRUD.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BooksController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Books.ToList());
        }

        //Get : Book/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _db.Add(book);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }
        // Details : Book/Details/id
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = await _db.Books.SingleOrDefaultAsync(x => x.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            };
        }
    }
}