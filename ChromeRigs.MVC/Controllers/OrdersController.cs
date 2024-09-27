using AutoMapper;
using ChromeRigs.Entities.Orders;
using ChromeRigs.Entities.PCs;
using ChromeRigs.MVC.Data;
using ChromeRigs.MVC.Models.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ChromeRigs.MVC.Controllers
{
    public class OrdersController : Controller
    {
        #region Data & Const

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrdersController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

        #endregion

        #region Actions

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var orders = await _context
                                .Orders
                                .Include(order => order.Customer)
                                .ToListAsync();

            var orderVMs = _mapper.Map<List<OrderViewModel>>(orders);

            return View(orderVMs);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var createUpdateOrderVM = new CreateOrderViewModel();

            createUpdateOrderVM.CustomerLookup = new SelectList(_context.Customers, "Id", "FullName");
            createUpdateOrderVM.PCLookUp = new MultiSelectList(_context.PCs, "Id", "Name");

            return View(createUpdateOrderVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateOrderViewModel createOrderVM)
        {
            if (ModelState.IsValid)
            {
                var order = _mapper.Map<Order>(createOrderVM);

                order.OrderTime = DateTime.Now;

                await UpdateOrderPCs(order, createOrderVM.PCIds);

                order.TotalPrice = GetOrderTotalPrice(order.PCs);

                _context.Add(order);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            createOrderVM.CustomerLookup = new SelectList(_context.Customers, "Id", "FullName");
            createOrderVM.PCLookUp = new MultiSelectList(_context.PCs, "Id", "Name");

            return View(createOrderVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context
                                    .Orders
                                    .Include(order => order.PCs)
                                    .Where(order => order.Id == id)
                                    .SingleOrDefaultAsync();

            if (order == null)
            {
                return NotFound();
            }

            var updateOrderVM = _mapper.Map<UpdateOrderViewModel>(order);

            updateOrderVM.CustomerLookup = new SelectList(_context.Customers, "Id", "FullName");
            updateOrderVM.PCLookUp = new MultiSelectList(_context.PCs, "Id", "Name");

            return View(updateOrderVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateOrderViewModel updateViewModel)
        {
            if (id != updateViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Get the order including PCs from the DB
                var order = await _context
                                        .Orders
                                        .Include(order => order.PCs)
                                        .Where(order => order.Id == id)
                                        .SingleOrDefaultAsync();

                if (order == null)
                {
                    return NotFound();
                }

                // Patch the order
                _mapper.Map(updateViewModel, order);

                // Update Order PC
                await UpdateOrderPCs(order, updateViewModel.PCIds);

                // Update Order Total Price
                order.TotalPrice = GetOrderTotalPrice(order.PCs);

                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(updateViewModel.Id))
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

            updateViewModel.CustomerLookup = new SelectList(_context.Customers, "Id", "FullName");
            updateViewModel.PCLookUp = new MultiSelectList(_context.PCs, "Id", "Name");

            return View(updateViewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Private Methods

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }

        private async Task UpdateOrderPCs(Order order, List<int> pcIds)
        {
            order.PCs.Clear();

            var pcs = await _context
                                .PCs
                                .Where(pc => pcIds.Contains(pc.Id))
                                .ToListAsync();

            order.PCs.AddRange(pcs);
        }

        private decimal GetOrderTotalPrice(List<PC> pcs)
        {
            var pcsPrice = pcs.Sum(pc => pc.Price);
            var pcsPriceWithProfit = pcsPrice * 1.25m;

            return pcsPriceWithProfit;
        }

        #endregion
    }
}