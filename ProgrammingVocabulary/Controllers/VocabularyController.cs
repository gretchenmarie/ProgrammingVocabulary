using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProgrammingVocabulary.Data;
using ProgrammingVocabulary.Models;

namespace ProgrammingVocabulary.Controllers
{
    public class VocabularyController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

       

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public VocabularyController(ApplicationDbContext ctx,
                          UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = ctx;
        }


        public async Task<IActionResult> FavoriteVocabulary([FromRoute]int id)
        {
            ApplicationUser user = await GetCurrentUserAsync();

            UserVocabulary newUV = new UserVocabulary()
            {

                UserId = user.Id,
                VocabularyId = id
            };
            _context.Add(newUV);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }


        // GET: Vocabulary
        public async Task<IActionResult> Index( string sortOrder)
        {         
            var applicationDbContext = _context.Vocabulary.Include(v => v.Language).OrderBy(v => v.Word);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> GetJavaScript()
        {
            var applicationDbContext = _context.Vocabulary.Where(l => l.LanguageId == 1);
            return View(await applicationDbContext.ToListAsync());
            
        }
        public async Task<IActionResult> GetCSharp()
        {
            var applicationDbContext = _context.Vocabulary.Where(l => l.LanguageId == 2);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> GetGeneralVocabulary()
        {
            var applicationDbContext = _context.Vocabulary.Where(l => l.LanguageId == 3);
            return View(await applicationDbContext.ToListAsync());
        }
        //public async Task<IActionResult> Favoritelist()
        //{
        //    var applicationDbContext = _context.Vocabulary.Include(v => v.UserVocabulary).OrderBy(v => v.Word); 
        //    return View(await applicationDbContext.ToListAsync());
        //}



        // GET: Vocabularies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vocabulary = await _context.Vocabulary
                .Include(v => v.Language)
                .FirstOrDefaultAsync(m => m.VocabularyId == id);
            if (vocabulary == null)
            {
                return NotFound();
            }

            return View(vocabulary);
        }

        // GET: Vocabularies/Create
        public IActionResult Create()
        {
            ViewData["LanguageId"] = new SelectList(_context.Language, "LanguageId", "Name");
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: Vocabularies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VocabularyId,Word,Definition,Link,UserId,LanguageId")] Vocabulary vocabulary)
        {


            var user = await GetCurrentUserAsync();

           ModelState.Remove("User");
           ModelState.Remove("UserId");

            if (ModelState.IsValid)
            {

                
                vocabulary.User = user;
                
                _context.Add(vocabulary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = vocabulary.VocabularyId.ToString() });
            }
            ViewData["LanguageId"] = new SelectList(_context.Language, "LanguageId", "Name", vocabulary.LanguageId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", vocabulary.UserId);
            return View(vocabulary);
        }

        // GET: Vocabularies/Edit/5
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
            return View(vocabulary);
        }

        // POST: Vocabularies/Edit/5
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
            var user = await GetCurrentUserAsync();
            ModelState.Remove("User");
            ModelState.Remove("UserId");


            if (ModelState.IsValid)
            {
                try
                {
                    vocabulary.User = user;

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
            return View(vocabulary);
        }

        // GET: Vocabularies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vocabulary = await _context.Vocabulary
                .Include(v => v.Language)
                .FirstOrDefaultAsync(m => m.VocabularyId == id);
            if (vocabulary == null)
            {
                return NotFound();
            }

            return View(vocabulary);
        }

        // POST: Vocabularies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed( int id)
        {
            var vocabulary = await _context.Vocabulary.FindAsync(id);
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
