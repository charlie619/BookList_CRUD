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

        //Get : Books/Create
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
        // Get : Books/Details/id
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

        // Get : Books/Edit/id
        public async Task<IActionResult> Edit(int? id)
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

        // Post : Books/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Update(book);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // Get : Books/Delete/id
        public async Task<IActionResult> Delete(int? id)
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

        // Post : Books/Delete/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _db.Books.SingleOrDefaultAsync(x => x.Id == id);
            _db.Remove(book);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));        
       
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