namespace backapitest.Controllers;

using System.Collections.Generic;
using System.Threading.Tasks;
using backapitest.DTO;
using global::backapitest.DTO;
using Microsoft.AspNetCore.Mvc;

public interface ISupabaseService
{
    Task<SupabaseResponse> CreateUser(SupabaseRequest supabaseRequest);
    Task<SupabaseResponse> GetUserById(long id);
    Task<IEnumerable<SupabaseResponse>> GetAllUsers();
    Task<IActionResult?> SignUpUser(string email, string password);

}



