using CarApi.Data;
using CarApi.Helpers;
using CarApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private ApiDbContext _dbcontext;
        public BrandsController(ApiDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetBrands()
        {
            var brands = await (from brand in _dbcontext.Brands
                                select new
                                {
                                    id = brand.Id,
                                    name = brand.Name,
                                    madeby= brand.MadeBy,
                                    imageUrl = brand.ImageUrl,
                                    audioUrl= brand.AudioUrl,
                                }).ToListAsync();
            return Ok(brands);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrands(int id)
        {
            var brand =  await _dbcontext.Brands.FindAsync(id);
            if (brand==null)
            {
                return NotFound("No Record Found");
            }
            else
            {
                var brands = await (from temporaryBrand in _dbcontext.Brands
                                    select new
                                    {
                                        id = brand.Id,
                                        name = brand.Name,
                                        madeby = brand.MadeBy,
                                        imageUrl = brand.ImageUrl,
                                        audioUrl = brand.AudioUrl,
                                    }).FirstOrDefaultAsync();
                return Ok(brands);
            } 
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Brand Recivedcar)
        {
            var imageUrl = await FileHelper.UploadImage(Recivedcar.Image);
            Recivedcar.ImageUrl = imageUrl;
            var audioUrl = await FileHelper.UploadAudio(Recivedcar.AudioFile);
            Recivedcar.AudioUrl = audioUrl;
            await _dbcontext.Brands.AddAsync(Recivedcar);
            await _dbcontext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] Brand RecivedCar)
        {
            var car = await _dbcontext.Brands.FindAsync(id);
            if (car == null)
            {
                return NotFound("NO RECORD FOUND WITH THIS ID");
            }
            else
            {
                var imageUrl = await FileHelper.UploadImage(RecivedCar.Image);
                var audioUrl = await FileHelper.UploadAudio(RecivedCar.AudioFile);

                car.Name = RecivedCar.Name;
                car.ImageUrl = imageUrl;
                car.MadeBy = RecivedCar.MadeBy;
                car.AudioUrl = audioUrl;
                _dbcontext.SaveChanges();
                return Ok("Updated Successfully");
            }

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var brand =   await _dbcontext.Brands.FindAsync(id);
            if (brand==null)
            {
                return NotFound("Record Not Found");
            }
            else
            {
                _dbcontext.Brands.Remove(brand);
                await _dbcontext.SaveChangesAsync();
                return Ok("Deleted");
            }
        }


    }
}
