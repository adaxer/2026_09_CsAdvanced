namespace TPL_Uebung;

public class Program
{
	public const string ImagesPath = "Images";
	public const string ImagesBackupPath = "ImagesBackup";
	public const string ImagesOutputPath = "ImagesOutput";

	static void Main(string[] args)
	{
		//Hier wird die Übung zurückgesetzt
		Reset();

		//Hier wird die Übung gesteuert über Tastaturinputs (1-4, Num1-Num4)
		while (true)
		{
			Console.WriteLine("Eingaben: ");
			Console.WriteLine("1: Neuen Scanner erstellen");
			Console.WriteLine("2: Anzahl Worker Tasks anpassen");
			Console.WriteLine("3: Prozess starten/fortsetzen");
			Console.WriteLine("4: Prozess pausieren");

			ConsoleKey inputKey = Console.ReadKey(true).Key;

			switch (inputKey)
			{
				case ConsoleKey.D1 or ConsoleKey.NumPad1:
					CreateScanner(); //Füge in die untenstehenden Methoden Code ein
					break;

				case ConsoleKey.D2 or ConsoleKey.NumPad2:
					AdjustWorkerAmount(); //Füge in die untenstehenden Methoden Code ein
					break;

				case ConsoleKey.D3 or ConsoleKey.NumPad3:
					StartProcess(); //Füge in die untenstehenden Methoden Code ein
					break;

				case ConsoleKey.D4 or ConsoleKey.NumPad4:
					PauseProcess(); //Füge in die untenstehenden Methoden Code ein
					break;
			}
		}
	}

	#region Input Methoden
	private static void CreateScanner()
	{

	}

	private static void AdjustWorkerAmount()
	{

	}

	private static void StartProcess()
	{

	}

	private static void PauseProcess()
	{

	}
	#endregion

	private static void Reset()
	{
		//Backup erzeugen
		if (!Directory.Exists(ImagesBackupPath))
		{
			Directory.CreateDirectory(ImagesBackupPath);
			foreach (string img in Directory.GetFiles(ImagesPath))
			{
				string fileName = Path.GetFileName(img);
				string backupImagePath = Path.Combine(ImagesBackupPath, fileName);
				File.Copy(img, backupImagePath, true);
			}
		}

		//Backup wiederherstellen
		foreach (string img in Directory.GetFiles(ImagesBackupPath))
		{
			string fileName = Path.GetFileName(img);
			string regularImagePath = Path.Combine(ImagesPath, fileName);
			if (!File.Exists(regularImagePath))
				File.Copy(img, regularImagePath);
		}

		//Output entleeren
		if (Directory.Exists(ImagesOutputPath))
			Directory.Delete(ImagesOutputPath, true);
		Directory.CreateDirectory(ImagesOutputPath);
	}
}