// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="Application.cs">
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
//   Application specific settings.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Settings.ConstSettings
{
    #region Required Namespaces

    using System;
    using System.IO;
    using System.Reflection;

    #endregion

    /// <summary>
    ///   Application specific settings.
    /// </summary>
    public static class Application
    {
        #region Constants

        /// <summary>
        ///   Type (AKA Donor)
        /// </summary>
        public const string ApplicationMessage = "Alpha";

        /// <summary>
        ///   The string "YANFOE"
        /// </summary>
        public const string ApplicationName = "YANFOE";

        /// <summary>
        ///   The current version of YANFOE.
        /// </summary>
        public const string ApplicationVersion = "2.0 Alpha 3";

        /// <summary>
        /// The donate url.
        /// </summary>
        public const string DonateUrl =
            @"https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=MV2TUT8RB3QDW&lc=US&item_name=YANFOE%20Development&currency_code=USD&bn=PP%2dDonationsBF%3abtn_donate_LG%2egif%3aNonHosted";

        /// <summary>
        ///   The Api Key for MovieMeter.nl
        /// </summary>
        public const string MovieMeterApi = "8ga8k24yegyemdz7a148d2cy0xhrbxqa";

        /// <summary>
        ///   The API key for TheMovieDB.
        /// </summary>
        public const string TheMovieDBApi = "8b86f88ebee1b9257e58357076e1816f";

        /// <summary>
        ///   The API Key for TVDb.
        /// </summary>
        public const string TvdbApi = "71F9C54F38B71B4F";

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="Application"/> class.
        /// </summary>
        static Application()
        {
            ApplicationBuild = Assembly.GetExecutingAssembly().GetName().Version.Revision.ToString();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   The current version of YANFOE.
        /// </summary>
        public static string ApplicationBuild { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Retrieves the linker timestamp By Joe Spivey
        /// </summary>
        /// <returns>
        /// The <see cref="DateTime"/>.
        /// </returns>
        public static DateTime RetrieveLinkerTimestamp()
        {
            string filePath = Assembly.GetCallingAssembly().Location;
            const int c_PeHeaderOffset = 60;
            const int c_LinkerTimestampOffset = 8;
            byte[] b = new byte[2048];
            Stream s = null;

            try
            {
                s = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                s.Read(b, 0, 2048);
            }
            finally
            {
                if (s != null)
                {
                    s.Close();
                }
            }

            int i = BitConverter.ToInt32(b, c_PeHeaderOffset);
            int secondsSince1970 = BitConverter.ToInt32(b, i + c_LinkerTimestampOffset);
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0);
            dt = dt.AddSeconds(secondsSince1970);
            dt = dt.AddHours(TimeZone.CurrentTimeZone.GetUtcOffset(dt).Hours);
            return dt;
        }

        #endregion
    }
}