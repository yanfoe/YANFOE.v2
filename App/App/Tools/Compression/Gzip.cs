// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Gzip.cs" company="The YANFOE Project">
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

namespace YANFOE.Tools.Compression
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.IO.Compression;
    using System.Text;

    using BitFactory.Logging;

    using YANFOE.InternalApps.Logs;
    using YANFOE.InternalApps.Logs.Enums;

    /// <summary>
    /// Handles compression and decompression of files using Gzip
    /// </summary>
    public static class Gzip
    {
        /// <summary>
        /// Compresses the specified source file name.
        /// </summary>
        /// <param name="sourceFileName">Name of the source file.</param>
        /// <param name="destinationFileName">Name of the destination file.</param>
        public static void Compress(string sourceFileName, string destinationFileName)
        {
            const string LogCatagory = "Gzip Compression";

            try
            {
                using (var sourceStream = new FileStream(sourceFileName, FileMode.Open))
                {
                    using (var destinationStream = new FileStream(destinationFileName, FileMode.Create))
                    {
                        using (var output = new GZipStream(destinationStream, CompressionMode.Compress))
                        {
                            Pump(sourceStream, output);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, LoggerName.GeneralLog, LogCatagory, ex.Message);
            }
        }

        /// <summary>
        /// Compresses the string to file
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="destinationFileName">Name of the destination file.</param>
        public static void CompressString(string value, string destinationFileName)
        {
            const string LogCatagory = "Gzip String Compression";

            try
            {
                using (var sourceStream = new MemoryStream(
                    Encoding.UTF8.GetBytes(value)))
                {
                    using (var destinationStream = new FileStream(destinationFileName, FileMode.Create))
                    {
                        using (var output = new GZipStream(destinationStream, CompressionMode.Compress))
                        {
                            Pump(sourceStream, output);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, LoggerName.GeneralLog, LogCatagory, ex.Message);
            }
        }

        /// <summary>
        /// Decompresses the specified file path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>Decompressed file path</returns>
        public static string Decompress(string filePath)
        {
            const string LogCatagory = "Gzip Decompression";

            try
            {
                var unpackedFilePath = filePath.Replace(".gz", string.Empty);

                string f;

                using (var fileStreamSource = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (var gzipDecompressed = new GZipStream(fileStreamSource, CompressionMode.Decompress, true))
                    {
                        var bufferWrite = new byte[4];
                        fileStreamSource.Position = (int)fileStreamSource.Length - 4;
                        fileStreamSource.Read(bufferWrite, 0, 4);
                        fileStreamSource.Position = 0;

                        var bufferLength = BitConverter.ToInt32(bufferWrite, 0);
                        var buffer = new byte[bufferLength + 100];
                        var readOffset = 0;
                        var totalBytes = 0;

                        while (true)
                        {
                            int bytesRead = gzipDecompressed.Read(buffer, readOffset, 100);

                            if (bytesRead == 0)
                            {
                                break;
                            }

                            readOffset += bytesRead;
                            totalBytes += bytesRead;
                        }

                        var fileStreamDest = new FileStream(unpackedFilePath, FileMode.Create);
                        fileStreamDest.Write(buffer, 0, totalBytes);

                        fileStreamSource.Close();
                        gzipDecompressed.Close();
                        fileStreamDest.Close();

                        f = File.ReadAllText(unpackedFilePath, Encoding.UTF8);
                    }
                }

                try
                {
                    File.Delete(unpackedFilePath);
                }
                catch (Exception ex)
                {
                    Log.WriteToLog(
                        LogSeverity.Error, 0, "Gzip Unpacking : Could not delete " + unpackedFilePath, ex.Message);
                }

                return f;
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, LoggerName.GeneralLog, LogCatagory, ex.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// Pumps the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="output">The output.</param>
        private static void Pump(Stream input, Stream output)
        {
            const string LogCatagory = "Gzip Pump";

            try
            {
                var bytes = new byte[4096];
                int n;
                while ((n = input.Read(bytes, 0, bytes.Length)) != 0)
                {
                    output.Write(bytes, 0, n);
                }
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, LoggerName.GeneralLog, LogCatagory, ex.Message);
            }
        }
    }
}
