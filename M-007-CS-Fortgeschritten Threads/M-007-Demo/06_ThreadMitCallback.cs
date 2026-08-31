using static Multithreading._06_ThreadMitCallback;

namespace Multithreading
{
	public class _06_ThreadMitCallback
	{
		public delegate void CallbackDelegate(ReturnObject r);

		private static ReturnObject Obj;

		static void Main(string[] args)
		{
			ThreadWithReturn twr = new ThreadWithReturn(new CallbackDelegate(Result));
			Thread t = new Thread(twr.ThreadReturn);
			t.Start();
			t.Join(); //Auf Callback warten
		}

		static void Result(ReturnObject o)
		{
			Obj = o;
		}
	}

	public class ThreadWithReturn
	{
		private CallbackDelegate callback;

		public ThreadWithReturn(CallbackDelegate callback)
		{
			this.callback = callback;
		}

		public void ThreadReturn()
		{
			ReturnObject o = new ReturnObject();
			o.Text = "Test";
			o.Zahl = 5;
			callback(o);
		}
	}

	public class ReturnObject
	{
		public string Text { get; set; }

		public int Zahl { get; set; }
	}
}
