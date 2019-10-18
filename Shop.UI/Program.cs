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
using Shop.Domain;
using System;
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

      string connectionString = configurationRoot.GetConnectionString("DebugConnectionString");

      Category category = new Category
      {
        Name = "Бытовая техника",
        ImagePath = "C:/data",
      };

      Item item = new Item
      {
        Name = "Phone",
        ImagePath = "C:/data",
        Price = 50000,
        Description = "Мощный телефон",
        CategoryId = category.Id
      };

      User user = new User
      {
        Name = "Bekzat",
        Surname = "Toleutai",
        PhoneNumber = "+77774213007",
        Email = "toleutaib@gmail.com",
        Address = "Ильинка",
        Password = "samiykrutoiparol666789",
        VerificationCode = "548724"
      };

      using (var context = new ShopContext(connectionString))
      {
        context.Categories.Add(category);
        context.Items.Add(item);
        context.Users.Add(user);
        context.SaveChanges();
      }
    }
  }
}
