using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Padoru.Lib.API
{
    public static partial class Extensions
    {
        /// <summary>
        /// Ищет и возвращает реализации интерфейса из <see cref="Assembly"/>
        /// </summary>
        /// <param name="assembly"><see cref="Assembly"/> в котором нужно искать</param>
        /// <param name="needle">Искомый <see cref="Type">тип интерфейса</see></param>
        public static IEnumerable<Type> FindInterfaces(this Assembly assembly, Type needle)
        {
            return assembly
                .GetTypes()
                .Where(type => !type.IsAbstract && type.GetInterfaces().Contains(needle));
        }

        /// <summary>
        /// Ищет и возвращает производные от <see cref="Type"/> классы в <see cref="Assembly"/>
        /// </summary>
        /// <param name="assembly"><see cref="Assembly"/> в котором нужно искать</param>
        /// <param name="needle">Искомый <see cref="Type">тип объекта</see></param>
        public static IEnumerable<Type> FindBaseClasses(this Assembly assembly, Type needle)
        {
            return assembly
                .GetTypes()
                .Where(type =>
                {
                    var curr = type;

                    while (curr != null)
                    {
                        curr = curr.BaseType;

                        if (curr == needle)
                        {
                            return true;
                        }
                    }

                    return false;
                });
        }
    }
}