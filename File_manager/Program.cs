using System;
using System.Diagnostics;
using System.IO;

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
                Console.WriteLine("ls command");
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
                        Console.WriteLine("Вывод\t\tдерева\t\tфайлов/директорий:\tls<ПРОБЕЛ><путь до каталога><пробел><ПРОБЕЛ><номер страницы>");
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
