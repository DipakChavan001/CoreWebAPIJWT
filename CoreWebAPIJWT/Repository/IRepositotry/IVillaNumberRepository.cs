using CoreWebAPIJWT.Models;
using System.Linq.Expressions;

namespace CoreWebAPIJWT.Repository.IRepository
{
    public interface IVillaNumberRepository :IRepository<VillaNumber>
    {
        Task<VillaNumber> UpdateNumberAsync(VillaNumber entity);
    }
}
