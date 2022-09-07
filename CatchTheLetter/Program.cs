using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CatchTheLetter
{
    internal class Program
    {
        public static int direction, barSpeed , width, hight;
        public static int xPositionOld, xPosition;
        static void Main(string[] args)
        {
            width = 100;
            hight = 45;
            xPosition = width / 2;
            xPositionOld = xPosition;
            barSpeed = 4;


            Console.WindowHeight = hight;
            Console.WindowWidth = width;

            Console.CursorLeft = xPosition;
            Console.CursorTop = hight - 5;
            Console.Write(@"\______/");


            // Thread für die Tastatureingab erstellen
            Thread KeyInmput = new Thread(AskAKey);
            KeyInmput.Start();

            while (true)
            {
                Thread.Sleep(20);
                
                if (direction == 1)
                {
                    xPosition += barSpeed;
                    if (xPosition > width-9) xPosition = width-9;
                    
                }
                
                
                if (direction == -1)
                {
                    xPosition -= barSpeed;
                    if (xPosition < 1) xPosition = 1;
                }
                
                Console.CursorLeft = xPositionOld;
                Console.CursorTop = hight - 5;
                if (direction != 0) Console.Write("        ");
                    

                Console.CursorLeft = xPosition;
                // Console.CursorTop  = hight-5;
                if (direction != 0) Console.Write(@"\______/");

                xPositionOld = xPosition;
                Console.CursorLeft = 1;
                Console.CursorTop = hight;

                //direction = 0;
            }

            Console.ReadKey();

        }

        public static void AskAKey()           // Get Key Methode
        {
            ConsoleKeyInfo c = new ConsoleKeyInfo();

            while(true)
            {
                Thread.Sleep(20);          // Zeit verschwenden und Prozessorlast senken...
               
                
                c = Console.ReadKey(true);
                string test = Convert.ToString(c.Key);
                if (test == "LeftArrow")    direction = -1;
                if (test == "RightArrow")   direction = 1;
               

            }


        }
    }
}
