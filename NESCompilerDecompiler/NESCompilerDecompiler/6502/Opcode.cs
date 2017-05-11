using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NESCompilerDecompiler._6502
{
    public class Opcode
    {

        public string command;
        public string hex;
        public int len;

        public Opcode()
        {

        }

        public Opcode(string command, string hex, int len)
        {
            this.command = command;
            this.hex = hex;
            this.len = len;
        }

    }
}
