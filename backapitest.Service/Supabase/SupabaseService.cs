
using backapitest.DTO;
using backapitest.Model;
using Microsoft.AspNetCore.Mvc;

namespace backapitest.Controllers
{
    public class SupabaseService : ISupabaseService
    {
        private readonly Supabase.Client _supabaseClient;

        public SupabaseService(Supabase.Client supabaseClient)
        {
            _supabaseClient = supabaseClient;
        }

        public async Task<SupabaseResponse> CreateUser(SupabaseRequest supabaseRequest)
        {
            var user = new User
            {
                Name = supabaseRequest.Name,
                Description = supabaseRequest.Description,
                Age = supabaseRequest.Age,
            };

            var response = await _supabaseClient.From<User>().Insert(user);
            var newUser = response.Models.First();

            var userModel = new SupabaseResponse
            {
                Id = newUser.Id,
                Name = newUser.Name,
                Age = newUser.Age,
                Description = newUser.Description,
            };

            return userModel;
        }

        public async Task<SupabaseResponse> GetUserById(long id)
        {
            var user = await _supabaseClient.From<User>()
                                .Where(n => n.Id == id)
                                .Get();
            var uewUser = user.Models.FirstOrDefault();

            if (uewUser is null)
            {
                return null;
            }

            var userModel = new SupabaseResponse
            {
                Id = id,
                Name = uewUser.Name,
                Age = uewUser.Age,
                Description = uewUser.Description,
            };

            return userModel;
        }

        public async Task<IEnumerable<SupabaseResponse>> GetAllUsers()
        {
            var users = await _supabaseClient.From<User>().Get();
            var allUsers = users.Models;

            if (allUsers is null)
            {
                return null;
            }

            var userModelResponse = allUsers.Select(user => new SupabaseResponse
            {
                Id = user.Id,
                Name = user.Name,
                Age = user.Age,
                Description = user.Description,
            });

            return userModelResponse;
        }

        public async Task<IActionResult?> SignUpUser(string email, string password)
        {

            var session = await _supabaseClient.Auth.SignUp(email, password);
           

            return session as IActionResult;
        }
    }
}
