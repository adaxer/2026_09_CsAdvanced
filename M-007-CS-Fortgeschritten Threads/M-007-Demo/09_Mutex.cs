using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multithreading
{
	internal class _09_Mutex
	{
		static void Main(string[] args)
		{
			if (Mutex.TryOpenExisting("09", out Mutex m)) //Checken ob 09 schon existiert
			{
				Console.WriteLine("Applikation läuft bereits");
				Environment.Exit(0);
			}
			else
			{
				m = new Mutex(false, "09"); //Hier belegen
			}

			m.ReleaseMutex(); //freigeben
		}
	}
}
