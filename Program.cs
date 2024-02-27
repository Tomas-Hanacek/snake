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
            Random random = new Random();
            int score = 5;
            int gameover = 0;
            Pixel snakeHead = new Pixel(appWindow.GetWidth()/2,appWindow.GetHeight()/2,ConsoleColor.Red);
            //Snake snake = new Snake(snakeHead,);
            Movement movement = Movement.Right;
            List<int> xPosBody = new List<int>();
            List<int> yPosBody = new List<int>();
            Pixel food = new Pixel(random.Next(0, appWindow.GetWidth()), random.Next(0, appWindow.GetHeight()));
            DateTime time;
            DateTime time2;
            var buttonpressed = false;
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
                if (food.GetXpos() == snakeHead.GetXpos() && food.GetYpos() == snakeHead.GetYpos())
                {
                    score++;
                    food.SetXpos(random.Next(1, appWindow.GetWidth()-2));
                    food.SetYpos(random.Next(1, appWindow.GetHeight()-2));
                } 
                for (int i = 0; i < xPosBody.Count(); i++)
                {
                    Console.SetCursorPosition(xPosBody[i], yPosBody[i]);
                    Console.Write("■");
                    if (xPosBody[i] == snakeHead.GetXpos() && yPosBody[i] == snakeHead.GetYpos())
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
                Console.SetCursorPosition(food.GetXpos(), food.GetYpos());
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("■");
                time = DateTime.Now;
                buttonpressed = false;
                while (true)
                {
                    time2 = DateTime.Now;
                    if (time2.Subtract(time).TotalMilliseconds > 500) { break; }
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo toets = Console.ReadKey(true);
                        if (toets.Key.Equals(ConsoleKey.UpArrow) && movement != Movement.Down && buttonpressed == false)
                        {
                            movement = Movement.Up;
                            buttonpressed = true;
                        }
                        if (toets.Key.Equals(ConsoleKey.DownArrow) && movement != Movement.Up && buttonpressed == false)
                        {
                            movement = Movement.Down;
                            buttonpressed = true;
                        }
                        if (toets.Key.Equals(ConsoleKey.LeftArrow) && movement != Movement.Right && buttonpressed == false)
                        {
                            movement = Movement.Left;
                            buttonpressed = true;
                        }
                        if (toets.Key.Equals(ConsoleKey.RightArrow) && movement != Movement.Left && buttonpressed == false)
                        {
                            movement = Movement.Right;
                            buttonpressed = true;
                        }
                    }
                }
                xPosBody.Add(snakeHead.GetXpos());
                yPosBody.Add(snakeHead.GetYpos());
                switch (movement)
                {
                    case Movement.Up:
                        snakeHead.SetYpos(snakeHead.GetYpos() - 1);
                        break;
                    case Movement.Down:
                        snakeHead.SetYpos(snakeHead.GetYpos() + 1);
                        break;
                    case Movement.Left:
                        snakeHead.SetXpos(snakeHead.GetXpos() - 1);
                        break;
                    case Movement.Right:
                        snakeHead.SetXpos(snakeHead.GetXpos() + 1);
                        break;
                }
                if (xPosBody.Count() > score)
                {
                    xPosBody.RemoveAt(0);
                    yPosBody.RemoveAt(0);
                }
            }
            Console.SetCursorPosition(appWindow.GetWidth() / 5, appWindow.GetHeight() / 2);
            Console.WriteLine("Game over, Score: "+ score);
            Console.SetCursorPosition(appWindow.GetWidth() / 5, appWindow.GetHeight() / 2 +1);
        }

        class Snake
        {
            private Pixel _snakeHead;
            private List<Pixel> _snakeBody;

            public Snake(Pixel head, List<Pixel> body)
            {
                _snakeHead = head;
                _snakeBody = body;
            }

            public Pixel GetHead() { return _snakeHead; }
            public List<Pixel> GetBody() { return _snakeBody; }
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
            
            public Pixel(int xPos, int yPos)
            {
                _xPos = xPos;
                _yPos = yPos;
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
        
        public enum Movement
        {
            Left,
            Right,
            Up,
            Down
        }

    }
}
