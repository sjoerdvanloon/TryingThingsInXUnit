using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    internal class Tombola<T>
    {
        readonly Queue<T> _items;
        readonly System.Random _random;

        public Tombola(IEnumerable<T> items, int seed = 1)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            _random = new System.Random(seed);

            // https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle
            var list = items.ToArray();
            int n = list.Count();
            while (n > 1)
            {
                n--;
                int k = _random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            _items = new Queue<T>(list);
        }

        /// <summary>
        /// Draws from the random bucket of items, which loop after one iteration
        /// </summary>
        /// <returns>A new random item</returns>
        public T Draw()
        {
            var port = _items.Dequeue();
            _items.Enqueue(port);

            return port;
        }
    }
}
