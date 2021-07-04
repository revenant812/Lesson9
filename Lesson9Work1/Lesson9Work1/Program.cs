using System;
using System.IO;


namespace Lesson9Work1
{
    class Program
    {
        static int count = 0;
        public static string StartString = @"C:\Users\Дима\Desktop\TestDir";
        static void Main(string[] args)
        {
            Tree(StartString);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            while (true)
            {                
                Console.WriteLine();
                FontColorGreen("Введите команду: ");             
                string command = Console.ReadLine();
                if (command.Contains("copy"))
                {
                    FontColorGreen("Что скопировать: ");
                    string FromStr = Console.ReadLine();
                    FontColorGreen("Куда скопировать: ");
                    string ToStr = Console.ReadLine();
                    try
                    {
                        CopyDir(FromStr, ToStr, StartString);
                    }
                    catch
                    {
                        Console.WriteLine("Ошибка в пути к папке!");
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                }
                if (command.Contains("cd"))
                {                   
                        FontColorGreen("Куда перейти: ");
                        string ChangeStr = Console.ReadLine();
                        StartString = ChangeDirToUp(StartString, ChangeStr);
                        Tree(StartString);
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine();                  
                }
                if (command.Contains(".."))
                {                                   
                        Console.WriteLine();
                        StartString = ChangeDirToDown(StartString);
                        Tree(StartString);
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine();                    
                }
                if (command.Contains("ls"))
                {                    
                    Console.WriteLine();
                    Tree(StartString);
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                }

            }            
        }
        static void FontColorRed(string str)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(str);
            Console.ResetColor();           
        }

        static void FontColorGreen(string str)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(str);
            Console.ResetColor();
        }

        public static string TreeLite(string str)
        {
            DirectoryInfo Dir = new DirectoryInfo(str);
            string result = Convert.ToString(Dir);
            //var ArrayDir = Dir.GetDirectories();
            return result;
        }
        public static void Tree(string str)
        {
            int MacCountElem = (Console.LargestWindowHeight - 10);
            try
            {
                DirectoryInfo Dir1 = new DirectoryInfo(str);
                var ArrayDir1 = Dir1.GetDirectories();
                Console.WriteLine(Dir1);
                foreach (DirectoryInfo DirLayer1 in ArrayDir1)
                {
                    count = count + 1;
                    /*
                    if(count > MacCountElem)
                    {
                        Console.WriteLine("Элементов больше 40");
                        return;
                    }
                    */
                    Console.WriteLine($"|-----{DirLayer1.Name}");
                    try
                    {
                        DirectoryInfo Dir2 = new DirectoryInfo(Convert.ToString(DirLayer1));
                        var ArrayDir2 = Dir2.GetDirectories();
                        foreach (DirectoryInfo DirLayer2 in ArrayDir2)
                        {
                            count = count + 1;
                            /*
                            if (count > MacCountElem)
                            {
                                Console.WriteLine("Элементов больше 40");
                                return;
                            }
                            */
                            Console.WriteLine($"     |-----{DirLayer2.Name}");
                            try
                            {
                                DirectoryInfo Dir3 = new DirectoryInfo(Convert.ToString(DirLayer2));
                                var ArrayDir3 = Dir3.GetDirectories();
                                foreach (DirectoryInfo DirLayer3 in ArrayDir3)
                                {
                                    count = count + 1;
                                    /*
                                    if (count > MacCountElem)
                                    {
                                        Console.WriteLine("Элементов больше 40");
                                        return;
                                    }
                                    */
                                    Console.WriteLine($"          |-----{DirLayer3.Name}");
                                }
                            }
                            catch
                            {
                                FontColorRed("          |-----Нет доступ к папке");                         
                            }
                        }
                    }
                    catch
                    {
                        FontColorRed("     |-----Нет доступ к папке");                   
                    }
                }
            }

            catch
            {
                FontColorRed("Папки не существует!!!");                
            }
            count = 0;
        }

        static void LineMenu(string str)
        {
            Console.SetCursorPosition(0, 0);
            var originalpos = Console.CursorTop;
            var k = Console.ReadKey();
            var i = 0;
            while (k.KeyChar != 'q')
            {
                if (k.Key == ConsoleKey.DownArrow)
                {
                    Console.SetCursorPosition(0, Console.CursorTop + i);                         
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.WriteLine(str);
                    Console.ResetColor();
                    i++;

                }

                //Console.SetCursorPosition(0, originalpos);
                k = Console.ReadKey();
            }
        }

        static void CopyDir(string FromDir, string ToDir, string StartDirFoo)
        {
            Directory.CreateDirectory(ToDir + "\\" + FromDir);
            string newstring = (StartDirFoo + "\\" + FromDir);            
            foreach (string s1 in Directory.GetFiles(newstring))
            {
                string s2 = ToDir + "\\" + FromDir + "\\" + Path.GetFileName(s1);
                File.Copy(s1, s2);
            }
            foreach (string s in Directory.GetDirectories(newstring))
            {
                CopyDir(s, ToDir + "\\" + FromDir + "\\" + Path.GetFileName(s), StartDirFoo);
            }
        }

        public static string ChangeDirToUp(string ChangeFromDir, string ChangetoDir)
        {
            //DirectoryInfo p = new DirectoryInfo(ChangeFromDir);
            string newstring = (ChangeFromDir + "\\" + ChangetoDir);
            if(Directory.Exists(newstring))
            {
                return newstring;
            }
            else
            {
                FontColorRed("Папка не существует");
                Console.WriteLine();
                return ChangeFromDir;              
            }
           
        }

        public static string ChangeDirToDown(string ChangeFromDir)
        {            
            var VarNewstring = Directory.GetParent(ChangeFromDir);
            string newstring = Convert.ToString(VarNewstring);            
            return newstring;
        }


    }
}
