using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilitiesLibrary
{
    public static class Arr
    {
        public static T[]? Merge<T>(T[]? source, T[] target)
        {
            if (target.Length == 0) return source;
            if(source is null) return target;

            var currentLength = source?.Length ?? 0;
            var newRes = new T[currentLength + target.Length];

            source?.CopyTo(newRes, 0);
            target.CopyTo(newRes, currentLength);
            return newRes;
        }
    }
}
