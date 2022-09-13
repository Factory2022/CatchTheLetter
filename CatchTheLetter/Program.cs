using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Media;

namespace CatchTheLetter
{
    internal class Program
    {
        public static int direction, barSpeed, width, hight;
        public static int xPositionOld, xPosition;
        public static int lifes, score, hiscore, level, round, fallSpeed;
        public static int count;

        public static int numberOfLetters = 30;
        public static int[,] letters = new int[numberOfLetters, 6];     // falling letters to catch
        public static string[] words = new string[50];                  // words to seek
        public static string[,] bigLetters = new string[26, 8];         // To display big letters on the console
        public static string[,] wordsToQuest = new string[5,10];        // 5 words eatch level - 10 level now
        public static string myWord;
       

        public static ConsoleKeyInfo cki;
        public static Random rnd = new Random();
        
        public static SoundPlayer sound1 = new SoundPlayer("01.wav");

        static void Main()
        {
            width = 100;
            hight = 60;
            xPosition = width / 2;
            xPositionOld = xPosition;
            barSpeed = 3;

            LoadBigLetters();

            
           

            Thread AskTheKey = new Thread(AskKey);
            AskTheKey.Start();

            

            wordsToQuest[0, 0] = "HUND";        // level 1
            wordsToQuest[1, 0] = "MAUS";
            wordsToQuest[2, 0] = "ELFE";
            wordsToQuest[3, 0] = "AUTO";
            wordsToQuest[4, 0] = "KIND";



            NewGame();  // call NewGame
            InGame();   // start theh Game




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
            Console.Write("           ");


            Console.CursorLeft = xPosition;
            Console.CursorTop = hight - 5;
            Console.Write(@"\______/");

            xPositionOld = xPosition;




        }

        public static void AskKey()
        {

            while (true)
            {

                cki = Console.ReadKey(true);

                if (cki.Key == ConsoleKey.LeftArrow) direction = -1;
                if (cki.Key == ConsoleKey.RightArrow) direction = 1;

                Thread.Sleep(20);

            }
        }

        public static void InGame()
        {
            var timer = new Stopwatch();
            timer.Start();

            while (lifes > 0)
            {

                DrawPlayer();
                MoveLetters();
                ShowGameValues();

                Thread.Sleep(20);

                

                TimeSpan timeTaken = timer.Elapsed;
                // Console.WriteLine(timeTaken);

            }
        }
        public static void NewGame()
        {
            lifes = 3;
            score = 0;
            level = 1;
            round = 1;
            fallSpeed = 2;
            numberOfLetters = 10;

            myWord = "";
            
            for (int i = 0; i < wordsToQuest[round - 1, level - 1].Length; i++) 
            {
                myWord += "-";
            }
            
            Console.Clear();

            Console.CursorLeft = 10;
            Console.CursorTop  = 2;
            Console.Write(" PUNKTE : ");

            Console.CursorLeft = 30;
            Console.Write(" STUFE : ");

            Console.CursorLeft = 50;
            Console.Write(" RUNDE : ");

            Console.CursorLeft = 70;
            Console.Write(" LEBEN : ");

            Console.CursorLeft = 10;
            Console.CursorTop = 6;
            Console.Write(" WORT ZU FANGEN       : ");

            Console.CursorLeft = 10;
            Console.CursorTop = 8;
            Console.Write(" GEFANGENE BUCHSTABEN : ");

            Console.CursorLeft=0;
            Console.CursorTop=9;
            for (int i = 0; i< width; i++) Console.Write("_");

            //Random rnd = new Random();
            for (int i = 0; i < numberOfLetters; i++)
            {
                NewLetter(i);
                letters[i, 5] = rnd.Next(fallSpeed + 1);
            }

            Console.WindowHeight = hight;
            Console.WindowWidth = width;
            Console.CursorLeft = xPosition;
            Console.CursorTop = hight - 5;
            Console.Write(@"\______/");

        }

        public static void ShowGameValues()
        {

            Console.CursorLeft = 20;
            Console.CursorTop = 2;
            
            Console.Write(score+" ");


            Console.CursorLeft = 40;
            Console.Write(level+" ");

            Console.CursorLeft = 60;
            Console.Write(round +" ");

            Console.CursorLeft = 80;
            Console.Write(lifes+" ");

            Console.CursorLeft = 40;
            Console.CursorTop = 6;
            Console.Write(wordsToQuest[round-1,level-1]);

            Console.CursorLeft = 40;
            Console.CursorTop = 8;
            Console.Write(myWord);
        }
        public static void MoveLetters()
        {
            Thread.Sleep(20);

            if (count >= 1)
            {
                for (int i = 0; i < numberOfLetters; i++)
                {
  
                        for (int j = 0; j < 7; j++)
                        {
                            // Clear Space used from old Pposition
                            if (letters[i, 2] + j <= hight && letters[i, 2] + j >= 0)
                            {

                                if (letters[i, 2] + j -1 >= 10)                         //>= 0    space from uppper border
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
                            
                            if (letters[i, 2] + j < hight-1 && letters[i, 2] +j >= 10)     // >=0  space from uppper border
                        {
                            
                                Console.CursorLeft = letters[i, 1];         // X
                                Console.CursorTop = letters[i, 2] + j;
                                Console.Write(bigLetters[letters[i, 0] - 65, j]);
                            
                            }
                        }
                        
                    
                   
                    letters[i, 2] += 1; // letters[i, 5]; fallSpeed
                 
                    if (letters[i, 2] > hight +5)
                    {
                        NewLetter(i);
                        sound1.Play();
                    }

                }
                count = 0;
            }
            count++;
        }

        // Load letters from file
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
            int testX = 0, testY =0;
            for (int j = 0; j < numberOfLetters; j++)
            {
                if (letters[j, 1] > letters[i, 1] - 10 && letters[j, 1] < letters[i, 1] + 10 && i != j) testX = 1;
                if (letters[j, 2] > letters[i, 2] - 10 && letters[j, 2] < letters[i, 2] + 10 && i != j) testY = 1;

                if (testY==1 && testX==1)
                {
                    letters[i, 1] = rnd.Next(width / 10 - 1) * 10;
                    letters[i, 2] = rnd.Next(60) - 65;
                    testX = 0;
                    testY = 0;

                }
            }

           
        }
    }

    
}

