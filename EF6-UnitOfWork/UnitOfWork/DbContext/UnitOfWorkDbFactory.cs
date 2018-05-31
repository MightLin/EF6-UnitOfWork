namespace EF6_UnitOfWork
{

	/// <summary>
	/// 
	/// </summary>
	public abstract class UnitOfWorkDbFactory : IUnitOfWorkFactory
	{
		private UnitOfWorkDb _unitOfWork;
		public virtual IUnitOfWork UnitOfWork
		{
			get
			{
				if (this._unitOfWork == null)
				{
					//					Type t = typeof(DbContext);
					this._unitOfWork = CreateEntities();
				}
				return _unitOfWork;
			}
		}

		public abstract string NameOrConnectionString { get; protected set; }

		/// <summary>
		/// Creates the entities.
		/// </summary>
		/// <returns></returns>
		private UnitOfWorkDb CreateEntities()
		{
			return new UnitOfWorkDb(NameOrConnectionString);
		}

	}
}