using System;
using System.Collections.Generic;
using System.Text;

namespace CustomList
{
   public class Item<T>
    {
        private T data = default;
        public T Data 
        { get
            {
                return data;
            }

            set
            {
                if (value is null) throw new ArgumentNullException(nameof(value));
                else this.data = value;
            }

        }
        public Item<T> Next { get; set; }
        public Item(T data)
        {
            this.Data = data;
        }
    }
}
