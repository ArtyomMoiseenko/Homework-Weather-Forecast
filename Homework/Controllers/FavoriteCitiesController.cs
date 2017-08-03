using Homework.Database.DAL.UnitOfWork;
using Homework.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ActionResult> Index()
        {
            var cities = new List<CityModel>();
            var citiesDB = await _unitOfWork.CityRepository.Get();
            citiesDB.ToList().ForEach(i => cities.Add(new CityModel { Id = i.Id, Name = i.Name }));
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
        public async Task<ActionResult> Edit(int id)
        {
            var data = await _unitOfWork.CityRepository.FindById(id);
            var city = new CityModel() { Id = data.Id, Name = data.Name };
            return View(city);
        }

        // POST: FavoriteCities/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(CityModel city)
        {
            try
            {
                var editCity = await _unitOfWork.CityRepository.FindById(city.Id);
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
        public async Task<ActionResult> Delete(int id)
        {
            var data = await _unitOfWork.CityRepository.FindById(id);
            var city = new CityModel() { Id = data.Id, Name = data.Name };
            return View(city);
        }

        // POST: FavoriteCities/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeletePost(int id)
        {
            try
            {
                var city = await _unitOfWork.CityRepository.FindById(id);
                _unitOfWork.CityRepository.Remove(city);
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
