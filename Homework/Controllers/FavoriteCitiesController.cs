using Homework.Database.DAL.UnitOfWork;
using Homework.Database.Entities;
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
            return View(_unitOfWork.CityRepository.Get().ToList());
        }

        // GET: FavoriteCities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FavoriteCities/Create
        [HttpPost]
        public ActionResult Create(City city)
        {
            try
            {
                _unitOfWork.CityRepository.Create(city);
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
            return View(_unitOfWork.CityRepository.FindById(id));
        }

        // POST: FavoriteCities/Edit/5
        [HttpPost]
        public ActionResult Edit(City city)
        {
            try
            {
                _unitOfWork.CityRepository.Update(city);
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
            return View(_unitOfWork.CityRepository.FindById(id));
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
                return View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
