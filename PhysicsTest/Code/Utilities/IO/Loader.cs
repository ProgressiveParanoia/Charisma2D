using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ParanoidGames.Utilities.IO
{
    /// <summary>
    /// Base class for  IO classes
    /// </summary>
    class Loader
    {
    
        /// <summary>
        /// Get all file paths inside a specified directory.
        /// </summary>
        /// <param name="targetDirectory">Check directory for files</param>
        /// <returns></returns>
        internal string[] GetFilePaths(string targetDirectory)
        {
            if (Directory.Exists(targetDirectory) == false)
            {
                return null;
            }

            string[] filePathCollection = Directory.GetFiles(targetDirectory);
            
            return filePathCollection;
        }

        private List<string> filePathList = new List<string>();
        /// <summary>
        /// Get all file path inside a specified directory and its subdirectories.
        /// </summary>
        /// <param name="targetDirectory">Check directory for files</param>
        /// <returns></returns>
        internal string[] RecurseGetFilePaths(string targetDirectory)
        {
            if(Directory.Exists(targetDirectory) == false)
            {
                return null;
            }
     
            filePathList.Clear();
            CheckSubdirectories(targetDirectory);

            return filePathList.ToArray();
        }

        /// <summary>
        /// Check file path for subdirectories. Adds files in subdirectories to file path list.
        /// </summary>
        /// <param name="targetDirectory">Check directory for files</param>
        private void CheckSubdirectories(string targetDirectory)
        {
            string[] filePathCollection = Directory.GetFiles(targetDirectory);
            filePathList.AddRange(filePathCollection);
            string[] subDirectories = Directory.GetDirectories(targetDirectory);

            foreach (string subDir in subDirectories)
            {
                CheckSubdirectories(subDir);
            }
        }
    }
}
