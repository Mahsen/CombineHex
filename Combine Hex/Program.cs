using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Combine_Hex
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {

                if (!File.Exists(args[0]))
                {
                    Console.WriteLine("CombineHex : Error the first file does not exist \"" + args[0] + "\" .");
                    return;
                }
                if (!File.Exists(args[1]))
                {
                    Console.WriteLine("CombineHex : Error the second file does not exist \"" + args[1] + "\" .");
                    return;
                }
                if (args[0].IndexOf("-") != -1)
                {
                    Console.WriteLine("CombineHex : Error the first file name \"" + args[0] + "\" .");
                    return;
                }
                if (args[1].IndexOf("-") != -1)
                {
                    Console.WriteLine("CombineHex : Error the second file name \"" + args[1] + "\" .");
                    return;
                }
                if (args[2].IndexOf("-") != -1)
                {
                    Console.WriteLine("CombineHex : Error the output file name \"" + args[2] + "\" .");
                    return;
                }

                byte[] exeBytes = Properties.Resources.hexmate;
                string exeToRun = @"C://Temp.exe";

                if (!File.Exists(exeToRun))
                {
                    FileStream exeFile = new FileStream(exeToRun, FileMode.CreateNew);
                    exeFile.Write(exeBytes, 0, exeBytes.Length);
                    exeFile.Close();
                }

                var exeProcess = new Process()
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        FileName = exeToRun,
                        Arguments = args[0] + " " + args[1] + "  -o" + args[2]
                    }
                };
                exeProcess.Start();
                exeProcess.WaitForExit();

                Console.WriteLine("CombineHex : Output file \"" + args[2] + "\" .");
                Console.WriteLine("CombineHex : Successfully Complete .");

                File.Delete(exeToRun);

                }
                catch (Exception er)
                {
                    Console.WriteLine("CombineHex : " + er.Message + " .");
                }

        }
    }
}
