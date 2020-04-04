using System.Collections.Generic;
using System.Data;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }
    internal IEnumerable<Vault> Get()
    {
      string sql = "SELECT * FROM vaults;";
      return _db.Query<Vault>(sql);
    }

    internal IEnumerable<Vault> GetMyVaults(string UserId)
    {
      string sql = "SELECT * FROM vaults WHERE userId = @UserId";
      return _db.Query<Vault>(sql, new { UserId });
    }

    internal Vault Get(int id)
    {
      string sql = "SELECT * FROM vaults WHERE id = @Id";
      return _db.QueryFirstOrDefault<Vault>(sql, new { Id = id });
    }

    internal Vault Create(Vault newVault)
    {
      string sql = @"
      INSERT INTO vaults
      (userId, name, description)
      VALUES
      (@UserId, @Name, @Description);
      SELECT LAST_INSERT_ID();
      ";
      newVault.Id = _db.ExecuteScalar<int>(sql, newVault);
      return newVault;
    }
    internal Vault Edit(Vault updatedVault)
    {
      string sql = @"
    UPDATE vaults SET
    name = @Name,
    description = @Description
    WHERE id = @Id
    ";
      _db.Execute(sql, updatedVault);
      return updatedVault;
    }//endof edit

    internal bool Delete(int Id)
    {
      string sql = "DELETE FROM vaults WHERE id = @Id LIMIT 1";
      int removed = _db.Execute(sql, new { Id });
      return removed == 1;
    }


  }//endof class
}//endof namespace