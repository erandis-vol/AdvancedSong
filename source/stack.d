module asong.stack;

// generic growable stack
class Stack(T)
{
private:
    T[] _items;
    int _size;

public:
    this()
    {
        _items = new T[4];
        _size = 0;
    }

    void push(T item)
    {
        if (_size >= _items.length) {
            auto temp = new T[_items.length * 2];
            for (int i = 0; i < _items.length; i++)
                temp[i] = _items[i];
            _items = temp;
        }

        _items[_size++] = item;
    }

    T pop()
    {
        if (size <= 0)
            assert(false, "Empty stack.");

        return _items[--_size];
    }

    T peek()
    {
        if (size <= 0)
            assert(false, "Empty stack.");

        return _items[_size - 1];
    }

    @property int size() const
    {
        return _size;
    }

	@property bool isEmpty() const
	{
		return size <= 0;
	}
}
