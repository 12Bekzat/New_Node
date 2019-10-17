using Shop.Domain;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Shop.DataAccess
{
  public class UserRepository
  {
    private readonly DbConnection connection;

    public UserRepository(DbConnection connection)
    {
      this.connection = connection;
    }
    public void Add(User user)
    {

    }
    public void Delete (Guid id)
    {

    }

  }
}
