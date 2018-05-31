namespace EF6_UnitOfWork
{

	/// <summary>
	/// 
	/// </summary>
	public interface IUnitOfWorkFactory
	{
		IUnitOfWork UnitOfWork { get; }
	}
}