using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MuniChorrillos2.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MuniChorrillos2.Controllers
{
    public class ControlVehicularController : Controller
    {
        private readonly Bdmultas2Context _context;

        public ControlVehicularController(Bdmultas2Context context)
        {
            _context = context;
        }

        // GET: ControlVehicular
        public async Task<IActionResult> Index()
        {
            var controlVehicularList = await _context.ControlVehiculars.ToListAsync();
            return View(controlVehicularList);
        }

        // GET: ControlVehicular/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ControlVehiculars == null)
            {
                return NotFound();
            }

            var controlVehicular = await _context.ControlVehiculars
                .FirstOrDefaultAsync(m => m.IdControl == id);
            if (controlVehicular == null)
            {
                return NotFound();
            }

            return View(controlVehicular);
        }

        // GET: ControlVehicular/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ControlVehicular/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdControl,Placa,Hingreso,Hsalida,Dia,Cochera,Estado")] ControlVehicular controlVehicular)
        {
            if (ModelState.IsValid)
            {
                _context.Add(controlVehicular);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(controlVehicular);
        }

        // POST: ControlVehicular/CreateOrUpdate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrUpdate([Bind("IdControl,Placa,Hingreso,Hsalida,Dia,Cochera,Estado")] ControlVehicular controlVehicular)
        {
            if (ModelState.IsValid)
            {
                if (controlVehicular.IdControl == 0)
                {
                    _context.Add(controlVehicular);
                }
                else
                {
                    _context.Update(controlVehicular);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(controlVehicular.IdControl == 0 ? "Create" : "Edit", controlVehicular);
        }

        // GET: ControlVehicular/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ControlVehiculars == null)
            {
                return NotFound();
            }

            var controlVehicular = await _context.ControlVehiculars.FindAsync(id);
            if (controlVehicular == null)
            {
                return NotFound();
            }
            return View(controlVehicular);
        }

        // POST: ControlVehicular/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdControl,Placa,Hingreso,Hsalida,Dia,Cochera,Estado")] ControlVehicular controlVehicular)
        {
            if (id != controlVehicular.IdControl)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(controlVehicular);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ControlVehicularExists(controlVehicular.IdControl))
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
            return View(controlVehicular);
        }

        // GET: ControlVehicular/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ControlVehiculars == null)
            {
                return NotFound();
            }

            var controlVehicular = await _context.ControlVehiculars
                .FirstOrDefaultAsync(m => m.IdControl == id);
            if (controlVehicular == null)
            {
                return NotFound();
            }

            return View(controlVehicular);
        }

        // POST: ControlVehicular/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var controlVehicular = await _context.ControlVehiculars.FindAsync(id);
            if (controlVehicular != null)
            {
                _context.ControlVehiculars.Remove(controlVehicular);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        private bool ControlVehicularExists(int id)
        {
            return (_context.ControlVehiculars?.Any(e => e.IdControl == id)).GetValueOrDefault();
        }


        //bloquear cocheras
        [HttpGet]
        public async Task<IActionResult> GetOccupiedCocheras()
        {
            var occupiedCocheras = await _context.ControlVehiculars
                                                  .Where(cv => cv.Estado == "Ocupado")
                                                  .Select(cv => cv.Cochera)
                                                  .ToListAsync();
            return Json(occupiedCocheras);
        }


        [HttpPost, ActionName("ActualizarHsalida")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualizarHsalida(int IdControl, TimeSpan Hsalida)
        {
            var controlVehicular = await _context.ControlVehiculars.FindAsync(IdControl);
            if (controlVehicular == null)
            {
                return NotFound();
            }

            controlVehicular.Hsalida = Hsalida;

            try
            {
                _context.Update(controlVehicular);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ControlVehicularExists(IdControl))
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


    }
}
