using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Inventory {
    public class ItemCollection<T> : IItemCollection<T> {
        #region Implementation of IEnumerable

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
        {
            yield return (T) objects.GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return objects.GetEnumerator();
        }

        #endregion

        public T this[int index] => objects[index];
        
        public int Count { get; private set; }

        private readonly T[] objects;

        public T Add(object item, int? index = null) {
            var it = (T) item;
            var j = 0;
            if (index == null) {
                var arr = objects.Take(Count).ToArray();

                for (; j < arr.Length; j++) {
                    if (arr[j].Equals(default)) {
                        break;
                    }
                }
            }
            else {
                j = index.Value;
            }

            var copy = objects[j];
            objects[j] = it;

            return copy;
        }

        public T Remove(int index) {
            var copy = objects[index];
            objects[index] = default;
            return copy;
        }

        public T[] Resize(int newSize) {
            if (newSize > objects.Length) throw new IndexOutOfRangeException("Index is more than array size");
            var delta = newSize - Count;
            var extra = new T[0];
            if (delta < 0) {
                extra = new T[delta];
                objects.CopyTo(extra, Count + delta);
            }

            Count = newSize;
            return extra;
        }

        public ItemCollection(int maxSize, int currentCount) {
            objects = new T[maxSize];
            Count = currentCount;
        }
    }
}