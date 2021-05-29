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
    public class PublishersController : Controller
    {
        private readonly SchoolContext _context;

        public PublishersController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Publishers
        public async Task<IActionResult> Index(
               string sortOrder,
               string currentFilter,
               string searchString,
               int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CitySortParam"] = sortOrder == "City" ? "City_desc" : "City";
            ViewData["NameSortParam"] = sortOrder == "Name" ? "Name_desc" : "Name";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var publisherVar = from s in _context.Publishers select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                publisherVar = publisherVar.Where(s =>
                                        s.PublishingCity.Contains(searchString) ||
                                        s.PublisherName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "City_desc":
                    publisherVar = publisherVar.OrderByDescending(s => s.PublishingCity);
                    break;
                case "City":
                    publisherVar = publisherVar.OrderBy(s => s.PublishingCity);
                    break;
                case "Name_desc":
                    publisherVar = publisherVar.OrderByDescending(s => s.PublisherName);
                    break;
                case "Name":
                    publisherVar = publisherVar.OrderBy(s => s.PublisherName);
                    break;
                default:
                    publisherVar = publisherVar.OrderBy(s => s.PublishingCity);
                    break;
            }
            int pageSize = 3;
            return View(await PaginatedList<Publisher>.CreateAsync(publisherVar.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Publishers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = await _context.Publishers
                .FirstOrDefaultAsync(m => m.PublisherID == id);
            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        // GET: Publishers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Publishers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PublisherID,PublishingCity,PublisherName")] Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(publisher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(publisher);
        }

        // GET: Publishers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }
            return View(publisher);
        }

        // POST: Publishers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PublisherID,PublishingCity,PublisherName")] Publisher publisher)
        {
            if (id != publisher.PublisherID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(publisher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublisherExists(publisher.PublisherID))
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
            return View(publisher);
        }

        // GET: Publishers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = await _context.Publishers
                .FirstOrDefaultAsync(m => m.PublisherID == id);
            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        // POST: Publishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublisherExists(int id)
        {
            return _context.Publishers.Any(e => e.PublisherID == id);
        }
    }
}
