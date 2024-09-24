using AutoMapper;
using ChromeRigs.Entities.PCs;
using ChromeRigs.MVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChromeRigs.MVC.Controllers
{
    public class PCsController : Controller
    {
        #region Data & Const

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PCsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #endregion

        #region Actions

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.PCs.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pC = await _context.PCs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pC == null)
            {
                return NotFound();
            }

            return View(pC);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price")] PC pC)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pC);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pC);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pC = await _context.PCs.FindAsync(id);
            if (pC == null)
            {
                return NotFound();
            }
            return View(pC);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price")] PC pC)
        {
            if (id != pC.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pC);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PCExists(pC.Id))
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
            return View(pC);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pC = await _context.PCs.FindAsync(id);
            if (pC != null)
            {
                _context.PCs.Remove(pC);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Private Methods

        private bool PCExists(int id)
        {
            return _context.PCs.Any(e => e.Id == id);
        }
        #endregion
    }
}
