using System;
using Inventor;

namespace InventorShims
{
    /// <summary>
    /// A collection of static methods to manipulate and test string paths
    /// </summary>
    public static class PathShim
    {
        /// <summary>
        /// Provided a string directory path, returns the path with the last
        /// directory removed.  E.g. UpOneLevel("C:\Work\Stuff\") returns "C:\Work"
        /// </summary>
        /// <param name="path">The path as a string</param>
        /// <returns>A string</returns>
        public static string UpOneLevel(string path)
        {
            char delimiter;
            char DirectorySeparatorChar = System.IO.Path.DirectorySeparatorChar;
            char AltDirectorySeparatorChar = System.IO.Path.AltDirectorySeparatorChar;

            if (path.Contains(DirectorySeparatorChar.ToString()))
            {
                delimiter = DirectorySeparatorChar;
            }
            else if (path.Contains(AltDirectorySeparatorChar.ToString()))
            {
                delimiter = AltDirectorySeparatorChar;
            }
            else //no slashes?  not a path...
            {
                return null;
            }

            //Catch paths which end with a delimiter, such as C:\Work\Stuff\
            //Clean up so that they look like C:\Work\Stuff
            path = TrimEndingDirectorySeparator(path);

            int delimPosition = path.LastIndexOf(delimiter);
            if (delimPosition == 0)
            {
                return null;
            }
            else if (delimPosition < 0)
            {
                return path;
            }

            return path.Remove(delimPosition + 1);
        }

        /// <summary>
        /// Provided a string that ends in a "/" or "\", returns the string without those
        /// ending characters.
        /// </summary>
        /// <param name="path">string</param>
        /// <returns>string</returns>
        public static string TrimEndingDirectorySeparator(string path)
        {
            char DirectorySeparatorChar = System.IO.Path.DirectorySeparatorChar;
            char AltDirectorySeparatorChar = System.IO.Path.AltDirectorySeparatorChar;

            if (path.EndsWith(DirectorySeparatorChar.ToString()))
            {
                path = path.Remove(path.LastIndexOf(DirectorySeparatorChar));
            }
            else if (path.EndsWith(AltDirectorySeparatorChar.ToString()))
            {
                path = path.Remove(path.LastIndexOf(AltDirectorySeparatorChar));
            }
            return path;
        }

        /// <summary>
        /// A function that checks if the provided string contains an Inventor library path.
        /// </summary>
        /// <param name="path">Path as a string.</param>
        /// <param name="inventorApp">Inventor.Application object.</param>
        /// <returns></returns>
        public static bool IsLibraryPath(this string path, Application inventorApp)
        {
            if (String.IsNullOrEmpty(path)) return false;

            DesignProject designProject = inventorApp.DesignProjectManager.ActiveDesignProject;
            ProjectPaths libraryPaths = designProject.LibraryPaths;
            //ProjectPath libraryPath;

            foreach (ProjectPath libraryPath in libraryPaths)
            {
                if (path.Contains(TrimEndingDirectorySeparator(libraryPath.Path)))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// A function that checks if the provided string contains a Content Center Library path.
        /// </summary>
        /// <param name="path">Path as a string.</param>
        /// <param name="inventorApp">Inventor.Application object.</param>
        /// <returns></returns>
        public static bool IsContentCenterPath(this string path, Application inventorApp)
        {
            if (String.IsNullOrEmpty(path)) return false;

            DesignProject designProject = inventorApp.DesignProjectManager.ActiveDesignProject;
            bool projectCCPathInUse = designProject.ContentCenterPathOverridden;
            string ccPath;

            if (projectCCPathInUse)
            {
                ccPath = designProject.ContentCenterPath;
            } else
            {
                ccPath = inventorApp.FileOptions.ContentCenterPath;
            }

            ccPath = TrimEndingDirectorySeparator(ccPath);

            if (path.Contains(ccPath)) return true;

            return false;
        }
    }
}