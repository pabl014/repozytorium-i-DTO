using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using uwierzytelnianieClaimsIdentity.Models;
namespace uwierzytelnianieClaimsIdentity.ViewModels.Search
{
    public class SearchVM
    {
        public int id { get; set; }
        public string? name { get; set; }
        public int year { get; set; }
        public DateTime date { get; set; }
        public string? usernumber { get; set; }
        public string? userlogin { get; set; }


        public string sentence()
        {
            if ((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0))
            {
                return name + " urodzil sie w roku " + year + ". Był to rok przestepny.";
            }
            else
            {
                return name + " urodzil sie w roku " + year + ". Był to rok nieprzestepny.";
            }
        }

        public string shortSentence()
        {
            if ((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0))
            {
                return "przestepny";
            }
            else
            {
                return "nieprzestepny";
            }
        }
    }
}
