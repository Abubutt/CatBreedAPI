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
    public class CatsController : ControllerBase
    {
        private readonly CatBreedAPIContext _context;
        public CatResponse Response = new CatResponse();

        public CatsController(CatBreedAPIContext context)
        {
            _context = context;
        }

        // GET: api/Cats
        [HttpGet]
        public async Task<ActionResult<CatResponse>> GetCats()
        {
            var cats = _context.Cats.ToList();
            if (cats.Count == 0)
            {
                Response.statusCode = 404;
                Response.statusDescription = "Failed, no cats exist";
                Response.Cats = new List<Cat>();
            }
            Response.statusCode = 200;
            Response.statusDescription = "Success, cats acquired";
            Response.Cats = cats;

            return Response;
        }

        // GET: api/Cats/5
        [HttpGet("{name}")]
        public async Task<ActionResult<CatResponse>> GetCat(string name)
        {
            var cat = await _context.Cats.FirstOrDefaultAsync(c => c.CatName == name);
            var responseList = new List<Cat>();
            responseList.Add(cat);
            Response.statusCode = 200;
            Response.statusDescription = "Success, cat with the name " + name + " found";
            Response.Cats = responseList;

            if (responseList.Count == 0)
            {
                Response.statusCode = 404;
                Response.statusDescription = "Failed, cat with the name " + name + " not found";
                Response.Cats = new List<Cat>();
            }

            return Response;
        }




        [HttpPost]
        public async Task<ActionResult<CatResponse>> PostCat(Cat cat)
        {
            Response.statusCode = 200;
            Response.Cats = new List<Cat>();
            _context.Cats.Add(cat);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CatExists(cat.CatId))
                {
                    Response.statusDescription = "Cat with id " + cat.CatId.ToString() + " already exist";
                    Response.statusCode = 400;
                    return Response;
                }
                else
                {
                    Response.statusCode = 404;
                    Response.statusDescription = "Failed to add cat";
                    return Response;
                }

            }
            Response.statusDescription = "Success, Cat " + cat.CatName + " has been added";
            return Response;
        }


        private bool CatExists(int id)
        {
            return _context.Cats.Any(e => e.CatId == id);
        }
    }
}