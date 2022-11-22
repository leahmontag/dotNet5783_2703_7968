
using BlApi;

namespace BlImplementation;

sealed public class Bl : IB1
{
    public IProduct Product { get; }
    public IOrder Order { get; }
    public ICart Cart { get; }
}
