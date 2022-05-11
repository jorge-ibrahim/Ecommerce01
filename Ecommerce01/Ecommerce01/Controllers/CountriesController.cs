#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ecommerce01.Data;
using Ecommerce01.Data.Entities;
using Ecommerce01.Models;
using Microsoft.AspNetCore.Authorization;

namespace Ecommerce01.Controllers
{
    [Authorize(Roles = "Admin")]//datanotation que permite asignar credencial de que solo un administrador puede trabajar con categoria,tambien
                                //la puedo asignar solo al algunos metodos como el eliminar. 
    public class CountriesController : Controller
    {
        private readonly DataContext _context;

        public CountriesController(DataContext context)
        {
            _context = context;
        }

        // GET: Countries
        public async Task<IActionResult> Index()
        {
            //siempre que un metodo sea asincrono lleva un awaint que basicamente lo que dice es has toda la consulta y recien 
            //guarda el parametro
            return View(await _context.Countries.Include(x => x.States).ToListAsync());
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries.Include(x => x.States).ThenInclude(s => s.Cities)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }



        // GET: Countries/Create
        [HttpGet]
        public IActionResult Create()
        {
            Country country = new()
            {
                States = new List<State>()
            };
            return View(country);
        }

        // POST: Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Country country)
        {
            if (ModelState.IsValid)//verifico que el modelo de la entidad sea valido, que respete las datanotations
            {
                try
                {
                    _context.Add(country);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch(DbUpdateException dbException)
                {
                    if (dbException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un pais con el mismo Nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbException.InnerException.Message);
                    }
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                
            }
            return View(country);
        }



        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();//NotFound(); en la pagina de error 404
            }

            var country = await _context.Countries.Include(x => x.States).FirstOrDefaultAsync(D => D.Id == id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Country country)
        {
            if (id != country.Id)
            {
                return NotFound();//NotFound(); en la pagina de error 404
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(country);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbException)
                {
                    if (dbException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un pais con el mismo Nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbException.InnerException.Message);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(country);
        }




        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries.Include(p => p.States)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }




        //GET
        public async Task<IActionResult> AddState(int? id)
        {
            //if(id == null)
            //{
            //    return NotFound();
            //}

            //Country country = await _context.Countries.FindAsync(id);
            //if(country == null)
            //{
            //    return NotFound();//pagina 404
            //}

            //StateViewModel model = new()
            //{
            //    CountryId = country.Id,
            //};
            //return View();
            if (id == null)
            {
                return NotFound();
            }

            Country country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            StateViewModel model = new()
            {
                CountryId = country.Id,
            };

            return View(model);
        }

        // POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddState(StateViewModel model)//aca devuelvo un modelo con solo el id del pais
        {
            //if (ModelState.IsValid)//verifico que el modelo de la entidad sea valido, que respete las datanotations
            //{
            //    try
            //    {
            //        State state = new()//creo un objeto del tipo state
            //        {
            //            Cities = new List<City>(),//como es un nuevo state tendra una lista
            //            Country = await _context.Countries.FindAsync(model.CountryId),//valla y trael el pais por el id
            //            Name = model.Name,//el nombre sera el que el usuario agrega al formulario
            //        };
            //        _context.Add(state);
            //        await _context.SaveChangesAsync();
            //        return RedirectToAction(nameof(Details),new {Id = model.CountryId});//al volve al detail tener en
            //        //cuenta que hay que pasar un parametro que esta en el model
            //    }
            //    catch (DbUpdateException dbException)
            //    {
            //        if (dbException.InnerException.Message.Contains("duplicate"))
            //        {
            //            ModelState.AddModelError(string.Empty, 
            //                "Ya existe una Provincia con el mismo Nombre en el Pais.");
            //        }
            //        else
            //        {
            //            ModelState.AddModelError(string.Empty, dbException.InnerException.Message);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        ModelState.AddModelError(string.Empty, ex.Message);
            //    }

            //}
            //return View(model);
            if (ModelState.IsValid)
            {
                try
                {
                    State state = new()
                    {
                        Cities = new List<City>(),
                        Country = await _context.Countries.FindAsync(model.CountryId),
                        Name = model.Name,
                    };
                    _context.Add(state);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new { Id = model.CountryId });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un departamento/estado con el mismo nombre en este país.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            return View(model);
        }





        public async Task<IActionResult> EditState(int? id)
        {
            if (id == null)
            {
                return NotFound();//NotFound(); en la pagina de error 404
            }

            State state = await _context.States.Include(s => s.Country).FirstOrDefaultAsync(x => x.Id == id);

            if (state == null)
            {
                return NotFound();
            }

            StateViewModel model = new()
            {
                CountryId = state.Country.Id,
                Id = state.Id,
                Name = state.Name,
            };

            return View(model);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditState(int id, StateViewModel entidad)
        {
            if (id != entidad.Id)
            {
                return NotFound();//NotFound(); en la pagina de error 404
            }

            if (ModelState.IsValid)
            {
                try
                {
                    State state = new()
                    {
                        Id = entidad.Id,
                        Name = entidad.Name,
                    };
                    _context.Update(state);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new {id = entidad.CountryId});
                }
                catch (DbUpdateException dbException)
                {
                    if (dbException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una Provincia con el mismo Nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbException.InnerException.Message);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(entidad);
        }



        public async Task<IActionResult> DetailsState(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            State entidad = await _context.States.Include(p => p.Country).Include(x => x.Cities)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entidad == null)
            {
                return NotFound();
            }

            return View(entidad);
        }



        //GET
        public async Task<IActionResult> AddCity(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            State entidad = await _context.States.FindAsync(id);
            if (entidad == null)
            {
                return NotFound();
            }

            CityViewModel model = new()
            {
                StateId = entidad.Id,
            };

            return View(model);
        }

        // POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCity(CityViewModel model)//aca devuelvo un modelo con solo el id del pais
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    City city = new()
                    {
                        State = await _context.States.FindAsync(model.StateId),
                        Name = model.Name,
                    };
                    _context.Add(city);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new { Id = model.StateId });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una Ciudad con el mismo nombre en esta Provincia.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            return View(model);
        }




        public async Task<IActionResult> EditCity(int? id)
        {
            if (id == null)
            {
                return NotFound();//NotFound(); en la pagina de error 404
            }

            City city = await _context.Cities.Include(s => s.State).FirstOrDefaultAsync(x => x.Id == id);

            if (city == null)
            {
                return NotFound();
            }

            CityViewModel model = new()
            {
                StateId = city.Id,
                Id = city.Id,
                Name = city.Name
            };

            return View(model);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCity(int id, CityViewModel entidad)
        {
            if (id != entidad.Id)
            {
                return NotFound();//NotFound(); en la pagina de error 404
            }

            if (ModelState.IsValid)
            {
                try
                {
                    City city = new()
                    {
                        Id = entidad.Id,
                        Name = entidad.Name,
                       // State = await _context.States.FindAsync(entidad.StateId)
                    };
                    _context.Update(city);

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(DetailsState), new { id = entidad.StateId });
                }
                catch (DbUpdateException dbException)
                {
                    if (dbException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una Ciudad con el mismo Nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbException.InnerException.Message);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(entidad);
        }



        public async Task<IActionResult> DetailsCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            City entidad = await _context.Cities.Include(p => p.State)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entidad == null)
            {
                return NotFound();
            }

            return View(entidad);
        }



        // GET: Countries/Delete/5
        public async Task<IActionResult> DeleteState(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            State state = await _context.States.Include(s => s.Country).FirstOrDefaultAsync(x => x.Id == id);
            if (state == null)
            {
                return NotFound();
            }

            return View(state);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("DeleteState")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteStateConfirmed(int id)
        {
            State state = await _context.States.Include(s => s.Country).FirstOrDefaultAsync(x => x.Id == id);
            _context.States.Remove(state);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new {Id = state.Country.Id});
        }


        // GET: Countries/Delete/5
        public async Task<IActionResult> DeleteCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            City citi = await _context.Cities.Include(x => x.State).FirstOrDefaultAsync(s => s.Id == id);
            if (citi == null)
            {
                return NotFound();
            }

            return View(citi);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("DeleteCity")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCityConfirmed(int id)
        {
            City citi = await _context.Cities.Include(x => x.State).FirstOrDefaultAsync(s => s.Id == id);
            _context.Cities.Remove(citi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(DetailsState), new { Id = citi.State.Id });
        }
    }
}
