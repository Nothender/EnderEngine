using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine.Core
{
    public static class Utils
    {
        #region ArrayUtils
        /// <summary>
        /// Appends an item at the end of the array (Returns a new instance of array)
        /// </summary>
        /// <param name="item">The item you want to append at the end of the array</param>
        /// <returns>A new array with the item added at the end</returns>
        public static T[] Append<T>(this T[] array, T item)
        {
            if (array == null)
                return new T[] { item };
            T[] result = new T[array.Length + 1];
            array.CopyTo(result, 0);
            result[array.Length] = item;
            return result;
        }

        /// <summary>
        /// Removes all occurences of an item in the array (Returns a new instance of array)
        /// </summary>
        /// <param name="item">The item you want to remove</param>
        /// <returns>A new array without any occurences of the item</returns>
        public static T[] Remove<T>(this T[] array, T item)
        {
            return array.Where(t => !t.Equals(item)).ToArray();
        }

        //TODO: add RemoveAt(int index), Remove(T item), Insert(T item, int index), etc...
        #endregion ArrayUtils
    }
}
