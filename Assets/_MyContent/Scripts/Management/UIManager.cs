using UnityEngine;
using DG.Tweening;

namespace LMS.Management
{
    // UI Sort : 8 고정 - UI 우선순위 8개로 조정
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private CanvasGroup group;
        
        // 사용만 가능
        public float duration = 1.0f;

        // 세팅 FadeIn/Out 세팅
        void Reset()
        {
            group = GetComponent<CanvasGroup>();
            if (!group) group = gameObject.AddComponent<CanvasGroup>();
        }

        public Tween FadeIn()
        {
            group.blocksRaycasts = true;
            group.interactable = true;
            return group.DOFade(1f, duration).SetUpdate(true);
        }

        public Tween FadeOut()
        {
            group.blocksRaycasts = false;
            group.interactable = false;
            return group.DOFade(0f, duration).SetUpdate(true);
        }
    }

}
