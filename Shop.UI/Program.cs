/*
 *1.(смс код / email код)
 * 2.
 * 3.(картинка в файловой системе
 * 4.PayPal/Qiwi/etc
 * 5.
 * 6.пагинация
*/
using Microsoft.Extensions.Configuration;
using Shop.DataAccess;
using Shop.DataAccess.Abstract;
using Shop.Domain;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace Shop.UI
{
  class Program
  {
    static void Main(string[] args)
    {
      var builder = new ConfigurationBuilder()
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", false, true);

      IConfigurationRoot configurationRoot = builder.Build();

      string providerName = configurationRoot
            .GetSection("AppConfig")
            .GetChildren().Single(item => item.Key == "ProviderName")
            .Value;

      DbProviderFactories.RegisterFactory(providerName, SqlClientFactory.Instance);

      Category category = new Category
      {
        Name = "Бытовая техника",
        ImagePath = "C:/data",
      };

      string connectionString = configurationRoot.GetConnectionString("DebugConnectionString");

      ICategoryRepository repository = new CategoryRepository(
        connectionString,
        providerName
        );
      repository.Add(category);

      var result = repository.GetAll();
    }
  }
}
