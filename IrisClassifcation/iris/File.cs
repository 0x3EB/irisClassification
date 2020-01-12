using System;
using System.Linq;

namespace iris
{
    public class File
    {
        private readonly string _fileName;

        public File(string filename)
        {
            _fileName = filename;
        }

        private static bool IsWindows => YourPlatform == PlatformID.Win32NT ||
                                         YourPlatform == PlatformID.Win32Windows || YourPlatform == PlatformID.Win32S ||
                                         YourPlatform == PlatformID.WinCE;

        private static PlatformID YourPlatform => Environment.OSVersion.Platform;

        private string GetFirstLine()
        {
            return System.IO.File.ReadLines(_fileName).First();
        }

        public int GetNbLine()
        {
            var firstSpaceIndex = GetFirstLine().IndexOf(' ');
            return Convert.ToInt32(GetFirstLine().Substring(0, firstSpaceIndex)) + 1;
        }

        public int GetNbCol()
        {
            var firstSpaceIndex = GetFirstLine().IndexOf(' ');
            return Convert.ToInt32(GetFirstLine().Substring(firstSpaceIndex));
        }

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