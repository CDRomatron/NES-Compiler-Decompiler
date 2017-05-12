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

            for(int i = 0; i < romData.Count; i++)
            {
                string romDataOutput = "";
                Opcode oc = arc.HexToOpcode(romData[i]);
                romDataOutput += (oc.command + "(" + romData[i] + ") ");
                for(int j = 0; j < oc.len-1; j++)
                {
                    i++;
                    if(i < romData.Count)
                    {
                        romDataOutput += (romData[i] + " ");
                    }
                }
                romDataOutput += '\n';

                asmFile.Write(romDataOutput);
            }
            
            asmFile.Close();

        }

    }
}
