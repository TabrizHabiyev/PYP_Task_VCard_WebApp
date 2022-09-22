using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PYP_Task_VCard_WebApp.Context;
using PYP_Task_VCard_WebApp.Interfaces.Services;
using PYP_Task_VCard_WebApp.Models;

namespace PYP_Task_VCard_WebApp.Controllers
{
    public class VCardsController : Controller
    {
        private readonly VCardContext _context;
        private readonly IVCardQRCodeIHandler _vCardQRCodeIHandler;
        public VCardsController(VCardContext context, IVCardQRCodeIHandler vCardQRCodeIHandler)
        {
            _context = context;
            _vCardQRCodeIHandler = vCardQRCodeIHandler;
        }

        // GET: VCards
        public async Task<IActionResult> Index()
        {
              return View(await _context.VCards.ToListAsync());
        }

        // GET: VCards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VCards == null)
            {
                return NotFound();
            }

            var vCard = await _context.VCards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vCard == null)
            {
                return NotFound();
            }

            return View(vCard);
        }

        // GET: VCards/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Firtname,Surname,Email,Phone,Country,City")] VCard vCard)
        {
            if (ModelState.IsValid)
            { 
                vCard.Image = await _vCardQRCodeIHandler.CreateQrCode(vCard);
                _context.Add(vCard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vCard);
        }

        // GET: VCards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VCards == null)
            {
                return NotFound();
            }

            var vCard = await _context.VCards.FindAsync(id);
            if (vCard == null)
            {
                return NotFound();
            }
            return View(vCard);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Firtname,Surname,Email,Phone,Country,City")] VCard vCard)
        {
            if (id != vCard.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    vCard.Image =  await _vCardQRCodeIHandler.CreateQrCode(vCard);
                    _context.Update(vCard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VCardExists(vCard.Id))
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
            return View(vCard);
        }

        // GET: VCards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VCards == null)
            {
                return NotFound();
            }

            var vCard = await _context.VCards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vCard == null)
            {
                return NotFound();
            }

            return View(vCard);
        }

        // POST: VCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VCards == null)
            {
                return Problem("Entity set 'VCardContext.VCards'  is null.");
            }
            var vCard = await _context.VCards.FindAsync(id);
            if (vCard != null)
            {
                _context.VCards.Remove(vCard);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VCardExists(int id)
        {
          return _context.VCards.Any(e => e.Id == id);
        }
    }
}
