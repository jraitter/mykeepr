using System;
using System.Collections.Generic;
using System.Security.Claims;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class VaultsController : ControllerBase
  {
    private readonly VaultService _vs;

    public VaultsController(VaultService vs)
    {
      _vs = vs;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Vault>> Get()
    {
      try
      {
        return Ok(_vs.Get());
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      };
    }

    [HttpGet("myVaults")]
    [Authorize]
    public ActionResult<IEnumerable<Vault>> GetMyVaults()
    {
      try
      {
        string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Ok(_vs.GetMyVaults(userId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{vaultId}")]
    [Authorize]
    public ActionResult<Vault> Get(int vaultId)
    {
      try
      {
        //get the vault by Id
        return Ok(_vs.Get(vaultId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }//endof get by id

    [HttpPost]
    [Authorize]
    public ActionResult<Vault> Post([FromBody] Vault newVault)
    {
      try
      {
        // NOTE DONT TRUST THE USER TO TELL YOU WHO THEY ARE!!!!
        string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        newVault.UserId = userId;
        return Ok(_vs.Create(newVault));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{vaultId}")]
    [Authorize]
    public ActionResult<Vault> Edit(int vaultId, [FromBody] Vault updatedVault)
    {
      try
      {
        string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        updatedVault.UserId = userId;
        updatedVault.Id = vaultId;
        return Ok(_vs.Edit(updatedVault));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpDelete("{vaultId}")]
    [Authorize]
    public ActionResult<Vault> Delete(int vaultId)
    {
      try
      {
        // NOTE DONT TRUST THE USER TO TELL YOU WHO THEY ARE!!!!
        string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Ok(_vs.Delete(vaultId, userId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }//endof class
}//endof namespace