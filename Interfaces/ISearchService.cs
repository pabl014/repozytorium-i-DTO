using System;
using uwierzytelnianieClaimsIdentity.ViewModels.Search;

namespace uwierzytelnianieClaimsIdentity.Interfaces
{
    public interface ISearchService
    {
        SearchListVM GetYearsForList();

        void DeleteYears(int id_user);
    }
}
