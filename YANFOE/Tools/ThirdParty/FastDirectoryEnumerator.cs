// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="FastDirectoryEnumerator.cs">
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
//   Contains information about a file returned by the
//   <see cref="FastDirectoryEnumerator" /> class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Tools.ThirdParty
{
    #region Required Namespaces

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.ConstrainedExecution;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Security.Permissions;

    using Microsoft.Win32.SafeHandles;

    #endregion

    /// <summary>
    ///   Contains information about a file returned by the 
    ///   <see cref="FastDirectoryEnumerator" /> class.
    /// </summary>
    [Serializable]
    public class FileData
    {
        #region Fields

        /// <summary>
        ///   Attributes of the file.
        /// </summary>
        public readonly FileAttributes Attributes;

        /// <summary>
        ///   File creation time in UTC
        /// </summary>
        public readonly DateTime CreationTimeUtc;

        /// <summary>
        ///   File last access time in UTC
        /// </summary>
        public readonly DateTime LastAccessTimeUtc;

        /// <summary>
        ///   File last write time in UTC
        /// </summary>
        public readonly DateTime LastWriteTimeUtc;

        /// <summary>
        ///   Name of the file
        /// </summary>
        public readonly string Name;

        /// <summary>
        ///   Full path to the file.
        /// </summary>
        public readonly string Path;

        /// <summary>
        ///   Size of the file in bytes
        /// </summary>
        public readonly long Size;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FileData"/> class.
        /// </summary>
        /// <param name="dir">
        /// The directory that the file is stored at 
        /// </param>
        /// <param name="findData">
        /// WIN32_FIND_DATA structure that this object wraps. 
        /// </param>
        internal FileData(string dir, WIN32_FIND_DATA findData)
        {
            this.Attributes = findData.dwFileAttributes;

            this.CreationTimeUtc = ConvertDateTime(
                findData.ftCreationTime_dwHighDateTime, findData.ftCreationTime_dwLowDateTime);

            this.LastAccessTimeUtc = ConvertDateTime(
                findData.ftLastAccessTime_dwHighDateTime, findData.ftLastAccessTime_dwLowDateTime);

            this.LastWriteTimeUtc = ConvertDateTime(
                findData.ftLastWriteTime_dwHighDateTime, findData.ftLastWriteTime_dwLowDateTime);

            this.Size = CombineHighLowInts(findData.nFileSizeHigh, findData.nFileSizeLow);

            this.Name = findData.cFileName;
            this.Path = System.IO.Path.Combine(dir, findData.cFileName);
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets CreationTime.
        /// </summary>
        public DateTime CreationTime
        {
            get
            {
                return this.CreationTimeUtc.ToLocalTime();
            }
        }

        /// <summary>
        ///   Gets the last access time in local time.
        /// </summary>
        public DateTime LastAccesTime
        {
            get
            {
                return this.LastAccessTimeUtc.ToLocalTime();
            }
        }

        /// <summary>
        ///   Gets the last access time in local time.
        /// </summary>
        public DateTime LastWriteTime
        {
            get
            {
                return this.LastWriteTimeUtc.ToLocalTime();
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///   Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
        /// </summary>
        /// <returns> A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" /> . </returns>
        public override string ToString()
        {
            return this.Name;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The combine high low ints.
        /// </summary>
        /// <param name="high">
        /// The high. 
        /// </param>
        /// <param name="low">
        /// The low. 
        /// </param>
        /// <returns>
        /// The combine high low ints. 
        /// </returns>
        private static long CombineHighLowInts(uint high, uint low)
        {
            return (((long)high) << 0x20) | low;
        }

        /// <summary>
        /// The convert date time.
        /// </summary>
        /// <param name="high">
        /// The high. 
        /// </param>
        /// <param name="low">
        /// The low. 
        /// </param>
        /// <returns>
        /// The <see cref="DateTime"/>.
        /// </returns>
        private static DateTime ConvertDateTime(uint high, uint low)
        {
            long fileTime = CombineHighLowInts(high, low);
            if (fileTime > DateTime.Now.Ticks || fileTime < DateTime.MinValue.Ticks)
            {
                return DateTime.UtcNow;
            }
            else
            {
                return DateTime.FromFileTimeUtc(fileTime);
            }
        }

        #endregion
    }

    /// <summary>
    ///   Contains information about the file that is found 
    ///   by the FindFirstFile or FindNextFile functions.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    [BestFitMapping(false)]
    internal class WIN32_FIND_DATA
    {
        /// <summary>
        ///   The dw file attributes.
        /// </summary>
        public FileAttributes dwFileAttributes;

        /// <summary>
        ///   The ft creation time_dw low date time.
        /// </summary>
        public uint ftCreationTime_dwLowDateTime;

        /// <summary>
        ///   The ft creation time_dw high date time.
        /// </summary>
        public uint ftCreationTime_dwHighDateTime;

        /// <summary>
        ///   The ft last access time_dw low date time.
        /// </summary>
        public uint ftLastAccessTime_dwLowDateTime;

        /// <summary>
        ///   The ft last access time_dw high date time.
        /// </summary>
        public uint ftLastAccessTime_dwHighDateTime;

        /// <summary>
        ///   The ft last write time_dw low date time.
        /// </summary>
        public uint ftLastWriteTime_dwLowDateTime;

        /// <summary>
        ///   The ft last write time_dw high date time.
        /// </summary>
        public uint ftLastWriteTime_dwHighDateTime;

        /// <summary>
        ///   The n file size high.
        /// </summary>
        public uint nFileSizeHigh;

        /// <summary>
        ///   The n file size low.
        /// </summary>
        public uint nFileSizeLow;

        /// <summary>
        ///   The dw reserved 0.
        /// </summary>
        public int dwReserved0;

        /// <summary>
        ///   The dw reserved 1.
        /// </summary>
        public int dwReserved1;

        /// <summary>
        ///   The c file name.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string cFileName;

        /// <summary>
        ///   The c alternate file name.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
        public string cAlternateFileName;

        /// <summary>
        ///   Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
        /// </summary>
        /// <returns> A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" /> . </returns>
        public override string ToString()
        {
            return "File name=" + this.cFileName;
        }
    }

    /// <summary>
    ///   A fast enumerator of files in a directory.  Use this if you need to get attributes for 
    ///   all files in a directory.
    /// </summary>
    /// <remarks>
    ///   This enumerator is substantially faster than using <see cref="FastDirectoryEnumerator.EnumarateFilesPathList(string)" />
    ///   and then creating a new FileInfo object for each path.  Use this version when you 
    ///   will need to look at the attibutes of each file returned (for example, you need
    ///   to check each file in a directory to see if it was modified after a specific date).
    /// </remarks>
    public static class FastDirectoryEnumerator2
    {
        #region Public Methods and Operators

        /// <summary>
        /// The enumarate files path list.
        /// </summary>
        /// <param name="path">
        /// The path. 
        /// </param>
        /// <param name="searchPattern">
        /// The search pattern. 
        /// </param>
        /// <param name="searchOption">
        /// The search option. 
        /// </param>
        /// <returns>
        /// The <see cref="string[]"/>.
        /// </returns>
        public static string[] EnumarateFilesPathList(
            string path, string searchPattern = null, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            if (searchPattern == null)
            {
                searchPattern = "*.*";
            }

            IEnumerable<FileData> fileList = EnumerateFiles(path, searchPattern, searchOption);

            return (from f in fileList select f.Path).ToArray();
        }

        /// <summary>
        /// Gets <see cref="FileData"/> for all the files in a directory.
        /// </summary>
        /// <param name="path">
        /// The path to search. 
        /// </param>
        /// <returns>
        /// An object that implements <see cref="IEnumerable{FileData}"/> and allows you to enumerate the files in the given directory. 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/>
        ///   is a null reference (Nothing in VB)
        /// </exception>
        public static IEnumerable<FileData> EnumerateFiles(string path)
        {
            return EnumerateFiles(path, "*");
        }

        /// <summary>
        /// Gets <see cref="FileData"/> for all the files in a directory that match a 
        ///   specific filter.
        /// </summary>
        /// <param name="path">
        /// The path to search. 
        /// </param>
        /// <param name="searchPattern">
        /// The search string to match against files in the path. 
        /// </param>
        /// <returns>
        /// An object that implements <see cref="IEnumerable{FileData}"/> and allows you to enumerate the files in the given directory. 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/>
        ///   is a null reference (Nothing in VB)
        /// </exception>
        public static IEnumerable<FileData> EnumerateFiles(string path, string searchPattern)
        {
            return EnumerateFiles(path, searchPattern, SearchOption.TopDirectoryOnly);
        }

        /// <summary>
        /// Gets <see cref="FileData"/> for all the files in a directory that 
        ///   match a specific filter, optionally including all sub directories.
        /// </summary>
        /// <param name="path">
        /// The path to search. 
        /// </param>
        /// <param name="searchPattern">
        /// The search string to match against files in the path. 
        /// </param>
        /// <param name="searchOption">
        /// One of the SearchOption values that specifies whether the search operation should include all subdirectories or only the current directory. 
        /// </param>
        /// <returns>
        /// An object that implements <see cref="IEnumerable{FileData}"/> and allows you to enumerate the files in the given directory. 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/>
        ///   is a null reference (Nothing in VB)
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="searchOption"/>
        ///   is not one of the valid values of the
        ///   <see cref="System.IO.SearchOption"/>
        ///   enumeration.
        /// </exception>
        public static IEnumerable<FileData> EnumerateFiles(string path, string searchPattern, SearchOption searchOption)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            if (searchPattern == null)
            {
                throw new ArgumentNullException("searchPattern");
            }

            if ((searchOption != SearchOption.TopDirectoryOnly) && (searchOption != SearchOption.AllDirectories))
            {
                throw new ArgumentOutOfRangeException("searchOption");
            }

            string fullPath = Path.GetFullPath(path);

            return new FileEnumerable(fullPath, searchPattern, searchOption);
        }

        /// <summary>
        /// Gets <see cref="FileData"/> for all the files in a directory that match a 
        ///   specific filter.
        /// </summary>
        /// <param name="path">
        /// The path to search. 
        /// </param>
        /// <param name="searchPattern">
        /// The search string to match against files in the path. 
        /// </param>
        /// <param name="searchOption">
        /// The search Option. 
        /// </param>
        /// <returns>
        /// An object that implements <see cref="IEnumerable{FileData}"/> and allows you to enumerate the files in the given directory. 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/>
        ///   is a null reference (Nothing in VB)
        /// </exception>
        public static FileData[] GetFiles(string path, string searchPattern, SearchOption searchOption)
        {
            IEnumerable<FileData> e = EnumerateFiles(path, searchPattern, searchOption);
            var list = new List<FileData>(e);

            var retval = new FileData[list.Count];
            list.CopyTo(retval);

            return retval;
        }

        #endregion

        /// <summary>
        ///   Provides the implementation of the 
        ///   <see cref="T:System.Collections.Generic.IEnumerable`1" /> interface
        /// </summary>
        private class FileEnumerable : IEnumerable<FileData>
        {
            #region Fields

            /// <summary>
            ///   The m_filter.
            /// </summary>
            private readonly string m_filter;

            /// <summary>
            ///   The m_path.
            /// </summary>
            private readonly string m_path;

            /// <summary>
            ///   The m_search option.
            /// </summary>
            private readonly SearchOption m_searchOption;

            #endregion

            #region Constructors and Destructors

            /// <summary>
            /// Initializes a new instance of the <see cref="FileEnumerable"/> class.
            /// </summary>
            /// <param name="path">
            /// The path to search. 
            /// </param>
            /// <param name="filter">
            /// The search string to match against files in the path. 
            /// </param>
            /// <param name="searchOption">
            /// One of the SearchOption values that specifies whether the search operation should include all subdirectories or only the current directory. 
            /// </param>
            public FileEnumerable(string path, string filter, SearchOption searchOption)
            {
                this.m_path = path;
                this.m_filter = filter;
                this.m_searchOption = searchOption;
            }

            #endregion

            #region Public Methods and Operators

            /// <summary>
            ///   Returns an enumerator that iterates through the collection.
            /// </summary>
            /// <returns> A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection. </returns>
            public IEnumerator<FileData> GetEnumerator()
            {
                return new FileEnumerator(this.m_path, this.m_filter, this.m_searchOption);
            }

            #endregion

            #region Explicit Interface Methods

            /// <summary>
            ///   Returns an enumerator that iterates through a collection.
            /// </summary>
            /// <returns> An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection. </returns>
            IEnumerator IEnumerable.GetEnumerator()
            {
                return new FileEnumerator(this.m_path, this.m_filter, this.m_searchOption);
            }

            #endregion
        }

        /// <summary>
        ///   Provides the implementation of the 
        ///   <see cref="T:System.Collections.Generic.IEnumerator`1" /> interface
        /// </summary>
        [SuppressUnmanagedCodeSecurity]
        private class FileEnumerator : IEnumerator<FileData>
        {
            #region Fields

            /// <summary>
            ///   The m_context stack.
            /// </summary>
            private readonly Stack<SearchContext> m_contextStack;

            /// <summary>
            ///   The m_filter.
            /// </summary>
            private readonly string m_filter;

            /// <summary>
            ///   The m_search option.
            /// </summary>
            private readonly SearchOption m_searchOption;

            /// <summary>
            ///   The m_win_find_data.
            /// </summary>
            private readonly WIN32_FIND_DATA m_win_find_data = new WIN32_FIND_DATA();

            /// <summary>
            ///   The m_current context.
            /// </summary>
            private SearchContext m_currentContext;

            /// <summary>
            ///   The m_hnd find file.
            /// </summary>
            private SafeFindHandle m_hndFindFile;

            /// <summary>
            ///   The m_path.
            /// </summary>
            private string m_path;

            #endregion

            #region Constructors and Destructors

            /// <summary>
            /// Initializes a new instance of the <see cref="FileEnumerator"/> class.
            /// </summary>
            /// <param name="path">
            /// The path to search. 
            /// </param>
            /// <param name="filter">
            /// The search string to match against files in the path. 
            /// </param>
            /// <param name="searchOption">
            /// One of the SearchOption values that specifies whether the search operation should include all subdirectories or only the current directory. 
            /// </param>
            public FileEnumerator(string path, string filter, SearchOption searchOption)
            {
                this.m_path = path;
                this.m_filter = filter;
                this.m_searchOption = searchOption;
                this.m_currentContext = new SearchContext(path);

                if (this.m_searchOption == SearchOption.AllDirectories)
                {
                    this.m_contextStack = new Stack<SearchContext>();
                }
            }

            #endregion

            #region Public Properties

            /// <summary>
            ///   Gets the element in the collection at the current position of the enumerator.
            /// </summary>
            /// <value> </value>
            /// <returns> The element in the collection at the current position of the enumerator. </returns>
            public FileData Current
            {
                get
                {
                    return new FileData(this.m_path, this.m_win_find_data);
                }
            }

            #endregion

            #region Explicit Interface Properties

            /// <summary>
            ///   Gets the element in the collection at the current position of the enumerator.
            /// </summary>
            /// <value> </value>
            /// <returns> The element in the collection at the current position of the enumerator. </returns>
            object IEnumerator.Current
            {
                get
                {
                    return new FileData(this.m_path, this.m_win_find_data);
                }
            }

            #endregion

            #region Public Methods and Operators

            /// <summary>
            ///   Performs application-defined tasks associated with freeing, releasing, 
            ///   or resetting unmanaged resources.
            /// </summary>
            public void Dispose()
            {
                if (this.m_hndFindFile != null)
                {
                    this.m_hndFindFile.Dispose();
                }
            }

            /// <summary>
            ///   Advances the enumerator to the next element of the collection.
            /// </summary>
            /// <returns> true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection. </returns>
            /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
            public bool MoveNext()
            {
                bool retval = false;

                // If the handle is null, this is first call to MoveNext in the current 
                // directory.  In that case, start a new search.
                if (this.m_currentContext.SubdirectoriesToProcess == null)
                {
                    if (this.m_hndFindFile == null)
                    {
                        new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.m_path).Demand();

                        string searchPath = Path.Combine(this.m_path, this.m_filter);
                        this.m_hndFindFile = FindFirstFile(searchPath, this.m_win_find_data);
                        retval = !this.m_hndFindFile.IsInvalid;
                    }
                    else
                    {
                        // Otherwise, find the next item.
                        retval = FindNextFile(this.m_hndFindFile, this.m_win_find_data);
                    }
                }

                // If the call to FindNextFile or FindFirstFile succeeded...
                if (retval)
                {
                    if ((this.m_win_find_data.dwFileAttributes & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        // Ignore folders for now.   We call MoveNext recursively here to 
                        // move to the next item that FindNextFile will return.
                        return this.MoveNext();
                    }
                }
                else if (this.m_searchOption == SearchOption.AllDirectories)
                {
                    // SearchContext context = new SearchContext(m_hndFindFile, m_path);
                    // m_contextStack.Push(context);
                    // m_path = Path.Combine(m_path, m_win_find_data.cFileName);
                    // m_hndFindFile = null;
                    if (this.m_currentContext.SubdirectoriesToProcess == null)
                    {
                        try
                        {
                            string[] subDirectories = Directory.GetDirectories(this.m_path);
                            this.m_currentContext.SubdirectoriesToProcess = new Stack<string>(subDirectories);
                        }
                        catch (UnauthorizedAccessException)
                        {
                            string[] subDirectories = new string[0];
                            this.m_currentContext.SubdirectoriesToProcess = new Stack<string>(subDirectories);
                        }
                    }

                    if (this.m_currentContext.SubdirectoriesToProcess.Count > 0)
                    {
                        string subDir = this.m_currentContext.SubdirectoriesToProcess.Pop();

                        this.m_contextStack.Push(this.m_currentContext);
                        this.m_path = subDir;
                        this.m_hndFindFile = null;
                        this.m_currentContext = new SearchContext(this.m_path);
                        return this.MoveNext();
                    }

                    // If there are no more files in this directory and we are 
                    // in a sub directory, pop back up to the parent directory and
                    // continue the search from there.
                    if (this.m_contextStack.Count > 0)
                    {
                        this.m_currentContext = this.m_contextStack.Pop();
                        this.m_path = this.m_currentContext.Path;
                        if (this.m_hndFindFile != null)
                        {
                            this.m_hndFindFile.Close();
                            this.m_hndFindFile = null;
                        }

                        return this.MoveNext();
                    }
                }

                return retval;
            }

            /// <summary>
            ///   Sets the enumerator to its initial position, which is before the first element in the collection.
            /// </summary>
            /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
            public void Reset()
            {
                this.m_hndFindFile = null;
            }

            #endregion

            #region Methods

            /// <summary>
            /// The find first file.
            /// </summary>
            /// <param name="fileName">
            /// The file name. 
            /// </param>
            /// <param name="data">
            /// The data. 
            /// </param>
            /// <returns>
            /// The <see cref="SafeFindHandle"/>.
            /// </returns>
            [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            private static extern SafeFindHandle FindFirstFile(string fileName, [In] [Out] WIN32_FIND_DATA data);

            /// <summary>
            /// The find next file.
            /// </summary>
            /// <param name="hndFindFile">
            /// The hnd find file. 
            /// </param>
            /// <param name="lpFindFileData">
            /// The lp find file data. 
            /// </param>
            /// <returns>
            /// The find next file. 
            /// </returns>
            [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            private static extern bool FindNextFile(
                SafeFindHandle hndFindFile, 
                [In] [Out] [MarshalAs(UnmanagedType.LPStruct)] WIN32_FIND_DATA lpFindFileData);

            #endregion

            /// <summary>
            ///   Hold context information about where we current are in the directory search.
            /// </summary>
            private class SearchContext
            {
                #region Fields

                /// <summary>
                ///   The path.
                /// </summary>
                public readonly string Path;

                /// <summary>
                ///   The subdirectories to process.
                /// </summary>
                public Stack<string> SubdirectoriesToProcess;

                #endregion

                #region Constructors and Destructors

                /// <summary>
                /// Initializes a new instance of the <see cref="SearchContext"/> class.
                /// </summary>
                /// <param name="path">
                /// The path. 
                /// </param>
                public SearchContext(string path)
                {
                    this.Path = path;
                }

                #endregion
            }
        }

        /// <summary>
        ///   Wraps a FindFirstFile handle.
        /// </summary>
        private sealed class SafeFindHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            #region Constructors and Destructors

            /// <summary>
            ///   Initializes a new instance of the <see cref="SafeFindHandle" /> class.
            /// </summary>
            [SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
            internal SafeFindHandle()
                : base(true)
            {
            }

            #endregion

            #region Methods

            /// <summary>
            ///   When overridden in a derived class, executes the code required to free the handle.
            /// </summary>
            /// <returns> true if the handle is released successfully; otherwise, in the event of a catastrophic failure, false. In this case, it generates a releaseHandleFailed MDA Managed Debugging Assistant. </returns>
            protected override bool ReleaseHandle()
            {
                return FindClose(base.handle);
            }

            /// <summary>
            /// The find close.
            /// </summary>
            /// <param name="handle">
            /// The handle. 
            /// </param>
            /// <returns>
            /// The find close. 
            /// </returns>
            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            [DllImport("kernel32.dll")]
            private static extern bool FindClose(IntPtr handle);

            #endregion
        }
    }
}