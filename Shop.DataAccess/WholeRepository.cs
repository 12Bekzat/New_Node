using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Shop.DataAccess
{
  public class WholeRepository : IDisposable
  {
    // 1. в конструкторе открываем подключение
    // 2. реализуем Idisposable(как раньше) 
    // 3. создаем переменные для каждого из наших репозиториев,
    //    которые пользуются единым подключением

    private readonly DbConnection connection;
    private readonly DbProviderFactory providerFactory;

    public UserRepository Users { get; set; }

    public WholeRepository(string providerName, string connectionString)
    {
      providerFactory = DbProviderFactories.GetFactory(providerName);

      connection = providerFactory.CreateConnection();
      connection.ConnectionString = connectionString;
      connection.Open();

      Users = new UserRepository(connection);
    }

    public void Dispose()
    {
      connection.Close();
    }
  }
}
