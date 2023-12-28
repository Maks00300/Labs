using System;
using System.IO;
using McMaster.Extensions.CommandLineUtils;

namespace Lab_4
{
    class Program
    {
        private static string getPathToFile(string labPath)
        {
            labPath = Environment.GetEnvironmentVariable("LAB_PATH") ?? "";
            if (labPath.Length > 0) return labPath;
            else return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        }
        public static int Main(string[] args)
        {
            var LAB_PATH="";
            var app = new CommandLineApplication
            {
                Name = "pr4",
                Description = "Console application pr4",
            };

            app.HelpOption(inherited: true);
            app.Command("version", configCmd =>
            {
                configCmd.OnExecute(() =>
                {
                    Console.WriteLine("Author: Kalyuh Maxim");
                    Console.WriteLine("Version: 1.1.1");
                    return 1;
                });
            });
            app.Command("run", configCmd =>
            {
                configCmd.OnExecute(() =>
                {
                    Console.WriteLine("Specify a lab to execute");
                    return 1;
                });

                configCmd.Command("lab1", setCmd =>
                {
                    setCmd.Description = "Execute lab1";
                    var input1 = setCmd.Option("--input| -i", "input file", CommandOptionType.SingleValue);
                    var output1 = setCmd.Option("--output| -o", "output file", CommandOptionType.SingleValue);
                    Lab1 l1 = new Lab1();
                    if (LAB_PATH == "")
                        LAB_PATH = getPathToFile(LAB_PATH);
                    input1.DefaultValue = $"{LAB_PATH}/Input.txt";
                    output1.DefaultValue = $"{LAB_PATH}/Output.txt";

                    setCmd.OnExecute(() =>
                    {
                        var result = l1.lab1(input1.Value());
                        File.WriteAllText(output1.Value(), result.ToString());
                        Console.WriteLine("===================INPUT FILE===================");
                        Console.WriteLine(File.ReadAllText(input1.Value()));
                        Console.WriteLine("===================OUTPUT FILE===================");
                        Console.WriteLine(File.ReadAllText(output1.Value()));
                    });
                });

                configCmd.Command("lab2", setCmd =>
                {
                    setCmd.Description = "Execute lab2";
                    var input2 = setCmd.Option("--input| -i ", "input file", CommandOptionType.SingleValue);
                    var output2 = setCmd.Option("--output| -o ", "output file", CommandOptionType.SingleValue);
                    Lab2 l2 = new Lab2();
                    if (LAB_PATH == "")
                        LAB_PATH = getPathToFile(LAB_PATH);
                    input2.DefaultValue = $"{LAB_PATH}/Input.txt";
                    output2.DefaultValue = $"{LAB_PATH}/Output.txt";

                    setCmd.OnExecute(() =>
                    {
                        var result = l2.lab2(input2.Value());
                        File.WriteAllText(output2.Value(), result.ToString());
                        Console.WriteLine("===================INPUT FILE===================");
                        Console.WriteLine(File.ReadAllText(input2.Value()));
                        Console.WriteLine("===================OUTPUT FILE===================");
                        Console.WriteLine(File.ReadAllText(output2.Value()));
                    });
                });

                configCmd.Command("lab3", setCmd =>
                {
                    setCmd.Description = "Execute lab3";
                    var input3 = setCmd.Option("--input| -i", "input file", CommandOptionType.SingleValue);
                    var output3 = setCmd.Option("--output| -o", "output file", CommandOptionType.SingleValue);
                    Lab3 l3 = new Lab3();
                    if (LAB_PATH == "")
                        LAB_PATH = getPathToFile(LAB_PATH);
                    input3.DefaultValue = $"{LAB_PATH}/Input.txt";
                    output3.DefaultValue = $"{LAB_PATH}/Output.txt";
                    setCmd.OnExecute(() =>
                    {
                        var result = l3.lab3(input3.Value());
                        if (result < 0)
                            File.WriteAllText(output3.Value(), "INCORRECT");
                        else
                            File.WriteAllText(output3.Value(), (result+1).ToString());
                        Console.WriteLine("===================INPUT FILE===================");
                        Console.WriteLine(File.ReadAllText(input3.Value()));
                        Console.WriteLine("===================OUTPUT FILE===================");
                        Console.WriteLine(File.ReadAllText(output3.Value()));
                    });
                });
                
            });
            app.Command("set-path", setCmd =>
            {
                var path = setCmd.Option("--path| -p", "path to directory", CommandOptionType.SingleValue);
                setCmd.OnExecute(() =>
                {
                    LAB_PATH = path.Value();
                });
            });

            app.OnExecute(() =>
            {
                Console.WriteLine("Specify a command to execute");
                return 1;
            });

            return app.Execute(args);
        }
    }
}