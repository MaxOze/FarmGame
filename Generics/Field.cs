using System;

namespace Generics
{
    public class Field
    {
        private Plant _plant;
        
        public Field(Plant plant)
        {
            _plant = plant;
        }

        public Type WhatPlant()
        {
            return _plant.GetType();
        }
        
        public void NextDay()
        {
            _plant?.Grow();
        }
        
        public Plant CollectHarvest()
        {
            if (WhatPlant() != typeof(Empty))
            {
                _plant.Collect();
                return _plant;
            }
            return null;
        }

        public bool IsReady()
        {
            return _plant.ReadyToCollect();
        }

        public bool IsEmpty()
        {
            return WhatPlant() == typeof(Empty);
        }
        
        public void ToEmpty()
        {
            _plant = new Empty();
        }

        public void Change(Plant plant)
        {
            if (WhatPlant() == typeof(Empty))
                _plant = plant;
            else 
                Console.Write("Грядка занята!!!");
        }
        
        public void ToConsole(char c)
        {
            var left = Console.CursorLeft;
            var top = Console.CursorTop;
            
            Console.Write("#############");
            Console.SetCursorPosition(left, ++top);

            for (var i = 0; i < 2; i++)
            {
                Console.Write("#");

                switch (_plant.GetStage())
                {
                    case 1:
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        break;
                }
                
                for (var j = 0; j < 11; j++)
                    Console.Write(c);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("#");
                Console.SetCursorPosition(left, ++top);
            }
            Console.WriteLine("#############");
        }

        public override string ToString()
        {
            return _plant.ToString();
        }
    }
}