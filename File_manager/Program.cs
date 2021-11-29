using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

namespace File_manager
{
    class Program
    {
        static void Main(string[] args)
        {
            string start_directory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            while (true)
            {
                Console.Clear();
                var user_input = GetCommandFromUser();
                var command_name = user_input.command_name;
                var command_place1 = user_input.command_place1;
                var command_place2 = user_input.command_place2;

                Program.ExecuteUserCommand(command_name, command_place1, command_place2);

                Console.WriteLine("Команда выполнена! Нажмите Enter");
                Console.ReadLine();
            }
        }
        static void ExecuteUserCommand(string command_name, string command_place1, string command_place2)
        {
            if (command_name == "ls")
            {
                
                // массив всех каталогов первого уровня по заданному пути
                string[] units_dir_arr1 = Directory.GetDirectories(@command_place1);
                // массив всех файлов первого уровня по заданному пути
                string[] units_file_arr1 = Directory.GetFiles(@command_place1);
                
                //массив всех каталогов и файлов первого уровня по заданному пути
                string[] units_arr1_all = new string[units_dir_arr1.Length + units_file_arr1.Length];
                units_dir_arr1.CopyTo(units_arr1_all, 0);
                units_file_arr1.CopyTo(units_arr1_all, units_dir_arr1.Length);

                //список всех директорий и файлов второго уровня 
                List<string> units_arr2_all = new List<string>();

                foreach (var item in units_dir_arr1)
                {
                    string[] units_dir_arr2 = Directory.GetDirectories(item);
                    for (int i = 0; i < units_dir_arr2.Length; i++)
                    {
                        units_arr2_all.Add(units_dir_arr2[i]);
                    }
                    string[] units_file_arr2 = Directory.GetFiles(item);
                    for (int i = 0; i < units_file_arr2.Length; i++)
                    {
                        units_arr2_all.Add(units_file_arr2[i]);
                    }
                }
                int count_all_dir_file = Math.Max(units_arr1_all.Length, units_arr2_all.Count);

                //массив всех каталогов и файлов первого  и второго уровня по заданному пути
                string[] units_arr_all = new string[units_arr1_all.Length + units_arr2_all.Count];
                units_arr1_all.CopyTo(units_arr_all, 0);
                units_arr2_all.ToArray().CopyTo(units_arr_all, units_arr1_all.Length);

                int count_pages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(units_arr_all.Length)/10));

                var list_all_units = new List<string>(units_arr_all);
                var count_chars = 10 - (list_all_units.Count) % 10;
                for (int i = 0; i < count_chars; i++)
                {
                    list_all_units.Add("-");
                }

                uint page_number;
                if (UInt32.TryParse(command_place2, out uint number))
                {
                    page_number = Convert.ToUInt32(command_place2);

                }
                else
                {
                    page_number = 0;
                    Console.WriteLine($"Вы ввели некорректный номер страницы. Страница по умолчанию: {page_number} из {count_pages-1}");
                }
                if (page_number>=count_pages)
                {
                    page_number = 0;
                    Console.WriteLine($"Вы ввели некорректный номер страницы. Страница по умолчанию: {page_number} из {count_pages-1}");
                }
                if ((page_number<count_pages))
                {
                    for (uint i = page_number; i < 10*(page_number+1); i++)
                    {
                        Console.WriteLine(list_all_units.ToArray()[i]);
                    }
                }
                Console.WriteLine(command_place1);
                
            }
            if (command_name == "cp")
            {
                Console.WriteLine("cp command");
            }
            if (command_name == "rm")
            {
                Console.WriteLine("rm command");
            }
            if (command_name == "info")
            {
                Console.WriteLine("info command");
            }

        }
        static string GetStringFromUser(string messageToUser)
        {
            Console.WriteLine(messageToUser);
            return Console.ReadLine();
        }

        static (string command_name, string command_place1, string command_place2) GetCommandFromUser()
        {
            Console.WriteLine("Ожидается следующая команда.\nДля получения списка команд введите \"help\"\nДля выхода из программы введите \"exit\"");
            string user_input = Console.ReadLine();
            string[] input_words = user_input.Split(' ');
            int input_count = input_words.Length;
            string command_name = "";
            string command_place1 = "";
            string command_place2 = "";
            switch (input_count)
            {
                case 1:
                    command_name = input_words[0].ToLower();
                    if (command_name == "exit")
                    {
                        var check = GetStringFromUser("Вы уверены, что хотите выйти? (введите: \"yes\"/\"no\")").ToLower();
                        if (check == "yes")
                        {
                            Process processToKill = Process.GetCurrentProcess();
                            processToKill.Kill();
                        }
                        else
                        {
                            return GetCommandFromUser();
                        }
                    }
                    if (command_name == "help")
                    {
                        Console.WriteLine("\n\"Базовый файловый менеджер\" приветствует Вас!");
                        Console.WriteLine("Данная программа выполняет следующие команды:");
                        Console.WriteLine("Вывод\t\tдерева\t\tфайлов/директорий:\tls<ПРОБЕЛ><путь до каталога><ПРОБЕЛ><номер страницы>");
                        Console.WriteLine("Копирование\tзаданного\tфайла/директории:\tcp<ПРОБЕЛ><исходный путь к файлу/директории><ПРОБЕЛ><новый путь к файлу/директории>");
                        Console.WriteLine("Удаление\tзаданного\tфайла/директории:\trm<ПРОБЕЛ><путь к исходному файлу/директории>");
                        Console.WriteLine("Информация\tо\t\tфайле/директории:\tinfo<ПРОБЕЛ><путь к файлу/директории>\n");
                        return GetCommandFromUser();
                    }

                    Console.WriteLine("Команда введена некорректно!");
                    return GetCommandFromUser();
                case 2:
                    command_name = input_words[0].ToLower();
                    if ((command_name != "ls") && (command_name != "cp") && (command_name != "rm") && (command_name != "info"))
                    {
                        Console.WriteLine("Название команды введено некорректно!");
                        return GetCommandFromUser();
                    }
                    command_place1 = input_words[1];
                    break;
                case 3:
                    command_name = input_words[0].ToLower();
                    if ((command_name != "ls") && (command_name != "cp") && (command_name != "rm") && (command_name != "info"))
                    {
                        Console.WriteLine("Название команды введено некорректно!");
                        return GetCommandFromUser();
                    }
                    command_place1 = input_words[1];
                    command_place2 = input_words[2];
                    break;
                default:
                    Console.WriteLine("Команда введена некорректно!");
                    return GetCommandFromUser();
            }


            return (command_name, command_place1, command_place2);
        }
    }
}
