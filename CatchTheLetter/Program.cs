using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;

namespace CatchTheLetter
{
    internal class Program
    {
        public static int direction, barSpeed , width, hight;
        public static int xPositionOld, xPosition;
        public static ConsoleKeyInfo cki;
        public static int counter, marker1;
        
        
        static void Main()
        {
            width = 100;
            hight = 45;
            xPosition = width / 2;
            xPositionOld = xPosition;
            barSpeed = 3;
            counter = 0;
            
            var timer = new Stopwatch();

            Thread AskTheKey = new Thread(AskKey);
            AskTheKey.Start();

            Thread StopMoving = new Thread(DiectionOff);
            StopMoving.Start();


            Console.WindowHeight = hight;
            Console.WindowWidth = width;
            Console.CursorLeft = xPosition;
            Console.CursorTop = hight -5 ;
            Console.Write(@"\______/");


           // public static ConsoleKeyInfo cki;

            while (true)
            {

                DrawPlayer();
                Thread.Sleep(20);
                
                timer.Start();
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
            }


            if (direction == -1)
            {
                xPosition -= barSpeed;
                if (xPosition < 1) xPosition = 1;
            }

            if (xPositionOld < 2) xPositionOld = 2;
            Console.CursorLeft = xPositionOld-1;
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

                

                while (Console.KeyAvailable == true)
                {
                    cki = Console.ReadKey(true);

                    if (cki.Key == ConsoleKey.LeftArrow) direction = -1;
                    if (cki.Key == ConsoleKey.RightArrow)  direction = 1;
                    if (cki.Key != ConsoleKey.RightArrow && cki.Key != ConsoleKey.LeftArrow) direction = 0;
                    
                }
               Thread.Sleep(20);
               
                
                //Control.KeyUp;



            }
        }

        public static void DiectionOff()
        {
            while (true)
            {
                Thread.Sleep(500);
                //if (Console.KeyAvailable == false) direction = 0;
            }
        }
    }
}
