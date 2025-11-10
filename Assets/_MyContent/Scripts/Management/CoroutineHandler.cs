using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.Management
{
    public class CoroutineHandler : Singleton<CoroutineHandler>
    {
        private Dictionary<string, Coroutine> routineDict = new Dictionary<string, Coroutine>();

        public void StartRoutine(string key, IEnumerator routine)
        {
            if (routineDict.ContainsKey(key))
                StopCoroutine(routineDict[key]);

            routineDict[key] = StartCoroutine(routine);
        }

        public void StopRoutine(string key)
        {
            if (routineDict.ContainsKey(key))
            {
                StopCoroutine(routineDict[key]);
                routineDict.Remove(key);
            }
        }

        public bool IsRunning(string key)
        {
            return routineDict.ContainsKey(key);
        }
    }
}