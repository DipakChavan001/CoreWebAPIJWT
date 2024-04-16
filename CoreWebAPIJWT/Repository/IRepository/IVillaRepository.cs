using CoreWebAPIJWT.Models;
using System.Linq.Expressions;

namespace CoreWebAPIJWT.Repository.IRepository
{
    public interface IVillaRepository
    {
        Task<List<Villa>> GetAll(Expression<Func<Villa,bool>> filter =null);
        Task<List<Villa>> Get(Expression<Func<Villa,bool>> filter = null,bool tracked=true);
      
        Task Create(Villa entity);
      
        Task Remove(Villa entity);
        Task Save();
    }
}
