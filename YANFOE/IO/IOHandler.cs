// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="IOHandler.cs">
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
// <summary>
//   The io handler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.IO
{
    #region Required Namespaces

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using YANFOE.Tools.UI;

    #endregion

    /// <summary>
    /// The io handler.
    /// </summary>
    public static class IOHandler
    {
        #region Public Methods and Operators

        /// <summary>
        /// The return all handlers.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public static List<IOInterface> ReturnAllHandlers()
        {
            List<IOInterface> instances = (from t in Assembly.GetExecutingAssembly().GetTypes()
                                           where
                                               t.GetInterfaces().Contains(typeof(IOInterface))
                                               && t.GetConstructor(Type.EmptyTypes) != null
                                           select Activator.CreateInstance(t) as IOInterface).ToList();

            var sortedScrapers = new SortedDictionary<string, IOInterface>();

            foreach (var s in instances)
            {
                sortedScrapers.Add(s.IOHandlerName, s);
            }

            instances.Clear();

            instances.AddRange(sortedScrapers.Select(s => s.Value));

            return instances;
        }

        /// <summary>
        /// The return all handlers as string list.
        /// </summary>
        /// <returns>
        /// The <see cref="ThreadedBindingList"/>.
        /// </returns>
        public static ThreadedBindingList<string> ReturnAllHandlersAsStringList()
        {
            List<IOInterface> scrapers = ReturnAllHandlers();

            var tempSortedList = new SortedList<string, string>();

            foreach (IOInterface scraper in scrapers)
            {
                tempSortedList.Add(scraper.IOHandlerName, null);
            }

            var output = new ThreadedBindingList<string>();

            foreach (var scraper in tempSortedList)
            {
                output.Add(scraper.Key);
            }

            return output;
        }

        #endregion
    }
}