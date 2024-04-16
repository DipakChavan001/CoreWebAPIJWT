using CoreWebAPIJWT.Models;
using System.Linq.Expressions;

namespace CoreWebAPIJWT.Repository.IRepository
{
    public interface IVillaRepository :IRepository<Villa>
    {
        Task<Villa> UpdateAsync(Villa entity);
    }
}
