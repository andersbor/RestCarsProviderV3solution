using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CarsV3RESTprovider.Model;

namespace CarsV3RESTprovider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private static readonly List<Car> Cars = new List<Car>
        {
            new Car {Id=1, Model = "Amazon", Vendor = "Volvo", Price = 12345},
            new Car {Id=2, Model = "A8", Vendor = "Audi", Price = 2222222},
            new Car {Id=3, Model = "Punto", Vendor = "Fiat", Price = 102022}
        };

        private static int _nextId = 4;

        // GET: api/Cars
        [HttpGet]
        public IEnumerable<Car> GetAll()
        {
            return Cars;
        }

        // GET: api/Cars/5
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)] // no content
        public ActionResult<Car> GetById(int id)
        {
            Car car = Cars.FirstOrDefault(c => c.Id == id);
            if (car == null) return NoContent();
            else return car;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)] // no content
        [Route("vendor/{vendor}")]
        public IEnumerable<Car> GetByVendor(string vendor)
        {
            return Cars.FindAll(car => car.Vendor.StartsWith(vendor));
        }

        [HttpGet]
        [Route("model/{model}")]
        public IEnumerable<Car> GetByModel(string model)
        {
            return Cars.FindAll(car => car.Model.StartsWith(model));
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)] // no content
        [ProducesResponseType(400)]
        [Route("price/{fromPrice:int}/{toPrice:int}")]
        public ActionResult<IEnumerable<Car>> GetByPriceRange(int fromPrice, int toPrice)
        {
            if (fromPrice > toPrice) return BadRequest();

            List<Car> cars = Cars.FindAll(car => fromPrice <= car.Price && car.Price <= toPrice);
            if (cars.Count == 0) return NoContent();
            return cars;
        }

        // POST: api/Cars
        [HttpPost]
        public int Post([FromBody] Car car)
        {
            car.Id = _nextId++;
            Cars.Add(car);
            return car.Id;
        }

        // PUT: api/Cars/5
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)] // Not found
        public ActionResult<Car> Put(int id, [FromBody] Car updates)
        {
            Car car = Cars.FirstOrDefault(c => c.Id == id);
            if (car == null) { return NotFound(); }
            else
            {
                car.Vendor = updates.Vendor;
                car.Model = updates.Model;
                car.Price = updates.Price;
                return car;
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [ProducesResponseType(204)] // No content
        [ProducesResponseType(404)] // not found
        // https://restfulapi.net/http-methods/#put
        public ActionResult Delete(int id)
        {
            int howMany = Cars.RemoveAll(car => car.Id == id);
            if (howMany == 0) return NotFound();
            else return NoContent();
        }
    }
}
