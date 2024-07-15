using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;

namespace TestTask.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly decimal _vatRate;

        public ProductController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _vatRate = configuration.GetValue<decimal>("VATRate");
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Product.ToListAsync();

            var productViewModel = products.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Title = p.Title,
                Price = p.Price,
                Quantity = p.Quantity,
                TotalPriceWithVAT = p.TotalPriceWithVAT(_vatRate)
            });
            return View(productViewModel);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Quantity,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await LogAuditTrail(null, product, ChangeType.Create);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Quantity,Price")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldProduct = await _context.Product
                        .AsNoTracking()
                        .FirstOrDefaultAsync(p => p.Id == product.Id);
                    _context.Product.Update(product);
                    await LogAuditTrail(oldProduct, product, ChangeType.Update);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }
            await LogAuditTrail(product, null, ChangeType.Delete);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }

        private async ValueTask LogAuditTrail
            (
            Product oldProduct,
            Product newProduct,
            ChangeType changeType,
            bool toSave = false)
        {
            var audit = new ProductAudit
            {
                ProductId = oldProduct?.Id ?? newProduct.Id,
                OldTitle = oldProduct?.Title,
                OldQuantity = oldProduct?.Quantity,
                OldPrice = oldProduct?.Price,
                NewTitle = newProduct?.Title,
                NewQuantity = newProduct?.Quantity,
                NewPrice = newProduct?.Price,
                ChangedBy = "",
                ChangeDate = DateTime.UtcNow,
                ChangeType = changeType
            };

            _context.ProductAudit.Add(audit);
            if (toSave)
            {
                await _context.SaveChangesAsync();
            }
        }
    }
}
