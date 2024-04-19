using System.ComponentModel.DataAnnotations;

namespace CoreWebAPIJWT.Models.DTO
{
    public class VillaNumberCreateDTO
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public int ViillaId { get; set; }
        public string SpecialDetails { get; set; }
        
    }
}
