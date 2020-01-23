namespace Musaca.Services
{
    public interface IOrdersService
    {
        void AddProductToOrder(string cashierId, string productName);
    }
}