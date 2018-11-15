using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;


namespace StarChart.Controllers
{
    [Route("")]
    [ApiController]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet("{id:int}", Name = "GetById")]
        public IActionResult GetById(int id)
        {
            var celestialObject = _context.CelestialObjects.Find(id);
            if (celestialObject == null)
                return NotFound();
            celestialObject.Satellites = _context.CelestialObjects.Where(e => e.OrbitedObjectId == id).ToList();
                return Ok(celestialObject);

        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var celestialObjects = _context.CelestialObjects.Where(e => e.Name == name);
            if (!celestialObjects.Any())
                return NotFound();
            foreach(var celestialObject in celestialObjects)
            {
                celestialObject.Satellites = _context.CelestialObjects.Where(e => e.OrbitedObjectId == celestialObject.Id).ToList();
            }
            
            return Ok(celestialObjects);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var celestialObjects = _context.CelestialObjects.ToList();  
            foreach (var celestialObject in celestialObjects)
            {
                celestialObject.Satellites = _context.CelestialObjects.Where(e => e.OrbitedObjectId == celestialObject.Id).ToList();
            }

            return Ok(celestialObjects);
        }

    }
}


//Create GetById Action

//Watch video

//In the CelestialObjectController create a new method GetById

//This method should have a return type of IActionResult
//This method should accept a parameter of type int named id.
//This method should have an HttpGet attribute with an value of "{id:int}" and the Name property set to "GetById".
//This method should return NotFound() when there is no CelestialObject with an Id property that matches the parameter.
//This method should also set the Satellites property to any CelestialObjects who's OrbitedObjectId is the current CelestialObject's Id.
//This method should return an Ok with a value of the CelestialObject whose Id property matches the id parameter.
//Create GetByName Action

//Watch video

//Create the GetByName method

//This method should have a return type of IActionResult.
//This method should accept a parameter of type string named name.
//This method should have an HttpGet attribute with a value of "{name}".
//This method should return NotFound() when there is no CelestialObject with a Name property that matches the name parameter.
//This method should also set the Satellites property for each returned CelestialObject to any CelestialObjects who's OrbitedObjectId is the current CelestialObject's Id.
//This method should return an Ok with a value of all CelestialObjects whose Name property matches the name parameter.
