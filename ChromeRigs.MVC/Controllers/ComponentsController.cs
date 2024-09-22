using AutoMapper;
using ChromeRigs.Entities.Components;
using ChromeRigs.MVC.Data;
using ChromeRigs.MVC.Models.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChromeRigs.MVC.Controllers
{
    public class ComponentsController : Controller
    {
        #region Data & Const

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ComponentsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #endregion

        #region Actions

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var components = await _context
                                        .Components
                                        .ToListAsync();

            var componentVMs = _mapper.Map<List<Component>, List<ComponentViewModel>>(components);

            return View(componentVMs);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var component = await _context
                                        .Components
                                        .Where(component => component.Id == id)
                                        .SingleOrDefaultAsync();

            if (component == null)
            {
                return NotFound();
            }

            var componentVM = _mapper.Map<ComponentDetailsViewModel>(component);

            return View(componentVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUpdateComponentViewModel createUpdateComponentVM)
        {
            if (ModelState.IsValid)
            {
                var component = _mapper.Map<Component>(createUpdateComponentVM);

                _context.Add(component);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(createUpdateComponentVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var component = await _context
                                        .Components
                                        .FindAsync(id);

            if (component == null)
            {
                return NotFound();
            }

            var createUpdateComponentVM = _mapper.Map<CreateUpdateComponentViewModel>(component);

            return View(createUpdateComponentVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateUpdateComponentViewModel createUpdateComponentVM)
        {
            if (id != createUpdateComponentVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var component = _mapper.Map<Component>(createUpdateComponentVM);

                try
                {
                    _context.Update(component);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComponentExists(createUpdateComponentVM.Id))
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

            return View(createUpdateComponentVM);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var component = await _context
                                        .Components
                                        .FindAsync(id);

            if (component == null)
            {
                return NotFound();
            }

            _context.Components.Remove(component);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Private Methods
        private bool ComponentExists(int id)
        {
            return _context.Components.Any(e => e.Id == id);
        }
        #endregion
    }
}
