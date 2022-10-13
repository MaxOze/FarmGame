using System;


namespace Generics
{
    public class Game
    {
        private readonly Farm _farm;
        private readonly Logger _logger;

        public Game(Farm farm)
        {
            _farm = farm;
            _logger = new Logger();
            _logger.Notify += LoggerMethods.LogInFile;
        }

        public void StartGame()
        {
            Console.Title = "Farm game";
            Map.ToConsole();
            _logger.OnNotify($"Game started");
            _logger.Notify += LoggerMethods.LogInConsole;
            _farm.ToConsole();
            _farm.Player.ToConsole();
            _farm.HarvestToConsole();
            Console.SetCursorPosition(196, 2);
            Console.Write("День 0");

            var end = true;
            var showScore = true;
            while (end)
            {
                Clear();
                ShowNextDay();
                
                Func<Plant, Plant, int> sorting;
                if (showScore)
                {
                    Console.SetCursorPosition(204, 28);
                    Console.Write("Количество урожая, шт"); 
                    sorting = Plant.SortByScore;
                    _farm.Player.FruitBox.SortBox(sorting);
                    _farm.Player.VegetableBox.SortBox(sorting);
                    _farm.Player.BerryBox.SortBox(sorting);
                    _farm.ScoreToConsole();
                }
                else
                {
                    Console.SetCursorPosition(204, 28);
                    Console.Write("  Качество урожая, %   ");
                    sorting = Plant.SortByOk;
                    _farm.Player.FruitBox.SortBox(sorting);
                    _farm.Player.VegetableBox.SortBox(sorting);
                    _farm.Player.BerryBox.SortBox(sorting);
                    _farm.OkToConsole();
                }


                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.DownArrow:
                        Moving(key);
                        break;
                    case ConsoleKey.F:
                    {
                        Selecting();
                        _farm.ToConsole();
                        _farm.HarvestToConsole();
                        break;
                    }
                    case ConsoleKey.E:
                        _farm.NextDay();
                        _logger.OnNotify($"Day changed ({_farm.GetDay()-1} -> {_farm.GetDay()})");
                        _farm.ToConsole();
                        break;
                    case ConsoleKey.S:
                        showScore = true;
                        break;
                    case ConsoleKey.O:
                        showScore = false;
                        break;
                    case ConsoleKey.Q:
                        end = false;
                        _logger.OnNotify($"Game closed\n");
                        break;
                }
            }
        }

        private static void Clear()
        {
            for (var i = 16; i < 24; i++)
            {
                Console.SetCursorPosition(196, i);
                Console.WriteLine("                                       ");
            }
        }

