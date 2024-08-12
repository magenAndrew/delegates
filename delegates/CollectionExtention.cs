using System.Reflection.Metadata.Ecma335;

namespace Delegates
{
    /// <summary>
    /// Расширение коллекций
    /// </summary>
    public static class EnumerableExtensions
    {
        public static T GetMax<T>(this IEnumerable<T> collection, Func<T, float> convertToNumber) where T : class
        {
            float tmp, maxSize = float.MinValue;
            T ret = default(T);
            var array = collection.ToArray();
            for (var i = 0; i < array.Length; i++)
            {
                tmp = convertToNumber(array[i]);
                if (tmp > maxSize)
                {
                    ret = array[i];
                    maxSize = tmp;
                }
            }
            return ret;
        }
    }
}
