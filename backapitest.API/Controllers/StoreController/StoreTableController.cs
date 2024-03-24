using backapitest.DTO;
using backapitest.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Supabase;

namespace backapitest.Controllers;

[ApiController]
[Route("[controller]")]
public class StoreTableController : ControllerBase
{
    private readonly IStoreService _storeService;
    private readonly Supabase.Client _supabaseClient;


    public StoreTableController(IStoreService storeService, Client supabaseClient)
    {
        _storeService = storeService;
        _supabaseClient = supabaseClient;
    }

    [HttpPost("/createStore")]
    [Authorize]
    public async Task<IActionResult> CreateStore(StoreRegistrationModel storeModel)
    {
        // Call the service to create a user
        
        var userName = User.Claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value; // get user from token
        var result = await _storeService.CreateStore(storeModel, userName);
        return Ok(result);
    }

    [HttpGet("/store/{id}")]
    public async Task<IActionResult> GetStoreById(long id)
    {
        // Call the service to get a user by ID
        var result = await _storeService.GetStoreById(id);
        return result != null ? Ok(result) : NotFound();
    }

    [HttpGet("/allStore")]
    [Authorize]
    public async Task<IActionResult> GetAllUsers()
    {
        // Call the service to get all users
        var result = await _storeService.GetAllStoreTable();
        if (result.Any())
        {
            return Ok(new { data = result });
        }

        else
        {
            return Ok(new List<object>());
        }
    }
}