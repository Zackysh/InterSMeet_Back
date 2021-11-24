﻿using System;
using System.Diagnostics;

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InterSMeet.DAL.Repositories
{
    internal class EntityPropertyMapper
    {
        private static class PropertyLister<T1, T2>
        {
            public static readonly IEnumerable<Tuple<PropertyInfo, PropertyInfo>> PropertyMap;

            static PropertyLister()
            {
                var b = BindingFlags.Public | BindingFlags.Instance;
                PropertyMap =
                    (from f in typeof(T1).GetProperties(b)
                     join t in typeof(T2).GetProperties(b) on f.Name equals t.Name
                     select Tuple.Create(f, t))
                        .ToArray();
            }
        }


        public static T InjectNonNull<T>(T dest, T src)
        {
            foreach (var propertyPair in PropertyLister<T, T>.PropertyMap)
            {
                var fromValue = propertyPair.Item2.GetValue(src, null);
                if (fromValue != null && propertyPair.Item1.CanWrite)
                {
                    if ((propertyPair.Item1.PropertyType.Name == "Int32" && (int)fromValue == 0)) continue;
                    propertyPair.Item1.SetValue(dest, fromValue, null);
                }


                Debug.WriteLine(dest);
            }

            return dest;
        }
    }
}
