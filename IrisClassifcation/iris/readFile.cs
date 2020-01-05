using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iris
{
    public class readFile
    {
        private string fileName;
        public readFile(string filename) {
            this.fileName = filename;
        }
        public readFile()
        {
            this.fileName = null;
        }

        public string FileName {
            get
            {
                return fileName;
            }
            set
            {
                fileName = value;
            }
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

        public double[,] GetFile(int x, int y)
        {
            String input = File.ReadAllText(this.fileName);
            int i = 0, j = 0;
            double[,] result = new double[x, y];
            foreach (var row in input.Split('\n'))
            {
                j = 0;
                foreach (var col in row.Trim().Split(' '))
                {
                    double numb = 0.0;
                    if (col.Trim().Contains(".") || col.Trim().Contains(","))
                        numb = double.Parse(col.Trim().Replace('.', ','));
                    else
                    {
                        try
                        {
                            numb = double.Parse(col.Trim());
                        }catch (Exception ex)
                        {
                        }
                    }
                    if (col.Trim() != "" && col.Trim() != " " && col.Trim()!=null)
                    {
                        result[i, j] = numb;
                        j++;
                    }
                    
                }
                i++;
            }
            return result;
        }

        public double[] getDoubleCol(int col)
        {
            double[] vals = new double[getNbLine()];
            for (int i = 0; i < getNbLine() - 1; i++)
            {
                vals[i] = GetFile(getNbLine(), getNbCol())[i, col];
            }
            return vals;
        }
    }
}
