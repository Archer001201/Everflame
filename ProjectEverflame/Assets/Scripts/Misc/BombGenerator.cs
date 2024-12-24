using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Misc
{
    public class BombGenerator : MonoBehaviour
    {
        public GameObject prefab;
        public float radius;
        public Vector2 range;
        public int poolSize = 10;
        
        private Queue<GameObject> _objectPool;

        private void Awake()
        {
            InitializeObjectPool();
        }

        private void Start()
        {
            StartSpawn(10,3);
        }

        private void InitializeObjectPool()
        {
            _objectPool = new Queue<GameObject>();
            for (var i = 0; i < poolSize; i++)
            {
                var obj = Instantiate(prefab, transform);
                obj.GetComponent<Bomb>().generator = this;
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
                obj.GetComponent<Bomb>().generator = this;
                return obj;
            }
        }
        
        public void ReturnToPool(GameObject obj)
        {
            obj.SetActive(false);
            _objectPool.Enqueue(obj);
        }
        
        private IEnumerator SpawnPrefab(float time)
        {
            var timer = time;
            while (timer > 0)
            {
                var delay = Random.Range(range.x, range.y);
                yield return new WaitForSeconds(delay);
                var randomPosition = transform.position + new Vector3(Random.insideUnitCircle.x * radius, 0, Random.insideUnitCircle.y * radius);
                var instance = GetPooledObject();
                instance.transform.position = randomPosition;
                instance.transform.rotation = Quaternion.identity;
                timer -= delay;
            }
            // ReSharper disable once IteratorNeverReturns
        }

        public void StartSpawn(float time, int wave)
        {
            var num = 0;
            while (num < wave)
            {
                StartCoroutine(SpawnPrefab(time));
                num++;
            }
        }
    }
}
