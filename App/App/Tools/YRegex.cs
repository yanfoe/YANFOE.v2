// --------------------------------------------------------------------------------------------------------------------
// <copyright file="YRegex.cs" company="The YANFOE Project">
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

namespace YANFOE.Tools
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text.RegularExpressions;

    using BitFactory.Logging;

    using YANFOE.InternalApps.Logs;
    using YANFOE.InternalApps.Logs.Enums;
    using YANFOE.Tools.Extentions;
    using YANFOE.Tools.Models;

    /// <summary>
    /// </summary>
    public static class YRegex
    {
        /// <summary>
        /// Gets the value betweeen too strings
        /// </summary>
        /// <param name="text">The string for which the search will take place in.</param>
        /// <param name="begin">The start string</param>
        /// <param name="end">The stop string</param>
        /// <returns>Value between to input strings</returns>
        public static string GetValueBetween(string text, string begin, string end)
        {
            var titleStart = text.IndexOf(begin);
            if (titleStart == -1)
            {
                return string.Empty;
            }

            int titleStop;

            if (string.IsNullOrEmpty(end))
            {
                titleStop = text.Length - titleStart;
            }
            else
            {
                titleStop = text.IndexOf(end, titleStart);
            }

            if (titleStop == -1)
            {
                return string.Empty;
            }

            return text.Substring(titleStart + begin.Length, titleStop - titleStart - begin.Length);
        }

        /// <summary>
        /// Gets the value between two string, along with the ability to also including the outer sides of a string.
        /// </summary>
        /// <param name="begin">The start string</param>
        /// <param name="end">The stop string</param>
        /// <param name="text">The string for which the search will take place in.</param>
        /// <param name="includeBeginning">Include the begin value in the returned string</param>
        /// <param name="includeEnd">Include the end value in the returned string</param>
        /// <returns>
        /// Returns value between string
        /// </returns>
        public static string[] GetValueBetween(string begin, string end, string text, bool includeBeginning, bool includeEnd)
        {
            string[] result = { string.Empty, string.Empty };
            var indexOfBegin = text.IndexOf(begin);

            if (indexOfBegin != -1)
            {
                // include the Begin string if desired
                if (includeBeginning)
                {
                    indexOfBegin -= begin.Length;
                }

                text = text.Substring(indexOfBegin + begin.Length);
                var indexOfEnd = text.IndexOf(end);

                if (indexOfEnd != -1)
                {
                    // include the End string if desired
                    if (includeEnd)
                    {
                        indexOfEnd += end.Length;
                    }

                    result[0] = text.Substring(0, indexOfEnd);

                    // advance beyond this segment
                    if (indexOfEnd + end.Length < text.Length)
                    {
                        result[1] = text.Substring(indexOfEnd + end.Length);
                    }
                }
            }
            else
            {
                // stay where we are
                result[1] = text;
            }

            return result;
        }

        public static string GetValueToEnd (string source, string from)
        {
            source = source.Substring(source.LastIndexOf(from) + 1);
            return source;
        }


        /// <summary>
        /// Matches a regex expression against a string returning a sting value.
        /// </summary>
        /// <param name="regex">The regex expression</param>
        /// <param name="value">The value to run the regex against</param>
        /// <param name="group">The group to return</param>
        /// <param name="clean">if set to <c>true</c> the return value is cleaned.</param>
        /// <returns></returns>
        public static string Match(string regex, string value, string group, bool clean = false)
        {
            const string logCatagory = "Matchers > Matchers > Match (string)";

            try
            {
                var match = Regex.Match(value, regex, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    if (clean)
                    {
                        return Clean.Text.FullClean(match.Groups[group].Value);
                    }

                    return match.Groups[group].Value;
                }
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, LoggerName.GeneralLog, logCatagory, ex.Message);
            }

            return string.Empty;
        }

        public static DateTime MatchToDateTime(string regex, string html, string day, string month, string year)
        {
            var match = Regex.Match(html, regex);

            var yearVal= match.Groups[year].Value;
            var monthVal = match.Groups[month].Value;
            var dayVal = match.Groups[day].Value;

            var returnDate = new DateTime(1700, 1, 1);
            DateTime.TryParse(string.Format("{0}-{1}-{2}", yearVal, monthVal, dayVal), out returnDate);

            return returnDate;
        }

        public static DateTime MatchToDateTime(string regex, string html, string day, string month, string year, Dictionary<string, int> monthString)
        {
            var match = Regex.Match(html, regex);

            var yearVal = match.Groups[year].Value;
            var monthVal = match.Groups[month].Value;
            var dayVal = match.Groups[day].Value;

            DateTime returnDate;

            DateTime.TryParse(string.Format("{0}-{1}-{2}", yearVal, monthString[monthVal], dayVal), out returnDate);

            return returnDate;
        }

        public static BindingList<PersonModel> MatchToPersonList(string regex, string value, string group, char delimiter, bool clean = false)
        {
            const string logCatagory = "Matchers > Matchers > Match (Del > PersonList)";

            try
            {
                var match = Match(regex, value, group , clean);
                return match.ToPersonList(delimiter);
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, LoggerName.GeneralLog, logCatagory, ex.Message);
            }

            return null;
        }


        /// <summary>
        /// Matches a regex expression against a string returning a int value.
        /// </summary>
        /// <param name="regex">The regex expression</param>
        /// <param name="value">The value to run the regex against</param>
        /// <param name="group">The group to return</param>
        /// <param name="clean">if set to <c>true</c> the return value is cleaned.</param>
        /// <returns></returns>
        public static string Match(string regex, string value, int group, bool clean = false)
        {
            const string logCatagory = "Matchers > Matchers > Match (int)";

            try
            {

                var match = Regex.Match(value, regex, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    if (clean)
                    {
                        return Clean.Text.FullClean(match.Groups[group].Value);
                    }

                    return match.Groups[group].Value;
                }

            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, LoggerName.GeneralLog, logCatagory, ex.Message);
            }

            return string.Empty;
        }

        public static BindingList<string> MatchFilteredByList(string regex, string value, string group, IEnumerable<string> filters, bool clean = false)
        {
            var match = Match(regex, value, group, clean);
            return (from f in filters.AsParallel().AsOrdered() where match.Contains(f) select match).ToBindingList();
        }

        public static BindingList<string> MatchDelimitedToList(string regex, string html, string group, char delimiter, bool clean = false)
        {
            const string logCatagory = "Matchers > Matchers > MatchDelimitedToList (List<s>)";

            try
            {
                var output = Match(regex, html, group, clean);
                var split = output.Split(delimiter);

                return split.ToBindingStringList().ToBindingList();
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, LoggerName.GeneralLog, logCatagory, ex.Message);
            }

            return null;
        }

        /// <summary>
        /// Matches a regex expression against a string returning a a pair collecting in a List collection
        /// </summary>
        /// <param name="regex">The regex.</param>
        /// <param name="html">The HTML.</param>
        /// <param name="group">The group.</param>
        /// <param name="clean">if set to <c>true</c> [clean].</param>
        /// <returns></returns>
        public static BindingList<string> MatchesToList(string regex, string html, string group, bool clean = false)
        {
            const string logCatagory = "Matchers > Matchers > Matches (List<s>)";

            try
            {
                var matchCollection = Regex.Matches(html, regex, RegexOptions.IgnoreCase);
                var matches = new BindingList<string>();
                foreach (Match match in matchCollection)
                {
                    matches.Add(clean ? Clean.Text.FullClean(match.Groups[group].Value) : match.Groups[group].Value);
                }

                return matches;
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, LoggerName.GeneralLog, logCatagory, ex.Message);
            }

            return null;
        }

        public static BindingList<string> MatchesToListFilterByList(string regex, string html, string group, BindingList<string> filter, bool clean = false)
        {
            var matches = MatchesToList(regex, html, group, clean);
            return matches.Where(filter.Contains).ToBindingList();
        }


        public static BindingList<PersonModel> MatchesToPersonList(string regex, string html, string group, bool clean = false)
        {
            var matches = MatchesToList(regex, html, group, clean);
            return matches.Select(m => new PersonModel(m.Trim())).ToBindingList();
        }

        /// <summary>
        /// Returns a person list via a regex match
        /// </summary>
        /// <param name="regex">The regex to apply.</param>
        /// <param name="html">The HTML to apply the regex on.</param>
        /// <param name="name">The name group</param>
        /// <param name="role">The role group</param>
        /// <param name="imageurl">The imageurl group</param>
        /// <returns></returns>
        public static BindingList<PersonModel> MatchesToPersonList(
            string regex, 
            string matchHhtml, 
            string matchName, 
            string role = null, 
            string imageurl = null)
        {

            var personList = new BindingList<PersonModel>();
            var matches = Regex.Matches(matchHhtml, regex);

            foreach (Match m in matches)
            {
                var p = new PersonModel(m.Groups[matchName].Value.Clean());

                if (role != null && !string.IsNullOrEmpty(m.Groups[role].Value))
                {
                    p.Role = m.Groups[role].Value.Clean();
                }

                if (imageurl != null && !string.IsNullOrEmpty(m.Groups[imageurl].Value))
                {
                    p.ImageUrl = m.Groups[imageurl].Value;
                }

                p.Name = Clean.Text.ValidizeResult(p.Name);
                p.Role = Clean.Text.ValidizeResult(p.Role);

                personList.Add(p);
            }

            return personList;
        }

        /// <summary>
        /// Matches a regex expression against a string returning a delimited string
        /// </summary>
        /// <param name="regex">The regex expression</param>
        /// <param name="value">The value to run the regex against</param>
        /// <param name="group">The group to return</param>
        /// <param name="clean">if set to <c>true</c> the return value is cleaned.</param>
        /// <param name="returnDelimiter">The return delimiter.</param>
        /// <returns></returns>
        public static string Matches(string regex, string value, string group, bool clean = false, string returnDelimiter = ",")
        {
            const string logCatagory = "Matchers > Matchers > Matches (Delimiter)";

            try
            {
                var returnValue = MatchesToList(regex, value, group, clean);
                var collection = returnValue.Aggregate(string.Empty, (current, v) => current + (v + returnDelimiter));
                return collection.Length > 3
                           ? collection.Substring(0, collection.Length - (returnDelimiter.Length))
                           : string.Empty;
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, LoggerName.GeneralLog, logCatagory, ex.Message);
            }

            return string.Empty;
        }

        /// <summary>
        /// Matches a regex expression against a string returning a a pair collecting in a Dictionary collection
        /// </summary>
        /// <param name="regex">The regex.</param>
        /// <param name="value">The value to run the regex against</param>
        /// <param name="groupValue">The group value.</param>
        /// <param name="groupKey">The group key.</param>
        /// <param name="clean">if set to <c>true</c> [clean].</param>
        /// <returns></returns>
        public static Dictionary<string, string> Matches(string regex, string value, string groupValue, string groupKey, bool clean)
        {
            const string logCatagory = "Matchers > Matchers > Matches (Dict<s,s>)";

            var matches = new Dictionary<string, string>();

            try
            {
                var matchCollection = Regex.Matches(value, regex, RegexOptions.IgnoreCase);

                foreach (Match match in matchCollection)
                {
                    if (clean)
                    {
                        matches.Add(Clean.Text.FullClean(match.Groups[groupValue].Value),
                                    Clean.Text.FullClean(match.Groups[groupKey].Value));
                    }
                    else
                    {
                        matches.Add(match.Groups[groupValue].Value, match.Groups[groupKey].Value);
                    }

                    return matches;
                }
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, LoggerName.GeneralLog, logCatagory, ex.Message);
            }

            return matches;
        }
    }
}
