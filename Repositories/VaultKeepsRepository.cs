using System.Data;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }
    internal VaultKeep Create(VaultKeep newVaultKeep)
    {
      string sql = @"
            INSERT INTO vaultkeeps 
            (keepId, vaultId, userId) 
            VALUES 
            (@KeepId, @VaultId, @UserId);
            SELECT LAST_INSERT_ID();
            ";
      newVaultKeep.Id = _db.ExecuteScalar<int>(sql, newVaultKeep);
      return newVaultKeep;
    }

    internal VaultKeep Get(int Id)
    {
      string sql = "SELECT * FROM vaultkeeps WHERE id = @Id";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { Id });
    }

    internal VaultKeep Get(VaultKeep toFind)
    {
      string sql = @"
      SELECT * FROM vaultkeeps 
      WHERE keepId = @KeepId AND vaultId=@VaultId AND userId=@UserId";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, toFind);
    }

    internal bool DeleteVaultKeep(VaultKeep vaultKeep)
    {
      string sql = @"DELETE FROM vaultkeeps 
      WHERE keepId = @KeepId AND vaultId=@VaultId AND userId=@UserId";
      int removed = _db.Execute(sql, vaultKeep);
      return removed == 1;
    }

    internal bool Delete(int Id)
    {
      string sql = "DELETE FROM vaultkeeps WHERE id = @Id LIMIT 1";
      int removed = _db.Execute(sql, new { Id });
      return removed == 1;
    }

  }//endof class
}//endof namesapce