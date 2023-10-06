using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorAuthGoogleProgileClaimsPictureVideo;

[Route("/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
  [HttpGet("google")]
  public async Task<ActionResult> Google()
  {
    var properties = new AuthenticationProperties
    {
      RedirectUri = "/"
    };
    return Challenge(properties, GoogleDefaults.AuthenticationScheme);
  }

}