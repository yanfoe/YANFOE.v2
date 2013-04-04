// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DiskUtils.cs" company="The YANFOE Project">
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

namespace YANFOE.Tools.WrappedTools
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    using DiscUtils;
    using DiscUtils.Iso9660;
    using DiscUtils.Udf;

    /// <summary>
    /// The disk utils.
    /// </summary>
    public class DiskUtils
    {
        #region Constants and Fields

        /// <summary>
        /// The files.
        /// </summary>
        public List<DiscUtilFile> Files = new List<DiscUtilFile>();

        /// <summary>
        /// The iso path.
        /// </summary>
        public string IsoPath = string.Empty;

        #endregion

        #region Public Methods

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// The file count.
        /// </summary>
        /// <param name="regex">
        /// The regex.
        /// </param>
        /// <returns>
        /// The file count.
        /// </returns>
        public int FileCount(string regex)
        {
            int count = 0;

            foreach (DiscUtilFile file in this.Files)
            {
                Match match = Regex.Match(file.FileName, regex, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// The find biggest.
        /// </summary>
        /// <param name="regex">
        /// The regex.
        /// </param>
        /// <returns>
        /// The find biggest.
        /// </returns>
        public string FindBiggest(string regex)
        {
            IEnumerable<string> value = from n in this.Files
                                        where Regex.Match(n.FileName, regex, RegexOptions.IgnoreCase).Success
                                        orderby n.FileSize descending
                                        select n.FileName;

            return value.First();
        }

        /// <summary>
        /// The find file.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <returns>
        /// The find file.
        /// </returns>
        public int FindFile(string text)
        {
            int value = -1;

            for (int index = 0; index < this.Files.Count; index++)
            {
                DiscUtilFile file = this.Files[index];
                if (file.FileName.ToLower().Contains(text.ToLower()))
                {
                    value = index;
                    break;
                }
            }

            return value;
        }

        /// <summary>
        /// The get files.
        /// </summary>
        public void GetFiles()
        {
            using (var fileStream = new FileStream(this.IsoPath, FileMode.Open))
            {
                DiscFileSystem fs;

                if (UdfReader.Detect(fileStream))
                {
                    fs = new UdfReader(fileStream);
                }
                else
                {
                    fs = new CDReader(fileStream, true);
                }

                // DiscUtils.Iso9660.CDReader reader = new CDReader(fs, true);
                var returnFiles = new List<string>();
                this.Files.AddRange(RecursiveFolders(fs, fs.Root.FullName));
            }
        }

        /// <summary>
        /// The save file.
        /// </summary>
        /// <param name="FindText">
        /// The find text.
        /// </param>
        /// <param name="output">
        /// The output.
        /// </param>
        /// <returns>
        /// The save file.
        /// </returns>
        public int SaveFile(string FindText, string output)
        {
            int value = this.FindFile(FindText);

            if (value != -1)
            {
                SaveFile(value, output);
            }

            return value;
        }

        /// <summary>
        /// The save file.
        /// </summary>
        /// <param name="fileID">
        /// The file id.
        /// </param>
        /// <param name="output">
        /// The output.
        /// </param>
        public void SaveFile(int fileID, string output)
        {
            using (var fileStream = new FileStream(this.IsoPath, FileMode.Open))
            {
                var reader = new CDReader(fileStream, true);
                using (var outFile = new FileStream(output, FileMode.Create, FileAccess.ReadWrite))
                {
                    using (Stream inFile = reader.OpenFile(this.Files[fileID].FileName, FileMode.Open))
                    {
                        PumpStreams(inFile, outFile);
                    }
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The pump streams.
        /// </summary>
        /// <param name="inStream">
        /// The in stream.
        /// </param>
        /// <param name="outStream">
        /// The out stream.
        /// </param>
        private static void PumpStreams(Stream inStream, Stream outStream)
        {
            var buffer = new byte[4096];
            int bytesRead = inStream.Read(buffer, 0, 4096);
            while (bytesRead != 0)
            {
                outStream.Write(buffer, 0, bytesRead);
                bytesRead = inStream.Read(buffer, 0, 4096);
            }
        }

        /// <summary>
        /// The recursive files.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// </returns>
        private static List<DiscUtilFile> RecursiveFiles(DiscDirectoryInfo path)
        {
            var returnFiles = new List<DiscUtilFile>();

            foreach (DiscFileInfo file in path.GetFiles())
            {
                var newFile = new DiscUtilFile();
                newFile.FileName = file.FullName;
                newFile.FileSize = file.Length;
                returnFiles.Add(newFile);
            }

            return returnFiles;
        }

        /// <summary>
        /// The recursive folders.
        /// </summary>
        /// <param name="reader">
        /// The reader.
        /// </param>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// </returns>
        private static List<DiscUtilFile> RecursiveFolders(DiscFileSystem reader, string path)
        {
            var returnFolders = new List<DiscUtilFile>();

            DiscDirectoryInfo[] rootFolders = reader.Root.GetDirectories("*", SearchOption.AllDirectories);

            returnFolders.AddRange(RecursiveFiles(reader.Root));

            foreach (DiscDirectoryInfo folder in rootFolders)
            {
                returnFolders.AddRange(RecursiveFiles(folder));
            }

            return returnFolders;
        }

        #endregion
    }

    /// <summary>
    /// The disc util file.
    /// </summary>
    public class DiscUtilFile
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscUtilFile"/> class.
        /// </summary>
        public DiscUtilFile()
        {
            this.FileName = string.Empty;
            this.FileSize = 0;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets FileName.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets FileSize.
        /// </summary>
        public long FileSize { get; set; }

        #endregion
    }

    /// <summary>
    /// The tsg roup.
    /// </summary>
    public class TSGRoup
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TSGRoup"/> class.
        /// </summary>
        public TSGRoup()
        {
            this.FileName = string.Empty;
            this.TotalFileSize = 0;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets FileName.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets TotalFileSize.
        /// </summary>
        public long TotalFileSize { get; set; }

        #endregion
    }
}