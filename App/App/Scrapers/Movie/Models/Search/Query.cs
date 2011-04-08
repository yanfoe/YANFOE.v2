// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Query.cs" company="The YANFOE Project">
//   Copyright 2011 The YANFOE Project
// </copyright>
// <license>
//   This software is licensed under a Creative Commons License
//   Attribution-NonCommercial-ShareAlike 3.0 Unported (CC BY-NC-SA 3.0) 
//   http://creativecommons.org/licenses/by-nc-sa/3.0/
//   See this page: http://www.yanfoe.com/license
//   For any reuse or distribution, you must make clear to others the 
//   license terms of this work.  
// </license>
// --------------------------------------------------------------------------------------------------------------------

namespace YANFOE.Scrapers.Movie.Models.Search
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// A collection of search results
    /// </summary>
    public class Query
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Query"/> class.
        /// </summary>
        public Query()
        {
            this.Title = string.Empty;
            this.Year = string.Empty;
            this.Results = new BindingList<QueryResult>();
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title value</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>The year value</value>
        public string Year { get; set; }
        
        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>The results.</value>
        public BindingList<QueryResult> Results { get; set; }

        public string ImdbId { get; set; }
    }
}
