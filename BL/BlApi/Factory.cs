
using BlImplementation;

namespace BlApi
{
    public class Factory
    {
        public static IBl Get()
        {
            Bl a=new Bl();
            return a;
        }
    }
}
