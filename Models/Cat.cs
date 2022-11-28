using System;
using System.Collections.Generic;

namespace CatBreedAPI.Models
{
    public partial class Cat
    {
        public int CatId { get; set; }
        public string? CatName { get; set; }
        public int? Age { get; set; }
        public int BreedId { get; set; }

      
    }
}
