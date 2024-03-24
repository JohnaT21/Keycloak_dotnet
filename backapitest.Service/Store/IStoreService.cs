using backapitest.DTO;

namespace backapitest.Interface;

public interface IStoreService
{
    
    Task<StoreTableResponse>GetStoreById(long id);
    Task<IEnumerable<StoreTableResponse>> GetAllStoreTable();
    Task<StoreTableModel> CreateStore(StoreRegistrationModel storeTableResponse, String createdBy);

}