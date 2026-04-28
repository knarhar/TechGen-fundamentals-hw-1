namespace TechGen_fundamentals_hw_1
{
    internal class MyList
    {
        private int[] _items;
        private int _count;

        public int Count => _count;
        public MyList()
        {
            _items = new int[4];
            _count = 0;
        }

        public void Add(int item)
        {
            EnsureCapacity();
            _items[_count] = item;
            _count++;
        }

        public void AddRange(int[] items)
        {
            if (items == null) return;

            foreach (var item in items)
            {
                Add(item);
            }
        }

        public bool Remove(int item)
        {
            int index = IndexOf(item);
            if (index == -1)
                return false;

            // shift left
            for (int i = index; i < _count - 1; i++)
            {
                _items[i] = _items[i + 1];
            }

            _count--;
            return true;
        }

        public bool TryGet(int index, out int value)
        {
            if (index < 0 || index >= _count)
            {
                value = 0;
                return false;
            }

            value = _items[index];
            return true;
        }

        // validate the count and resizes the array
        private void EnsureCapacity()
        {
            if (_count < _items.Length)
                return;

            int newCapacity = _items.Length * 2;
            int[] newArray = new int[newCapacity];

            for (int i = 0; i < _items.Length; i++)
            {
                newArray[i] = _items[i];
            }

            _items = newArray;
        }

        // BONUS

        public int IndexOf(int item)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_items[i] == item)
                    return i;
            }
            return -1;
        }

        public bool Contains(int item)
        {
            return IndexOf(item) != -1;
        }

        public void Clear()
        {
            _count = 0;
        }

        public int this[int index]
        {
            get
            {
                if (index < 0 || index >= _count)
                    throw new IndexOutOfRangeException();

                return _items[index];
            }
            set
            {
                if (index < 0 || index >= _count)
                    throw new IndexOutOfRangeException();

                _items[index] = value;
            }
        }
    }
}
