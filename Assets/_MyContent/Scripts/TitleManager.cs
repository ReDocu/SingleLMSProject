using LMS.Library;
using LMS.Management;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.Title
{
    public class TitleManager : MonoBehaviour
    {
        [SerializeField] private LMS_LogoManager logoManager;
        [SerializeField] private LMS_PromotionManager promotionManager;
        [SerializeField] private LMS_MainManager mainManager;

        private void Awake()
        {
            logoManager.gameObject.SetActive(false);
            promotionManager.gameObject.SetActive(false);
            mainManager.gameObject.SetActive(false);
        }

        private IEnumerator Start()
        {
            // 로고 실행 중
            yield return WaitForSecondsCache.Get(2.0f);
            logoManager.gameObject.SetActive(true);
            logoManager.ShowAll();
            yield return new WaitUntil(() => !logoManager.processing);
            logoManager.gameObject.SetActive(false);

            // 영상 실행 중
            promotionManager.gameObject.SetActive(true);
            promotionManager.ShowVideo();
            if (promotionManager.promotion != null)
            {
                yield return new WaitUntil(() => !promotionManager.promotion.processing);
            }
            promotionManager.gameObject.SetActive(false);

            // 메인 타이틀 
            mainManager.gameObject.SetActive(true);
            mainManager.ShowTitle();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                logoManager.ShowAll();
            }
            else if(Input.GetKeyDown(KeyCode.X))
            {
                promotionManager.ShowVideo();
            }
        }

    }
}


