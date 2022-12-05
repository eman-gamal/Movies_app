using Microsoft.VisualStudio.TestTools.UnitTesting;
using Movie_API.Controllers;
using Movie_API.Models;
using System;
using System.Web.Http.Results;


namespace Movie_API.Tests
{
    [TestClass]
    public class TestMovieController
    {
        [TestMethod]
        public void GetMovieByName_ShouldReturnMovieDetails()
        {

            var controller = new MoviesController ();

            var result = controller.GetMovie("Our Planet");
            var contentResult = result as OkNegotiatedContentResult<Movies_Select_ByTitle_Result>;
            // Assert the result  
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual("Our Planet", contentResult.Content.Title);
        }
           
    }
}
