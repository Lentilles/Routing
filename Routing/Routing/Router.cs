using Routing.Routing.Interfaces;

namespace Routing.Routing
{

    public class Router : IRouter
    {
        public void RegisetRoute<T1>(string template, Action<T1> action)
        {
            throw new NotImplementedException();
        }

        public void RegisetRoute<T1, T2>(string template, Action<T1, T2> action)
        {
            throw new NotImplementedException();
        }

        public void Route(string route)
        {
            throw new NotImplementedException();
        }
    }
}
