
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todolist_App.Models;

public class TaskItemsController : Controller
{
    private readonly AppDbContext _context;

    public TaskItemsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: TASKITEMS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Tasks.ToListAsync());
    }

    // GET: TASKITEMS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var taskitem = await _context.Tasks
            .FirstOrDefaultAsync(m => m.Id == id);
        if (taskitem == null)
        {
            return NotFound();
        }

        return View(taskitem);
    }

    // GET: TASKITEMS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: TASKITEMS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,IsDone")] TaskItem taskitem)
    {
        if (ModelState.IsValid)
        {
            _context.Add(taskitem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(taskitem);
    }

    // GET: TASKITEMS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var taskitem = await _context.Tasks.FindAsync(id);
        if (taskitem == null)
        {
            return NotFound();
        }
        return View(taskitem);
    }

    // POST: TASKITEMS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Title,IsDone")] TaskItem taskitem)
    {
        if (id != taskitem.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(taskitem);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskItemExists(taskitem.Id))
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
        return View(taskitem);
    }

    // GET: TASKITEMS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var taskitem = await _context.Tasks
            .FirstOrDefaultAsync(m => m.Id == id);
        if (taskitem == null)
        {
            return NotFound();
        }

        return View(taskitem);
    }

    // POST: TASKITEMS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var taskitem = await _context.Tasks.FindAsync(id);
        if (taskitem != null)
        {
            _context.Tasks.Remove(taskitem);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TaskItemExists(int? id)
    {
        return _context.Tasks.Any(e => e.Id == id);
    }
}
