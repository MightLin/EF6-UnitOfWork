
# How to

- ## 在外部程式 ( service, api ) 取得 QueryRepository
  透過 `IUnitOfWork` 介面方法 _QueryRepository<TEntity>()_ 取得該實體的查詢用 IQueryRepository

```c#
// use factory get IUnitOfWork
IUnitOfWork uow = factory.UnitOfWork;
IBudgetRepository budgetResp = uow.QueryRepository<Budget>();
```

```c#
//use connection string
string connectionStr = "..."
IUnitOfWork uow　= new UnitOfWorkDb(connectionStr);
IBudgetRepository budgetResp = uow.QueryRepository<Budget>();
```

- ## 透過 Model Repository 查詢資料庫的實體

透過 `IQueryRepository` 介面的 _Get()_ 及 _GetAll()_ 方法篩選出需要的資料。

```c#
//example:BudgetService
public Budget GetBudget(int id)
{
	//取得符合條件一筆 Budget
	return budgetRepository.Get(bu => bu.BdgtId == id);
}

public IEnumerable<Budget> GetBudgets()
{
	//取得符合條件的多筆 Budget
	return budgetRepository.GetAll().Where(bu => bu.BdgtDocVoid != "Y");
}
```

- ## 透過 Model Repository 新增、刪除、修改資料庫的實體

透過 `IRepository` 介面的 _Create()_ 及 _Update()_ 及 _Delete()_ 方法，在變更結束後呼叫 `IUnitOfWork` 介面方法 _SaveChanges()_ 來儲存此次的變更。

```c#
//example:BudgetService
public void Create(int id)
{
	var newBudget = new Budget();
	budgetRepository.Create(newBudget);
	uow.SaveChanges();
}

public void Update(int id)
{
	//更新指定 id 的 Budget
	var budget = budgetRepository.Get(bd=>bd.BdgtId = id);
	/*
	...  do some change
	*/
	budgetRepository.Update(budget);
	uow.SaveChanges();
}

public void UpdateSomeValue(int id,int vender)
{
	//更新指定 id 的 Budget 的 VenderId 欄位
	budgetRepository.Update(new object[] { id }, bdgt => bdgt.VenderId , vender);
	uow.SaveChanges();
}

public void Delete(int id)
{
	//刪除指定 id 的 Budget
	var budget = budgetRepository.Get(bd=>bd.BdgtId = id);
	budgetRepository.Delete(budget);
	uow.SaveChanges();
}
```

- ## Begin transation

使用 `IUnitOfWork` 介面方法 _BeginTransaction()_

```c#
//example:BudgetService
public void UpdateSomething(int id)
{
	using (var tr = uow.BeginTransaction())
	{
		try
		{
			/*
			...  do some change
			*/

			uow.SaveChanges();
			tr.Commit();
		}
		catch (Exception)
		{
			tr.Rollback();
			throw;
		}
	}
}
```
