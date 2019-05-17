/******************************************
 * Trevor Taylor
 * May 05, 2018
 * Computer Architecture
 * Term Project
 ******************************************/

using System;
using System.IO;
using System.Text;
using static CompArch_Project.ReadBinary;
using static CompArch_Project.ProjectCode;
using System.Collections.Generic;


namespace CompArch_Project
{
    internal class ProjectCode
    {
        #region Data Fields
        //  size of code memory
        private int MAX_CODE = 65536;

        //  size of Address memory
        //private int MAX_ADDR = 65536;

        //  a few opcodes defined
        #region OpCodes
        const int HALT = 0;
        const int LDA = 1;
        const int STA = 2;
        const int ADD = 3;
        const int TCA = 4;
        const int BRU = 5;
        const int BIP = 6;
        const int BIN = 7;
        const int RWD = 8;
        const int WWD = 9;
        const int SHL = 0xA;
        const int SHR = 0xB;
        const int LDX = 0xC;
        const int STX = 0xD;
        const int TIX = 0xE;
        const int TDX = 0xF;
        #endregion
        private int PC;

        //  we need a couple of registers
        //private int REG;
        private int IR;
        private bool run;
        private int addNums;

        //Make Accumulator
        private int ACC;
        #endregion

        //  call stack
        public ProjectCode()
        {
            //  constructor initializes the stack machine;
            CodeArray = new int[MAX_CODE];
            //AddrArray = new int[MAX_ADDR];
            PC = 0;
            ACC = 0;
            addNums = 0;
            run = true;
        }

        public int[] CodeArray { get; set; }
        public int[] AddrArray { get; set; }

        public void Execute()
        {
            while (run)
            {
                GetNextInstruction();
                DecodeAndRunInstruction();
            }
        }

        public void GetNextInstruction()
        {
            //  fetching is straighforward
            //REG = AddrArray[PC++];
            IR = CodeArray[PC++];

            //Console.WriteLine("IR:" + IR);
        }

        public void DecodeAndRunInstruction()
        {

            //  pull of the opcode and the operands
            int opcode = IR>>12;  //= IR
            //Console.WriteLine("opcode:" + opcode);
            int operand = (IR & 0xFF); // = REG
            //Console.WriteLine("operand:" + operand);
            switch (opcode)
            {
                case HALT:
                    Console.WriteLine("\n*** opcode(HALT)...ending process ***");
                    run = false;
                    break;

                case LDA:
                    ACC = CodeArray[operand];
                    //Console.WriteLine("opcode(LDA): " + LDA);
                    //Console.WriteLine("ACC: " + ACC);
                    break;

                case STA:
                    //Console.WriteLine("opcode(STA): " + STA);
                    CodeArray[operand] = ACC;
                    //Console.WriteLine("ACC: " + ACC);
                    break;

                case ADD:
                    //Console.WriteLine("opcode(ADD): " + ADD);
                    ACC = ACC + CodeArray[operand];
                    addNums++;
                    //Console.WriteLine("ACC: " + ACC);
                    break;

                case TCA:
                    //Console.WriteLine("opcode(TCA): " + TCA);
                    ACC = (~ACC) + 1;
                    //Console.WriteLine("ACC: " + ACC);
                    break;

                case BRU:
                    //Console.WriteLine("opcode(BRU): " + BRU);
                    PC = operand;
                    //Console.WriteLine("ACC: " + ACC);
                    break;

                case BIP:
                    //Console.WriteLine("opcode(BIP): " + BIP);
                    //Console.WriteLine("ACC: " + ACC);
                    if (ACC > 0) PC = operand;
                    break;

                case BIN:
                    //Console.WriteLine("opcode(BIN): " + BIN);
                    //Console.WriteLine("ACC: " + ACC);
                    if (ACC < 0) PC = operand;
                    break;

                case RWD:
                    //Console.WriteLine("opcode(RWD): " + RWD);
                    //Console.WriteLine("ACC: " + ACC);
                    Console.Write("Enter a number (hit ENTER to continue): ");
                    int rword = Convert.ToInt32(Console.ReadLine());
                    ACC = rword;
                    break;

                case WWD:
                    //Console.WriteLine("opcode(WWD): " + WWD);
                    //Console.WriteLine("ACC: " + ACC);
                    Console.WriteLine("Your output: " + ACC);
                    break;

                //Anything beyond this point does nothing with current .bin file...so I did not implement it.
                case SHL:
                    //TODO: Fill with code for operation based on opcode
                    Console.WriteLine("opcode(SHL): " + SHL);
                    Console.WriteLine("ACC: " + ACC);
                    break;

                case SHR:
                    //TODO: Fill with code for operation based on opcode
                    Console.WriteLine("opcode(SHR): " + SHR);
                    Console.WriteLine("ACC: " + ACC);
                    break;

                case LDX:
                    //TODO: Fill with code for operation based on opcode
                    Console.WriteLine("opcode(LDX): " + LDX);
                    Console.WriteLine("ACC: " + ACC);
                    break;

                case STX:
                    //TODO: Fill with code for operation based on opcode
                    Console.WriteLine("opcode(STX): " + STX);
                    Console.WriteLine("ACC: " + ACC);
                    break;

                case TIX:
                    //TODO: Fill with code for operation based on opcode
                    Console.WriteLine("opcode(TIX): " + TIX);
                    Console.WriteLine("ACC: " + ACC);
                    break;

                case TDX:
                    //TODO: Fill with code for operation based on opcode
                    Console.WriteLine("opcode(TDX): " + TDX);
                    Console.WriteLine("ACC: " + ACC);
                    break;

                default:
                    Console.Error.WriteLine("Unimplemented opcode");
                    //Environment.Exit((int)opcode);
                    break;
            }
            if ((IR == 0))
            {
                run = false;
            }

            //  Isn't this redundant?  Oh, well leave it in for now
        }

