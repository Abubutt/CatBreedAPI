using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
namespace CatBreedAPI.Models
{
	public class BreedResponse
	{

		public int statusCode { get; set; }
        public string statusDescription { get; set; }
        public List<Breed> Breeds { get; set; } = new();
        //public virtual List<Cat> Cats { get; set; } = null!;
    }
}

