
using backapitest.DTO;
using backapitest.Model;
using Microsoft.AspNetCore.Mvc;
using static System.DateTime;

namespace backapitest.Interface;

public class StoreService : IStoreService {

private readonly Supabase.Client _supabaseClient;

        public StoreService(Supabase.Client supabaseClient)
        {
            _supabaseClient = supabaseClient;
        }

        public async Task<StoreTableModel> CreateStore(StoreRegistrationModel supabaseRequest, String createdBy)
        {
            var storeRegistration = new StoreRegistration
            {
                CreatedAt = Today.ToString(),
                CreatedBy = createdBy,
                Quantity = supabaseRequest.Quantity,
                PackageName = supabaseRequest.PackageName
            };

            var response = await _supabaseClient.From<StoreRegistration>().Insert(storeRegistration);
            var newUser = response.Models.First();

            var userModel = new StoreTableModel
            {
                
                PackageName = newUser.PackageName,
                Quantity = newUser.Quantity,
                CreatedBy = newUser.CreatedBy,
                CreatedAt = newUser.CreatedAt
            };

            return userModel;
        }

        public async Task<StoreTableResponse> GetStoreById(long id)
        {
            var user = await _supabaseClient.From<StoreRegistration>()
                                .Where(n => n.Id == id)
                                .Get();
            var newUser = user.Models.FirstOrDefault();

            if (newUser is null)
            {
                return null;
            }

            var userModel = new StoreTableResponse
            {
                Id = newUser.Id,
                PackageName = newUser.PackageName,
                Quantity = newUser.Quantity,
                CreatedBy = newUser.CreatedBy,
                CreatedAt = newUser.CreatedAt
            };

            return userModel;
        }

        public async Task<IEnumerable<StoreTableResponse>> GetAllStoreTable()
        {
            var users = await _supabaseClient.From<StoreRegistration>().Get();
            var allUsers = users.Models;

            if (allUsers is null)
            {
                return null;
            }

            var userModelResponse = allUsers.Select(user => new StoreTableResponse
            {
                Id = user.Id,
                PackageName = user.PackageName,
                Quantity = user.Quantity,
                CreatedBy = user.CreatedBy,
                CreatedAt = user.CreatedAt
            });

            return userModelResponse;
        }
    }