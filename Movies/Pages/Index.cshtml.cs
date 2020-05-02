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
        /// The current search terms 
        /// </summary>
        [BindProperty (SupportsGet = true)]
        public string SearchTerms { get; set; } = "";

        /// <summary>
        /// The filtered MPAA Ratings
        /// </summary>
        [BindProperty]
        public string[] MPAARatings { get; set; }

        /// <summary>
        /// The filtered genres
        /// </summary>
        [BindProperty]
        public string[] Genres { get; set; }

        /// <summary>
        /// The minimum IMDB Rating
        /// </summary>
        [BindProperty]
        public double? IMDBMin { get; set; }

        /// <summary>
        /// The maximum IMDB Rating
        /// </summary>
        [BindProperty]
        public double? IMDBMax { get; set; }

        /// <summary>
        /// The minimum Rotten Tomatoes Rating
        /// </summary>
        [BindProperty]
        public double? RottenMin { get; set; }

        /// <summary>
        /// The maximum Rotten Tomatoes Rating
        /// </summary>
        [BindProperty]
        public double? RottenMax { get; set; }

        /// <summary>
        /// Gets the search results for display on the page
        /// </summary>
        public void OnGet(double? IMDBMin, double? IMDBMax, int? RottenMin, int? RottenMax)
        {
            this.IMDBMin = IMDBMin;
            this.IMDBMax = IMDBMax;
            this.RottenMin = RottenMin;
            this.RottenMax = RottenMax;
            SearchTerms = Request.Query["SearchTerms"];
            MPAARatings = Request.Query["MPAARatings"];
            Genres = Request.Query["Genres"];
            Movies = MovieDatabase.All;
            if (SearchTerms != null)
            {
                Movies = MovieDatabase.All.Where(movie => 
                movie.Title != null && 
                movie.Title.Contains(SearchTerms, StringComparison.CurrentCultureIgnoreCase)
                );
                
            }
            if (MPAARatings != null && MPAARatings.Length != 0)
            {
                Movies = Movies.Where(movie => 
                movie.MPAARating != null &&
                MPAARatings.Contains(movie.MPAARating)
                );
            }
            if (Genres != null && Genres.Length != 0)
            {
                Movies = Movies.Where(movie =>
                movie.MajorGenre != null &&
                Genres.Contains(movie.MajorGenre)
                );
            }
            if (!(IMDBMin == null && IMDBMax == null))
            {
                if (IMDBMin == null && IMDBMax != null)
                {
                    Movies = Movies.Where(movie =>
                    movie.IMDBRating <= IMDBMax
                    );
                }
                else if (IMDBMin != null && IMDBMax == null)
                {
                    Movies = Movies.Where(movie =>
                    movie.IMDBRating >= IMDBMin
                    );
                }
                else
                {
                    Movies = Movies.Where(movie =>
                    movie.IMDBRating >= IMDBMin &&
                    movie.IMDBRating <= IMDBMax
                    );
                }
            }
            if (Genres != null && Genres.Length != 0)
            {
                Movies = Movies.Where(movie =>
                movie.MajorGenre != null &&
                Genres.Contains(movie.MajorGenre)
                );
            }
            if (!(RottenMin == null && RottenMax == null))
            {
                if (RottenMin == null && RottenMax != null)
                {
                    Movies = Movies.Where(movie =>
                    movie.RottenTomatoesRating <= RottenMax
                    );
                }
                else if (RottenMin != null && RottenMax == null)
                {
                    Movies = Movies.Where(movie =>
                    movie.RottenTomatoesRating >= RottenMin
                    );
                }
                else
                {
                    Movies = Movies.Where(movie =>
                    movie.RottenTomatoesRating >= RottenMin &&
                    movie.RottenTomatoesRating <= RottenMax
                    );
                }
            }



            /*
            Movies = MovieDatabase.Search(SearchTerms);
            Movies = MovieDatabase.FilterByMPAARating(Movies, MPAARatings);
            Movies = MovieDatabase.FilterByGenre(Movies, Genres);
            Movies = MovieDatabase.FilterByIMDBRating(Movies, IMDBMin, IMDBMax);
            Movies = MovieDatabase.FilterByRottenTomatoesRating(Movies, RottenMin, RottenMax);
    */
        }







    }
}
