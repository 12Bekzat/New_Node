using Shop.DataAccess.Abstract;
using Shop.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;

namespace Shop.DataAccess
{
  public class CategoryRepository : ICategoryRepository
  {
    /*
     * 1. Открыть подключения
     * 2. Создать запрос 
     * 3. Выполнить запрос
     * 4. Закрыть подключение
     */

    private readonly string connectionString;
    private readonly DbProviderFactory providerFactory;

    public CategoryRepository(string connectionString, string providerName)
    {
      this.connectionString = connectionString;
      providerFactory = DbProviderFactories.GetFactory(providerName);
    }

    public void Add(Category category)
    {
      using (DbConnection connection = providerFactory.CreateConnection())
      using (DbCommand sqlCommand = connection.CreateCommand())
      {
        string query = $"insert into Categories(Id, creationDate, name, imagePath) values(@Id, " +
            $"@CreationDate, " +
            $"@Name, " +
            $"@ImagePath);";
        sqlCommand.CommandText = query;

        DbParameter parameter = providerFactory.CreateParameter ();
        parameter.DbType = System.Data.DbType.Guid;
        parameter.ParameterName = "@Id";
        parameter.Value = category.Id;
        sqlCommand.Parameters.Add(parameter);

        parameter = providerFactory.CreateParameter();
        parameter.DbType = System.Data.DbType.DateTime;
        parameter.ParameterName = "@CreationDate";
        parameter.Value = category.Id;
        sqlCommand.Parameters.Add(parameter);



        parameter = providerFactory.CreateParameter();
        parameter.DbType = System.Data.DbType.String;
        parameter.ParameterName = "@Name";
        parameter.Value = category.Id;
        sqlCommand.Parameters.Add(parameter);

        parameter = providerFactory.CreateParameter();
        parameter.DbType = System.Data.DbType.String;
        parameter.ParameterName = "@ImagePath";
        parameter.Value = category.Id;
        sqlCommand.Parameters.Add(parameter);

        connection.ConnectionString = connectionString;
        connection.Open();
        using (DbTransaction transaction = connection.BeginTransaction())
        {
          try
          {
            sqlCommand.Transaction = transaction;
            sqlCommand.ExecuteNonQuery();
            // и так далее тоже самое с другими командами

            transaction.Commit();
          }
          catch
          {
            transaction.Rollback();
          }
        }
      }
    }

    public void Delete(Guid categoryId)
    {
      //using (SqlConnection connection = new SqlConnection(connectionString))
      //using (SqlCommand sqlCommand = connection.CreateCommand())
      //{
      //  string query = $"update Category" +
      //    $"set deletedDate = '{DateTime.Now}'" +
      //    $"where Id = '{categoryId}'";
      //  sqlCommand.CommandText = query;

      //  connection.Open();
      //  sqlCommand.ExecuteNonQuery();
      //}
    }

    public ICollection<Category> GetAll()
    {
      using (DbConnection connection = providerFactory.CreateConnection())
      using (DbCommand sqlCommand = connection.CreateCommand())
      {
        string query = $"select * from Categories";
        sqlCommand.CommandText = query;

        connection.ConnectionString = connectionString;
        connection.Open();
        DbDataReader sqlDataReader = sqlCommand.ExecuteReader();

        List<Category> categories = new List<Category>();
        while (sqlDataReader.Read())
        {
          categories.Add(new Category
          {
            Id = Guid.Parse(sqlDataReader["Id"].ToString()),
            CreationDate = DateTime.Parse(sqlDataReader["creationDate"].ToString()),
            DeletedDate = DateTime.Parse(sqlDataReader["deletedDate"].ToString()),
            Name = sqlDataReader["name"].ToString(),
            ImagePath = sqlDataReader["imagePath"].ToString(),
          });
        }

        return categories;
      }
    }

    public void Update(Category category, Guid tempId)
    {
      //using (SqlConnection connection = new SqlConnection(connectionString))
      //using (SqlCommand sqlCommand = connection.CreateCommand())
      //{
      //  string query = $"update Categories" +
      //    $"set Id = '{category.Id}', " +
      //    $"creationDate = '{category.CreationDate}'," +
      //    $"deletedDate = '{category.DeletedDate}', " +
      //    $"name = '{category.Name}', " +
      //    $"imagePath = '{category.ImagePath}'" +
      //    $"where Id = '{tempId}';";
      //  sqlCommand.CommandText = query;

      //  connection.Open();
      //  sqlCommand.ExecuteNonQuery();
      //}
    }
  }
}
