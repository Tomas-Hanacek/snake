using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            AppWindow appWindow = new AppWindow(32,16);
            Random randomnummer = new Random();
            int score = 5;
            int gameover = 0;
            Pixel snakeHead = new Pixel(appWindow.GetWidth()/2,appWindow.GetHeight()/2,ConsoleColor.Red);
            string movement = "RIGHT";
            List<int> xposlijf = new List<int>();
            List<int> yposlijf = new List<int>();
            int berryx = randomnummer.Next(0, appWindow.GetWidth());
            int berryy = randomnummer.Next(0, appWindow.GetHeight());
            DateTime tijd = DateTime.Now;
            DateTime tijd2 = DateTime.Now;
            string buttonpressed = "no";
            while (true)
            {
                Console.Clear();
                if (snakeHead.GetXpos() == appWindow.GetWidth()-1 || snakeHead.GetXpos() == 0 ||snakeHead.GetYpos() == appWindow.GetHeight()-1 || snakeHead.GetYpos() == 0)
                { 
                    gameover = 1;
                }
                for (int i = 0;i< appWindow.GetWidth(); i++)
                {
                    Console.SetCursorPosition(i, 0);
                    Console.Write("■");
                }
                for (int i = 0; i < appWindow.GetWidth(); i++)
                {
                    Console.SetCursorPosition(i, appWindow.GetHeight() -1);
                    Console.Write("■");
                }
                for (int i = 0; i < appWindow.GetHeight(); i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.Write("■");
                }
                for (int i = 0; i < appWindow.GetHeight(); i++)
                {
                    Console.SetCursorPosition(appWindow.GetWidth() - 1, i);
                    Console.Write("■");
                }
                Console.ForegroundColor = ConsoleColor.Green;
                if (berryx == snakeHead.GetXpos() && berryy == snakeHead.GetYpos())
                {
                    score++;
                    berryx = randomnummer.Next(1, appWindow.GetWidth()-2);
                    berryy = randomnummer.Next(1, appWindow.GetHeight()-2);
                } 
                for (int i = 0; i < xposlijf.Count(); i++)
                {
                    Console.SetCursorPosition(xposlijf[i], yposlijf[i]);
                    Console.Write("■");
                    if (xposlijf[i] == snakeHead.GetXpos() && yposlijf[i] == snakeHead.GetYpos())
                    {
                        gameover = 1;
                    }
                }
                if (gameover == 1)
                {
                    break;
                }
                Console.SetCursorPosition(snakeHead.GetXpos(), snakeHead.GetYpos());
                Console.ForegroundColor = snakeHead.GetColor();
                Console.Write("■");
                Console.SetCursorPosition(berryx, berryy);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("■");
                tijd = DateTime.Now;
                buttonpressed = "no";
                while (true)
                {
                    tijd2 = DateTime.Now;
                    if (tijd2.Subtract(tijd).TotalMilliseconds > 500) { break; }
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo toets = Console.ReadKey(true);
                        //Console.WriteLine(toets.Key.ToString());
                        if (toets.Key.Equals(ConsoleKey.UpArrow) && movement != "DOWN" && buttonpressed == "no")
                        {
                            movement = "UP";
                            buttonpressed = "yes";
                        }
                        if (toets.Key.Equals(ConsoleKey.DownArrow) && movement != "UP" && buttonpressed == "no")
                        {
                            movement = "DOWN";
                            buttonpressed = "yes";
                        }
                        if (toets.Key.Equals(ConsoleKey.LeftArrow) && movement != "RIGHT" && buttonpressed == "no")
                        {
                            movement = "LEFT";
                            buttonpressed = "yes";
                        }
                        if (toets.Key.Equals(ConsoleKey.RightArrow) && movement != "LEFT" && buttonpressed == "no")
                        {
                            movement = "RIGHT";
                            buttonpressed = "yes";
                        }
                    }
                }
                xposlijf.Add(snakeHead.GetXpos());
                yposlijf.Add(snakeHead.GetYpos());
                switch (movement)
                {
                    case "UP":
                        snakeHead.SetYpos(snakeHead.GetYpos() - 1);
                        break;
                    case "DOWN":
                        snakeHead.SetYpos(snakeHead.GetYpos() + 1);
                        break;
                    case "LEFT":
                        snakeHead.SetXpos(snakeHead.GetXpos() - 1);
                        break;
                    case "RIGHT":
                        snakeHead.SetXpos(snakeHead.GetXpos() + 1);
                        break;
                }
                if (xposlijf.Count() > score)
                {
                    xposlijf.RemoveAt(0);
                    yposlijf.RemoveAt(0);
                }
            }
            Console.SetCursorPosition(appWindow.GetWidth() / 5, appWindow.GetHeight() / 2);
            Console.WriteLine("Game over, Score: "+ score);
            Console.SetCursorPosition(appWindow.GetWidth() / 5, appWindow.GetHeight() / 2 +1);
        }
        class Pixel
        {
            private int _xPos;
            private int _yPos;
            private ConsoleColor _color;

            public Pixel(int xPos, int yPos, ConsoleColor color)
            {
                _xPos = xPos;
                _yPos = yPos;
                _color = color;
            }

            public int GetXpos() { return _xPos; }
            public void SetXpos(int xPos) { _xPos = xPos; }
            public int GetYpos() { return _yPos; }
            public void SetYpos(int yPos) { _yPos = yPos; }
            public ConsoleColor GetColor() { return _color; }
            public void SetColor(ConsoleColor color) { _color = color; }
        }

        private class AppWindow
        {
            public AppWindow(int width, int height)
            {
                Console.WindowWidth = width;
                Console.WindowHeight = height;
            }

            public int GetWidth() { return Console.WindowWidth; }
            public int GetHeight() { return Console.WindowHeight; }
            
        }
    }
}
