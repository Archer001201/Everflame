using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class AudioManager : MonoBehaviour
    {
        public GameObject prefab;
        public int poolSize = 10;
        private Queue<GameObject> _objectPool;
        
        private void Awake()
        {
            InitializeObjectPool();
        }

        private void OnEnable()
        {
            EventHandler.onPlayAudio += StartPlayAudio;
        }
        
        private void OnDisable()
        {
            EventHandler.onPlayAudio -= StartPlayAudio;
        }

        private void InitializeObjectPool()
        {
            _objectPool = new Queue<GameObject>();
            for (var i = 0; i < poolSize; i++)
            {
                var obj = Instantiate(prefab, transform);
                obj.SetActive(false);
                _objectPool.Enqueue(obj);
            }
        }
        
        private GameObject GetPooledObject()
        {
            if (_objectPool.Count > 0)
            {
                var obj = _objectPool.Dequeue();
                obj.SetActive(true);
                return obj;
            }
            else
            {
                var obj = Instantiate(prefab, transform);
                return obj;
            }
        }
        
        public void ReturnToPool(GameObject obj)
        {
            obj.SetActive(false);
            _objectPool.Enqueue(obj);
        }

        public void StartPlayAudio(AudioClip clip)
        {
            StartCoroutine(PlayAudioClip(clip));
        }

        private IEnumerator PlayAudioClip(AudioClip clip)
        {
            // 检查 clip 是否为空
            if (clip == null)
            {
                // Debug.LogError("AudioClip is null!");
                yield break; // 提前退出协程
            }

            // 获取对象池中的音频对象
            var obj = GetPooledObject();
            var source = obj.GetComponent<AudioSource>();

            // 设置音频参数
            source.clip = clip;
            source.loop = false; // 确保不会循环播放
            source.Play();

            // 等待音频播放结束
            float clipLength = clip.length; // 获取音频时长
            float timer = 0f;

            while (source.isPlaying && timer < clipLength)
            {
                timer += Time.deltaTime; // 记录播放时间
                yield return null;
            }

            // 停止播放并回收对象
            source.Stop();
            ReturnToPool(obj);

            // Debug.Log("Audio finished!");
        }

    }
}
