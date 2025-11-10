using UnityEngine;

namespace LMS.Management
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance) return _instance;

                // 유니티 버전에 따라 활용 가능하도록 세팅
                // 2023버전 이상 AnyObject 권장
                // - FindObjectOfType<T>()	예전 방식,                        | 씬 전체 탐색	아무거나 반환	느림	모든 
                // - FindAnyObjectByType<T>()    최신 방식, 빠름,              | 멀티스레드 대응 랜덤 객체 반환    
                // - FindFirstObjectByType<T>()  가장 낮은 인스턴스 ID 반환    | “첫 번째” 객체 반환 
#if UNITY_2023_1_OR_NEWER
                                _instance = FindAnyObjectByType<T>();
#else
                _instance = FindObjectOfType<T>();
#endif
                if (_instance) return _instance;

                var go = new GameObject(typeof(T).Name);
                _instance = go.AddComponent<T>();
                DontDestroyOnLoad(go);
                return _instance;
            }
        }

        // Awake 기본 세팅
        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}