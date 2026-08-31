/// <summary>
/// Diese Methode kann für das dritte Event bearbeitet werden
/// </summary>
public bool CheckPrime(int num)
{
	//Prüfe, ob die Zahl (num) gerade ist
	if (num % 2 == 0)
	{
		return false; //Keine Primzahl
	}

	//Prüfe, ob die Zahl (num) durch eine ungerade Zahl von 3 bis zur Hälfte der gegebenen Zahl teilbar ist
	for (int i = 3; i <= num / 2; i += 2)
	{
		if (num % i == 0)
		{
			return false; //Keine Primzahl
		}
	}
	return true; //Wenn alle Prüfungen false ergeben, haben ist der Parameter "num" eine Primzahl
}