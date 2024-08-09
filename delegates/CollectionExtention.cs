namespace Delegates
{
    public static class IEnuberableExtentions
    {
        public static T GetMax<T>(this IEnumerable<T> collection, Func<T, float> convertToNumber) => collection.FirstOrDefault(predicate: p => convertToNumber(p) == collection.Max(p => convertToNumber(p)));
        
    }
}
