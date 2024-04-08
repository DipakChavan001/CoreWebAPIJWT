using CoreWebAPIJWT.Models.DTO;

namespace CoreWebAPIJWT.Data
{
    public class VillaStore
    {
        public static List<VillaDTO> VillaList= new List<VillaDTO>() {
            new VillaDTO { Id = 1, Name = "Pool View",Sqft=400,Occupancy=4 },
            new VillaDTO { Id = 2, Name = "Beach View",Sqft=100,Occupancy=3 }
            };
    }
}
