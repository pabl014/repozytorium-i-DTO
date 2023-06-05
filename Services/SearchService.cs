using uwierzytelnianieClaimsIdentity.Models;
using uwierzytelnianieClaimsIdentity.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;


namespace uwierzytelnianieClaimsIdentity.Services
{
        public class SearchService : SearchInterface
        {
            private readonly SearchContext _context;

            public SearchService(SearchContext context)
            {
                _context = context;
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

            public async Task DeleteSearchAsync(int id, Search r)
            {
                r = _context.Search.Find(id);
                _context.Search.Remove(r);
                await _context.SaveChangesAsync();

            }

        }
}
