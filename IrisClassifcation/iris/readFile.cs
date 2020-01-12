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
    public class readFile
    {
        private string fileName;

        public readFile(string filename)
        {
            this.fileName = filename;
        }

        public readFile()
        {
            this.fileName = null;
        }

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        private string getFirstLine()
        {
            return File.ReadLines(this.fileName).First();
        }

        public int getNbLine()
        {
            var firstSpaceIndex = getFirstLine().IndexOf(" ");
            return Convert.ToInt32(getFirstLine().Substring(0, firstSpaceIndex)) + 1;
        }

        public int getNbCol()
        {
            var firstSpaceIndex = getFirstLine().IndexOf(" ");
            return Convert.ToInt32(getFirstLine().Substring(firstSpaceIndex));
        }

        public double[,] GetFile()
        {
            String input = File.ReadAllText(this.fileName);
            // Remove the first line, as it's not part of the sample
            input = input.Substring(input.IndexOf('\n') + 1);
            int i = 0, j = 0;
            double[,] result = new double[getNbLine() - 1, getNbCol()];
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
                    catch (Exception ex)
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

        public double[] GetDoubleCol(int col)
        {
            double[] vals = new double[getNbLine()];
            for (int i = 0; i < getNbLine() - 1; i++)
            {
                vals[i] = GetFile()[i, col];
            }

            return vals;
        }

        private static bool IsUnix => YourPlatformId == 4 || YourPlatformId == 6 || YourPlatformId == 128 ||
                                      YourPlatform == PlatformID.Unix;

        private static bool IsWindows => YourPlatform == PlatformID.Win32NT ||
                                         YourPlatform == PlatformID.Win32Windows || YourPlatform == PlatformID.Win32S ||
                                         YourPlatform == PlatformID.WinCE;

        private static OperatingSystem YourOs => Environment.OSVersion;
        private static int YourPlatformId => (int) YourPlatform;
        private static PlatformID YourPlatform => Environment.OSVersion.Platform;
        private static Version YourVersion => YourOs.Version;
    }
}