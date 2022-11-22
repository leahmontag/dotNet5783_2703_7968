using DO;
namespace DalApi
{
    public interface IDal//internal???
    {
        public IProduct Product { get; }
        public IOrder Order { get; }
        public IOrderItem OrderItem { get; }
    }
}
