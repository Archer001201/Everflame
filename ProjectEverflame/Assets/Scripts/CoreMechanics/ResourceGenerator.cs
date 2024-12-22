using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace CoreMechanics
{
    public class ResourceGenerator : MonoBehaviour
    {
        public GameObject prefab;
        public float radius;
        public Vector2 range;
        public int poolSize = 10;
        [FormerlySerializedAs("eventTypes")] public List<ResourceType> resourceTypes;

        private Queue<GameObject> _objectPool;

        private void Awake()
        {
            InitializeObjectPool();
        }

        private void OnEnable()
        {
            StartCoroutine(SpawnPrefab());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
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
                SetupObject(obj);
                return obj;
            }
            else
            {
                var obj = Instantiate(prefab, transform);
                SetupObject(obj);
                return obj;
            }
        }

        private void SetupObject(GameObject obj)
        {
            var e = obj.GetComponent<DevelopmentalResource>();
            e.Setup(this, resourceTypes[Random.Range(0, resourceTypes.Count)]);
        }

        public void ReturnToPool(GameObject obj)
        {
            obj.SetActive(false);
            _objectPool.Enqueue(obj);
        }

        private IEnumerator SpawnPrefab()
        {
            while (true)
            {
                var delay = Random.Range(range.x, range.y);
                yield return new WaitForSeconds(delay);
                var randomPosition = transform.position + new Vector3(Random.insideUnitCircle.x * radius, 0, Random.insideUnitCircle.y * radius);
                var instance = GetPooledObject();
                instance.transform.position = randomPosition;
                instance.transform.rotation = Quaternion.identity;
            }
            // ReSharper disable once IteratorNeverReturns
        }
    }
}