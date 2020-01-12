using System;
using System.Linq;

namespace iris
{
    public class File
    {
        private readonly string _fileName;

        /// <summary>
        /// Constructor of File Class
        /// </summary>
        /// <param name="filename"></param>
        public File(string filename)
        {
            _fileName = filename;
        }

        /// <summary>
        /// Return if ur system is Windows or not
        /// </summary>
        private static bool IsWindows => YourPlatform == PlatformID.Win32NT ||
                                         YourPlatform == PlatformID.Win32Windows || YourPlatform == PlatformID.Win32S ||
                                         YourPlatform == PlatformID.WinCE;

        /// <summary>
        /// Get your platformID
        /// </summary>
        private static PlatformID YourPlatform => Environment.OSVersion.Platform;

        /// <summary>
        /// Get the first line to set the 2D array properly
        /// </summary>
        /// <returns>Return the First line of the file</returns>
        private string GetFirstLine()
        {
            return System.IO.File.ReadLines(_fileName).First();
        }

        /// <summary>
        /// get the number of line fo the text file
        /// </summary>
        /// <returns>return number into int</returns>
        public int GetNbLine()
        {
            var firstSpaceIndex = GetFirstLine().IndexOf(' ');
            return Convert.ToInt32(GetFirstLine().Substring(0, firstSpaceIndex)) + 1;
        }

        /// <summary>
        /// Get the number of column of the text file
        /// </summary>
        /// <returns>>return number into int</returns>
        public int GetNbCol()
        {
            var firstSpaceIndex = GetFirstLine().IndexOf(' ');
            return Convert.ToInt32(GetFirstLine().Substring(firstSpaceIndex));
        }

        /// <summary>
        /// Set 2D array with correct dimension and store all the data into it. 
        /// </summary>
        /// <returns>Return 2D array with all the lines of file</returns>
        public double[,] GetFile()
        {
            var input = System.IO.File.ReadAllText(_fileName);
            // Remove the first line, as it's not part of the sample
            input = input.Substring(input.IndexOf('\n') + 1);
            int i = 0, j = 0;
            var result = new double[GetNbLine() - 1, GetNbCol()];
            foreach (var row in input.Split('\n'))
            {
                j = 0;
                var numb = -1.0;
                foreach (var col in row.Trim().Split(' '))
                {
                    var containDotOrComma = col.Contains(',') || col.Contains('.');
                    try
                    {
                        numb = double.Parse(col.Trim());
                    }
                    catch (Exception)
                    {
                        if (containDotOrComma)
                            // Replace . with , for windows and , with . for Unix
                            numb = IsWindows
                                ? double.Parse(col.Trim().Replace('.', ','))
                                : double.Parse(col.Trim().Replace(',', '.'));
                    }

                    if (col.Trim() != "" && col.Trim() != " " && col.Trim() != null)
                    {
                        result[i, j] = numb;
                        j++;
                    }
                }

                i++;
            }

            return result;
        }
    }
}