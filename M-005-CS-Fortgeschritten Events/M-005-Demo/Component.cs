namespace DelegatesEvents;

/// <summary>
/// Entwicklerseite
/// </summary>
internal class Component
{
	public event EventHandler Start;

	public event EventHandler End;

	public event EventHandler<int> Progress;

	/// <summary>
	/// Simuliert eine länger andauernde Arbeit
	/// </summary>
	public void DoWork()
	{
		Start?.Invoke(this, EventArgs.Empty);

		for (int i = 0; i < 10; i++)
		{
			Thread.Sleep(100);

			Progress?.Invoke(this, i);
		}

		End?.Invoke(this, EventArgs.Empty);
	}
}
