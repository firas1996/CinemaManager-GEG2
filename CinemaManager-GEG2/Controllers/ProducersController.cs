using CinemaManager_GEG2.Models.Cinema;
using CinemaManager_GEG2.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaManager_GEG2.Controllers
{
    public class ProducersController : Controller
    {
        CinemaDbGeg2Context ctx;
        public ProducersController(CinemaDbGeg2Context context)
        {
            ctx=context;
        }
        // GET: ProducersController
        public ActionResult Index()
        {
            return View(ctx.Producers.ToList());
        }
        public ActionResult ProdsAndTheirMovies()
        {
            var movies = ctx.Movies.ToList();
            return View(ctx.Producers.ToList());
        }

        public IActionResult ProdsAndTheirMovies_UsingModel()
        {
            var movies = ctx.Movies.ToList();
            var prods = ctx.Producers.ToList();
            var res = from m in movies
                      join p in prods on m.ProducerId equals p.Id
                      select new ProdMovie
                      {
                          mGenre = m.Genre,
                          mTitle = m.Title,
                          pName = p.Name,
                          pNat = p.Nationality
                      };
            return View(res.ToList());
        }

        public IActionResult MyMovies(int id)
        {
            return View(ctx.Movies.Where(x=>x.ProducerId == id).ToList());
        }

        // GET: ProducersController/Details/5
        public ActionResult Details(int id)
        {
            return View(ctx.Producers.Find(id));
        }

        // GET: ProducersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProducersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection,Producer p)
        {
            try
            {
                ctx.Producers.Add(p);
                ctx.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProducersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(ctx.Producers.Find(id));
        }

        // POST: ProducersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,Producer p)
        {
            try
            {
                ctx.Producers.Update(p);
                ctx.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProducersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(ctx.Producers.Find(id));
        }

        // POST: ProducersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection, Producer p)
        {
            try
            {
                ctx.Producers.Remove(p);
                ctx.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
