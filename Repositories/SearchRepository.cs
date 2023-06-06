using uwierzytelnianieClaimsIdentity.Data;
using uwierzytelnianieClaimsIdentity.Interfaces;
using uwierzytelnianieClaimsIdentity.Models;
using System;

namespace uwierzytelnianieClaimsIdentity.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        private readonly SearchContext _context;
        public SearchRepository(SearchContext context)
        {
            _context = context;
        }
        public IQueryable<Search> GetActiveLeapYears()
        {
            return _context.Search;
        }

        public void DeleteYear(int id)
        {
            var _post = _context.Search.Find(id);
            if (_post != null)
            {
                _post.id = id; 
                _context.Search.Remove(_post);
                _context.SaveChanges();
            }
        }
    }
}
