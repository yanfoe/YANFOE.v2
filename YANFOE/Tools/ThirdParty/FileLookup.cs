namespace YANFOE.Tools.ThirdParty
{
    using System.IO;
    using System.Collections.Generic;

    static class FileHelper
    {
        public static List<string> GetFilesRecursive(string b, string pattern = "*.*")
        {
            // 1.
            // Store results in the file results list.
            var result = new List<string>();

            // 2.
            // Store a stack of our directories.
            var stack = new Stack<string>();

            // 3.
            // Add initial directory.
            stack.Push(b);

            // 4.
            // Continue while there are directories to process
            while (stack.Count > 0)
            {
                // A.
                // Get top directory
                string dir = stack.Pop();

                try
                {
                    // B
                    // Add all files at this directory to the result List.
                    result.AddRange(Directory.GetFiles(dir, pattern));

                    // C
                    // Add all directories at this directory.
                    foreach (string dn in Directory.GetDirectories(dir))
                    {
                        stack.Push(dn);
                    }
                }
                catch
                {
                    // D
                    // Could not open the directory
                }
            }
            return result;
        }
    }
}
