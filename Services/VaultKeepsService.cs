using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repo;

    public VaultKeepsService(VaultKeepsRepository repo)
    {
      _repo = repo;
    }

    internal VaultKeep Create(VaultKeep newVaultKeep)
    {
      return _repo.Create(newVaultKeep);
    }

    internal VaultKeep Delete(int id, string UserId)
    {
      VaultKeep found = _repo.Get(id);
      if (found.UserId != UserId)
      {
        throw new UnauthorizedAccessException("Invalid Request");
      }
      if (_repo.Delete(id))
      {
        return found;
      }
      throw new Exception("Something went terribly wrong");
    }
    internal VaultKeep DeleteVaultKeep(VaultKeep toRemove)
    {
      VaultKeep found = _repo.Get(toRemove);
      if (found.UserId != toRemove.UserId)
      {
        throw new UnauthorizedAccessException("Invalid Request");
      }
      if (_repo.DeleteVaultKeep(toRemove))
      {
        return found;
      }
      throw new Exception("Invalid data");
    }

  }//endof class
}//endof namespace