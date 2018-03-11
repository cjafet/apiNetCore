using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class CampaignDtoesController : Controller
    {
        private readonly CampaignDb _context;

        public CampaignDtoesController(CampaignDb context)
        {
            _context = context;
        }

        // GET: CampaignDtoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Campaign.ToListAsync());
        }

        // GET: CampaignDtoes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaignDto = await _context.Campaign
                .SingleOrDefaultAsync(m => m.Id == id);
            if (campaignDto == null)
            {
                return NotFound();
            }

            return View(campaignDto);
        }

        // GET: CampaignDtoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CampaignDtoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] CampaignDto campaignDto)
        {
            if (ModelState.IsValid)
            {
                campaignDto.Id = Guid.NewGuid();
                _context.Add(campaignDto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(campaignDto);
        }

        // GET: CampaignDtoes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaignDto = await _context.Campaign.SingleOrDefaultAsync(m => m.Id == id);
            if (campaignDto == null)
            {
                return NotFound();
            }
            return View(campaignDto);
        }

        // POST: CampaignDtoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name")] CampaignDto campaignDto)
        {
            if (id != campaignDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(campaignDto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CampaignDtoExists(campaignDto.Id))
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
            return View(campaignDto);
        }

        // GET: CampaignDtoes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaignDto = await _context.Campaign
                .SingleOrDefaultAsync(m => m.Id == id);
            if (campaignDto == null)
            {
                return NotFound();
            }

            return View(campaignDto);
        }

        // POST: CampaignDtoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var campaignDto = await _context.Campaign.SingleOrDefaultAsync(m => m.Id == id);
            _context.Campaign.Remove(campaignDto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CampaignDtoExists(Guid id)
        {
            return _context.Campaign.Any(e => e.Id == id);
        }
    }
}
