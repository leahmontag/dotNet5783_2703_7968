using DO;
namespace DalApi;
public interface IProduct : ICrud<Product>
{
    Product existProductID(int num);
}

