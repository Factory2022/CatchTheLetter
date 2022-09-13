using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace CatchTheLetter
{
    internal class Program
    {
        public static int direction, barSpeed, width, hight;
        public static int xPositionOld, xPosition;
        public static int lifes, score, hiscore, level, fallSpeed;
        public static int count;

        public static int numberOfLetters = 30;
        public static int[,] letters = new int[numberOfLetters, 6];     // falling letters to catch
        public static string[] words = new string[50];                  // words to seek
        public static string[,] bigLetters = new string[26, 8];         // To display big letters on the console

        public static ConsoleKeyInfo cki;
        public static Random rnd = new Random();


        static void Main()
        {
            width = 100;
            hight = 60;
            xPosition = width / 2;
            xPositionOld = xPosition;
            barSpeed = 3;

            LoadBigLetters();

            var timer = new Stopwatch();
            timer.Start();

            Thread AskTheKey = new Thread(AskKey);
            AskTheKey.Start();


            NewGame(); // call NewGame


            Console.WindowHeight = hight;
            Console.WindowWidth = width;
            Console.CursorLeft = xPosition;
            Console.CursorTop = hight - 5;
            Console.Write(@"\______/");





            while (true)
            {

                DrawPlayer();
                MoveLetters();

                Thread.Sleep(20);

                //B: Run stuff you want timed
                //timer.Stop();

                TimeSpan timeTaken = timer.Elapsed;
                // Console.WriteLine(timeTaken);

            }
        }


        public static void DrawPlayer()
        {

            if (direction == 1)
            {
                xPosition += barSpeed;
                if (xPosition > width - 9) xPosition = width - 9;
                direction = 0;
            }


            if (direction == -1)
            {
                xPosition -= barSpeed;
                if (xPosition < 1) xPosition = 1;
                direction = 0;
            }

            if (xPositionOld < 2) xPositionOld = 2;
            Console.CursorLeft = xPositionOld - 1;
            Console.CursorTop = hight - 5;
            //if (direction != 0) Console.Write("           ");
            Console.Write("           ");


            Console.CursorLeft = xPosition;
            Console.CursorTop = hight - 5;
            //if (direction != 0) Console.Write(@"\______/");
            Console.Write(@"\______/");

            xPositionOld = xPosition;

            Console.CursorLeft = 1;
            Console.CursorTop = hight - 1;



        }

        public static void AskKey()
        {

            while (true)
            {

                cki = Console.ReadKey(true);

                if (cki.Key == ConsoleKey.LeftArrow) direction = -1;
                if (cki.Key == ConsoleKey.RightArrow) direction = 1;



                Thread.Sleep(20);


                //Control.KeyUp;



            }
        }

        public static void NewGame()
        {
            lifes = 3;
            score = 0;
            level = 1;
            fallSpeed = 2;
            numberOfLetters = 10;

            //Random rnd = new Random();
            for (int i = 0; i < numberOfLetters; i++)
            {
                NewLetter(i);
                /*
                letters[i, 0] = rnd.Next(65, 90);
                

                letters[i, 1] = rnd.Next(width/10 - 1) *10;            // X-Pos
                letters[i, 2] = (rnd.Next(60) - 65);              // Y-Pos
                letters[i, 3] = letters[i, 1];                  // old x-Pos
                letters[i, 4] = letters[i, 2];                  // old x-Pos
                */
                letters[i, 5] = rnd.Next(fallSpeed + 1);
                
            }

        }
        public static void MoveLetters()
        {
            Thread.Sleep(20);

            if (count >= 2)
            {
                for (int i = 0; i < numberOfLetters; i++)
                {


                   
                        for (int j = 0; j < 7; j++)
                        {
                            // Clear Space used from old Pposition
                            if (letters[i, 2] + j <= hight && letters[i, 2] + j >= 0)
                            {

                                if (letters[i, 2] + j -1 >= 0)
                                {
                                    Console.CursorLeft = letters[i, 1];
                                    Console.CursorTop = letters[i, 2] + j - 1;
                                    Console.Write("         ");
                                }
                           
                        }

                        }

                    // Paint Letters
                    for (int j = 0; j < 7; j++)   
                        { 
                            
                            if (letters[i, 2] + j < hight-1 && letters[i, 2] +j >= 0)     // >=0
                            {
                            
                                Console.CursorLeft = letters[i, 1];         // X
                                Console.CursorTop = letters[i, 2] + j;
                                Console.Write(bigLetters[letters[i, 0] - 65, j]);
                            
                            }
                        }
                        
                    
                   
                    letters[i, 2] += 1; // letters[i, 5];
                 
                    if (letters[i, 2] > hight +5)
                    {
                        NewLetter(i);
                    }

                }
                count = 0;
            }
            count++;
        }

        public static void LoadBigLetters()
        {
            StreamReader sr1 = new StreamReader("bigLetters.txt");
            for (int i = 0; i < 26; i++)
            {


                for (int j = 0; j < 7; j++)
                {
                    bigLetters[i, j] = sr1.ReadLine();
                }
                sr1.ReadLine();
            }
            sr1.Close();

        }

        // New Letter at random position 
        public static void NewLetter(int i)
        {
            letters[i, 0] = rnd.Next(65, 90);                   // new letter
            letters[i, 1] = rnd.Next(width / 10 - 1) * 10;      // X-Pos
            letters[i, 2] = rnd.Next(60) - 65;                  // Y-Pos

            // X-pos overlap ?
            for (int j = 0; j < numberOfLetters; j++)
            {
                if (letters[j, 1] > letters[i,1] -10 && letters[j, 1] < letters[i, 1] +10 && i!=j) letters[i, 1] = rnd.Next(width / 10 - 1) * 10;
            }

            //Y - position overlap ?
            for (int j = 0; j < 7; j++)
            {
                if (letters[j, 2] > letters[i, 2] - 10 && letters[j, 2] < letters[i, 2] + 10 && i != j) letters[i, 2] = rnd.Next(60) - 65; ;
            }
        }
    }

    
}

