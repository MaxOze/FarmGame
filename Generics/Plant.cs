using System;

namespace Generics
{
    public abstract class Plant         // Абстрактный класс растения
    {
        public int Level;
        public int Score;
        public int IsOk;
        public bool IsReady;
        public const int ReadyLvl = 10;
        public int GrowLevel;

        protected Plant()
        {
            Level = 0;
            IsOk = 100;
            IsReady = false;
            Score = 0;
        }

        public bool ReadyToCollect()
        {
            return IsReady;
        }

        public int GetStage()
        {
            if (Level < ReadyLvl / 2)
                return 1;
            else if (Level >= ReadyLvl / 2 && Level < ReadyLvl)
                return 2;
            else if (IsOk < 1)
                return 4;
            else
                return 3;
        }

        public int GetOk()
        {
            return IsOk;
        }

        public int GetScore()
        {
            return Score;
        }

        public virtual void Grow()
        {
            Level += GrowLevel;
            if (Level >= ReadyLvl)
                IsReady = true;
        }

        public abstract void Collect();

        public static int SortByScore(Plant plant1, Plant plant2)
        {
            return plant2.Score - plant1.Score;
        }

        public static int SortByOk(Plant plant1, Plant plant2)
        {
            return plant2.IsOk - plant1.IsOk;
        }
    }

    public class Fruit : Plant          // Класс фрукта, наследника растения
    {
        public Fruit()
        {
            GrowLevel = 2;
        }
        
        private void Rot()
        {
            if (IsOk > 14)
                IsOk -= 15;
            else
                IsOk = 0;
        }

        public override void Grow()
        {
            base.Grow();
            
            if (Level > ReadyLvl + 6)     // Фрукт перерос и сгнил
                Rot();
        }

        public override void Collect()
        {
            if (IsReady && IsOk > 0)
                Score = Level % 9;
            else if (IsOk < 2)
                Score = 0;
        }

        public override string ToString()
        {
            if (Score == 0)
                return "rotten fruits";
            if (Score == 1)
                return $"{Score} fruits";
            return $"{Score} fruits";
        }
    }

    public class Vegetable : Plant         // Класс овоща, наследника растения
    {
        private readonly Random _random;

        public Vegetable()
        {
            _random = new Random(DateTime.Now.Day);
            GrowLevel = 1;
        }

        private void Eated()
        {
            if (IsOk > 39)
                IsOk -= 40;
            else
                IsOk = 0;
        }
        
        public override void Grow()
        {
            base.Grow();
            
            if (Level > ReadyLvl)
            {
                if (_random.Next(0, 10) == 5)       // С шансом 10% готовый овощ съест крот 
                    Eated();
            }
        }

        public override void Collect()
        {
            if (IsReady && IsOk > 0)
                Score = Level % 4;
            else if (IsOk < 2)
                Score = 0;
        }
        
        public override string ToString()
        {
            if (Score == 0)
                return "eaten vagetables";
            if (Score == 1)
                return $"{Score} vegetable";
            return $"{Score} vegetables";
        }
    }

    public class Berry : Plant              // Класс ягоды, наследника растения
    {
        public Berry()
        {
            GrowLevel = 3;
        }
        public override void Grow()
        {
            base.Grow();

            if (Level > 0 && IsReady)
            {
                if (IsOk > 10)
                    IsOk -= 10;
                else
                    IsOk = 0;
            }
        }

        public override void Collect()
        {
            if (IsReady && IsOk > 0)
            {
                Score = Level * IsOk / 100;
            }
            else if (IsOk < 2)
                Score = 0;
        }
        
        public override string ToString()
        {
            if (Score == 0)
                return "bad berries";
            if (Score == 1)
                return $"{Score} berry";
            return $"{Score} berries";
        }
    }

    public class Empty : Plant
    {
        public override void Grow()
        { }

        public override void Collect()
        { }
    }
}