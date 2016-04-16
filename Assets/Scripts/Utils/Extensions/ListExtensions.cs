using System.Collections.Generic;
using Utils;

public static class ListExtensions
{
    public static void Shuffle<T>(this List<T> _list)
    {
        System.Random rng = new System.Random();
        int n = _list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = _list[k];
            _list[k] = _list[n];
            _list[n] = value;
        }
    }

    public static T Random<T>(this List<T> _list)
    {
        return _list[UnityEngine.Random.Range(0, _list.Count)];
    }

    public static T RandomAndRemove<T>(this List<T> _list)
    {
        int index = UnityEngine.Random.Range(0, _list.Count);
        T buffer = _list[index];
        _list.RemoveAt(index);
        return buffer;
    }

    public static T Random<T>(this List<Tuple<T, float>> _list)
	{
		// Calculate sum of each weight
		float sum = 0;
		List<Tuple<T, float>> listSum = new List<Tuple<T, float>>();
		foreach (Tuple<T, float> item in _list)
		{
			sum += item.Item2;
			listSum.Add(new Tuple<T, float>(item.Item1, sum));
		}

		// Random the sum
		float random = UnityEngine.Random.Range(0f, sum);

		// Get the result
		foreach (Tuple<T, float> item in listSum)
		{
			if (item.Item2 >= random)
				return item.Item1;
		}

		// Return by default the last
		return _list[_list.Count - 1].Item1;
	}

	public static T Random<T>(this List<Tuple<T, int>> _list)
	{
		// Calculate sum of each weight
		int sum = 0;
		List<Tuple<T, int>> listSum = new List<Tuple<T, int>>();
		foreach (Tuple<T, int> item in _list)
		{
			sum += item.Item2;
			listSum.Add(new Tuple<T, int>(item.Item1, sum));
		}

		// Random the sum
		float random = UnityEngine.Random.Range(0f, sum);

		// Get the result
		foreach (Tuple<T, int> item in listSum)
		{
			if (item.Item2 >= random)
				return item.Item1;
		}

		// Return by default the last
		return _list[_list.Count - 1].Item1;
    }

    public static void AddIfNotContains<T>(this List<T> _list, T _item)
    {
        if (!_list.Contains(_item))
            _list.Add(_item);
    }

    public static void AddMultiIfNotContains<T>(this List<T> _list, List<T> _items)
    {
        foreach (T iItem in _items)
            _list.AddIfNotContains(iItem);
    }

    public static void RemoveIfContains<T>(this List<T> _list, T _item)
    {
        if (_list.Contains(_item))
            _list.Remove(_item);
    }

    public static void RemoveMultiIfContains<T>(this List<T> _list, List<T> _items)
    {
        foreach (T iItem in _items)
            _list.RemoveIfContains(iItem);
    }
}