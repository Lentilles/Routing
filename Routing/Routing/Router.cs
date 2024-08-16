using Routing.Routing.Interfaces;

namespace Routing.Routing
{
    public class Router : IRouter
    {
        private Dictionary<string, RouteData> routingTable = new Dictionary<string, RouteData>();

        public void RegisterRoute(string template, Action action)
        {
            var routeData = new RouteData(template, action);
            routingTable.Add(template, routeData);
        }

        public void RegisterRoute<T1>(string template, Action<T1> action)
        {
            var routeData = new RouteData(template, action);
            routingTable.Add(template, routeData);
        }

        public void RegisterRoute<T1, T2>(string template, Action<T1, T2> action)
        {
            var routeData = new RouteData(template, action);
            routingTable.Add(template, routeData);
        }

        public void Route(string route)
        {
            if (routingTable.TryGetValue(route, out var result))
            {
                result.Method.DynamicInvoke();
            }
        }



        private class RouteData
        {
            public readonly Delegate Method;
            /// <summary>
            ///     ArgName - Type
            /// </summary>
            public readonly Dictionary<string, string> Arguments;
            public readonly string StaticPath;



            public RouteData(string path, Delegate method)
            {
                Method = method;
            }
        }
    }
}