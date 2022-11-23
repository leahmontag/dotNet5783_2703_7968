using BlApi;

namespace BlImplementation;

sealed public class Bl : IB1
{
    public IProduct Product => new Product();
    public IOrder Order => new Order();
    public ICart Cart => new Cart();
}