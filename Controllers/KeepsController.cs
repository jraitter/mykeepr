using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Keepr.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class KeepsController : ControllerBase
  {
    private readonly KeepsService _ks;
    public KeepsController(KeepsService ks)
    {
      _ks = ks;
    }
    [HttpGet]
    public ActionResult<IEnumerable<Keep>> Get()
    {
      try
      {
        return Ok(_ks.Get());
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      };
    }

    [HttpGet("myKeeps")]
    [Authorize]
    public ActionResult<IEnumerable<Keep>> GetMyKeeps()
    {
      try
      {
        string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Ok(_ks.GetMyKeeps(userId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{keepId}")]
    public ActionResult<Keep> Get(int keepId)
    {
      try
      {
        //get the keep by Id
        Keep keep = _ks.Get(keepId);
        if (keep.IsPrivate)
        {
          //check if use is logged in
          var user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
          //if logged in then user is owner
          if (user != null && user.Value == keep.UserId)
          {
            return Ok(keep);
          }
          return Unauthorized("You do not have access privileges to this Keep");
        }
        return Ok(keep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }//endof get by id

    [HttpPost]
    [Authorize]
    public ActionResult<Keep> Post([FromBody] Keep newKeep)
    {
      try
      {
        // NOTE DONT TRUST THE USER TO TELL YOU WHO THEY ARE!!!!
        string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        newKeep.UserId = userId;
        return Ok(_ks.Create(newKeep));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{keepId}")]
    [Authorize]
    public ActionResult<Keep> Edit(int keepId, [FromBody] Keep updatedKeep)
    {
      try
      {
        string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        updatedKeep.UserId = userId;
        updatedKeep.Id = keepId;
        return Ok(_ks.Edit(updatedKeep));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPut("{keepId}/viewcount")]
    public ActionResult<Keep> ViewCount(int keepId)
    {
      try
      {
        return Ok(_ks.ViewCount(keepId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPut("{keepId}/sharecount")]
    public ActionResult<Keep> ShareCount(int keepId)
    {
      try
      {
        return Ok(_ks.ShareCount(keepId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPut("{keepId}/keepcount")]
    public ActionResult<Keep> KeepCount(int keepId)
    {
      try
      {
        return Ok(_ks.KeepCount(keepId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{keepId}")]
    [Authorize]
    public ActionResult<Keep> Delete(int keepId)
    {
      try
      {
        // NOTE DONT TRUST THE USER TO TELL YOU WHO THEY ARE!!!!
        string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Ok(_ks.Delete(keepId, userId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }//endof class
}//endof namespace