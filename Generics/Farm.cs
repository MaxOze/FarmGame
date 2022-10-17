using System;

namespace Generics
{
    public class Farm
    {
        public readonly Field[,] Fields;
        private int _day;
        public readonly Player Player;
        private readonly Logger _logger;

        public Farm(Player player)
        {
            Player = player;
            _logger = new Logger();
            _logger.Notify += LoggerMethods.LogInFile;
            _logger.Notify += LoggerMethods.LogInConsole;
            Fields = new Field[5,5];
            
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Fields[i, j] = new Field(new Empty());
                }
            }
        }

        public int GetDay()
        {
            return _day;
        }
        
        public void NextDay()
        {
            _day++;
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    Fields[i, j].NextDay();
                }
            }
            Console.SetCursorPosition(196, 2);
            Console.Write($"День {_day}");
        }
    
        public int SelectedFieldStatus(int x, int y)
        {
            try
            {
                var selectedField = Fields[x, y];
                Console.SetCursorPosition(120, 2);

                if (selectedField.IsEmpty())
                    return 0;
                if (selectedField.IsReady())
                    return 2;

                return 1;
            }
            catch (Exception e)
            {
                _logger.OnNotify(e.Message);
            }

            return 1;
        }

        public bool PlantField(Plant plant, int x, int y)
        {
            var selectedField = Fields[x, y];

            var what = plant.GetType();

            if (what != typeof(Empty))
            {
                if (what == typeof(Fruit))
                {
                    if (Player.FruitBox.GetSeeds() > 0)
                    {
                        Player.FruitBox.UseSeed();
                        selectedField.Change(plant);
                        return true;
                    }
                }
                else if (what == typeof(Vegetable))
                {
                    if (Player.VegetableBox.GetSeeds() > 0)
                    {
                        Player.VegetableBox.UseSeed();
                        selectedField.Change(plant);
                        return true;
                    }
                }
                else if (what == typeof(Berry))
                {
                    if (Player.BerryBox.GetSeeds() > 0)
                    {
                        Player.BerryBox.UseSeed();
                        selectedField.Change(plant);
                        return true;
                    }
                }
            }

            return false;
        }

        public void CollectField(int x, int y)
        {
            var selectedField = Fields[x, y];
            bool check = false;

            var plant = selectedField.WhatPlant();

            if (plant == typeof(Fruit) && Player.FruitBox.Count() < 10)
            {
                Player.FruitBox.ToTheBox((Fruit)selectedField.CollectHarvest());
                check = true;
            }
            else if (plant == typeof(Vegetable) && Player.VegetableBox.Count() < 10)
            {
                Player.VegetableBox.ToTheBox((Vegetable)selectedField.CollectHarvest());
                check = true;
            }
            else if (plant == typeof(Berry) && Player.BerryBox.Count() < 10)
            {
                Player.BerryBox.ToTheBox((Berry)selectedField.CollectHarvest());
                check = true;
            }

            if (check)
            {
                _logger.OnNotify($"Player collect {selectedField}");
                selectedField.ToEmpty();
            }
        }

        public void ToConsole()
        {
            var x = 80;
            var y = 3;
            var top = -4;
            
            for (var i = 0; i < 5; i++)
            {
                Console.SetCursorPosition(x, y);
                
                for (var j = 0; j < 5; j++)
                {
                    Type plant = Fields[i, j].WhatPlant();
                    Console.SetCursorPosition(x, top + 7);

                    if (plant == typeof(Fruit))
                    {
                        Fields[i, j].ToConsole('@');
                    }
                    else if (plant == typeof(Vegetable))
                    {
                        Fields[i, j].ToConsole('&');
                    }
                    else if (plant == typeof(Berry))
                    {
                        Fields[i, j].ToConsole('%');
                    }
                    else
                    {
                        Fields[i, j].ToConsole(' ');
                    }
                    
                    top = Console.CursorTop;
                }

                x += 15;
                y = 3;
                top = -4;
            }
            Console.CursorVisible = false;
        }

        public void HarvestToConsole()
        {
            var fruitHarvest = Player.FruitBox.GetScore() * 2;
            var vegetableHarvest = Player.VegetableBox.GetScore() * 3;
            var berryHarvest = Player.BerryBox.GetScore();

            Console.SetCursorPosition(196, 4);
            Console.Write($"Фруктов в корзине: {fruitHarvest} штук   ");
            Console.SetCursorPosition(196, 5);
            Console.Write($"Овощей в корзине:  {vegetableHarvest} штук   ");
            Console.SetCursorPosition(196, 6);
            Console.Write($"Ягод в корзине:    {berryHarvest} штук   ");

            Console.SetCursorPosition(196, 8);
            Console.Write($"Семена фруктов:    {Player.FruitBox.GetSeeds()} штук   ");
            Console.SetCursorPosition(196, 9);
            Console.Write($"Семена овощей:     {Player.VegetableBox.GetSeeds()} штук   ");
            Console.SetCursorPosition(196, 10);
            Console.Write($"Семена ягод:       {Player.BerryBox.GetSeeds()} штук   ");

            Console.SetCursorPosition(196, 12);
            Console.Write($"На счету {Player.Money}$   ");

            if (Player.Money > 100)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.BackgroundColor = ConsoleColor.White;
                Console.SetCursorPosition(167, 25);
                Console.Write($"Вы победили!!!");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        public void ScoreToConsole()
        {
            int y;
            var left = 206;
            var top = 31;
            foreach (Fruit fruit in Player.FruitBox.Plants)
            {
                Console.SetCursorPosition(left, ++top);
                Console.Write(fruit.GetScore());
            }

            var count = Player.FruitBox.Count();
            if (count < 10)
            {
                y = 10 - count;
                for (var i = 0; i < y; i++)
                {
                    Console.SetCursorPosition(left, ++top);
                    Console.Write("   ");
                }
            }

            left = 214;
            top = 31;
            foreach (Vegetable vegetable in Player.VegetableBox.Plants)
            {
                Console.SetCursorPosition(left, ++top);
                Console.Write(vegetable.GetScore());
            }
            
            count = Player.VegetableBox.Count();
            if (count < 10)
            {
                y = 10 - count;
                for (var i = 0; i < y; i++)
                {
                    Console.SetCursorPosition(left, ++top);
                    Console.Write("   ");
                }
            }
            
            left = 222;
            top = 31;
            foreach (Berry berry in Player.BerryBox.Plants)
            {
                Console.SetCursorPosition(left, ++top);
                Console.Write(berry.GetScore());
            }
            
            count = Player.BerryBox.Count();
            if (count < 10)
            {
                y = 10 - count;
                for (var i = 0; i < y; i++)
                {
                    Console.SetCursorPosition(left, ++top);
                    Console.Write("   ");
                }
            }
        }
        
        public void OkToConsole()
        {
            int y;
            var left = 206;
            var top = 31;
            foreach (Fruit fruit in Player.FruitBox.Plants)
            {
                Console.SetCursorPosition(left, ++top);
                Console.Write(fruit.GetOk());
            }

            var count = Player.FruitBox.Count();
            if (count < 10)
            {
                y = 10 - count;
                for (var i = 0; i < y; i++)
                {
                    Console.SetCursorPosition(left, ++top);
                    Console.Write("   ");
                }
            }

            left = 214;
            top = 31;
            foreach (Vegetable vegetable in Player.VegetableBox.Plants)
            {
                Console.SetCursorPosition(left, ++top);
                Console.Write(vegetable.GetOk());
            }
            
            count = Player.VegetableBox.Count();
            if (count < 10)
            {
                y = 10 - count;
                for (var i = 0; i < y; i++)
                {
                    Console.SetCursorPosition(left, ++top);
                    Console.Write("   ");
                }
            }
            
            left = 222;
            top = 31;
            foreach (Berry berry in Player.BerryBox.Plants)
            {
                Console.SetCursorPosition(left, ++top);
                Console.Write(berry.GetOk());
            }
            
            count = Player.BerryBox.Count();
            if (count < 10)
            {
                y = 10 - count;
                for (var i = 0; i < y; i++)
                {
                    Console.SetCursorPosition(left, ++top);
                    Console.Write("   ");
                }
            }
        }
    }
}