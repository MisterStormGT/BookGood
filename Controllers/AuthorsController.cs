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
    public class AuthorsController : Controller
    {
        private readonly SchoolContext _context;

        public AuthorsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Authors
        public async Task<IActionResult> Index(
                string sortOrder,
                string currentFilter,
                string searchString,
                int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["SurnameSortParam"] = sortOrder == "Surname" ? "Surname_desc" : "Surname";
            ViewData["NameSortParam"] = sortOrder == "Name" ? "Name_desc" : "Name";
            ViewData["MiddleNameSortParam"] = sortOrder == "MiddleName" ? "MiddleName_desc" : "MiddleName";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var authorVar = from s in _context.Authors select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                authorVar = authorVar.Where(s =>
                                        s.Surname.Contains(searchString) ||
                                        s.Name.Contains(searchString)||
                                        s.MiddleName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Surname_desc":
                    authorVar = authorVar.OrderByDescending(s => s.Surname);
                    break;
                case "Surname":
                    authorVar = authorVar.OrderBy(s => s.Surname);
                    break;
                case "Name_desc":
                    authorVar = authorVar.OrderByDescending(s => s.Name);
                    break;
                case "Name":
                    authorVar = authorVar.OrderBy(s => s.Name);
                    break;
                case "MiddleName_desc":
                    authorVar = authorVar.OrderByDescending(s => s.MiddleName);
                    break;
                case "MiddleName":
                    authorVar = authorVar.OrderBy(s => s.MiddleName);
                    break;
                default:
                    authorVar = authorVar.OrderBy(s => s.Surname);
                    break;
            }
            int pageSize = 3;
            return View(await PaginatedList<Author>.CreateAsync(authorVar.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors
                .FirstOrDefaultAsync(m => m.AuthorID == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthorID,Surname,Name,MiddleName")] Author author)
        {
            if (ModelState.IsValid)
            {
                _context.Add(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AuthorID,Surname,Name,MiddleName")] Author author)
        {
            if (id != author.AuthorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(author);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.AuthorID))
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
            return View(author);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors
                .FirstOrDefaultAsync(m => m.AuthorID == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.AuthorID == id);
        }
    }
}
