using Routing.Helpers;
using Routing.Interfaces;
using Routing.Model;
using System.Text.RegularExpressions;

namespace Routing
{
    public class Router : IRouter
    {
        private static Router? _router;

        public Router router 
        {
            get 
            { 
                return _router ??= new Router();
            }
        }

        private Dictionary<string, Route> routingTable = new Dictionary<string, Route>();

        /// <summary>
        /// Регистрирует маршрут
        /// </summary>
        /// <param name="template"></param>
        /// <param name="action"></param>
        public void RegisterRoute(string template, Delegate action)
        {
            var routeData = new Route(template, action);
            routingTable.Add(template, routeData);
        }

        //TODO зарефакторить
        /// <summary>
        /// Вызывает делегат по зарегистрированному маршруту
        /// </summary>
        /// <param name="route"></param>
        public async void Route(string route)
        {
            route = await FixRouteIfInvalid(route);

            // Либо маршрута не существует, либо он динамический
            if (!routingTable.ContainsKey(route))
            {
                var staticRoutePart = route;

                Dictionary<string, Route> routesWithSameStaticRoute = new();
                while (routesWithSameStaticRoute.Count() == 0)
                {
                    staticRoutePart = Regex.Replace(staticRoutePart, "([^/]*)/$", "");
                    routesWithSameStaticRoute = routingTable.Where(x => x.Value.StaticPath == staticRoutePart).ToDictionary();
                }

                var argumentsInRoute = route.Replace(staticRoutePart, "");

                var arguments = argumentsInRoute.Split("/", StringSplitOptions.RemoveEmptyEntries);

                List<string> argumentNames = new();

                foreach (var argument in arguments)
                {
                    argumentNames.Add(await TypeParser.GetTypeNameFromStringValueAsync(argument));
                }

                var result = await GetRouteByArgumentTypeMatch(routesWithSameStaticRoute, argumentNames);

                List<object?> args = new();

                foreach (var argument in arguments)
                {
                    args.Add(await TypeParser.ConvertFromStringToObjectAsync(argument));
                }


                if (result != null)
                    result.Method.DynamicInvoke(args.ToArray());

                return;
            }


            // Только для статических маршрутов
            if (routingTable.TryGetValue(route, out var routeObject))
            {
                if (routeObject.ArgumentsInRoute.Count() == 0)
                {
                    routeObject.Method.DynamicInvoke();
                    return;
                }
            }
        }

        /// <summary>
        /// Исправляет маршрут, если в начале или в конце не содержит символ '/'
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        private static Task<string> FixRouteIfInvalid(string route)
        {
            if (route.First() != '/')
                route = route.Insert(0, "/");
            if (route.Last() != '/')
                route.Append('/');

            return Task.FromResult(route);
        }


        /// <summary>
        /// Среди маршрутов с одинаковым статическим маршрутом находит тот который подходит по типам
        /// </summary>
        /// <param name="routesWithSameStaticRoute"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        private Task<Route?> GetRouteByArgumentTypeMatch(Dictionary<string, Route> routesWithSameStaticRoute, List<string> types)
        {
            Route? result = null;

            foreach (var routeRow in routesWithSameStaticRoute)
            {
                if (routeRow.Value.ArgumentsInRoute.Count() != types.Count())
                    continue;

                for (int i = 0; i < types.Count; i++)
                {
                    if (routeRow.Value.ArgumentsInRoute.ElementAt(i).Value != types[i])
                        break;

                    if (i == types.Count - 1)
                    {
                        result = routeRow.Value;
                    }
                }
            }

            return Task.FromResult(result);
        }
    }
}