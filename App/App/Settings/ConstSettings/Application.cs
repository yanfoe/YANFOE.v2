// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Application.cs" company="The YANFOE Project">
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

namespace YANFOE.Settings.ConstSettings
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Application specific settings.
    /// </summary>
    public static class Application
    {
        
        static Application()
        {
            ApplicationBuild = String.Format("{0:yyMMddhh}", RetrieveLinkerTimestamp());
        }

        #region ApplicationNamingConditions
        /// <summary>
        /// The string "YANFOE"
        /// </summary>
        public const string ApplicationName = "YANFOE";

        /// <summary>
        /// The current version of YANFOE.
        /// </summary>
        public const string ApplicationVersion = "2.0 Alpha 3";

        /// <summary>
        /// The current version of YANFOE.
        /// </summary>
        public static string ApplicationBuild { get; private set; }

        /// <summary>
        /// Type (AKA Donor)
        /// </summary>
        public const string ApplicationMessage = "Alpha";

        public const string DonateUrl = @"https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=3HFMBVFJE8XGA";

        #endregion

        #region ApiKeys
        /// <summary>
        /// The API key for TheMovieDB.
        /// </summary>
        public const string TheMovieDBApi = "8b86f88ebee1b9257e58357076e1816f";

        /// <summary>
        /// The API Key for TVDb.
        /// </summary>
        public const string TvdbApi = "71F9C54F38B71B4F";

        /// <summary>
        /// The Api Key for MovieMeter.nl
        /// </summary>
        public const string MovieMeterApi = "8ga8k24yegyemdz7a148d2cy0xhrbxqa";
        #endregion

        /// <summary>
        /// Retrieves the linker timestamp By Joe Spivey
        /// </summary>
        /// <returns></returns>
        public static DateTime RetrieveLinkerTimestamp()
        {
            string filePath = System.Reflection.Assembly.GetCallingAssembly().Location;
            const int c_PeHeaderOffset = 60;
            const int c_LinkerTimestampOffset = 8;
            byte[] b = new byte[2048];
            System.IO.Stream s = null;

            try
            {
                s = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                s.Read(b, 0, 2048);
            }
            finally
            {
                if (s != null)
                {
                    s.Close();
                }
            }

            int i = System.BitConverter.ToInt32(b, c_PeHeaderOffset);
            int secondsSince1970 = System.BitConverter.ToInt32(b, i + c_LinkerTimestampOffset);
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0);
            dt = dt.AddSeconds(secondsSince1970);
            dt = dt.AddHours(TimeZone.CurrentTimeZone.GetUtcOffset(dt).Hours);
            return dt;
        }
    }
}
