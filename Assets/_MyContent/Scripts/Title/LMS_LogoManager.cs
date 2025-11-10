using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LMS.Management;
using LMS.Library;

namespace LMS.Title
{
    public class LMS_LogoManager : MonoBehaviour
    {

        [SerializeField] private Transform logoCollect;
        
        // 관리 변수
        private List<GameObject> logoList = new List<GameObject>();
        private float showTime = 1.0f;

        // 활용 상태
        public bool processing = false;

        private void Awake()
        {
            if (logoCollect == null)
            {
                Debug.LogError("ERROR) Null Reference : Logo Prefab Setting Error");
                return;
            }

            logoList.Clear(); 
            
            for (int i = 0; i < logoCollect.childCount; i++) {
                logoList.Add(logoCollect.GetChild(i).gameObject);
            }
        }

 
        public void ShowAll()
        {
            processing = true;
            CoroutineHandler.Instance.StartRoutine("Logo", ShowStarting());
        }

        private IEnumerator ShowStarting()
        {
            

            foreach (GameObject logo in logoList)
            {
                logo.gameObject.SetActive(false);
            }

            foreach (GameObject logo in logoList)
            {
                logo.gameObject.SetActive(true);

                UIManager.Instance.FadeOut();
                yield return WaitForSecondsCache.Get(UIManager.Instance.duration);

                yield return WaitForSecondsCache.Get(showTime);

                UIManager.Instance.FadeIn();
                yield return WaitForSecondsCache.Get(UIManager.Instance.duration);

                logo.gameObject.SetActive(false);
            }

            CoroutineHandler.Instance.StopRoutine("Logo");
            processing = false;
        }
    }
}