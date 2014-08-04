using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace REU2014FileReader
{
    class Program
    {
        static void Main(string[] args)
        {
            string csv = "";
            string csv2 = "";
            for (int i = 25000; i <= 500000; i += 25000)
            {
                for (int j = 1; j <= 16; j++)
                {
                    csv += "cilkp," + i + "," + j + "," + File.ReadAllText("timing-sort-cilkp-" + i + "-" + j).Split('|')[1].Replace(" ", "") + "\n";
                    string[] lines = File.ReadAllLines("timing-sort-cilkp-" + i + "-" + j);
                    double max = 1337;
                    foreach (string l in lines)
                    {
                        string num = l.Split(' ')[0];
                        if (num.Length < 5)
                            break;
                        double d = Double.Parse(num);
                        if (d/max > 1.20)
                            break;
                        max = d;
                    }
                    csv2 += "cilkp," + i + "," + j + "," + (max + 0.000001) + "\n";
                }
            }
            for (int i = 25000; i <= 500000; i += 25000)
            {
                for (int j = 1; j <= 16; j++)
                {
                    csv += "omp," + i + "," + j + "," + File.ReadAllText("timing-sort-omp-" + i + "-" + j).Split('|')[1].Replace(" ", "") + "\n";
                    string[] lines = File.ReadAllLines("timing-sort-omp-" + i + "-" + j);
                    double max = 1337;
                    foreach (string l in lines)
                    {
                        string num = l.Split(' ')[0];
                        if (num.Length < 5)
                            break;
                        double d = Double.Parse(num);
                        if (d/max > 1.20)
                            break;
                        max = d;
                    }
                    csv2 += "omp," + i + "," + j + "," + (max + 0.000001) + "\n";
                }
            }
            File.WriteAllText("000-timing-sort.csv", csv);
            File.WriteAllText("000-timing-sort-no-outliers.csv", csv2);
        }
    }
}
