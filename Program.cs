using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChristmasTree
{
    class Program
    {
        static void Main(string[] args)
        {
            const int SCREEN_WIDTH = 120;
            const int SCREEN_HEIGHT = 40;
            const int LINES_BEFORE_TREE = 3; //Ab welcher Zeile beginnt der Baum
            const int TRUNK_OF_TREE = 4; //Baumstammhöhe

            char[,] world = new char[SCREEN_HEIGHT, SCREEN_WIDTH];
            Random random=new Random();

            Console.SetWindowSize(SCREEN_WIDTH, SCREEN_HEIGHT);
            Console.CursorVisible = false;
            Console.BackgroundColor=ConsoleColor.Gray;
            

            //Baum in Array zeichnen:
            for (int line = LINES_BEFORE_TREE; line < world.GetLength(0)-TRUNK_OF_TREE; line++) //geht nicht bis ganz unten 
            {
                for (int col = world.GetLength(1)/2-(line-LINES_BEFORE_TREE); col <= world.GetLength(1)/2+(line-LINES_BEFORE_TREE); col++)
                {
                    world[line, col] = '#';
                }
            }

            //Fuss des Baumes
            for (int line = world.GetLength(0)-TRUNK_OF_TREE; line < world.GetLength(0); line++) //geht nicht bis ganz unten 
            {
                for (int col = world.GetLength(1) / 2 - 1; col <= world.GetLength(1) / 2 + 1; col++)
                {
                    world[line, col] = '|';
                }
            }
            //Welt am Bildschirm zeichnen 
            for (int line = 0; line < world.GetLength(0); line++) //geht nicht bis ganz unten 
            {
                for (int col = 0; col < world.GetLength(1); col++)
                {

                    switch (world[line, col])
                    {
                        case '#':
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case '|':
                            Console.ForegroundColor = ConsoleColor.Black;
                            break;
                    }
                    Console.SetCursorPosition(col, line);
                    Console.Write(world[line, col]);

                }
            }
            while (true)  //Endlosschleife
            {
                Thread.Sleep(50);
                Console.ForegroundColor = ConsoleColor.White;
                //Neue Schneeflocke in Zeile 1 in zufälliger Spalte platzieren
                world[0, random.Next(world.GetLength(1))] = '*';

                //Von unten nach oben alle Schneeflocken um eins nach unten verschieben, außer unter der Flocke befindet sich kein \0
                for (int line = world.GetLength(0)-2; line >= 0; line--) //Unterste Zeile muss nicht beachtet werden, da Schneeflocken nicht weiter fallen können -> daher -2!!
                {
                    for (int col = 0; col < world.GetLength(1); col++)
                    {
                       if (world[line,col]=='*' && world[line+1,col]=='\0')
                       {
                            Console.SetCursorPosition(col, line);
                            Console.Write(' ');
                            world[line, col] = '\0';
                           world[line + 1, col] = '*';
                            Console.SetCursorPosition(col, line+1);
                            Console.Write('*');
                        }
                    }
                }
            }
        }
    }
}
