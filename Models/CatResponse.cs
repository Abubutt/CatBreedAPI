using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
namespace CatBreedAPI.Models
{
	public class CatResponse
	{
        

        public int statusCode { get; set; }
        public string statusDescription { get; set; }
        public virtual List<Cat> Cats { get; set; } = new();

    }
}

