namespace DalApi
{
    public interface ICrud<T>
    {
        public T Add(T val);
        public T Delete(T val);
        public T Update(T val);
        public IEnumerable<T> Get(T val);

    }
}


