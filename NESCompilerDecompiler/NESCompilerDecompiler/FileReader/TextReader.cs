using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace NESCompilerDecompiler.FileReader
{
    public class TextReader
    {

        public void ReadFile(string headerPath, string romPath, string savePath)
        {
            FileStream save = new FileStream(savePath, FileMode.OpenOrCreate, FileAccess.Write);

            string header = "";
            string rom = "";

            FileStream fsHeader = new FileStream(headerPath, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fsHeader, Encoding.UTF8))
            {
                header = streamReader.ReadToEnd();
            }

            string[] seperator = { "\r\n" };

            string[] headerData = header.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
            int hex = 0;

            foreach (string data in headerData)
            {
                hex = Int32.Parse(data, System.Globalization.NumberStyles.HexNumber);
                save.WriteByte(Byte.Parse(hex.ToString()));
            }


            FileStream fsRom = new FileStream(romPath, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fsRom, Encoding.UTF8))
            {
                rom = streamReader.ReadToEnd();
            }

            rom = Regex.Replace(rom, "[(]", "");
            rom = Regex.Replace(rom, "[)]", "");
            rom = Regex.Replace(rom, "[A-Z][A-Z][A-Z]", "");
            rom = Regex.Replace(rom, "[\n]", "");

            string[] romSeperator = { " " };
            string[] splitRom = rom.Split(romSeperator, StringSplitOptions.RemoveEmptyEntries);

            foreach (string data in splitRom)
            {
                hex = Int32.Parse(data, System.Globalization.NumberStyles.HexNumber);
                save.WriteByte(Byte.Parse(hex.ToString()));
            }

            save.Close();


            Console.WriteLine("");
        }

    }
}
