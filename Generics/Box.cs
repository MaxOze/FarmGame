using System;
using System.Collections;
using System.Collections.Generic;

namespace Generics
{
    public class Box<T> : IEnumerable where T : Plant
    {
        private readonly List<T> _plants;
        private int _seeds;

        public Box()
        {
            _plants = new List<T>();
            _seeds = 5;
        }

        public void UseSeed()
        {
            _seeds -= 1;
        }

        public void BuySeeds()
        {
            _seeds += 5;
        }

        public int GetSeeds()
        {
            return _seeds;
        }

        public void ToTheBox(T plant)
        {
            if (Count() < 10)
                _plants.Add(plant);
            else
                throw new BoxOverException(Config.ERRORTEXT);
        }

        public void Sell()
        {
            _plants.Clear();
        }

        public int GetScore()
        {
            var score = 0;
            foreach (var plant in _plants)
            {
                score += plant.GetScore();
            }

            return score;
        }

        public int Count()
        {
            return _plants.Count;
        }

        public void SortBox(Func<Plant, Plant, int> compare)
        {
            if (_plants.Count > 1)
            {
                bool isSorted;

                do
                {
                    isSorted = true;

                    for (var i = 0; i < _plants.Count - 1; i++)
                    {
                        if (compare(_plants[i], _plants[i + 1]) > 0)
                        {
                            (_plants[i], _plants[i + 1]) = (_plants[i + 1], _plants[i]);
                            isSorted = false;
                        }
                    }
                } while (isSorted != true);
            }
        }

        public IEnumerator GetEnumerator() => _plants.GetEnumerator();
    }
}