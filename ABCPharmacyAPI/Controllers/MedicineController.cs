using ABCPharmacyAPI.Entities;
using ABCPharmacyAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ABCPharmacyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineService _medicineService;

        public MedicineController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Medicine>>> GetAllMedicines(string ? name, int pageNumber , int pageSize)
        {
            var result = await _medicineService.GetAllMedicinesAsync(name, pageNumber, pageSize);

            return Ok(new
            {
                success = true,
                count = result.TotalCount,
                pageNumber,
                pageSize,
                data = result.Data
            });

        }

        //[HttpGet("search")]
        //public async Task<ActionResult<List<Medicine>>> SearchMedicines([FromQuery] string name, int pageNumber=1, int pageSize = 10)
        //{
        //    var result = await _medicineService.GetAllMedicinesAsync(name, pageNumber, pageSize);

        //    return Ok(new
        //    {
        //        success = true,
        //        count = result.TotalCount,
        //        pageNumber,
        //        pageSize,
        //        data = result.Data
        //    });
        //}
        [HttpPost]
        public async Task<ActionResult<Medicine>> AddMedicine([FromBody] Medicine medicine)
        {
               if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newMedicine = await _medicineService.AddMedicine(medicine);

            return Ok(newMedicine);
        }



    }
}
