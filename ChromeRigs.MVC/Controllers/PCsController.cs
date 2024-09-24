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
            var pcs = await _context
                                .PCs
                                .ToListAsync();

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
                // TO DO
                // TRANSFORM createUpdatePCViewModel -> PC

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

        private async Task UpdatePCComponents(PC pc, List<int> componentsIds)
        {
            // TO DO
            // CLEAR PC COMPONENTS
            pc.Components.Clear();
            // GET COMPONENTS FROM DATABASE USING THE COMPONENT IDS
            var components = await _context
                                        .Components
                                        .Where(components => componentsIds.Contains(components.Id))
                                        .ToListAsync();

            // ADD COMPONENTS TO PC.COMPONENTS
            pc.Components.AddRange(components);

        }

        private async Task<decimal> GetPCPrice(List<Component> components)
        {
            return components.Sum(component => component.Price);
            var pcPriceWithProfit = pcPrice * 1.4m;

            return
        }

        #endregion
    }
}
