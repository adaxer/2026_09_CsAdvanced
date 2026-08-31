namespace Multithreading
{
	internal class _05_ThreadMitReturn
	{
		static string ReturnString = string.Empty;

		static void Main(string[] args)
		{
			string s = "Test";
			Thread t = new(() => //Workaround um Thread Limits
			{
				ReturnString = ToUpper(s); //"Parameter" und "Return Wert"
			});
			t.Start();
		}

		static string ToUpper(string param) => param.ToUpper();
	}
}
