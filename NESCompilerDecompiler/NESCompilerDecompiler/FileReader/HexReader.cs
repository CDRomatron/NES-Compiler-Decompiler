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

            /*
             *Reminder to implement trainers 
             */
            List<string> headerData = new List<string>();
            List<string> romData = new List<string>();

            for(int i = 0; i < output.Count; i++)
            {
                if(i<16)
                {
                    headerData.Add(output[i]);
                }
                else
                {
                    romData.Add(output[i]);
                }
            }

            StreamWriter headerFile = new StreamWriter(Path.GetFileNameWithoutExtension(path) + ".nesh");
            foreach(var line in headerData)
            {
                headerFile.WriteLine(line);
            }

            headerFile.Close();


            Architecture arc = new Architecture();

            arc.PopulateOpcodes("6502.csv");


            StreamWriter asmFile = new StreamWriter(Path.GetFileNameWithoutExtension(path) + ".nesa");

            while (romData.Count > 0)
            {
                string romDataOutput = "";
                Opcode oc = arc.HexToOpcode(romData[0]);
                romDataOutput += (oc.command + "(" + oc.hex + ") ");
                romData.RemoveAt(0);
                for(int i = 0; i < oc.len; i++)
                {
                    romDataOutput += (romData[0] + " ");
                    romData.RemoveAt(0);
                }
                romDataOutput += '\n';

                asmFile.Write(romDataOutput);
            }
            
            asmFile.Close();

            Console.WriteLine("");

        }

    }
}
