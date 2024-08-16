using Routing.Helpers;
using Routing.Interfaces;
using Routing.Model;
using System.Text;

namespace Routing
{
    public class Router : IRouter
    {
        private static Router _router;

        public Router router 
        {
            get 
            { 
                return _router ??= new Router();
            }
        }

        private Dictionary<string, Route> routingTable = new Dictionary<string, Route>();

        public void RegisterRoute(string template, Delegate action)
        {
            var routeData = new Route(template, action);
            routingTable.Add(template, routeData);
        }

        public void Route(string route)
        {
            /*  TODO Переделать получение маршрута, таким образом,
            *   что мы находим сначала все объекты с подходящим статическим
            *   маршрутом, потом выясняем, какие типы были переданы,
            *   проверяем есть ли маршрут с такой сигнатурой или нет
            */
            if (routingTable.TryGetValue(route, out var routeObject))
            {
                if(routeObject.ArgumentsInRoute.Count() == 0)
                {
                    routeObject.Method.DynamicInvoke();
                    return;
                }

                bool argumentNamesMatch = true;
                foreach (var argument in routeObject.ArgumentsInRoute)
                {
                    // Сопоставляем имена аргументов с именами параметров переданных в Delegate
                    if (!routeObject.ArgumentsInDelegate.ContainsKey(argument.Key))
                    {
                        argumentNamesMatch = false;
                        break;
                    }
                }


                // Получить аргументы из запроса и отправить их как параметры
                if (!argumentNamesMatch)
                {
                    routeObject.Method.DynamicInvoke();
                }
            }
        }
    }
}