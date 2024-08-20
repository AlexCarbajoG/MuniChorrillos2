using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MuniChorrillos2.Models;


namespace MuniChorrillos2.Controllers
{
    public class MultasController : Controller
    {
        private readonly Bdmultas2Context _context;

        public MultasController(Bdmultas2Context context)
        {
            _context = context;
        }

        // GET: MultasController
        public async Task<IActionResult> Index(string searchTerm)
        {
            var multas = _context.Multa
                .Include(m => m.IdDepositoNavigation)
                .Include(m => m.IdInfraccionNavigation)
                .Include(m => m.IdPersonalNavigation)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                multas = multas.Where(m => m.Placa.Contains(searchTerm) ||
                                           m.Propietario.Contains(searchTerm) ||
                                           m.NroSerie.Contains(searchTerm));
            }

            var result = await multas.ToListAsync();

            ViewData["IdDeposito"] = new SelectList(_context.Depositos, "IdDeposito", "NomDeposito");
            ViewData["IdInfraccion"] = new SelectList(_context.Infraccions, "IdInfraccion", "Descripcion");
            ViewData["IdPersonal"] = new SelectList(_context.Personals, "IdPersonal", "UsuarioAcceso");

            ViewData["SearchTerm"] = searchTerm ?? string.Empty; // Pasar el valor de búsqueda a la vista, asegurando que no sea null

            return View(result);
        }





        // POST: MultasController/CreateOrUpdate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrUpdate([Bind("IdMulta,FecMulta,HoraMulta,LugarMulta,DistritoMulta,NroSerie,Placa,Marca,Modelo,Nmotor,Año,Color,Estado,Propietario,Email,Direcion,Grua,IdDeposito,IdInfraccion,IdPersonal,EstPago,CodPago,Telefono,Observaciones,Imagen,ImagenBase64")] Multum multa)
        {
            if (ModelState.IsValid)
            {
                if (multa.IdMulta == 0)
                {
                    _context.Add(multa);
                    
                }
                else
                {
                    _context.Update(multa);
                    
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDeposito"] = new SelectList(_context.Depositos, "IdDeposito", "NomDeposito", multa.IdDeposito);
            ViewData["IdInfraccion"] = new SelectList(_context.Infraccions, "IdInfraccion", "Descripcion", multa.IdInfraccion);
            ViewData["IdPersonal"] = new SelectList(_context.Personals, "IdPersonal", "UsuarioAcceso", multa.IdPersonal);
            return View("Index", multa);
        }

        // GET: MultasController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var multa = await _context.Multa
                .Include(m => m.IdDepositoNavigation)
                .Include(m => m.IdInfraccionNavigation)
                .Include(m => m.IdPersonalNavigation)
                .FirstOrDefaultAsync(m => m.IdMulta == id);

            if (multa == null)
            {
                return NotFound();
            }

            return View(multa);
        }

        // POST: MultasController/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var multa = await _context.Multa.FindAsync(id);
            _context.Multa.Remove(multa);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Multa eliminada con éxito";
            return RedirectToAction(nameof(Index));
        }

        private bool MultaExists(int id)
        {
            return _context.Multa.Any(e => e.IdMulta == id);
        }

        // Redirección a otra área (por ejemplo)
        public IActionResult RedirectToExternal()
        {
            return RedirectToAction("Index", "Areas");
        }
    }
}
