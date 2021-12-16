using Helpers;

namespace AocRunner
{
    internal class Aoc16
    {
        /*
         * --- Day 16: Packet Decoder ---
         * https://adventofcode.com/2021/day/16
         *
         */

        readonly string input;

        public Aoc16()
        {
            //input = File.ReadAllText("inputs\\sample16-1.txt");
            input = File.ReadAllText("inputs\\day16.txt");

            // sum
            //input = "C200B40A82";
            // product
            //input = "04005AC33890";
            // min 
            //input = "880086C3E88112";
            // max
            //input = "CE00C43D881120";
            // less than
            //input = "D8005AC2A8F0";
            // greater than
            //input = "F600BC2D8F";
            //equal to
            //input = "9C005AC2F8F0";
            // 1 + 3 = 2 * 2?
            //input = "9C0141080250320F1802104A08";
        }

        public long RunPuzzle1()
        {
            var bits = new BitsSystem();
            bits.AddData(input);

            var version = AddVersion(bits.Instruction);

            return version;
        }

        public long RunPuzzle2()
        {
            var bits = new BitsSystem();
            bits.AddData(input);

            var total = bits.Instruction.Calculate();
            return total;
        }

        private long AddVersion(Instruction instruction)
        {
            long sum = instruction.Version;
            foreach (var instr in instruction.SubInstructions)
            {
                sum += AddVersion(instr);
            }

            return sum;
        }
    }
}