using System;
using System.Collections;
using System.Collections.Generic;

namespace CustomList
{
    public class CustomList<T> : IList<T>
    {
        /// <summary>
        /// The property return first element of list 
        /// </summary>
        public Item<T> Head { get; private set; }
        public Item<T> Tail { get; private set; }
        /// <summary>
        /// The property return number of elements contained in the CustomList
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the IList is read-only.
        /// Make it always false
        /// </summary>
        public bool IsReadOnly => false;


        /// <summary>
        /// Constructor that gets params T as parameter       
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when values is null</exception>
        /// <param name="values"></param>
        public CustomList(params T[] values)
        {
            if (values is null) throw new ArgumentNullException();
            else
            {
                for (int i = 0; i < values.Length; i++)
                {
                    Add(values[i]);
                }
            }
        }


        /// <summary>
        /// Constructor that gets Ienumerable collection as parameter       
        /// </summary>
        ///<exception cref="ArgumentNullException">Thrown when values is null</exception>
        /// <param name="values"></param>
        public CustomList(IEnumerable<T> values)
        {
            if (values is null) throw new ArgumentNullException();
            else
            {
                foreach (var item in values)
                {
                    Add(item);
                }
            }
        }

        /// <summary>
        /// Get or set data at the position.
        /// </summary>
        /// <param name="index">Position</param>
        /// <exception cref="IndexOutOfRangeException">Throw when index is less than 0 or greater than Count - 1</exception>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count) throw new IndexOutOfRangeException(nameof(index));
                else
                {
                    var current = Head;
                    int i = 0;
                    while (i < Count)
                    {
                        if (i == index)
                        {
                            return current.Data;
                        }
                        else
                        {
                            i++;
                            current = current.Next;
                        }
                    }
                    throw new IndexOutOfRangeException(nameof(index));
                }
            }
            set
            {
                if (index < 0 || index >= Count) throw new IndexOutOfRangeException(nameof(index));
                else
                {
                    var current = Head;
                    int i = 0;
                    while (i < Count)
                    {
                        if (i == index)
                        {
                            current.Data = value;
                            break;
                        }
                        else
                        {
                            i++;
                            current = current.Next;
                        }
                    }
                }
            }
        }


        /// <summary>
        ///  Adds an object to the end of the CustomList.
        /// </summary>
        /// <param name="data">Object that should be added in the CustomList</param>
        /// <exception cref="ArgumentNullException">Throws when you try to add null</exception>
        public void Add(T data)
        {
            if (Tail != null)
            {
                var item = new Item<T>(data);
                Tail.Next = item;
                Tail = item;
                Count++;
            }
            else
            {
                SetHeadAndTailData(data);
            }
        }

        private void SetHeadAndTailData(T data)
        {
            var item = new Item<T>(data);
            Head = item;
            Tail = item;
            Count = 1;
        }
        /// <summary>
        /// Removes all elements from the CustomList
        /// </summary>
        public void Clear()
        {
            var current = Head;
            while (current != null)
            {
                current.Data = default;
                current = current.Next;
            }
            Count = 0;
            Head = null;
            Tail = null;
        }

        /// <summary>
        /// Determines whether an element is in the CustomList
        /// </summary>
        /// <param name="item">Object we check to see if it is on the CustomLIst</param>
        /// <returns>True if the element exists in the CustomList, else false</returns>
        public bool Contains(T item)
        {
            if (Count == 0) return false;
            else
            {
                var current = Head;
                while (current.Next != null)
                {
                    if (current.Data.GetHashCode() == item.GetHashCode()) return true;
                    else
                    {
                        current = current.Next;
                    }
                }
                return false;
            }
        }


        /// <summary>
        /// Removes the first occurrence of a specific object from the CustomList.
        /// </summary>
        /// <param name="item"> The object to remove from the CustomList</param>
        /// <returns>True if item is successfully removed; otherwise, false. This method also returns
        ///     false if item was not found in the CustomList.</returns>
        /// <exception cref="ArgumentNullException">Throws when you try to remove null</exception>
        public bool Remove(T item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            else
            {
                if (Head != null)
                {
                    if (Head.Data.Equals(item))
                    {
                        Head = Head.Next;
                        Count--;
                        return true;
                    }
                    var current = Head.Next;
                    var previos = Head;
                    while (current != null)
                    {
                        if (current.Data.Equals(item))
                        {
                            previos.Next = current.Next;
                            Count--;
                            return true;
                        }
                        previos = current;
                        current = current.Next;
                    }
                }
                return false;
                /*var current = Head;
                int i = 0;
                while (i < Count)
                {
                    if (current.Data.GetHashCode() == item.GetHashCode())
                    {
                        current.Data = current.Next.Data;
                        current.Next = current.Next.Next;
                        Count--;
                        return true;
                    }
                    else
                    {
                        i++;
                        if (i == Count - 1)
                        {
                            current.Next = null;
                            Count--;
                            return true;
                        }
                        else current = current.Next;
                    }
                }
                return false;*/
            }
        }


        /// <summary>
        /// Searches for the specified object and returns the zero-based index of the first
        ///     occurrence within the CustomList.
        /// </summary>
        /// <param name="item">The object whose index we want to get </param>
        /// <returns>The zero-based index of the first occurrence of item within the entire CustomList,
        ///    if found; otherwise, -1.</returns>
        public int IndexOf(T item)
        {
            if (Count < 1) return -1;
            else
            {
                var current = Head;
                int count = 0;
                while (current.Next != null)
                {
                    if (current.Data.GetHashCode() == item.GetHashCode()) return count;
                    else
                    {
                        current = current.Next;
                        count++;
                    }
                }
                return -1;
            }
        }


        /// <summary>
        /// Inserts an element into the CustomList at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <param name="item">The object to insert.</param>
        /// <exception cref="ArgumentOutOfRangeException">Throw when index is less than 0 or greater than Count - 1</exception>
        /// <exception cref="ArgumentNullException">Thrown when item is null</exception>
        public void Insert(int index, T item)
        {
            if (index < 0 || index > Count) throw new ArgumentOutOfRangeException(nameof(index));
            else if (item is null) throw new ArgumentNullException(nameof(item));
            else if (index == Count - 1) this.Add(item);
            else if (index == 0)
            {
                var x = new Item<T>(item);
                x.Next = Head;
                Head = x;
                Count++;
            }
            else
            {
                var current = Head;
                int i = 0;
                while (i < Count)
                {
                    if (i == index - 1)
                    {
                        var x = new Item<T>(item);
                        x.Next = current.Next;
                        current.Next = x;
                        Count++;
                        return;
                    }
                    else
                    {
                        i++;
                        current = current.Next;
                    }
                }
            }
        }


        /// <summary>
        /// Removes the element at the specified index of the CustomList.
        /// </summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        /// <exception cref="ArgumentOutOfRangeException">Throw when index is less than 0 or greater than Count - 1</exception>
        public void RemoveAt(int index)
        {
            if (index < 0 || index > Count - 1)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            else
            {
                this.Remove(this[index]);
            }
        }


        /// <summary>
        /// Copies the entire CustomList to a compatible one-dimensional array, starting at the beginning of the target array.
        /// </summary>
        /// <param name="array">The one-dimensional System.Array that is the destination of the elements copied
        ///     from CustomList</param>
        /// <param name="arrayIndex">The zero-based index in the source System.Array at which
        ///   copying begins.</param>
        ///   <exception cref="ArgumentNullException">Array is null.</exception>
        ///   <exception cref="ArgumentException">The number of elements in the source CustomList is greater
        ///    than the number of elements that the destination array can contain</exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            else if (array.Length - arrayIndex < Count) throw new ArgumentException(nameof(array));
            else
            {
                for (int i = 0; i < Count; i++)
                {
                    array[arrayIndex + i] = this[i];
                }
            }
        }


        /// <summary>
        /// Returns an enumerator that iterates through the CustomList.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            var current = Head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            var current = Head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }



    }
}
