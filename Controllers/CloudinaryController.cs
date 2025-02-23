using CampaignNexus.Models.DTOs;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CampaignNexus.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CloudinaryController : ControllerBase
{
    private Cloudinary _cloudinary;

    public CloudinaryController(IOptions<CloudinarySettings> config)
    {
        var account = new Account(
            config.Value.CloudName,
            config.Value.ApiKey,
            config.Value.ApiSecret
        );

        _cloudinary = new Cloudinary(account);
    }

    [HttpPost("delete-image")]
    [Authorize]
    public IActionResult DeleteImage([FromBody] DeleteImageDTO image)
    {
        try
        {
           var deletionParams = new DeletionParams(image.PublicId);
           var result = _cloudinary.Destroy(deletionParams);

           if (result.Result == "ok")
           {
            return Ok("Image deleted successfully");
           }
           else
           {
            return BadRequest("Failed to delete image");
           }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error deleting image: {ex}");
            return StatusCode(500, "Error deleting image");
        }
    }
}