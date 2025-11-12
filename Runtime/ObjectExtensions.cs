

namespace FreakshowStudio.UnityObjectOrDefault.Runtime
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Returns the object if it is valid; otherwise, returns null.
        /// Specifically checks if the object is a UnityEngine.Object
        /// and ensures it is not destroyed.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the object being checked.
        /// </typeparam>
        /// <param name="obj">
        /// The object to validate or provide a default for.
        /// </param>
        /// <returns>
        /// The input object if valid; otherwise, null.
        /// </returns>
        public static T? UnityObjectOrDefault<T>(this T? obj) where T : class
        {
            return obj is UnityEngine.Object unityObj
                ? unityObj != null ? obj : null
                : obj ?? null;
        }
    }
}
