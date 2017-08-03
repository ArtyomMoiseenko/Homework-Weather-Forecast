using Homework.Database.DAL.UnitOfWork;
using Homework.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IHttpActionResult> GetCities()
        {
            var cities = new List<CityModel>();
            var data = await _unitOfWork.CityRepository.Get();
            if(data == null)
            {
                return NotFound();
            }
            data.ToList().ForEach(i => cities.Add(new CityModel { Id = i.Id, Name = i.Name }));

            return Ok(cities);
        }

        // GET: api/Cities/5
        [HttpGet]
        public async Task<IHttpActionResult> GetCity(int id)
        {
            var data = await _unitOfWork.CityRepository.FindById(id);
            if (data == null)
            {
                return NotFound();
            }
            var city = new CityModel { Id = data.Id, Name = data.Name };
            return Ok(city);
        }

        // POST: api/Cities
        [HttpPost]
        public async Task<IHttpActionResult> CreateCity([FromBody]CityModel city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _unitOfWork.CityRepository.Create(new Database.Entities.City { Id = city.Id, Name = city.Name });
            await _unitOfWork.Save();

            return CreatedAtRoute("DefaultApi", new { id = city.Id }, city);
        }

        // PUT: api/Cities/5
        [HttpPut]
        public async Task<IHttpActionResult> EditCity(int id, [FromBody]CityModel city)
        {
            if (id == city.Id)
            {
                _unitOfWork.CityRepository.Update(new Database.Entities.City { Id = city.Id, Name = city.Name });
                await _unitOfWork.Save();
            }
            return Ok(city);
        }

        // DELETE: api/Cities/5
        [HttpDelete]
        public async Task DeleteCity(int id)
        {
            var city = await _unitOfWork.CityRepository.FindById(id);
            if (city != null)
            {
                _unitOfWork.CityRepository.Remove(city);
                await _unitOfWork.Save();
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
