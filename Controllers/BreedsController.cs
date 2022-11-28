using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CatBreedAPI.Data;
using CatBreedAPI.Models;

namespace CatBreedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreedsController : ControllerBase
    {

        public BreedResponse Response = new BreedResponse();
        private readonly CatBreedAPIContext _context;

        public BreedsController(CatBreedAPIContext context)
        {
            _context = context;
        }

   
        [HttpGet]
        public async Task<ActionResult<BreedResponse>> GetBreeds()
        {
            var breed = _context.Breeds.Include(c => c.Cats).ToList();

            Response.statusCode = 200;
            Response.statusDescription = "Success, Breeds Acquired";
            Response.Breeds = breed;

            if (breed.Count == 0)
            {
                Response.statusCode = 404;
                Response.statusDescription = "Failed, No Breeds Exist";
                Response.Breeds = new List<Breed>();
            }
         
            return Response;
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<BreedResponse>> GetBreed(string name)
        {
            var breed = await _context.Breeds.Include(c => c.Cats).FirstOrDefaultAsync(c => c.BreedName == name);
            var responseList = new List<Breed>();
            responseList.Add(breed);
            Response.statusCode = 200;
            Response.statusDescription = "Success, Breed with name " + name + " found";
            Response.Breeds = responseList;

            if (breed == null)
            {
                Response.statusCode = 404;
                Response.statusDescription = "Failed, Breed with name " + name + " not found";
                Response.Breeds = new List<Breed>();
            }

            return Response;
        }

       
        [HttpPost]
        public async Task<ActionResult<BreedResponse>> PostBreed(Breed breed)
        {
            Response.statusCode = 200;
            Response.Breeds = new List<Breed>();
            _context.Breeds.Add(breed);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BreedExists(breed.BreedId))
                {
                    Response.statusDescription = "Breed with id " + breed.BreedId.ToString() + " already exist";
                    Response.statusCode = 400;
                    return Response;
                }
                else
                {
                    Response.statusCode = 404;
                    Response.statusDescription = "Failed to add breed";
                    return Response;
                }
         
            }
            Response.statusDescription = "Success, Breed " + breed.BreedName + " has been added";
            return Response;
        }

      
        [HttpDelete("{name}")]
        public async Task<ActionResult<BreedResponse>> DeleteBreed(string name)
        {
            var breed = await _context.Breeds.Include(b => b.Cats).FirstOrDefaultAsync(b => b.BreedName == name);
            if (breed == null)
            {
                Response.statusCode = 404;
                Response.statusDescription = "Failed, could not find the breed with the " + name;
                Response.Breeds = new List<Breed>();
                return Response;
            }

            _context.Breeds.Remove(breed);
            await _context.SaveChangesAsync();
            Response.statusCode = 200;
            Response.statusDescription = "Success, breed " + name + " has been removed";
            return Response;
        }

        private bool BreedExists(int id)
        {
            return _context.Breeds.Any(e => e.BreedId == id);
        }
    }
}