        private void Moving(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    _farm.Player.ClearConsole();
                    _farm.Player.MoveLeft();
                    _farm.Player.ToConsole();
                    break;
                case ConsoleKey.RightArrow:
                    _farm.Player.ClearConsole();
                    _farm.Player.MoveRight();
                    _farm.Player.ToConsole();
                    break;
                case ConsoleKey.UpArrow:
                    _farm.Player.ClearConsole();
                    _farm.Player.MoveTop();
                    _farm.Player.ToConsole();
                    break;
                case ConsoleKey.DownArrow:
                    _farm.Player.ClearConsole();
                    _farm.Player.MoveBottom();
                    _farm.Player.ToConsole();
                    break;
            }
        }

        private void Selecting()
        {
            if (_farm.Player.X < 5 && _farm.Player.Y < 5)
            {
                _logger.OnNotify($"Player interacts with field ({_farm.Player.X},{_farm.Player.Y})");
                bool close = true;

                while (close)
                {
                    int status = _farm.SelectedFieldStatus(_farm.Player.X, _farm.Player.Y);

                    ConsoleKey key;
                    switch (status)
                    {
                        case 0: // Если поле пустое
                            Console.SetCursorPosition(196, 16);
                            Console.Write("Выберите, что вы хотите посадить:");
                            Console.SetCursorPosition(196, 17);
                            Console.Write("\t[1] Фрукты");
                            Console.SetCursorPosition(196, 18);
                            Console.Write("\t[2] Овощи");
                            Console.SetCursorPosition(196, 19);
                            Console.Write("\t[3] Ягоды");
                            Console.SetCursorPosition(196, 20);
                            Console.Write("\t[4] Отмена");

                            bool close1 = true;
                            int choice = 0;

                            while (close1)
                            {
                                key = Console.ReadKey(true).Key;

                                switch (key)
                                {
                                    case ConsoleKey.NumPad1:
                                    case ConsoleKey.D1:
                                        Console.SetCursorPosition(196, 17);
                                        Console.BackgroundColor = ConsoleColor.Gray;
                                        Console.ForegroundColor = ConsoleColor.Black;
                                        Console.Write("\t[1] Фрукты");
                                        Console.SetCursorPosition(196, 18);
                                        Console.BackgroundColor = ConsoleColor.Black;
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                        Console.Write("\t[2] Овощи");
                                        Console.SetCursorPosition(196, 19);
                                        Console.Write("\t[3] Ягоды");
                                        Console.SetCursorPosition(196, 20);
                                        Console.Write("\t[4] Отмена");
                                        Console.SetCursorPosition(196, 22);
                                        Console.Write("Нажмите Enter чтобы подтвердить выбор!");
                                        choice = 1;
                                        break;
                                    case ConsoleKey.NumPad2:
                                    case ConsoleKey.D2:
                                        Console.SetCursorPosition(196, 17);
                                        Console.Write("\t[1] Фрукты");
                                        Console.SetCursorPosition(196, 18);
                                        Console.BackgroundColor = ConsoleColor.Gray;
                                        Console.ForegroundColor = ConsoleColor.Black;
                                        Console.Write("\t[2] Овощи");
                                        Console.SetCursorPosition(196, 19);
                                        Console.BackgroundColor = ConsoleColor.Black;
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                        Console.Write("\t[3] Ягоды");
                                        Console.SetCursorPosition(196, 20);
                                        Console.Write("\t[4] Отмена");
                                        Console.SetCursorPosition(196, 22);
                                        Console.Write("Нажмите Enter чтобы подтвердить выбор!");
                                        choice = 2;
                                        break;
                                    case ConsoleKey.NumPad3:
                                    case ConsoleKey.D3:
                                        Console.SetCursorPosition(196, 17);
                                        Console.Write("\t[1] Фрукты");
                                        Console.SetCursorPosition(196, 18);
                                        Console.Write("\t[2] Овощи");
                                        Console.SetCursorPosition(196, 19);
                                        Console.BackgroundColor = ConsoleColor.Gray;
                                        Console.ForegroundColor = ConsoleColor.Black;
                                        Console.Write("\t[3] Ягоды");
                                        Console.SetCursorPosition(196, 20);
                                        Console.BackgroundColor = ConsoleColor.Black;
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                        Console.Write("\t[4] Отмена");
                                        Console.SetCursorPosition(196, 22);
                                        Console.Write("Нажмите Enter чтобы подтвердить выбор!");
                                        choice = 3;
                                        break;
                                    case ConsoleKey.NumPad4:
                                    case ConsoleKey.D4:
                                        Console.SetCursorPosition(196, 17);
                                        Console.Write("\t[1] Фрукты");
                                        Console.SetCursorPosition(196, 18);
                                        Console.Write("\t[2] Овощи");
                                        Console.SetCursorPosition(196, 19);
                                        Console.Write("\t[3] Ягоды");
                                        Console.SetCursorPosition(196, 20);
                                        Console.BackgroundColor = ConsoleColor.Gray;
                                        Console.ForegroundColor = ConsoleColor.Black;
                                        Console.Write("\t[4] Отмена");
                                        Console.BackgroundColor = ConsoleColor.Black;
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                        Console.SetCursorPosition(196, 22);
                                        Console.Write("Нажмите Enter чтобы подтвердить выбор!");
                                        choice = 4;
                                        break;
                                    case ConsoleKey.Enter:
                                        switch (choice)
                                        {
                                            case 1:
                                                _farm.PlantField(new Fruit(), _farm.Player.X, _farm.Player.Y);
                                                _logger.OnNotify($"Player plant fruit in the field ({_farm.Player.X},{_farm.Player.Y})");
                                                Clear();
                                                close = false;
                                                close1 = false;
                                                break;
                                            case 2:
                                                _farm.PlantField(new Vegetable(), _farm.Player.X, _farm.Player.Y);
                                                _logger.OnNotify($"Player plant vegetable in the field ({_farm.Player.X},{_farm.Player.Y})");
                                                Clear();
                                                close = false;
                                                close1 = false;
                                                break;
                                            case 3:
                                                _farm.PlantField(new Berry(), _farm.Player.X, _farm.Player.Y);
                                                _logger.OnNotify($"Player plant berry in the field ({_farm.Player.X},{_farm.Player.Y})");
                                                Clear();
                                                close = false;
                                                close1 = false;
                                                break;
                                            case 4:
                                                Clear();
                                                close = false;
                                                close1 = false;
                                                break;
                                        }

                                        break;
                                    default:
                                        choice = 0;
                                        break;
                                }
                            }

                            break;
                        case 1: // Если поле засажено но не выросло
                            Console.SetCursorPosition(196, 16);
                            Console.Write("Поле еще не готово!!!");
                            Console.SetCursorPosition(196, 18);
                            Console.Write("Нажмите Enter чтобы продолжить!");

                            while (close)
                            {
                                key = Console.ReadKey(true).Key;
                                if (key == ConsoleKey.Enter)
                                {
                                    Clear();
                                    close = false;
                                }
                            }

                            break;
                        case 2: // Если поле засажено и готово
                            Console.SetCursorPosition(196, 16);
                            Console.WriteLine("Собрать урожай:");
                            Console.SetCursorPosition(196, 17);
                            Console.Write("\t[1] Да");
                            Console.SetCursorPosition(196, 18);
                            Console.Write("\t[2] Нет");

                            close1 = true;
                            choice = 0;

                            while (close1)
                            {
                                key = Console.ReadKey(true).Key;

                                switch (key)
                                {
                                    case ConsoleKey.NumPad1:
                                    case ConsoleKey.D1:
                                        Console.SetCursorPosition(196, 17);
                                        Console.BackgroundColor = ConsoleColor.Gray;
                                        Console.ForegroundColor = ConsoleColor.Black;
                                        Console.Write("\t[1] Да");
                                        Console.SetCursorPosition(196, 18);
                                        Console.BackgroundColor = ConsoleColor.Black;
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                        Console.Write("\t[2] Нет");
                                        Console.SetCursorPosition(196, 20);
                                        Console.Write("Нажмите Enter чтобы подтвердить выбор!");
                                        choice = 1;
                                        break;
                                    case ConsoleKey.NumPad2:
                                    case ConsoleKey.D2:
                                        Console.SetCursorPosition(196, 17);
                                        Console.Write("\t[1] Да");
                                        Console.SetCursorPosition(196, 18);
                                        Console.BackgroundColor = ConsoleColor.Gray;
                                        Console.ForegroundColor = ConsoleColor.Black;
                                        Console.Write("\t[2] Нет");
                                        Console.SetCursorPosition(196, 20);
                                        Console.BackgroundColor = ConsoleColor.Black;
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                        Console.Write("Нажмите Enter чтобы подтвердить выбор!");
                                        choice = 2;
                                        break;
                                    case ConsoleKey.Enter:
                                        switch (choice)
                                        {
                                            case 1:
                                                _farm.CollectField(_farm.Player.X, _farm.Player.Y);
                                                Clear();
                                                close = false;
                                                close1 = false;
                                                break;
                                            case 2:
                                                Clear();
                                                close = false;
                                                close1 = false;
                                                break;
                                        }

                                        break;
                                    default:
                                        choice = 0;
                                        break;
                                }
                            }

                            break;
                    }
                }
            }
            else if (_farm.Player.X == 6 && _farm.Player.Y == 0)
            {
                _logger.OnNotify($"Player interacts with shop");
                Shopping();
            }
        }

        private void Shopping()
        {
            Console.SetCursorPosition(196, 16);
            Console.Write("Выберите, что вы хотите сделать:");
            Console.SetCursorPosition(196, 17);
            Console.Write("\t[1] Купить");
            Console.SetCursorPosition(196, 18);
            Console.Write("\t[2] Продать");
            Console.SetCursorPosition(196, 19);
            Console.Write("\t[3] Выйти");
            Console.SetCursorPosition(196, 24);
            Console.Write("                                       ");
            

            var close = true;
            var choice = 0;

            while (close)
            {
                if (choice == 0)
                {
                    Console.SetCursorPosition(196, 16);
                    Console.Write("Выберите, что вы хотите сделать:");
                    Console.SetCursorPosition(196, 17);
                    Console.Write("\t[1] Купить");
                    Console.SetCursorPosition(196, 18);
                    Console.Write("\t[2] Продать");
                    Console.SetCursorPosition(196, 19);
                    Console.Write("\t[3] Выйти");
                    Console.SetCursorPosition(196, 24);
                }
                
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.NumPad1:
                    case ConsoleKey.D1:
                        Console.SetCursorPosition(196, 16);
                        Console.Write("Выберите, что вы хотите сделать:");
                        Console.SetCursorPosition(196, 17);
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("\t[1] Купить");
                        Console.SetCursorPosition(196, 18);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("\t[2] Продать");
                        Console.SetCursorPosition(196, 19);
                        Console.Write("\t[3] Выйти");
                        Console.SetCursorPosition(196, 21);
                        Console.Write("Нажмите Enter чтобы подтвердить выбор!");
                        choice = 1;
                        break;
                    case ConsoleKey.NumPad2:
                    case ConsoleKey.D2:
                        Console.SetCursorPosition(196, 16);
                        Console.Write("Выберите, что вы хотите сделать:");
                        Console.SetCursorPosition(196, 17);
                        Console.Write("\t[1] Купить");
                        Console.SetCursorPosition(196, 18);
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("\t[2] Продать");
                        Console.SetCursorPosition(196, 19);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("\t[3] Выйти");
                        Console.SetCursorPosition(196, 21);
                        Console.Write("Нажмите Enter чтобы подтвердить выбор!");
                        choice = 2;
                        break;
                    case ConsoleKey.NumPad3:
                    case ConsoleKey.D3:
                        Console.SetCursorPosition(196, 16);
                        Console.Write("Выберите, что вы хотите сделать:");
                        Console.SetCursorPosition(196, 17);
                        Console.Write("\t[1] Купить");
                        Console.SetCursorPosition(196, 18);
                        Console.Write("\t[2] Продать");
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.SetCursorPosition(196, 19);
                        Console.Write("\t[3] Выйти");
                        Console.SetCursorPosition(196, 21);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("Нажмите Enter чтобы подтвердить выбор!");
                        choice = 3;
                        break;
                    case ConsoleKey.Enter:
                        switch (choice)
                        {
                            case 1:
                                Clear();
                                Buying();
                                _farm.HarvestToConsole();
                                choice = 0;
                                break;
                            case 2:
                                _farm.Player.SellHarvest();
                                _farm.HarvestToConsole();
                                choice = 0;
                                break;
                            case 3:
                                Clear();
                                close = false;
                                choice = 0;
                                break;
                        }

                        break;
                    default:
                        choice = 0;
                        break;
                }
            }
        }

        private void Buying()
        {
            Console.SetCursorPosition(196, 16);
            Console.Write("Выберите, что вы хотите купить:");
            Console.SetCursorPosition(196, 17); 
            Console.Write("\t[1] 5 семян фруктов (5$)");
            Console.SetCursorPosition(196, 18);
            Console.Write("\t[2] 5 семян овощей (8$)");
            Console.SetCursorPosition(196, 19);
            Console.Write("\t[3] 5 семян ягод (3$)");
            Console.SetCursorPosition(196, 20);
            Console.Write("\t[4] Назад");
            Console.SetCursorPosition(196, 24);
            Console.Write("                                       ");

            var close = true;
            var choice = 0;

            while (close)
            {
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.NumPad1:
                    case ConsoleKey.D1:
                        Console.SetCursorPosition(196, 17);
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("\t[1] 5 семян фруктов (5$)");
                        Console.SetCursorPosition(196, 18);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("\t[2] 5 семян овощей (8$)");
                        Console.SetCursorPosition(196, 19);
                        Console.Write("\t[3] 5 семян ягод (3$)");
                        Console.SetCursorPosition(196, 20);
                        Console.Write("\t[4] Назад");
                        Console.SetCursorPosition(196, 22);
                        Console.Write("Нажмите Enter чтобы подтвердить выбор!");
                        choice = 1;
                        break;
                    case ConsoleKey.NumPad2:
                    case ConsoleKey.D2:
                        Console.SetCursorPosition(196, 17);
                        Console.Write("\t[1] 5 семян фруктов (5$)");
                        Console.SetCursorPosition(196, 18);
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("\t[2] 5 семян овощей (8$)");
                        Console.SetCursorPosition(196, 19);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("\t[3] 5 семян ягод (3$)");
                        Console.SetCursorPosition(196, 20);
                        Console.Write("\t[4] Назад");
                        Console.SetCursorPosition(196, 22);
                        Console.Write("Нажмите Enter чтобы подтвердить выбор!");
                        choice = 2;
                        break;
                    case ConsoleKey.NumPad3:
                    case ConsoleKey.D3:
                        Console.SetCursorPosition(196, 17);
                        Console.Write("\t[1] 5 семян фруктов (5$)");
                        Console.SetCursorPosition(196, 18);
                        Console.Write("\t[2] 5 семян овощей (8$)");
                        Console.SetCursorPosition(196, 19);
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("\t[3] 5 семян ягод (3$)");
                        Console.SetCursorPosition(196, 20);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("\t[4] Назад");
                        Console.SetCursorPosition(196, 22);
                        Console.Write("Нажмите Enter чтобы подтвердить выбор!");
                        choice = 3;
                        break;
                    case ConsoleKey.NumPad4:
                    case ConsoleKey.D4:
                        Console.SetCursorPosition(196, 17);
                        Console.Write("\t[1] 5 семян фруктов (5$)");
                        Console.SetCursorPosition(196, 18);
                        Console.Write("\t[2] 5 семян овощей (8$)");
                        Console.SetCursorPosition(196, 19);
                        Console.Write("\t[3] 5 семян ягод (3$)");
                        Console.SetCursorPosition(196, 20);
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("\t[4] Назад");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.SetCursorPosition(196, 22);
                        Console.Write("Нажмите Enter чтобы подтвердить выбор!");
                        choice = 4;
                        break;
                    case ConsoleKey.Enter:
                        if (choice > 0 && choice < 4)
                        {
                            if (_farm.Player.BuySeeds(choice))
                            {
                                Clear();
                                close = false;
                            }
                        }
                        else if (choice == 4)
                        {
                            Clear();
                            close = false;
                        }
                                        
                        break;
                    default:
                        choice = 0;
                        break;
                }
            }
            _farm.HarvestToConsole();
        }

        private void ShowNextDay()
        {
            if (_farm.Player.Money < 100)
            {
                Console.SetCursorPosition(196, 24);
                Console.Write("Нажмите [E] чтобы начать следующий день");
            }
        }
    }
}