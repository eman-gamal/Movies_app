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
    public class WatchListController : ApiController
    {
        private Movies_DB_Entities db = new Movies_DB_Entities();

        // GET: api/WatchList
        [ResponseType(typeof(ObjectResult<Watch_List_Select_Result>))]
        public ObjectResult<Watch_List_Select_Result> GetWatch_List_Select_Result(User user)
        {
            return db.Watch_List_Select(user.Username,user.Password);
        }

        // POST: api/WatchList
        [ResponseType(typeof(Movies_Select_ByTitle_Result))]
        public IHttpActionResult PostWatch_List_Select_Result(User user,string movie_name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Watch_List_Insert(user.Username,user.Password, movie_name);
            db.SaveChanges();

            ObjectResult<Movies_Select_ByTitle_Result> sel_movie = db.Movies_Select_ByTitle(movie_name);

            return CreatedAtRoute("DefaultApi", new { name = movie_name }, sel_movie); 
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