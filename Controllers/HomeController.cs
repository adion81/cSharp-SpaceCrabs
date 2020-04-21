using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaceCrabs.Models;

namespace SpaceCrabs.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context;

        public HomeController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("create/crab")]
        public IActionResult Crab(Crab newCrab)
        {
            if(ModelState.IsValid)
            {
                _context.Crabs.Add(newCrab);
                _context.SaveChanges();
                return Redirect("/");
            }
            else
            {
                return View("Index");
            }
        }

        [HttpPost("create/planet")]
        public IActionResult Planet(Planet newPlanet)
        {
            if(ModelState.IsValid)
            {
                _context.Planets.Add(newPlanet);
                _context.SaveChanges();
                return Redirect("/");
            }
            else
            {
                return View("Index");
            }
        }

        [HttpGet("display/crabs")]
        public IActionResult DisplayCrabs()
        {
            List<Crab> AllCrabs = _context.Crabs.Include( c => c.Trips )
                                                .ThenInclude( t => t.Vacation )
                                                .ToList();

            ViewBag.Planets = _context.Planets.ToList();
            return View(AllCrabs);
        }

        [HttpPost("/trip/{crabId}")]
        public IActionResult Trip(int crabId, int planetId)
        {
            Trip trippin = new Trip();
            trippin.CrabId = crabId;
            trippin.PlanetId = planetId;
            _context.Trips.Add(trippin);
            _context.SaveChanges();
            return RedirectToAction("DisplayCrabs");
        }

        [HttpPost("leave/{tripId}")]
        public IActionResult Leave(int tripId)
        {
            Trip tripToLeave = _context.Trips.FirstOrDefault(t => t.TripId == tripId);
            _context.Trips.Remove(tripToLeave);
            _context.SaveChanges();
            return RedirectToAction("DisplayCrabs");
        }

        [HttpGet("display/planets")]
        public IActionResult DisplayPlanets()
        {
            return View(
                _context.Planets
                        .Include( p => p.Tours )
                        .ThenInclude( t => t.Tourist )
                        .OrderByDescending( p => p.Tours.Count )
                        .ToList()
            );
        }

        [HttpPost("search")]
        public JsonResult Search(Query data)
        {
            List<Planet> planets = new List<Planet>();
            if(data.query == "all")
            {
               planets = _context.Planets.ToList();
            }
            else
            {
                planets = _context.Planets.Where( p => p.Name.Contains(data.query)).ToList();
            }
            return Json(new {planets = planets} );
        }
    }
}
