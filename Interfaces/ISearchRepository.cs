using System;
using uwierzytelnianieClaimsIdentity.Models;

namespace uwierzytelnianieClaimsIdentity.Interfaces
{
    public interface ISearchRepository
    {
        IQueryable<Search> GetActiveLeapYears();
        void DeleteYear(int id);
    }
}
