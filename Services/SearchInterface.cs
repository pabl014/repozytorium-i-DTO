using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using uwierzytelnianieClaimsIdentity.Models;


namespace uwierzytelnianieClaimsIdentity.Services
{
    public interface SearchInterface
    {
        Task<List<Search>> GetAllSearchAsync();
        Task<Search> GetSearchByIdAsync(int id);
        Task CreateSearchAsync(Search year);
        Task UpdateSearchAsync(Search year);
        Task DeleteSearchAsync(int id, Search r);
    }
}
