using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repository.IRepository
{
    public interface IVillaRepository : IRepository<Villa>
    {
      



        Task<Villa> UpdateAsync(Villa entity);
      

    }
}
