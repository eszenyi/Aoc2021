using System.Text;

namespace Helpers
{
    static class BitLength
    {
        public static readonly int VersionLength = 3;
        public static readonly int PacketIdLength = 3;
        public static readonly int LiteralGroupHeaderLength = 1;
        public static readonly int LiteralLength = 4;
        public static readonly int LengthTypeLength = 1;
        public static readonly int TotalLengthInBitsLength = 15;
        public static readonly int NumberOfSubPacketsLength = 11;
    }

    static class LengthType
    {
        public static readonly int TotalLengthInBits = 0;
        public static readonly int NumberOfSubPackets = 1;
    }

    public class BitsSystem
    {
        private string _raw;
        private Instruction _instruction;

        public Instruction Instruction => _instruction;

        public void AddData(string data)
        {
            var builder = new StringBuilder();
            foreach (char b in data)
            {
                builder.Append(ConvertHexDigit(b));
            }
            _raw = builder.ToString();

            var instruction = new Instruction();
            DecodeNext(_raw, instruction);
            _instruction = instruction;
        }

        private int DecodeNext(string packet, Instruction instruction)
        {
            int index = 0;

            instruction.Version = Binary.BinaryToLong(packet.Substring(index, BitLength.VersionLength));
            index += BitLength.VersionLength;

            instruction.PacketId = (PacketType)Binary.BinaryToInt(packet.Substring(index, BitLength.PacketIdLength));
            index += BitLength.PacketIdLength;

            if (instruction.PacketId == PacketType.Literal)
            {
                var offset = DecodeLiteral(instruction, packet[index..]);
                index += offset;
            }
            else
            {
                var offset = DecodeOperator(instruction, packet[index..]);
                index += offset;
            }

            return index;
        }

        private int DecodeOperator(Instruction instruction, string packetPart)
        {
            int index = 0;
            int lengthType = Binary.BinaryToInt(packetPart.Substring(index, BitLength.LengthTypeLength));
            index += BitLength.LengthTypeLength;

            if (lengthType == LengthType.TotalLengthInBits)
            {
                var bitLength = Binary.BinaryToInt(packetPart.Substring(index, BitLength.TotalLengthInBitsLength));
                index += BitLength.TotalLengthInBitsLength;

                var accumulatedOffset = 0;
                var length = bitLength;
                while (accumulatedOffset < bitLength)
                {
                    var newInstruction = new Instruction();
                    instruction.AddSubInstruction(newInstruction);

                    var offset = DecodeNext(packetPart.Substring(index, length), newInstruction);
                    accumulatedOffset += offset;
                    index += offset;
                    length -= offset;
                }
            }
            else // if (lengthType == LengthType.NumberOfSubPackets)
            {
                var numPackets = Binary.BinaryToLong(packetPart.Substring(index, BitLength.NumberOfSubPacketsLength));
                index += BitLength.NumberOfSubPacketsLength;

                for (int i = 0; i < numPackets; i++)
                {
                    var newInstruction = new Instruction();
                    instruction.AddSubInstruction(newInstruction);

                    var offset = DecodeNext(packetPart[index..], newInstruction);
                    index += offset;
                }
            }

            return index;
        }

        private static int DecodeLiteral(Instruction instruction, string packetPart)
        {
            int index = 0;
            StringBuilder literalBuilder = new();
            bool keepReading = true;
            while (keepReading)
            {
                keepReading = packetPart.Substring(index, BitLength.LiteralGroupHeaderLength) == "1";
                index++;
                literalBuilder.Append(packetPart.Substring(index, BitLength.LiteralLength));
                index += BitLength.LiteralLength;
            }

            instruction.LiteralValue = Binary.BinaryToLong(literalBuilder.ToString());
            return index;
        }

        private static string ConvertHexDigit(char b) => b switch
        {
            '0' => "0000",
            '1' => "0001",
            '2' => "0010",
            '3' => "0011",
            '4' => "0100",
            '5' => "0101",
            '6' => "0110",
            '7' => "0111",
            '8' => "1000",
            '9' => "1001",
            'A' => "1010",
            'B' => "1011",
            'C' => "1100",
            'D' => "1101",
            'E' => "1110",
            'F' => "1111",
            _ => throw new NotImplementedException()
        };
    }

    public class Instruction
    {
        public long Version { get; internal set; }
        public PacketType PacketId { get; internal set; }
        public long? LiteralValue { get; internal set; }

        public List<Instruction> SubInstructions { get; internal set; }

        public Instruction()
        {
            SubInstructions = new List<Instruction>();
        }

        public override string ToString()
        {
            return $"v{Version} {(LiteralValue.HasValue ? "literal" : "operator")} {(LiteralValue.HasValue ? LiteralValue : PacketId)}";
        }

        internal void AddSubInstruction(Instruction instruction)
        {
            SubInstructions.Add(instruction);
        }

        public long Calculate()
        {
            if (LiteralValue.HasValue)
            {
                return LiteralValue.Value;
            }

            long result = 0;
            switch (PacketId)
            {
                case PacketType.Sum:
                    result = SubInstructions.Sum(instr => instr.Calculate());
                    break;
                case PacketType.Product:
                    result = SubInstructions.Aggregate((long)1, (acc, instr) => acc * instr.Calculate());
                    break;
                case PacketType.Min:
                    result = SubInstructions.Min(instr => instr.Calculate());
                    break;
                case PacketType.Max:
                    result = SubInstructions.Max(instr => instr.Calculate());
                    break;
                case PacketType.GreaterThan:
                    result = SubInstructions[0].Calculate() > SubInstructions[1].Calculate() ? 1 : 0;
                    break;
                case PacketType.LessThan:
                    result = SubInstructions[0].Calculate() < SubInstructions[1].Calculate() ? 1 : 0;
                    break;
                case PacketType.EqualTo:
                    result = SubInstructions[0].Calculate() == SubInstructions[1].Calculate() ? 1 : 0;
                    break;
            }
            return result;
        }
    }

    public enum PacketType
    {
        Sum,
        Product,
        Min,
        Max,
        Literal,
        GreaterThan,
        LessThan,
        EqualTo
    }

    public enum Operator
    {
        Sum,
        Product,
        Min,
        Max,
        GreaterThan,
        LessThan,
        EqualTo
    }
}