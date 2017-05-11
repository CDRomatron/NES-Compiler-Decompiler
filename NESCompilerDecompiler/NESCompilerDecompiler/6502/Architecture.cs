using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NESCompilerDecompiler._6502
{
    public class Architecture
    {

        public List<Opcode> opcodes;

        public Architecture()
        {
            opcodes = new List<Opcode>();
        }

        public void PopulateOpcodes(string path)
        {

            using (var fs = File.OpenRead(path))
            using (var reader = new StreamReader(fs))
            {
                List<string> listA = new List<string>();
                List<string> listB = new List<string>();
                while(!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    Opcode oc = new Opcode(values[0], values[1], Int32.Parse(values[2]));

                    opcodes.Add(oc);
                }
            }

        }

        public Opcode HexToOpcode(string hex)
        {
            Opcode value = new Opcode("UND", "", 1);

            foreach(Opcode opcode in opcodes)
            {
                if(opcode.hex == hex)
                {
                    value = opcode;
                }
            }

            return value;
        }

    }
}
