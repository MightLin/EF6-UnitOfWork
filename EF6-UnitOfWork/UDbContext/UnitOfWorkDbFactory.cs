namespace EF6_UnitOfWork.UDbContext
{

	/// <summary>
	/// 
	/// </summary>
	public class UnitOfWorkDbFactory : IUnitOfWorkFactory
	{
		public UnitOfWorkDbFactory()
		{

		}

		public UnitOfWorkDbFactory(string nameOrConnectionString)
		{
			NameOrConnectionString = nameOrConnectionString;
		}

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

		public string NameOrConnectionString { get; protected set; }

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