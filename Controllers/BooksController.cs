using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;

namespace ContosoUniversity.Controllers
{
    public class BooksController : Controller
    {
        private readonly SchoolContext _context;

        public BooksController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index(
                 string sortOrder,
                 string currentFilter,
                 string searchString,
                 int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["AuthorSortParam"] = sortOrder == "Author" ? "Author_desc" : "Author";
            ViewData["SectionSortParam"] = sortOrder == "Section" ? "Section_desc" : "Section";
            ViewData["PublisherSortParamSortParam"] = sortOrder == "Publisher" ? "Publisher_desc" : "Publisher";
            ViewData["BookNameParamSortParam"] = sortOrder == "BookName" ? "BookName_desc" : "BookName";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var bookVar = from s in _context.Books.Include(i => i.Author).Include(i => i.Section).Include(i => i.Publisher) select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                bookVar = bookVar.Where(s =>
                                        s.BookName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Author_desc":
                    bookVar = bookVar.OrderByDescending(s => s.Author);
                    break;
                case "Author":
                    bookVar = bookVar.OrderBy(s => s.Author);
                    break;
                case "Section_desc":
                    bookVar = bookVar.OrderByDescending(s => s.Section);
                    break;
                case "Section":
                    bookVar = bookVar.OrderBy(s => s.Section);
                    break;
                case "Publisher_desc":
                    bookVar = bookVar.OrderByDescending(s => s.Publisher);
                    break;
                case "Publisher":
                    bookVar = bookVar.OrderBy(s => s.Publisher);
                    break;
                case "BookName_desc":
                    bookVar = bookVar.OrderByDescending(s => s.BookName);
                    break;
                case "BookName":
                    bookVar = bookVar.OrderBy(s => s.BookName);
                    break;
                default:
                    bookVar = bookVar.OrderBy(s => s.Author);
                    break;
            }
            int pageSize = 3;
            return View(await PaginatedList<Book>.CreateAsync(bookVar.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.Section)
                .FirstOrDefaultAsync(m => m.BookID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["AuthorID"] = new SelectList(_context.Authors, "AuthorID", "Name");
            ViewData["PublisherID"] = new SelectList(_context.Publishers, "PublisherID", "PublisherName");
            ViewData["SectionID"] = new SelectList(_context.Sections, "SectionID", "SectionName");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookID,AuthorID,SectionID,PublisherID,BookName,YearOfPublishing")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorID"] = new SelectList(_context.Authors, "AuthorID", "Name", book.AuthorID);
            ViewData["PublisherID"] = new SelectList(_context.Publishers, "PublisherID", "PublisherName", book.PublisherID);
            ViewData["SectionID"] = new SelectList(_context.Sections, "SectionID", "SectionName", book.SectionID);
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["AuthorID"] = new SelectList(_context.Authors, "AuthorID", "Name", book.AuthorID);
            ViewData["PublisherID"] = new SelectList(_context.Publishers, "PublisherID", "PublisherName", book.PublisherID);
            ViewData["SectionID"] = new SelectList(_context.Sections, "SectionID", "SectionName", book.SectionID);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookID,AuthorID,SectionID,PublisherID,BookName,YearOfPublishing")] Book book)
        {
            if (id != book.BookID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorID"] = new SelectList(_context.Authors, "AuthorID", "Name", book.AuthorID);
            ViewData["PublisherID"] = new SelectList(_context.Publishers, "PublisherID", "PublisherName", book.PublisherID);
            ViewData["SectionID"] = new SelectList(_context.Sections, "SectionID", "SectionName", book.SectionID);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.Section)
                .FirstOrDefaultAsync(m => m.BookID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookID == id);
        }
    }
}
