namespace DalApi
{
    public interface ICrud<T> where T : struct
    {
        public int Create(T val);
        public void Delete(int val);
        public void Update(T val);
        public T Get(Func<T?, bool>? a);
        public IEnumerable<T?> GetAll(Func<T?, bool>? a = null);
    }
}


