using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Inventory {
    [Serializable]
    public class ItemCollection<T> : IItemCollection<T> where T : class {
        #region Implementation of IEnumerable

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator() {
            return objects.Take(Count).GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() {
            return objects.Take(Count).GetEnumerator();
        }

        public override string ToString() {
            var s = "";
            foreach (var o in objects.Take(Count)) {
                if (o == null)
                    s += "null";
                else
                    s += o.ToString();
                s += " ";
            }
            
            return s;
        }

        #endregion

        public T this[int index] => objects[index];
        
        public int Count { get; private set; }
        public IItemCollection<T1> ToParent<T1>() {
            return this as IItemCollection<T1>;
        }

        private readonly T[] objects;

        /// <inheritdoc />
        public T Add(object item, int? index = null) {
            var itemCasted = (T) item;
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
            objects[j] = itemCasted;

            return copy;
        }

        /// <inheritdoc />
        public T Remove(int index) {
            var copy = objects[index];
            objects[index] = default;
            return copy;
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public ItemCollection(int maxSize, int currentCount) {
            objects = new T[maxSize];
            Count = currentCount;
        }
    }
}