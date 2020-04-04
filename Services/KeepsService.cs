using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _repo;
    public KeepsService(KeepsRepository repo)
    {
      _repo = repo;
    }
    public IEnumerable<Keep> Get()
    {
      return _repo.GetPublic();
    }

    internal IEnumerable<Keep> GetPrivateKeeps(string UserId)
    {
      return _repo.GetPrivateKeeps(UserId);
    }

    internal Keep Get(int Id)
    {
      //NOTE If you do not null check you could get a 204 (No Context) if the blog was not found
      Keep found = _repo.Get(Id);
      if (found == null)
      {
        throw new Exception("Invalid Id");
      }
      return found;
    }

    internal Keep Create(Keep newKeep)
    {
      return _repo.Create(newKeep);
    }

    internal Keep Edit(Keep updatedKeep)
    {
      Keep found = Get(updatedKeep.Id);
      if (found.UserId != updatedKeep.UserId)
      {
        throw new Exception("Invalid Requesto or Privileges");
      }
      found.Name = updatedKeep.Name;
      found.IsPrivate = updatedKeep.IsPrivate;
      found.Description = updatedKeep.Description != null ? updatedKeep.Description : found.Description;
      found.Img = updatedKeep.Img != null ? updatedKeep.Img : found.Img;
      found.Views = updatedKeep.Views != 0 ? updatedKeep.Views : found.Views;
      found.Shares = updatedKeep.Shares != 0 ? updatedKeep.Shares : found.Shares;
      found.Keeps = updatedKeep.Keeps != 0 ? updatedKeep.Keeps : found.Keeps;
      return _repo.Edit(found);
    }//endof edit

    internal Keep Delete(int id, string userId)
    {
      Keep found = Get(id);
      if (found.UserId != userId)
      {
        throw new Exception("Invalid Request");
      }
      if (_repo.Delete(id))
      {
        return found;
      }
      throw new Exception("Something went terribly wrong");
    }

  }//endof class
}//endof namespace