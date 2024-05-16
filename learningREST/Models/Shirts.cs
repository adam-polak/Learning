using System.ComponentModel.DataAnnotations;

namespace learningREST.Models {
    public class Shirt {

        [Range(1, 10000)]
        public int ShirtId {get; set; }

        [Required]
        public string? Brand { get; set; }
        
        [Required]
        public string? Color { get; set; }

        public int Size { get; set; }

        public double Price { get; set; }
    }
}