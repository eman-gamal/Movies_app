using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Movie_API.Models;

namespace Movie_API.Controllers
{
    public class MoviesController : ApiController
    {
        private Movies_DB_Entities db = new Movies_DB_Entities();

        // GET: api/Movies
        public ObjectResult<Movies_Select_All_Result> GetMovies()
        {
            ObjectResult < Movies_Select_All_Result > m= db.Movies_Select_All(); ;
            return m;
        }

        // GET: api/Movies/The Godfather
        [ResponseType(typeof(ObjectResult<Movies_Select_ByTitle_Result>))]
        public IHttpActionResult GetMovie(string Movie_name)
        {
            ObjectResult<Movies_Select_ByTitle_Result> movie = db.Movies_Select_ByTitle(Movie_name);
            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }



        // POST: api/Movies
        [ResponseType(typeof(Movie_Insert_Param))]
        public IHttpActionResult PostMovie(Movie_Insert_Param movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int movie_id = db.Movie_Insert(movie.Movie_name,movie.Release_year,movie.Description,movie.Type_name,movie.Rate,movie.Popularity,movie.Source_name);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = movie_id }, movie);
        }

        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

       
    }
}