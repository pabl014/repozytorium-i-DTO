using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using uwierzytelnianieClaimsIdentity.Data;
using uwierzytelnianieClaimsIdentity.Models;
using Newtonsoft.Json;
using System;
namespace uwierzytelnianieClaimsIdentity.Pages
{
	public class IndexModel : PageModel
	{
		public bool checkIfStart = false;

		private readonly ILogger<IndexModel> _logger;


		public Search search { get; set; } = new Search();

        private readonly IHttpContextAccessor _contextAccessor; // odczyt statusu zalogowania

        private readonly SearchContext _searchContext;

		public IndexModel(ILogger<IndexModel> logger, SearchContext context, IHttpContextAccessor contextAccessor)
		{
			_logger = logger;
			_searchContext = context;
			_contextAccessor = contextAccessor;	
        }
        public void OnGet()
		{
			var checkOnGet = ViewData["message"];

            if (checkOnGet == "1")
			{
				checkIfStart = true;
			}
            var najnowszyRekord = _searchContext.Search
			.OrderByDescending(r => r.date)
			.FirstOrDefault();

			search = najnowszyRekord;

		
        }
        public IActionResult OnPost(int year, string? name) 
		{	
			search.name = name;
			search.year = year;
			search.date = DateTime.Now;

            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var user_id = _contextAccessor.HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                search.userNumber = user_id.Value;
                search.userLogin = _contextAccessor.HttpContext.User.Identity.Name;
            }

            ViewData["message"] = "1";
			if (search != null)
			{
                checkIfStart = true;
			}
            _searchContext.Search.Add(search);
            _searchContext.SaveChanges();

            return Page();

        }
	}
}