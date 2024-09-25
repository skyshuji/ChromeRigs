using AutoMapper;
using ChromeRigs.Entities.Components;
using ChromeRigs.Entities.PCs;
using ChromeRigs.MVC.Data;
using ChromeRigs.MVC.Models.PCs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var pcs = await _context.PCs.ToListAsync();
            var pcVMs = _mapper.Map<List<PCViewModel>>(pcs);
            return View(pcVMs);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pc = await _context
                                .PCs
                                .Include(pc => pc.Components)
                                .Where(pc => pc.Id == id)
                                .SingleOrDefaultAsync();

            if (pc == null)
            {
                return NotFound();
            }

            var pcVM = _mapper.Map<PCDetailsViewModel>(pc);

            return View(pcVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var createUpdatePCVM = new CreateUpdatePCViewModel();
            createUpdatePCVM.ComponentLookup = new MultiSelectList(_context.Components, "Id", "Name");

            return View(createUpdatePCVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUpdatePCViewModel createUpdatePCViewModel)
        {
            if (ModelState.IsValid)
            {
                var pc = _mapper.Map<PC>(createUpdatePCViewModel);

                // Update PC Components
                await UpdatePCComponents(pc, createUpdatePCViewModel.ComponentsIds);

                // Update PC Price
                pc.Price = await GetPCPrice(pc.Components);

                _context.Add(pc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(createUpdatePCViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pc = await _context.PCs.FindAsync(id);
            if (pc == null)
            {
                return NotFound();
            }
            return View(pc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price")] PC pc)
        {
            if (id != pc.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PCExists(pc.Id))
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
            return View(pc);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pc = await _context.PCs.FindAsync(id);
            if (pc != null)
            {
                _context.PCs.Remove(pc);
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

        private async Task UpdatePCComponents(PC pc, List<int> componentsIds)
        {
            pc.Components.Clear();

            var components = await _context
                .Components
                .Where(c => componentsIds.Contains(c.Id))
                .ToListAsync();

            pc.Components.AddRange(components);
        }

        private async Task<decimal> GetPCPrice(List<Component> components)
        {
            var pcPrice = components.Sum(component => component.Price);
            var pcPriceWithProfit = pcPrice * 1.4m;

            return pcPriceWithProfit;
        }

        #endregion
    }
}
