using uwierzytelnianieClaimsIdentity.Models;
using uwierzytelnianieClaimsIdentity.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using uwierzytelnianieClaimsIdentity.ViewModels.Search;
using uwierzytelnianieClaimsIdentity.Repositories;
using NuGet.Protocol.Plugins;
using uwierzytelnianieClaimsIdentity.Interfaces;


namespace uwierzytelnianieClaimsIdentity.Services
{
    public class SearchService : ISearchService
    {
        private readonly SearchContext _context;
        private readonly ISearchRepository _repository;

        public SearchService(SearchContext context, ISearchRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        public async Task<List<Search>> GetAllSearchAsync()
        {
            return await _context.Search.ToListAsync();
        }

        public async Task<Search> GetSearchByIdAsync(int id)
        {
            return await _context.Search.FindAsync(id);
        }

        public async Task CreateSearchAsync(Search year)
        {
            _context.Search.Add(year);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSearchAsync(Search year)
        {
            _context.Search.Update(year);
            await _context.SaveChangesAsync();
        }

        //public async Task DeleteSearchAsync(int id, Search r)
        //{
        //    r = _context.Search.Find(id);
        //    _context.Search.Remove(r);
        //    await _context.SaveChangesAsync();

        //}
        public void DeleteYears(int id)
        {
            _repository.DeleteYear(id);
        }
        public SearchListVM GetYearsForList()
        {
            var years = _repository.GetActiveLeapYears();
            SearchListVM result = new SearchListVM();
            result.Years = new List<SearchVM>();
            foreach (var year in years)
            {
                // mapowanie obiektów
                var rokVM = new SearchVM()
                {
                    id = year.id,
                    name = year.name,
                    year = year.year,
                    date = year.date,
                    usernumber = year.userNumber,
                    userlogin = year.userLogin
                };
                result.Years.Add(rokVM);
            }
            return result;
        }

    } 
}
