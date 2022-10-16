using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace Generics
{
    public class Box<T> where T : Plant
    {

        public List<T> Plants;

        public int Seeds;

        public Box()
        {
            Plants = new List<T>();
            Seeds = 5;
        }

        public void UseSeed()
        {
            Seeds -= 1;
        }

        public void BuySeeds()
        {
            Seeds += 5;
        }

        public int GetSeeds()
        {
            return Seeds;
        }

        public void ToTheBox(T plant)
        {
            if (Count() < 10)
                Plants.Add(plant);
            else
                throw new BoxOverException(Config.ERRORTEXT);
        }

        public void Sell()
        {
            Plants.Clear();
        }

        public int GetScore()
        {
            var score = 0;
            foreach (var plant in Plants)
            {
                score += plant.GetScore();
            }

            return score;
        }

        public int Count()
        {
            return Plants.Count;
        }

        public void SortBox(Func<Plant, Plant, int> compare)
        {
            if (Plants.Count > 1)
            {
                bool isSorted;

                do
                {
                    isSorted = true;

                    for (var i = 0; i < Plants.Count - 1; i++)
                    {
                        if (compare(Plants[i], Plants[i + 1]) > 0)
                        {
                            (Plants[i], Plants[i + 1]) = (Plants[i + 1], Plants[i]);
                            isSorted = false;
                        }
                    }
                } while (isSorted != true);
            }
        }
    }
}