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
                return RedirectToAction("DisplayCrabs");
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
                return RedirectToAction("AllPlanets");
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
