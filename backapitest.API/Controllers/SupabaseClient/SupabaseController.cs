using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backapitest.Controllers;
using backapitest.DTO;
using backapitest.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Supabase;

[ApiController]
[Route("[controller]")]
public class SupabaseController : ControllerBase
{
    private readonly ISupabaseService _supabaseService;
    private readonly Supabase.Client _supabaseClient;


    public SupabaseController(ISupabaseService supabaseService, Client supabaseClient)
    {
        _supabaseService = supabaseService;
        _supabaseClient = supabaseClient;
    }

    [HttpPost("/supabase")]
    public async Task<IActionResult> CreateUser(SupabaseRequest supabaseRequest)
    {
        // Call the service to create a user
        var result = await _supabaseService.CreateUser(supabaseRequest);
        return Ok(result);
    }

    [HttpGet("/user/{id}")]
    public async Task<IActionResult> GetUserById(long id)
    {
        // Call the service to get a user by ID
        var result = await _supabaseService.GetUserById(id);
        return result != null ? Ok(result) : NotFound();
    }

    [HttpGet("/allUser")]
    [Authorize]
    public async Task<IActionResult> GetAllUsers()
    {
        // Call the service to get all users
        var userId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
        var userName = User.Claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value;
        var userEmail = User.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
        

        var result = await _supabaseService.GetAllUsers();
        if (result.Any())
        {
            return Ok(new { data = userName });
        }

        else
        {
            return Ok(new List<object>());
        }
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
    {
        try
        {
            // Validate request data if needed

            var session = await _supabaseService.SignUpUser(request.Email, request.Password);

            return Ok("");
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("signIn")]
    public async Task<IActionResult> SignIn([FromBody] SignUpRequest request)
    {
        try
        {
            // Validate request data if needed
          var session = await _supabaseClient.Auth.SignIn(request.Email, request.Password);
          //  var didSendMagicLink = await _supabaseClient.Auth.SendMagicLink(request.Email);

          

            return Ok("");
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}
