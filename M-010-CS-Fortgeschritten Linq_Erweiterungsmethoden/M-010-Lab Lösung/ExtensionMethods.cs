public static class ExtensionMethods
{
	public static string PrintList(this IEnumerable<Person> list)
	{
		return list.Take(10).Aggregate(new StringBuilder(), (agg, p) => agg.AppendLine($"Die Person {($"{p.Vorname} {p.Nachname}")} ist {p.Alter} Jahre alt und ...")).ToString();
	}
}