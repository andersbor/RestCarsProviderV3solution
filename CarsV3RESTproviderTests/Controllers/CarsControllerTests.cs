using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using CarsV3RESTprovider.Model;
using Microsoft.AspNetCore.Mvc;

namespace CarsV3RESTprovider.Controllers.Tests
{
    [TestClass()]
    public class CarsControllerTests
    {
        private readonly CarsController controller = new CarsController();

        [TestMethod]
        public void GetAllTest()
        {
            IEnumerable<Car> res = controller.GetAll();
            Assert.AreEqual(3, res.Count()); // using System.Linq
        }

        [TestMethod]
        public void GetByIdTest()
        {
            ActionResult<Car> res = controller.GetById(1);
            Car car = res.Value;
            Assert.AreEqual("Volvo", car.Vendor);

            res = controller.GetById(-1);
            car = res.Value;
            Assert.IsNull(car);
        }

        [TestMethod]
        public void GetByVendor()
        {
            IEnumerable<Car> res = controller.GetByVendor("Volvo");
            Assert.AreEqual(1, res.Count());

            res = controller.GetByVendor("NonExisting");
            Assert.AreEqual(0, res.Count());
        }

        [TestMethod]
        public void GetByModel()
        {
            IEnumerable<Car> res = controller.GetByModel("A8");
            Assert.AreEqual(1, res.Count());

            res = controller.GetByModel("NonExisting");
            Assert.AreEqual(0, res.Count());
        }

        [TestMethod]
        public void PostTest()
        {
            Car newCar = new Car { Vendor = "Audi", Model = "A6", Price= 123457};
            int newId = controller.Post(newCar);
            Assert.AreEqual(4, newId);
        }

        [TestMethod]
        public void PutTest()
        {
            Car updates = new Car { Vendor = "Volvo", Model = "Amazon", Price = 3333 };
            ActionResult<Car> res = controller.Put(1, updates);
            Car updatedCar = res.Value;
            Assert.AreEqual("Amazon", updatedCar.Model);

            ActionResult<Car> res2 = controller.Put(-188, updates);
            Assert.IsNull(res2.Value);
            //Assert.AreEqual("Volvo", res2.Value.Model);
            // Assert.IsInstanceOfType(res2, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeleteTest()
        {
            ActionResult res = controller.Delete(1);
            Assert.IsInstanceOfType(res, typeof(NoContentResult));

            res = controller.Delete(-1);
            Assert.IsInstanceOfType(res, typeof(NotFoundResult));
        }
    }
}