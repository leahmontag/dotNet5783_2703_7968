using DalApi;
namespace Dal;

sealed internal class DalList : IDal
{
    private static object syncRoot = new object();
    public static IDal Instance { get; }
    private DalList() { }
    static DalList()
    {
        if (Instance == null)
        {
            lock (syncRoot)
            {
                if (Instance == null)
                    Instance = new DalList();
            }
        }
    }
    public IProduct Product => new DalProduct();
    public IOrder Order => new DalOrder();
    public IOrderItem OrderItem => new DalOrderItem();

}