using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NESCompilerDecompiler._6502;

namespace NESCompilerDecompiler.FileReader
{
    /*
     * The function of the hexreader is to read in a file that is not currently in a text format, and convert it to a string.
     */
    public class HexReader
    {

        public void ReadFile(string path)
        {

            FileStream fs = new FileStream(path, FileMode.Open);
            int hexIn;
            string hex;
            List<string> output = new List<string>();


            for(int i = 0; (hexIn = fs.ReadByte()) != -1; i++)
            {
                hex = string.Format("{0:X2}", hexIn);
                output.Add(hex);
            }

            Architecture arc = new Architecture();

            arc.PopulateOpcodes("6502.csv");

            Console.WriteLine("");

        }

    }
}
