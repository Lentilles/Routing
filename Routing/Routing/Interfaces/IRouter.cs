using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Routing.Routing.Interfaces
{
    public interface IRouter
    {
        void RegisetRoute<T1>(string template, Action<T1> action);
        void RegisetRoute<T1, T2>(string template, Action<T1, T2> action);

        void Route(string route);
    }
}
