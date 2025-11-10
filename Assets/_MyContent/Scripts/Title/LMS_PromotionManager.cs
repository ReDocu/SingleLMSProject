using LMS.Management;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.Title
{
    public class LMS_PromotionManager : MonoBehaviour
    {
        // 활용 상태
        private List<IntroVideo> promotionList = new List<IntroVideo>();

        public IntroVideo promotion;

        private void Awake()
        {
            promotionList.Clear();

            for (int i = 0; i < transform.childCount; i++)
            {
                promotionList.Add(transform.GetChild(i).GetComponent<IntroVideo>());
            }
        }


        public void ShowVideo()
        {
            promotion = null;

            if (!CoroutineHandler.Instance.IsRunning("VideoPlaying"))
            {
                promotion = promotionList[Random.Range(0, transform.childCount)];
                promotion.ShowVideo();
            }
        }


    }
}