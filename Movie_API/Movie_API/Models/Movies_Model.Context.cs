//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Movie_API.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class Movies_DB_Entities : DbContext
    {
        public Movies_DB_Entities()
            : base("name=Movies_DB_Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Movie_Rating_Log> Movie_Rating_Log { get; set; }
        public virtual DbSet<Source> Sources { get; set; }
        public virtual DbSet<Type> Types { get; set; }
        public virtual DbSet<User> Users { get; set; }
    
        public virtual ObjectResult<Movies_Select_All_Result> Movies_Select_All()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Movies_Select_All_Result>("Movies_Select_All");
        }
    
        public virtual ObjectResult<Movies_Select_ByTitle_Result> Movies_Select_ByTitle(string movie_Name)
        {
            var movie_NameParameter = movie_Name != null ?
                new ObjectParameter("Movie_Name", movie_Name) :
                new ObjectParameter("Movie_Name", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Movies_Select_ByTitle_Result>("Movies_Select_ByTitle", movie_NameParameter);
        }
    
        public virtual ObjectResult<Sources_Select_All_Result> Sources_Select_All()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Sources_Select_All_Result>("Sources_Select_All");
        }
    
        public virtual int Watch_List_Insert(string username, string password, string movie_Name)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("Username", username) :
                new ObjectParameter("Username", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            var movie_NameParameter = movie_Name != null ?
                new ObjectParameter("Movie_Name", movie_Name) :
                new ObjectParameter("Movie_Name", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Watch_List_Insert", usernameParameter, passwordParameter, movie_NameParameter);
        }
    
        public virtual ObjectResult<Watch_List_Select_Result> Watch_List_Select(string username, string password)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("Username", username) :
                new ObjectParameter("Username", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Watch_List_Select_Result>("Watch_List_Select", usernameParameter, passwordParameter);
        }
    
        public virtual int Movie_Insert(string movie_name, Nullable<int> release_year, string description, string type_name, Nullable<decimal> rate, Nullable<decimal> popularity, string source_name)
        {
            var movie_nameParameter = movie_name != null ?
                new ObjectParameter("Movie_name", movie_name) :
                new ObjectParameter("Movie_name", typeof(string));
    
            var release_yearParameter = release_year.HasValue ?
                new ObjectParameter("Release_year", release_year) :
                new ObjectParameter("Release_year", typeof(int));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            var type_nameParameter = type_name != null ?
                new ObjectParameter("Type_name", type_name) :
                new ObjectParameter("Type_name", typeof(string));
    
            var rateParameter = rate.HasValue ?
                new ObjectParameter("Rate", rate) :
                new ObjectParameter("Rate", typeof(decimal));
    
            var popularityParameter = popularity.HasValue ?
                new ObjectParameter("Popularity", popularity) :
                new ObjectParameter("Popularity", typeof(decimal));
    
            var source_nameParameter = source_name != null ?
                new ObjectParameter("Source_name", source_name) :
                new ObjectParameter("Source_name", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Movie_Insert", movie_nameParameter, release_yearParameter, descriptionParameter, type_nameParameter, rateParameter, popularityParameter, source_nameParameter);
        }
    }
}
