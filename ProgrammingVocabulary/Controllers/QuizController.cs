using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProgrammingVocabulary.Data;
using ProgrammingVocabulary.Models;

namespace ProgrammingVocabulary.Controllers
{
    public class QuizController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuizController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Quiz
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Vocabulary.Include(v => v.Language).Include(v => v.User);
            return View(await applicationDbContext.ToListAsync());

        }

        // GET: Quiz/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vocabulary = await _context.Vocabulary
                .Include(v => v.Language)
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.VocabularyId == id);
            if (vocabulary == null)
            {
                return NotFound();
            }

            return View(vocabulary);
        }

        // GET: Quiz/Create
        public IActionResult Create()
        {
            ViewData["LanguageId"] = new SelectList(_context.Language, "LanguageId", "Name");
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: Quiz/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VocabularyId,Word,Definition,Link,UserId,LanguageId")] Vocabulary vocabulary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vocabulary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LanguageId"] = new SelectList(_context.Language, "LanguageId", "Name", vocabulary.LanguageId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", vocabulary.UserId);
            return View(vocabulary);
        }

        // GET: Quiz/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vocabulary = await _context.Vocabulary.FindAsync(id);
            if (vocabulary == null)
            {
                return NotFound();
            }
            ViewData["LanguageId"] = new SelectList(_context.Language, "LanguageId", "Name", vocabulary.LanguageId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", vocabulary.UserId);
            return View(vocabulary);
        }

        // POST: Quiz/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VocabularyId,Word,Definition,Link,UserId,LanguageId")] Vocabulary vocabulary)
        {
            if (id != vocabulary.VocabularyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vocabulary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VocabularyExists(vocabulary.VocabularyId))
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
            ViewData["LanguageId"] = new SelectList(_context.Language, "LanguageId", "Name", vocabulary.LanguageId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", vocabulary.UserId);
            return View(vocabulary);
        }

        // GET: Quiz/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vocabulary = await _context.Vocabulary
                .Include(v => v.Language)
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.VocabularyId == id);
            if (vocabulary == null)
            {
                return NotFound();
            }

            return View(vocabulary);
        }

        // POST: Quiz/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vocabulary = await _context.Vocabulary.Include(v => v.User).Include(u => u.UserVocabulary).SingleOrDefaultAsync(Vocabulary => Vocabulary.VocabularyId == id);
            _context.Vocabulary.Remove(vocabulary);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VocabularyExists(int id)
        {
            return _context.Vocabulary.Any(e => e.VocabularyId == id);
        }
    }
}
