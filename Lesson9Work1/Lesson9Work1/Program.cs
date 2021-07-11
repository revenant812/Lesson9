using System;
using System.IO;


namespace Lesson9Work1
{
    class Program
    {
        static int count = 0;
        public static string StartString = @"C:\";
        
        static void Main(string[] args)
        {
           
            TreeLite(StartString);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            while(true)
            {
                Console.WriteLine();
                FontColorGreen("Введите команду: ");
                string command = Console.ReadLine();
                ParserCom(command);
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

        static void FontColorYellow(string str)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(str);
            Console.ResetColor();
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
                    Console.WriteLine($"|-----{DirLayer1.Name}");
                    try
                    {
                        DirectoryInfo Dir2 = new DirectoryInfo(Convert.ToString(DirLayer1));
                        var ArrayDir2 = Dir2.GetDirectories();
                        foreach (DirectoryInfo DirLayer2 in ArrayDir2)
                        {
                            count = count + 1;                           
                            Console.WriteLine($"     |-----{DirLayer2.Name}");
                            try
                            {
                                DirectoryInfo Dir3 = new DirectoryInfo(Convert.ToString(DirLayer2));
                                var ArrayDir3 = Dir3.GetDirectories();
                                foreach (DirectoryInfo DirLayer3 in ArrayDir3)
                                {
                                    count = count + 1;                                   
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
                
                foreach (FileInfo file1 in Dir1.GetFiles())
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"|{file1.Name}");
                    Console.ResetColor();
                }
                

            }

            catch
            {
                FontColorRed("Папки не существует!!!");                
            }
            count = 0;
        }

        public static void TreeLite(string str)
        {
            DirectoryInfo Dir = new DirectoryInfo(str);
            var ArrayDir = Dir.GetDirectories();
            Console.WriteLine(Dir);
            foreach (DirectoryInfo DirLayer1 in ArrayDir)
            {

                Console.WriteLine($"|-----{DirLayer1.Name}");
            }
            //Directory.GetFiles(str);
            
            foreach(FileInfo file in Dir.GetFiles())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;                
                Console.WriteLine($"|{file.Name}");             
                Console.ResetColor();
            }
            
        }
              
        static void CopyDir(string FromDir, string ToDir)
        {
            Directory.CreateDirectory(ToDir);                        
            foreach (string s1 in Directory.GetFiles(FromDir))
            {               
                string s2 = ToDir + "\\" + Path.GetFileName(s1);
                File.Copy(s1, s2);
            }
            foreach (string s in Directory.GetDirectories(FromDir))
            {
                CopyDir(s, ToDir + "\\" + Path.GetFileName(s));                
            }
        }

        public static string ChangeDirToUp(string ChangeFromDir, string ChangetoDir)
        {
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

        public static void StatInfo(string str, string str1)
        {
            DirectoryInfo Dir = new DirectoryInfo(str);
            foreach (FileInfo file in Dir.GetFiles())
            {
                if(file.Name.Equals(str1))
                {
                    Console.WriteLine(file.CreationTime);
                    Console.WriteLine(file.Length + " " + "byte");                  
                }
            }
        }

        public static void ParserCom(string FullCom)
        {
            
            string[] array = FullCom.Split(' ');
            if(array[0].Equals("ls"))
            {
                Console.WriteLine();
                Tree(StartString);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
            }
            else if(array[0].Equals("cd"))
            {

                string ChangeStr = array[1];
                StartString = ChangeDirToUp(StartString, ChangeStr);
                Tree(StartString);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
            }
            else if(array[0].Equals(".."))
            {
                Console.WriteLine();
                StartString = ChangeDirToDown(StartString);
                Tree(StartString);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
            }
            else if(array[0].Equals("copy"))
            {
                array[1] = StartString + "\\" + array[1];                
                try
                {
                    CopyDir(array[1], array[2]);                  
                }
                catch
                {
                    Console.WriteLine("Ошибка в пути к папке!");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                }
                Tree(StartString);
            }
            else if (array[0].Equals("stat"))
            {
                StatInfo(StartString, array[1]);
                //Tree(StartString);
            }
            else
            {
                Console.WriteLine("Не правильная команда.");
            }
        }
    }
}
