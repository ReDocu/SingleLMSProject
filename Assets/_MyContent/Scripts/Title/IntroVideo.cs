using LMS.Library;
using LMS.Management;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

namespace LMS.Title
{
    public class IntroVideo : MonoBehaviour
    {
        [Header("Source (choose one)")]
        [SerializeField] private VideoClip clips; // Project-imported VideoClips

        [Header("Behavior")]
        public bool skippable = true;
        public KeyCode skipKey = KeyCode.Space;

        public bool processing = false;


        // 비디오 촬영을 위한 기본 세팅
        private RawImage _rawImage;
        private VideoPlayer _vp;
        private AudioSource _audio;
        private RenderTexture _rt;

        private void Awake()
        {
            SetupUI();
            SetupVideoPlayer();


        }

        public void ShowVideo()
        {
            processing = true;
            CoroutineHandler.Instance.StartRoutine("VideoPlaying", PlayOneClip(clips));
        }


        private void SetupUI()
        {

            // (fullscreen)
            GameObject rawGO = new GameObject("Video");
            rawGO.transform.SetParent(transform, false);
            _rawImage = rawGO.AddComponent<RawImage>();

            RectTransform rt = _rawImage.rectTransform;
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;
        }

        private void SetupVideoPlayer()
        {
            _vp = gameObject.AddComponent<VideoPlayer>();
            _audio = gameObject.AddComponent<AudioSource>();

            _vp.playOnAwake = false;
            _vp.waitForFirstFrame = true;
            _vp.audioOutputMode = VideoAudioOutputMode.AudioSource;
            _vp.SetTargetAudioSource(0, _audio);
            _vp.source = VideoSource.VideoClip; // default; will swap to URL if used
            _vp.renderMode = VideoRenderMode.RenderTexture; // we drive a RawImage with a RenderTexture
            _vp.isLooping = false;
            _vp.skipOnDrop = true;
        }

        private IEnumerator PlayOneClip(VideoClip clip)
        {
            // 비디오 클립 세팅 
            if (clip != null)
            {
                _vp.source = VideoSource.VideoClip;
                _vp.clip = clip;
            }

            _vp.Prepare();
            while (!_vp.isPrepared)
                yield return null;

            // Render Texture 세팅 및 생성
            int w = (int)_vp.width;
            int h = (int)_vp.height;
            if (_rt == null || _rt.width != w || _rt.height != h)
            {
                if (_rt != null) _rt.Release();
                _rt = new RenderTexture(Mathf.Max(16, w), Mathf.Max(16, h), 0);
            }
            _vp.targetTexture = _rt;
            _rawImage.texture = _rt;

            // Audio
            _audio.playOnAwake = false;
            _audio.Stop();


            // Play
            _vp.Play();
            _audio.Play();

            UIManager.Instance.FadeOut();
            yield return WaitForSecondsCache.Get(UIManager.Instance.duration);

            // Skip 세팅
            while (_vp.isPlaying || (_vp.frame > 0 && _vp.frame < (long)_vp.frameCount - 1))
            {
                // 종료해야 다시 실행됨
                if (skippable && Input.GetKeyDown(skipKey))
                {
                    break;
                }
                yield return null;
            }

            // 종료
            _vp.Stop();
            _audio.Stop();

            UIManager.Instance.FadeIn();
            yield return WaitForSecondsCache.Get(UIManager.Instance.duration);

            CoroutineHandler.Instance.StopRoutine("VideoPlaying");
            processing = false;
        }

        private void OnDestroy()
        {
            if (_rt != null)
            {
                _rt.Release();
                _rt = null;
            }
        }

    }
}
