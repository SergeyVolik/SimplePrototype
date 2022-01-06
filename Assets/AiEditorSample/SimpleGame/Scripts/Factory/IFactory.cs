namespace SerV112.UtilityAI.Game
{
	/// <summary>
	/// Represents a factory.
	/// </summary>
	/// <typeparam name="T">Specifies the type to create.</typeparam>
	public interface IFactory<T>
	{
		T Create();
	}
}
