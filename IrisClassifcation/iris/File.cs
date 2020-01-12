using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace iris
{
    public class File
    {
        private string _fileName;

        public File(string filename)
        {
            this._fileName = filename;
        }

        private string GetFirstLine()
        {
            return System.IO.File.ReadLines(this._fileName).First();
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
            String input = System.IO.File.ReadAllText(this._fileName);
            // Remove the first line, as it's not part of the sample
            input = input.Substring(input.IndexOf('\n') + 1);
            int i = 0, j = 0;
            double[,] result = new double[GetNbLine() - 1, GetNbCol()];
            foreach (var row in input.Split('\n'))
            {
                j = 0;
                double numb = -1.0;
                foreach (var col in row.Trim().Split(' '))
                {
                    bool containDotOrComma = col.Contains(',') || col.Contains('.');
                    try
                    {
                        numb = double.Parse(col.Trim());
                    }
                    catch (Exception)
                    {
                        if (containDotOrComma)
                        {
                            numb = (IsWindows)
                                ? double.Parse(col.Trim().Replace('.', ','))
                                : double.Parse(col.Trim().Replace(',', '.'));
                        }
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

        private static bool IsWindows => YourPlatform == PlatformID.Win32NT ||
                                         YourPlatform == PlatformID.Win32Windows || YourPlatform == PlatformID.Win32S ||
                                         YourPlatform == PlatformID.WinCE;

        private static PlatformID YourPlatform => Environment.OSVersion.Platform;
    }
}