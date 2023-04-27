using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaAPI.Models.Dto 
{

    public class VillaDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public String Name { get; set; }
        public string Details { get; set; }
        public double Rate { get; set; }
        public string ImageUrl { get; set; }
        public string Amenity { get; set; }

        public int Occupancy { get; set; }

        public int Sqft { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

    }

}