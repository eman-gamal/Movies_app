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
    using System.Collections.Generic;
    
    public partial class Movie_Rating_Log
    {
        public int Id { get; set; }
        public int Movie_id { get; set; }
        public decimal Rate { get; set; }
        public System.DateTime Update_date { get; set; }
        public int Source_id { get; set; }
        public decimal Popularity { get; set; }
    
        public virtual Movie Movie { get; set; }
        public virtual Source Source { get; set; }
    }
}