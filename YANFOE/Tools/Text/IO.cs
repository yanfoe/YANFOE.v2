// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IO.cs" company="The YANFOE Project">
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

namespace YANFOE.Tools.Text
{
    using System.IO;
    using System.Text;

    public static class IO
    {
        public static string ReadTextFromFile(string path)
        {
            if (!File.Exists(path))
            {
                return string.Empty;
            }

            return File.ReadAllText(path);
        }

        public static void WriteTextToFile(string path, string value)
        {
            var textWriter = new StreamWriter(path, false, Encoding.UTF8);

            try
            {
                textWriter.Write(value);

            }
            finally
            {
                textWriter.Flush();
                textWriter.Close(); 
            }
        }
    }
}
