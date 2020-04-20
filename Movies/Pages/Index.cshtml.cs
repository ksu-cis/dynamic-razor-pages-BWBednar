using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movies.Pages
{
    public class IndexModel : PageModel
    {

        /// <summary>
        /// The movies to display on the index page 
        /// </summary>
        public IEnumerable<Movie> Movies { get; protected set; }

        /// <summary>
        /// The filtered MPAA Ratings
        /// </summary>
        public string[] MPAARatings { get; set; }


        /// <summary>
        /// The current search terms 
        /// </summary>
        public string SearchTerms { get; set; }

        /// <summary>
        /// Gets the search results for display on the page
        /// </summary>
        public void OnGet()
        {
            SearchTerms = Request.Query["SearchTerms"];
            Movies = MovieDatabase.Search(SearchTerms);
            MPAARatings = Request.Query["MPAARatings"];


        }


    }
}
