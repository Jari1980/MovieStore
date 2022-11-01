using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movie_Store.Data;
using Movie_Store.Models;

namespace Movie_Store.Controllers
{
    public class OrderRowsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderRowsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OrderRows
        public async Task<IActionResult> Index()
        {
              return View(await _context.OrderRows.ToListAsync());
        }

        // GET: OrderRows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrderRows == null)
            {
                return NotFound();
            }

            var orderRows = await _context.OrderRows
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderRows == null)
            {
                return NotFound();
            }

            return View(orderRows);
        }

        // GET: OrderRows/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrderRows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrderId,MovieId,Price")] OrderRows orderRows)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderRows);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderRows);
        }

        // GET: OrderRows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrderRows == null)
            {
                return NotFound();
            }

            var orderRows = await _context.OrderRows.FindAsync(id);
            if (orderRows == null)
            {
                return NotFound();
            }
            return View(orderRows);
        }

        // POST: OrderRows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderId,MovieId,Price")] OrderRows orderRows)
        {
            if (id != orderRows.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderRows);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderRowsExists(orderRows.Id))
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
            return View(orderRows);
        }

        // GET: OrderRows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrderRows == null)
            {
                return NotFound();
            }

            var orderRows = await _context.OrderRows
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderRows == null)
            {
                return NotFound();
            }

            return View(orderRows);
        }

        // POST: OrderRows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrderRows == null)
            {
                return Problem("Entity set 'ApplicationDbContext.OrderRows'  is null.");
            }
            var orderRows = await _context.OrderRows.FindAsync(id);
            if (orderRows != null)
            {
                _context.OrderRows.Remove(orderRows);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderRowsExists(int id)
        {
          return _context.OrderRows.Any(e => e.Id == id);
        }
    }
}
