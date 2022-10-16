using System;
using System.Collections.Generic;

namespace Generics
{
    public class Player
    {
        public Box<Fruit> FruitBox;
        public Box<Vegetable> VegetableBox;
        public Box<Berry> BerryBox;
        public int Money;
        public int X;
        public int Y;
        private readonly Logger _logger;

        public Player()
        {
            X = 6;
            Y = 1;
            FruitBox = new Box<Fruit>();
            VegetableBox = new Box<Vegetable>();
            BerryBox = new Box<Berry>();
            Money = 0;
            _logger = new Logger();
            _logger.Notify += LoggerMethods.LogInFile;
            _logger.Notify += LoggerMethods.LogInConsole;
        }

        public void SavePlayer(int choice)
        {
            switch (choice)
            {
                case 1:
                    Json<Fruit> saveFruitJson = new Json<Fruit>();
                    Json<Vegetable> saveVegetableJson = new Json<Vegetable>();
                    Json<Berry> saveBerryJson = new Json<Berry>();
            
                    saveFruitJson.SerializeBox(FruitBox, Config.FRUIT_PATH);
                    saveVegetableJson.SerializeBox(VegetableBox, Config.VEGETABLE_PATH);
                    saveBerryJson.SerializeBox(BerryBox, Config.BERRY_PATH);
                    break;
                case 2:
                    Xml<Fruit> saveFruitXml = new Xml<Fruit>();
                    Xml<Vegetable> saveVegetableXml = new Xml<Vegetable>();
                    Xml<Berry> saveBerryXml = new Xml<Berry>();
                    
                    saveFruitXml.SerializeBox(FruitBox, Config.XML_FRUIT_PATH);
                    saveVegetableXml.SerializeBox(VegetableBox, Config.XML_VEGETABLE_PATH);
                    saveBerryXml.SerializeBox(BerryBox, Config.XML_BERRY_PATH);
                    break;
                case 3:
                    PlayerXml savePlayer = new PlayerXml();
                    savePlayer.Serialize(this, Config.XML_PATH);
                    break;

            }
        }
        
        public void LoadPlayer(int choice)
        {
            switch (choice)
            {
                case 1:
                    Json<Fruit> loadFruitJson = new Json<Fruit>();
                    Json<Vegetable> loadVegetableJson = new Json<Vegetable>();
                    Json<Berry> loadBerryJson = new Json<Berry>();
            
                    FruitBox = loadFruitJson.DeserializeBox(Config.FRUIT_PATH);
                    VegetableBox = loadVegetableJson.DeserializeBox(Config.VEGETABLE_PATH);
                    BerryBox = loadBerryJson.DeserializeBox(Config.BERRY_PATH);
                    break;
                case 2:
                    Xml<Fruit> loadFruitXml = new Xml<Fruit>();
                    Xml<Vegetable> loadVegetableXml = new Xml<Vegetable>();
                    Xml<Berry> loadBerryXml = new Xml<Berry>();
                    
                    FruitBox = loadFruitXml.DeserializeBox(Config.XML_FRUIT_PATH);
                    VegetableBox = loadVegetableXml.DeserializeBox(Config.XML_VEGETABLE_PATH);
                    BerryBox = loadBerryXml.DeserializeBox(Config.XML_BERRY_PATH);
                    break;
                case 3:
                    PlayerXml loadPlayer = new PlayerXml();
                    Player test = loadPlayer.Deserialize(Config.XML_PATH);
                    this.Money = test.Money;
                    this.FruitBox = test.FruitBox;
                    this.VegetableBox = test.VegetableBox;
                    this.BerryBox = test.BerryBox;
                    break;
            }
        }

        public void MoveLeft()
        {
            if (X == 6 && Y == 1)
                X--;
            else if (X != 0 && X != 6)
                X--;
        }

        public void MoveRight()
        {
            if ((X < 5) || (X == 5 && Y == 1) )
                X++;
        }

        public void MoveTop()
        {
            if (X == 5 && Y > 0)
                Y--;
            else if (X == 6 && Y == 1)
            {
                Y--;
            }
        }

        public void MoveBottom()
        {
            if (X == 5 && Y < 4)
                Y++;
            else if (X == 6 && Y == 0)
            {
                Y++;
            }
        }

        public bool BuySeeds(int choice)
        {
            switch (choice)
            {
                case 1:
                    if (Money > 5)
                    {
                        FruitBox.BuySeeds();
                        Money -= 5;
                        _logger.OnNotify($"Player buy 5 fruit seeds");
                        return true;
                    }
                   
                    Console.SetCursorPosition(196, 24);
                    Console.Write("Недостаточно денег!                    ");
                    return false;
                case 2:
                    if (Money > 8)
                    {
                        VegetableBox.BuySeeds();
                        Money -= 8;
                        _logger.OnNotify($"Player buy 5 vegetable seeds");
                        return true;
                    }
                    
                    Console.SetCursorPosition(196, 24);
                    Console.Write("Недостаточно денег!                    ");
                    return false;
                case 3: 
                    if (Money > 3)
                    {
                        BerryBox.BuySeeds();
                        Money -= 3;
                        _logger.OnNotify($"Player buy 5 berry seeds");
                        return true;
                    }
                    
                    Console.SetCursorPosition(196, 24);
                    Console.Write("Недостаточно денег!                    ");
                    return false;
            }

            return false;
        }
        
        public void SellHarvest()
        {
            var profit = FruitBox.GetScore() * 2 + VegetableBox.GetScore() * 3 + BerryBox.GetScore();

            if (profit > 0)
            {
                Money += profit;
                FruitBox.Sell();
                VegetableBox.Sell();
                BerryBox.Sell();
                Console.SetCursorPosition(196, 24);
                Console.Write($"Вы продали урожая на {profit}$            ");
                _logger.OnNotify($"Player sell harvest and earn {profit}$");
            }
            else
            {
                Console.SetCursorPosition(196, 24);
                Console.Write($"Вам нечего продавать!                  ");
            }
        }
        
        public void ToConsole()
        {
            int left = 84 + (X * 15);
            int top = 9 + (Y * 11);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(left, top);
            Console.WriteLine("{0_0}");
            Console.SetCursorPosition(left, ++top);
            Console.WriteLine("+-|-+");
            Console.SetCursorPosition(left, ++top);
            Console.WriteLine(" / \\");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void ClearConsole()
        {
            int left = 84 + (X * 15);
            int top = 9 + (Y * 11);
            
            Console.SetCursorPosition(left, top);
            Console.WriteLine("     ");
            Console.SetCursorPosition(left, ++top);
            Console.WriteLine("     ");
            Console.SetCursorPosition(left, ++top);
            Console.WriteLine("     ");
        }
    }
}