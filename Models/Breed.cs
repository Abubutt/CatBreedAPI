using System;
using System.Collections.Generic;

namespace CatBreedAPI.Models
{
    public partial class Breed
    {
       

        public int BreedId { get; set; }
        public string BreedName { get; set; } = null!;
        public string? PriceRange { get; set; }
        public string? Location { get; set; }

        public List<Cat> Cats { get; set; } = new();
    }
}