        public static void Main(String[] args)
        {
            var path = Directory.GetCurrentDirectory();
            ProjectCode vm = new ProjectCode();
            //  create our virtual machine
            Console.WriteLine("Beginning Execution...");
            if (args.Length != 0)
            {
                if(File.Exists(path + "\\"+ args[0]))
                    ReadBinaryInit(vm, args[0]);
            }
            else ReadBinaryInit(vm);
            vm.Execute();
            //  and, let it go, let it go .....

            Console.WriteLine("Done! ");
            Console.WriteLine("Press Enter to Close...");
            Console.Read();
        }

     
    }//End class

    class ReadBinary
    {
        #region ReadBinary Datafield
        private static string file;
        private static string extractedBits;
        private static string valToBin;
        private static string contents;
        //private static ProjectCode PrjCode;// = new ProjectCode();
        #endregion

        public static void ReadBinaryInit(ProjectCode vm, dynamic args)
        {
            file = "Divide.bin";    //This file is for term project
            //var path = Directory.GetCurrentDirectory(); //.data files needs to be in same directory as the program
                                                        //string file = path + "\\Numbers.dat"; //Not needed but works       
            ReadBinFile(file, vm, args);
            //Console.ReadLine();
        }//end ctor

        public static void ReadBinaryInit(ProjectCode vm)
        {
            file = "Divide.bin";    //This file is for term project
            //var path = Directory.GetCurrentDirectory(); //.data files needs to be in same directory as the program; file = path + "Divide.bin" <-needed if file not in same place as exe  
            ReadBinFile(file, vm);
            //Console.ReadLine();
        }//end ctor

        #region Methods - ReadBinary
        //with args
        public static void ReadBinFile(string file, ProjectCode vm, dynamic args)
        {
            using (BigEndianBinaryReader br = new BigEndianBinaryReader(File.Open(args, FileMode.Open))) //This will open and close file after its done with the data
            {
                int count = 0;
                while (!br.EOF())
                {
                    // Read in numbers.
                    UInt16 v = (UInt16)br.ReadInt16();
                    //vm.CodeArray[count] = Convert.ToInt32(v);
                    //manipulate data and print to console    

                    contents = String.Format("{0,04:X4}", v);
                    valToBin = ToBinary(v);
                    vm.CodeArray[count] = Convert.ToUInt16(v);
                    //vm.CodeArray[count] = Convert.ToInt32(OpCodes(valToBin));
                    //vm.AddrArray[count] = Convert.ToInt32(Addresses(valToBin));
                    //extract bits and display them
                    //extractedBits = ToExtracted(valToBin);
                    //Console.Write(valToBin + "    " + contents + "    " + extractedBits + "\n");
                    count++;
                }//end while loop
                //Console.WriteLine();
                //Console.WriteLine("Codearray with file info");
                //for (int i = 0; i < count; i++)
                //{
                //    Console.WriteLine("Opcode: "+vm.CodeArray[i]); // + " Addr:" + vm.AddrArray[i]
                //}
                //Console.ReadLine();
            }//end using
        }//end ReadBinFile Method

        //without args
        public static void ReadBinFile(string file, ProjectCode vm)
        {
            using (BigEndianBinaryReader br = new BigEndianBinaryReader(File.Open(file, FileMode.Open))) //This will open and close file after its done with the data
            {
                int count = 0;
                while (!br.EOF())
                {
                    // Read in numbers.
                    UInt16 v = (UInt16)br.ReadInt16();
                    //vm.CodeArray[count] = Convert.ToInt32(v);
                    //manipulate data and print to console    

                    contents = String.Format("{0,04:X4}", v);
                    valToBin = ToBinary(v);
                    vm.CodeArray[count] = Convert.ToUInt16(v);
                    //vm.CodeArray[count] = Convert.ToInt32(OpCodes(valToBin));
                    //vm.AddrArray[count] = Convert.ToInt32(Addresses(valToBin));
                    //extract bits and display them
                    //extractedBits = ToExtracted(valToBin);
                    //Console.Write(valToBin + "    " + contents + "    " + extractedBits + "\n");
                    count++;
                }//end while loop
                //Console.WriteLine();
                //Console.WriteLine("Codearray with file info");
                //for (int i = 0; i < count; i++)
                //{
                //    Console.WriteLine("Opcode: " + vm.CodeArray[i]); // + " Addr:" + vm.AddrArray[i]
                //}
                //Console.ReadLine();
            }//end using
        }//end ReadBinFile Method

