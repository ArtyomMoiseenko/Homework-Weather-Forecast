using Homework.Database.DAL.UnitOfWork;
using Homework.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Homework.Controllers
{
    public class FavoriteCitiesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public FavoriteCitiesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: FavoriteCities
        public ActionResult Index()
        {
            var cities = new List<CityModel>();
            _unitOfWork.CityRepository.Get().ToList()
                .ForEach(i => cities.Add(new CityModel { Id = i.Id, Name = i.Name }));
            return View(cities);
        }

        // GET: FavoriteCities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FavoriteCities/Create
        [HttpPost]
        public ActionResult Create(CityModel city)
        {
            try
            {
                var newCity = new Homework.Database.Entities.City { Id = city.Id, Name = city.Name };
                _unitOfWork.CityRepository.Create(newCity);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(city);
            }
        }

        // GET: FavoriteCities/Edit/5
        public ActionResult Edit(int id)
        {
            var data = _unitOfWork.CityRepository.FindById(id);
            var city = new CityModel() { Id = data.Id, Name = data.Name };
            return View(city);
        }

        // POST: FavoriteCities/Edit/5
        [HttpPost]
        public ActionResult Edit(CityModel city)
        {
            try
            {
                var editCity = _unitOfWork.CityRepository.FindById(city.Id);
                editCity.Name = city.Name;
                _unitOfWork.CityRepository.Update(editCity);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(city);
            }
        }

        // GET: FavoriteCities/Delete/5
        public ActionResult Delete(int id)
        {
            var data = _unitOfWork.CityRepository.FindById(id);
            var city = new CityModel() { Id = data.Id, Name = data.Name };
            return View(city);
        }

        // POST: FavoriteCities/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            try
            {
                _unitOfWork.CityRepository.Remove(_unitOfWork.CityRepository.FindById(id));
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Delete");
            }
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
