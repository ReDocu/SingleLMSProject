using System.Collections.Generic;
using UnityEngine;

namespace LMS.Library
{
    public static class WaitForSecondsCache
    {
        private static readonly Dictionary<float, WaitForSeconds> waitCache = new Dictionary<float, WaitForSeconds>();

        public static WaitForSeconds Get(float time)
        {
            if (!waitCache.ContainsKey(time))
                waitCache[time] = new WaitForSeconds(time);

            return waitCache[time];
        }
    }

}
