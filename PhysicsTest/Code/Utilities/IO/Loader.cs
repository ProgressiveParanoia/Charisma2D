using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ParanoidGames.Constants;

namespace ParanoidGames.Utilities.IO
{
    /// <summary>
    /// Base class for  IO classes
    /// </summary>
    class Loader
    {
        private string targetDirectory = FileDirectory.Content;
        /// <summary>
        /// Get all file paths inside a specified directory.
        /// </summary>
        /// <param name="targetDirectory">Check directory for files</param>
        /// <returns></returns>
        internal string[] GetFilePaths(string targetDirectory)
        {
            this.targetDirectory += targetDirectory;

            if (Directory.Exists(this.targetDirectory) == false)
            {
                return null;
            }

            string[] filePathCollection = Directory.GetFiles(this.targetDirectory);

            this.targetDirectory = FileDirectory.Content;

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
            this.targetDirectory += targetDirectory;

            if(Directory.Exists(this.targetDirectory) == false)
            {
                return null;
            }
     
            this.filePathList.Clear();
            CheckSubdirectories(this.targetDirectory);

            this.targetDirectory = FileDirectory.Content;

            return this.filePathList.ToArray();
        }

        /// <summary>
        /// Check file path for subdirectories. Adds files in subdirectories to file path list.
        /// </summary>
        /// <param name="targetDirectory">Check directory for files</param>
        private void CheckSubdirectories(string targetDirectory)
        {

            string[] filePathCollection = Directory.GetFiles(targetDirectory);
            this.filePathList.AddRange(filePathCollection);
            string[] subDirectories = Directory.GetDirectories(targetDirectory);

            foreach (string subDir in subDirectories)
            {
                CheckSubdirectories(subDir);
            }
        }
    }
}
