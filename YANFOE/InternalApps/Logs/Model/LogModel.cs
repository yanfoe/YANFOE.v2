// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogModel.cs" company="The YANFOE Project">
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

namespace YANFOE.InternalApps.Logs.Model
{
    using System;

    using BitFactory.Logging;

    using YANFOE.Tools.Models;

    /// <summary>
    /// The log model.
    /// </summary>
    public class LogModel : ModelBase
    {
        #region Constants and Fields

        /// <summary>
        /// The header.
        /// </summary>
        private string header;

        /// <summary>
        /// The message.
        /// </summary>
        private string message;

        /// <summary>
        /// The time stamp.
        /// </summary>
        private string timeStamp;

        /// <summary>
        /// The log severity type.
        /// </summary>
        private LogSeverity type;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets Header.
        /// </summary>
        public string Header
        {
            get
            {
                return this.header;
            }

            set
            {
                this.header = value;
                this.OnPropertyChanged("Header");
            }
        }

        /// <summary>
        /// Gets or sets Message.
        /// </summary>
        public string Message
        {
            get
            {
                return this.message;
            }

            set
            {
                this.message = value;
                this.OnPropertyChanged("Message");
            }
        }

        /// <summary>
        /// Gets TimeStamp.
        /// </summary>
        public string TimeStamp
        {
            get
            {
                if (string.IsNullOrEmpty(this.timeStamp))
                {
                    this.timeStamp = DateTime.Now.ToLongTimeString();
                }

                return this.timeStamp;
            }
        }

        /// <summary>
        /// Gets or sets Type.
        /// </summary>
        public LogSeverity Type
        {
            get
            {
                return this.type;
            }

            set
            {
                this.type = value;
                this.OnPropertyChanged("Type");
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Create a log entry
        /// </summary>
        /// <param name="type">The log type</param>
        /// <param name="header">The log header.</param>
        /// <param name="message">The log message.</param>
        /// <returns>The LogModel</returns>
        public static LogModel Create(LogSeverity type, string header, string message)
        {
            var log = new LogModel { Type = type, Header = header, Message = message };

            return log;
        }

        #endregion
    }
}