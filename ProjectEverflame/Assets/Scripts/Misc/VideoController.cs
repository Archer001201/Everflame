using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using EventHandler = Utilities.EventHandler;

namespace Misc
{
    public class VideoController : MonoBehaviour
    {
        private VideoPlayer _videoPlayer;
        private Coroutine _coroutine;

        public VideoClip startClip;
        public VideoClip endClip;
        public GameObject videoDisplay;

        private void Awake()
        {
            _videoPlayer = GetComponent<VideoPlayer>();
        }

        private void Start()
        {
            PlayVideo(startClip);
        }

        private void OnEnable()
        {
            EventHandler.onPlayEndVideo += PlayEndVideo;
        }

        private void OnDisable()
        {
            EventHandler.onPlayEndVideo -= PlayEndVideo;
        }

        private void PlayEndVideo()
        {
            PlayVideo(endClip);
        }

        private void PlayVideo(VideoClip clip)
        {
            _videoPlayer.clip = clip;
            StartCoroutine(PlayVideoCoroutine());
        }

        private IEnumerator PlayVideoCoroutine()
        {
            Time.timeScale = 0;
            // 显示视频界面
            videoDisplay.SetActive(true);

            // 确保视频准备好
            if (!_videoPlayer.isPrepared)
            {
                _videoPlayer.Prepare(); // 准备视频
                while (!_videoPlayer.isPrepared) // 等待准备完成
                {
                    yield return null;
                }
            }

            // 开始播放
            _videoPlayer.Play();

            // 等待播放结束
            while (_videoPlayer.isPlaying || _videoPlayer.frame < (long)_videoPlayer.frameCount - 1)
            {
                // Debug.Log($"Playing frame {_videoPlayer.frame}/{_videoPlayer.frameCount}");
                yield return null;
            }

            // 停止播放并隐藏视频
            _videoPlayer.Stop(); // 确保播放结束
            videoDisplay.SetActive(false);

            Time.timeScale = 1;

            // Debug.Log("Video finished!");
        }

    }
}
