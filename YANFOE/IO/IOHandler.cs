// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOHandler.cs" company="The YANFOE Project">
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

namespace YANFOE.IO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;

    public static class IOHandler
    {
        public static List<IoInterface> ReturnAllHandlers()
        {
            List<IoInterface> instances = (from t in Assembly.GetExecutingAssembly().GetTypes()
                                             where
                                                 t.GetInterfaces().Contains(typeof(IoInterface)) &&
                                                 t.GetConstructor(Type.EmptyTypes) != null
                                             select Activator.CreateInstance(t) as IoInterface)
                                             .ToList();

            var sortedScrapers = new SortedDictionary<string, IoInterface>();

            foreach (var s in instances)
            {
                sortedScrapers.Add(s.IOHandlerName, s);
            }

            instances.Clear();

            instances.AddRange(sortedScrapers.Select(s => s.Value));

            return instances;
        }

        public static BindingList<string> ReturnAllHandlersAsStringList()
        {
            List<IoInterface> scrapers = ReturnAllHandlers();

            var tempSortedList = new SortedList<string, string>();

            foreach (IoInterface scraper in scrapers)
            {
                tempSortedList.Add(scraper.IOHandlerName, null);
            }

            var output = new BindingList<string>();

            foreach (var scraper in tempSortedList)
            {
                output.Add(scraper.Key);
            }

            return output;
        }
    }
}