        public static string ToExtracted(string binaryBits)
        {
            //Covnverts sets of bits to unsigned interger values.
            string val5Bits = "";
            string val1Bit = "";
            string val2Bits = "";
            string val8Bits = "";

            //bits:  15 14 13 12 11 10 09 08 07 06 05 04 03 02 01 00
            //Index: 00 01 02 03 04 05 06 07 08 09 10 11 12 13 14 15

            //5bit unsigned base10 from bits 15-11
            val5Bits = binaryBits.Substring(0, 4);
            uint firstNum = Convert.ToUInt32(val5Bits, 2);
            string num1 = Convert.ToString(firstNum).PadLeft(2, ' ');

            //1bit val from bit 10
            val1Bit = binaryBits.Substring(5, 1);
            uint secNum = Convert.ToUInt32(val1Bit, 2);
            string num2 = Convert.ToString(secNum, 10).PadLeft(2, ' ');

            //2bit unsigned base10 from bits 9-8
            val2Bits = binaryBits.Substring(6, 2);
            uint thirdNum = Convert.ToUInt32(val2Bits, 2);
            string num3 = Convert.ToString(thirdNum, 10).PadLeft(2, ' ');

            //8bit unsigned base10 from bits 7-0
            val8Bits = binaryBits.Substring(8, 8);
            uint forthNum = Convert.ToUInt32(val8Bits, 2);
            string num4 = Convert.ToString(forthNum, 10).PadLeft(2, ' ');

            string result = num1 + " " + num2 + " " + num3 + " " + num4;
            return result;
        }//end ToExtracted Method

        public static UInt32 OpCodes(string binaryBits)
        {
            string val5Bits = "";
            //bits:  15 14 13 12 11 10 09 08 07 06 05 04 03 02 01 00
            //Index: 00 01 02 03 04 05 06 07 08 09 10 11 12 13 14 15

            //5bit unsigned base10 from bits 15-11
            val5Bits = binaryBits.Substring(0, 4); //only need 15-12 for this project
            uint firstNum = Convert.ToUInt32(val5Bits, 2);
            //string num1 = Convert.ToString(firstNum).PadLeft(2, ' ');
            return firstNum;
        }

        public static UInt32 Addresses(string binaryBits)
        {
            string val8Bits = "";
            //bits:  15 14 13 12 11 10 09 08 07 06 05 04 03 02 01 00
            //Index: 00 01 02 03 04 05 06 07 08 09 10 11 12 13 14 15

            //8bit unsigned base10 from bits 7-0
            val8Bits = binaryBits.Substring(8, 8);
            uint forthNum = Convert.ToUInt32(val8Bits, 2);
            //string num1 = Convert.ToString(firstNum).PadLeft(2, ' ');
            return forthNum;
        }

        public static string ToBinary(dynamic num)
        {
            //Converts the numbers to binary
            string result = Convert.ToString(num, 2).PadLeft(16, '0');
            //result = String.Format("{0,16:X16}", result);
            return result;
        }//end ToBinary Method
        #endregion
    }//end ReadBinary Class

    #region Special Classes
    //***Both fo these methods I found from the internet.***

    //This class changes the Endianness to Big-Endian (C# uses little-Endian)
    class BigEndianBinaryReader : BinaryReader
    {
        public BigEndianBinaryReader(Stream input) : base(input)
        {

        }

        public override short ReadInt16()
        {
            byte[] b = ReadBytes(2);
            return (short)(b[1] + (b[0] << 8));
        }

        public override int ReadInt32()
        {
            byte[] b = ReadBytes(4);
            return b[3] + (b[2] << 8) + (b[1] << 16) + (b[0] << 24);
        }

        public override long ReadInt64()
        {
            byte[] b = ReadBytes(8);
            return b[7] + (b[6] << 8) + (b[5] << 16) + (b[4] << 24) + (b[3] << 32) + (b[2] << 40) + (b[1] << 48) + (b[0] << 56);
        }

        /// <summary>Returns <c>true</c> if the Int32 read is not zero, otherwise, <c>false</c>.</summary>
        /// <returns><c>true</c> if the Int32 is not zero, otherwise, <c>false</c>.</returns>
        public bool ReadInt32AsBool()
        {
            byte[] b = ReadBytes(4);
            if (b[0] == 0 || b[1] == 0 || b[2] == 0 || b[3] == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Reads a string prefixed by a 32-bit integer identifying its length, in chars.
        /// </summary>
        public string ReadString32BitPrefix()
        {
            int length = ReadInt32();
            return Encoding.ASCII.GetString(ReadBytes(length));
        }

        public float ReadFloat()
        {
            return (float)ReadDouble();
        }
    }

    //Allows for reading the file using End of Stream 
    //(made it easy to read data based on how I was reading the data)
    public static class StreamEOF
    {
        public static bool EOF(this BinaryReader binaryReader)
        {
            var bs = binaryReader.BaseStream;
            return (bs.Position == bs.Length);
        }
    }
    #endregion
}//End Namespace