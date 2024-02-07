﻿using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EfCore.Extensions
{
    public static class OrderQueryBuilder
    {
      public static String CreateOrderQuery<T>(String orderByQueryString)
        {
            var orderParams = orderByQueryString.Trim().Split(",");

            var propertyInfos = typeof(T)
               .GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();
            foreach (var param in orderParams)
            {
                if (string.IsNullOrEmpty(param))
                    continue;
                var propertyQueryName = param.Split(' ')[0];
                var objectProperty = propertyInfos
                    .FirstOrDefault(pi => pi.Name.Equals(propertyQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty is null)
                    continue;
                var direction = param.EndsWith(" desc") ? "descending" : "ascending";

                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction},");
               

            }
            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            return orderQuery;
        }
    }
}
