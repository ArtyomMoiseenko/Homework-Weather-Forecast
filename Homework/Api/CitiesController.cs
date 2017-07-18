using Homework.Database.DAL.UnitOfWork;
using Homework.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Homework.Api
{
    public class CitiesController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public CitiesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Cities
        [HttpGet]
        public IHttpActionResult GetCities()
        {
            var cities = new List<CityModel>();
            var data = _unitOfWork.CityRepository.Get().ToList();
            if(data == null)
            {
                return NotFound();
            }
            data.ForEach(i => cities.Add(new CityModel { Id = i.Id, Name = i.Name }));

            return Ok(cities);
        }

        // GET: api/Cities/5
        [HttpGet]
        public IHttpActionResult GetCity(int id)
        {
            var data = _unitOfWork.CityRepository.FindById(id);
            if (data == null)
            {
                return NotFound();
            }
            var city = new CityModel { Id = data.Id, Name = data.Name };
            return Ok(city);
        }

        // POST: api/Cities
        [HttpPost]
        public IHttpActionResult CreateCity([FromBody]CityModel city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _unitOfWork.CityRepository.Create(new Database.Entities.City { Id = city.Id, Name = city.Name });
            _unitOfWork.Save();

            return CreatedAtRoute("DefaultApi", new { id = city.Id }, city);
        }

        // PUT: api/Cities/5
        [HttpPut]
        public IHttpActionResult EditCity(int id, [FromBody]CityModel city)
        {
            if (id == city.Id)
            {
                _unitOfWork.CityRepository.Update(new Database.Entities.City { Id = city.Id, Name = city.Name });
                _unitOfWork.Save();
            }
            return Ok(city);
        }

        // DELETE: api/Cities/5
        [HttpDelete]
        public void DeleteCity(int id)
        {
            var city = _unitOfWork.CityRepository.FindById(id);
            if (city != null)
            {
                _unitOfWork.CityRepository.Remove(city);
                _unitOfWork.Save();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
