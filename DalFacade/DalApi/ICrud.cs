namespace DalApi
{
    public interface ICrud<T>
    {
        public int Create(T val);
        public void Delete(int val);
        public void Update(T val);
        public T Get(int val);
        public IEnumerable<T?> GetAll();
    }
}


