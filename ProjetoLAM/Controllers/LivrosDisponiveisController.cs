using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoLAM.Data;
using ProjetoLAM.Models;

namespace ProjetoLAM.Controllers
{
    public class LivrosDisponiveisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LivrosDisponiveisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LivrosDisponiveis
        // Busca opcional via query string: ?searchString=...
        public async Task<IActionResult> Index(string searchString)
        {
            var query = _context.LivrosDisponiveis
                                .Include(l => l.Livros)
                                .Include(l => l.Genero)
                                .AsNoTracking()
                                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                var s = searchString.Trim();

                // Busca case-insensitive usando EF.Functions.Like (mais portátil que ToLower em todas as collations)
                var pattern = $"%{s}%";
                query = query.Where(l =>
                    (l.Livros != null && EF.Functions.Like(l.Livros.Titulo, pattern)) ||
                    (l.Livros != null && EF.Functions.Like(l.Livros.Autor, pattern)) ||
                    (l.Genero != null && EF.Functions.Like(l.Genero.Nome, pattern))
                );
            }

            var lista = await query.OrderBy(l => l.Livros.Autor).ToListAsync();
            return View(lista);
        }

        // GET: LivrosDisponiveis/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var livrosDisponiveis = await _context.LivrosDisponiveis
                .Include(l => l.Genero)
                .Include(l => l.Livros)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.LivrosDisponiveisId == id);

            if (livrosDisponiveis == null) return NotFound();

            return View(livrosDisponiveis);
        }

        // GET: LivrosDisponiveis/Create
        public IActionResult Create()
        {
            ViewData["GeneroId"] = new SelectList(_context.Genero.AsNoTracking(), "GeneroId", "Nome");
            ViewData["LivroId"] = new SelectList(_context.Livros.AsNoTracking(), "LivroId", "Titulo");
            return View();
        }

        // POST: LivrosDisponiveis/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LivrosDisponiveisId,Estoque,LivroId,GeneroId,Status")] LivrosDisponiveis livrosDisponiveis)
        {
            if (ModelState.IsValid)
            {
                livrosDisponiveis.LivrosDisponiveisId = Guid.NewGuid();
                _context.Add(livrosDisponiveis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GeneroId"] = new SelectList(_context.Genero.AsNoTracking(), "GeneroId", "Nome", livrosDisponiveis.GeneroId);
            ViewData["LivroId"] = new SelectList(_context.Livros.AsNoTracking(), "LivroId", "Titulo", livrosDisponiveis.LivroId);
            return View(livrosDisponiveis);
        }

        // GET: LivrosDisponiveis/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var livrosDisponiveis = await _context.LivrosDisponiveis.FindAsync(id);
            if (livrosDisponiveis == null) return NotFound();

            ViewData["GeneroId"] = new SelectList(_context.Genero.AsNoTracking(), "GeneroId", "Nome", livrosDisponiveis.GeneroId);
            ViewData["LivroId"] = new SelectList(_context.Livros.AsNoTracking(), "LivroId", "Titulo", livrosDisponiveis.LivroId);
            return View(livrosDisponiveis);
        }

        // POST: LivrosDisponiveis/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("LivrosDisponiveisId,Estoque,LivroId,GeneroId,Status")] LivrosDisponiveis livrosDisponiveis)
        {
            if (id != livrosDisponiveis.LivrosDisponiveisId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(livrosDisponiveis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivrosDisponiveisExists(livrosDisponiveis.LivrosDisponiveisId))
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
            ViewData["GeneroId"] = new SelectList(_context.Genero.AsNoTracking(), "GeneroId", "Nome", livrosDisponiveis.GeneroId);
            ViewData["LivroId"] = new SelectList(_context.Livros.AsNoTracking(), "LivroId", "Titulo", livrosDisponiveis.LivroId);
            return View(livrosDisponiveis);
        }

        // GET: LivrosDisponiveis/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var livrosDisponiveis = await _context.LivrosDisponiveis
                .Include(l => l.Genero)
                .Include(l => l.Livros)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.LivrosDisponiveisId == id);

            if (livrosDisponiveis == null) return NotFound();

            return View(livrosDisponiveis);
        }

        // POST: LivrosDisponiveis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var livrosDisponiveis = await _context.LivrosDisponiveis.FindAsync(id);
            if (livrosDisponiveis != null)
            {
                _context.LivrosDisponiveis.Remove(livrosDisponiveis);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool LivrosDisponiveisExists(Guid id)
        {
            return _context.LivrosDisponiveis.Any(e => e.LivrosDisponiveisId == id);
        }
    }
}
