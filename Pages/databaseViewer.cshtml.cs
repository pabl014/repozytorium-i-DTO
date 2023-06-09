using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using uwierzytelnianieClaimsIdentity.Data;
using uwierzytelnianieClaimsIdentity.Models;
using uwierzytelnianieClaimsIdentity.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using uwierzytelnianieClaimsIdentity.Interfaces;
using uwierzytelnianieClaimsIdentity.ViewModels.Search;

namespace uwierzytelnianieClaimsIdentity.Pages
{
    public class databaseViewerModel : PageModel
    {
        public IEnumerable<SearchVM> searchList;
        private readonly ILogger<IndexModel> _logger;
        private readonly SearchContext _searchContext;
        private readonly IConfiguration Configuration;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ISearchService _searchService;
        public SearchListVM Records {get; set;}
        public databaseViewerModel(ILogger<IndexModel> logger, SearchContext context, IConfiguration configuration, IHttpContextAccessor contextAccessor, ISearchService searchService)
		{
			_logger = logger;
			_searchContext = context;
			Configuration = configuration;
			_contextAccessor = contextAccessor;
			_searchService = searchService;
		}
		public Search itemToSearch { get; set; } = new Search();
		public string NameSort { get; set; }
		public string DateSort { get; set; }
		public string CurrentFilter { get; set; }
		public string CurrentSort { get; set; }

		public PaginatedList<SearchVM> search { get; set; }

		public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
		{
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var user_id = _contextAccessor.HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                itemToSearch.userNumber = user_id.Value;

            }

            CurrentSort = sortOrder;
			NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
			DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            Records = _searchService.GetYearsForList();
            switch (sortOrder)
            {
                case "Date":
                    Records.Years = Records.Years.OrderBy(s => s.date).ToList();
                    break;
                case "date_desc":
                    Records.Years = Records.Years.OrderByDescending(s => s.date).ToList();
                    break;
                default:
                    Records.Years = Records.Years.OrderByDescending(s => s.date).ToList();
                    break;
            }

            if (searchString != null)
			{
				pageIndex = 1;
			}
			else
			{
				searchString = currentFilter;
			}
			CurrentFilter = searchString;

            //IQueryable<Search> studentsIQ = from s in _searchContext.Search.OrderByDescending(x => x.date) select s;
            var yearsQueryable = Records.Years.AsQueryable();
            var pageSize = Configuration.GetValue("PageSize", 20);
            //search = await PaginatedList<Search>.CreateAsync(studentsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
            search = await PaginatedList<SearchVM>.CreateAsync(Records.Years, pageIndex ?? 1, pageSize);

        }
       
        public IActionResult OnPost(int searchID)
		{
            //itemToSearch = _searchContext.Search.Find(searchID); 
            //itemToSearch.id = searchID;							// znalezienie danego rekordu w bazie danych po id
            //_searchContext.Search.Remove(itemToSearch);			// usuniecie
            //_searchContext.SaveChanges();						// zapisanie

            _searchService.DeleteYears(searchID);
            return RedirectToAction("Async");
			//return Page();
        }
        

    }
}
