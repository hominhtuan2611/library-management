﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.API.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using LibraryManagement.Application.Common;

namespace LibraryManagement.Admin.Controllers
{
    public class PhieuMuonController : Controller
    {
        private readonly LibraryDBContext _context;

        public IConfiguration _configuration;

        private HttpClient _apiService;
        private readonly string apiAddress;

        public PhieuMuonController(LibraryDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

            apiAddress = _configuration.GetSection("ApiAddress").GetSection("Url").Value;
            _apiService = ApiService.GetAPI(apiAddress);
        }

        // GET: PhieuMuon
        public async Task<IActionResult> Index()
        {
            var libraryDBContext = _context.PhieuMuon.Include(p => p.MaDgNavigation).Include(p => p.MaNvNavigation);
            return View(await libraryDBContext.ToListAsync());
        }

        // GET: PhieuMuon/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phieuMuon = await _context.PhieuMuon
                .Include(p => p.MaDgNavigation)
                .Include(p => p.MaNvNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phieuMuon == null)
            {
                return NotFound();
            }

            return View(phieuMuon);
        }

        // GET: PhieuMuon/Create
        public IActionResult Create()
        {
            ViewData["MaDg"] = new SelectList(_context.DocGia, "Id", "Cmnd");
            ViewData["MaNv"] = new SelectList(_context.NhanVien, "Id", "Cmnd");
            return View();
        }

        // POST: PhieuMuon/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MaDg,MaNv,NgayMuon,TongSachMuon,HanTra,DaTra,TrangThai")] PhieuMuon phieuMuon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phieuMuon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaDg"] = new SelectList(_context.DocGia, "Id", "Cmnd", phieuMuon.MaDg);
            ViewData["MaNv"] = new SelectList(_context.NhanVien, "Id", "Cmnd", phieuMuon.MaNv);
            return View(phieuMuon);
        }

        // GET: PhieuMuon/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phieuMuon = await _context.PhieuMuon.FindAsync(id);
            if (phieuMuon == null)
            {
                return NotFound();
            }
            ViewData["MaDg"] = new SelectList(_context.DocGia, "Id", "Cmnd", phieuMuon.MaDg);
            ViewData["MaNv"] = new SelectList(_context.NhanVien, "Id", "Cmnd", phieuMuon.MaNv);
            return View(phieuMuon);
        }

        // POST: PhieuMuon/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MaDg,MaNv,NgayMuon,TongSachMuon,HanTra,DaTra,TrangThai")] PhieuMuon phieuMuon)
        {
            if (id != phieuMuon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phieuMuon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhieuMuonExists(phieuMuon.Id))
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
            ViewData["MaDg"] = new SelectList(_context.DocGia, "Id", "Cmnd", phieuMuon.MaDg);
            ViewData["MaNv"] = new SelectList(_context.NhanVien, "Id", "Cmnd", phieuMuon.MaNv);
            return View(phieuMuon);
        }

        // GET: PhieuMuon/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phieuMuon = await _context.PhieuMuon
                .Include(p => p.MaDgNavigation)
                .Include(p => p.MaNvNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phieuMuon == null)
            {
                return NotFound();
            }

            return View(phieuMuon);
        }

        // POST: PhieuMuon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phieuMuon = await _context.PhieuMuon.FindAsync(id);
            _context.PhieuMuon.Remove(phieuMuon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhieuMuonExists(int id)
        {
            return _context.PhieuMuon.Any(e => e.Id == id);
        }
    }
}
