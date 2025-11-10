using DG.Tweening;
using TMPro;
using UnityEngine;

namespace LMS.Title
{
    public class MainTitle : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI pressToStartText;
        [SerializeField] private float blinkDuration = 0.5f; // 한 번 깜빡이는 속도

        private Tween blinkTween;

        private void Start()
        {
            // 텍스트 알파값을 0 → 1 → 0 반복
            blinkTween = pressToStartText.DOFade(0f, blinkDuration)
                .SetLoops(-1, LoopType.Yoyo)  // 무한 반복
                .SetEase(Ease.InOutSine);     // 부드럽게 페이드
        }

        private void OnDisable()
        {
            blinkTween?.Kill(); // 씬 전환 시 안전하게 정리
        }
    }
}
