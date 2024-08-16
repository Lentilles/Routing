using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Routing.Interfaces
{
    public interface IRouter
    {

        /*
         * Изменил контракты интерфейса на более универсальный параметр
         * За счет этого не нужно будет создавать новые контракты в случае необходимости
         * С любым количеством параметров
         * Но тогда у нас меняется объявление аргументов с (a, b) на (T a, T b)
         */

        /// <summary>
        /// Регистрирует маршрут формата /foo/bar/{name:type}/{name2:type}/
        /// /foo/bar - статическая часть.
        /// Где {name:type} - динамическая часть.
        /// </summary>
        /// <param name="template">Шаблон маршрута</param>
        /// <param name="action">Делегат с любым набором аргументов</param>
        void RegisterRoute(string template, Delegate action);
        void Route(string route);
    }
}
