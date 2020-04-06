using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Dapper;

namespace Keepr.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal IEnumerable<Keep> GetPublic()
    {
      string sql = "SELECT * FROM Keeps WHERE isPrivate = 0;";
      return _db.Query<Keep>(sql);
    }

    internal IEnumerable<Keep> GetMyKeeps(string UserId)
    {
      string sql = "SELECT * FROM keeps WHERE userId = @UserId";
      return _db.Query<Keep>(sql, new { UserId });
    }

    internal Keep Get(int id)
    {
      string sql = "SELECT * FROM keeps WHERE id = @Id";
      return _db.QueryFirstOrDefault<Keep>(sql, new { Id = id });
    }
    // internal IEnumerable<Keep> GetKeepsByVaultId(int Id)
    // {
    //   string sql = @"
    //         SELECT k.* FROM vaultkeeps vk
    //         INNER JOIN keeps k ON k.id = vk.keepId
    //         WHERE vaultId = @Id;";
    //   return _db.Query<Keep>(sql, new { Id });
    // }
    internal IEnumerable<Keep> GetKeepsByVaultId(int vaultId, string userId)
    {
      string sql = @"
      SELECT 
      k.*,
      vk.id as vaultKeepId
      FROM vaultkeeps vk
      INNER JOIN keeps k ON k.id = vk.keepId 
      WHERE (vaultId = @VaultId AND vk.userId = @UserId) 
      ";
      return _db.Query<Keep>(sql, new { vaultId, userId });
    }
    internal Keep Create(Keep newKeep)
    {
      string sql = @"
      INSERT INTO keeps
      (userId, name, description, img, isPrivate, views, shares, keeps)
      VALUES
      (@UserId, @Name, @Description, @Img, @IsPrivate, @Views, @Shares, @Keeps);
      SELECT LAST_INSERT_ID();
      ";
      newKeep.Id = _db.ExecuteScalar<int>(sql, newKeep);
      return newKeep;
    }
    internal Keep Edit(Keep updatedKeep)
    {
      string sql = @"
    UPDATE keeps SET
    name = @Name,
    description = @Description,
    img = @Img,
    isPrivate = @IsPrivate,
    views = @Views,
    shares = @Shares,
    keeps = @Keeps
    WHERE id = @Id
    ";
      _db.Execute(sql, updatedKeep);
      return updatedKeep;
    }//endof edit

    internal bool Delete(int Id)
    {
      string sql = "DELETE FROM keeps WHERE id = @Id LIMIT 1";
      int removed = _db.Execute(sql, new { Id });
      return removed == 1;
    }

  }//endof class
}//endof namespace