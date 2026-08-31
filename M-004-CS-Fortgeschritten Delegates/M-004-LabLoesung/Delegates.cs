void ForEach<T>(IEnumerable<T> values, Action<T> action)
{
	if (values == null || action == null)
		throw new ArgumentNullException();

	foreach (T item in values) 
		action?.Invoke(item);
}

IEnumerable<TReturn> ForEachReturn<T, TReturn>(IEnumerable<T> values, Func<T, TReturn> func)
{
	if (values == null || func == null)
		throw new ArgumentNullException();

	List<TReturn> ret = [];
	foreach (T item in values)
	{
		TReturn r = func(item);
		ret.Add(r);
	}
	return ret;
}

List<int> zahlen = [1, 2, 3, 4, 5];
IEnumerable<int> r = ForEachReturn(zahlen, e => e * 2);
Console.WriteLine();