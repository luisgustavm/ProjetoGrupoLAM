using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoLAM.Data;
using ProjetoLAM.Models;

namespace ProjetoLAM.Controllers
{
    public class LivrosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LivrosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Livros
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Livros.Include(l => l.Genero);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Livros/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livros = await _context.Livros
                .Include(l => l.Genero)
                .FirstOrDefaultAsync(m => m.LivroId == id);
            if (livros == null)
            {
                return NotFound();
            }

            return View(livros);
        }

        // GET: Livros/Create
        public IActionResult Create()
        {
            ViewData["GeneroId"] = new SelectList(_context.Genero, "GeneroId", "Nome");
            return View();
        }

        // POST: Livros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LivroId,Titulo,Autor,AnoPublicacao,ISBN,GeneroId")] Livros livros)
        {
            if (ModelState.IsValid)
            {
                livros.LivroId = Guid.NewGuid();
                _context.Add(livros);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GeneroId"] = new SelectList(_context.Genero, "GeneroId", "Nome", livros.GeneroId);
            return View(livros);
        }

        // GET: Livros/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livros = await _context.Livros.FindAsync(id);
            if (livros == null)
            {
                return NotFound();
            }
            ViewData["GeneroId"] = new SelectList(_context.Genero, "GeneroId", "Nome", livros.GeneroId);
            return View(livros);
        }

        // POST: Livros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("LivroId,Titulo,Autor,AnoPublicacao,ISBN,GeneroId")] Livros livros)
        {
            if (id != livros.LivroId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(livros);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivrosExists(livros.LivroId))
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
            ViewData["GeneroId"] = new SelectList(_context.Genero, "GeneroId", "Nome", livros.GeneroId);
            return View(livros);
        }

        // GET: Livros/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livros = await _context.Livros
                .Include(l => l.Genero)
                .FirstOrDefaultAsync(m => m.LivroId == id);
            if (livros == null)
            {
                return NotFound();
            }

            return View(livros);
        }

        // POST: Livros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var livros = await _context.Livros.FindAsync(id);
            if (livros != null)
            {
                _context.Livros.Remove(livros);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LivrosExists(Guid id)
        {
            return _context.Livros.Any(e => e.LivroId == id);
        }
    }
}
