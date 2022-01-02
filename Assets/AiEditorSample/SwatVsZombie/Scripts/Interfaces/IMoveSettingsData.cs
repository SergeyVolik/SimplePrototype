namespace SerV112.UtilityAI.Game
{

	public interface IMoveSpeed
	{
		float MoveSpeed { get; set; }
	}

	public interface IRunSpeed
	{
		float RunSpeed { get; set; }
	}

	public interface IRotationSpeed
	{
		float RotationSpeed { get; set; }

	}

	public interface ICurrentSpeed
	{
		float CurrentSpeed { get; set; }
	}
	public interface IMoveSettingsData : IRotationSpeed, IMoveSpeed, IRunSpeed, ICurrentSpeed
	{
	
		
	}
}
