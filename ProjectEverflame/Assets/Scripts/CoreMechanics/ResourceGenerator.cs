using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;
using EventHandler = Utilities.EventHandler;
using Random = UnityEngine.Random;

namespace CoreMechanics
{
    public class ResourceGenerator : MonoBehaviour
    {
        public GameObject prefab;
        public float radius;
        public Vector2 range;
        public Vector2 period1Range;
        public Vector2 period2Range;
        public Vector2 period3Range;
        public Vector2 period4Range;
        public int poolSize = 10;
        public GameManager gameManager;
        [FormerlySerializedAs("eventTypes")] public List<ResourceType> resourceTypes;
        // public List<ResourceType> tempList = new List<ResourceType>();
        
        private Queue<GameObject> _objectPool;
        private Coroutine _lockdownCoroutine;

        private void Awake()
        {
            gameManager.SetResourceGenerator(this);
            InitializeObjectPool();
            UpdateEventList();
        }

        private void OnEnable()
        {
            EventHandler.onDestroyResource += SetLockdown;
            StartCoroutine(SpawnPrefab());
        }

        private void OnDisable()
        {
            EventHandler.onDestroyResource -= SetLockdown;
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
            if (resourceTypes.Count < 1) return null;
            var type = resourceTypes[Random.Range(0, resourceTypes.Count)];
            // if (type == lockdownType && lockdown) return null;
            if (_objectPool.Count > 0)
            {
                var obj = _objectPool.Dequeue();
                obj.SetActive(true);
                SetupObject(obj, type);
                return obj;
            }
            else
            {
                var obj = Instantiate(prefab, transform);
                SetupObject(obj, type);
                return obj;
            }
        }

        private void SetupObject(GameObject obj, ResourceType type)
        {
            var e = obj.GetComponent<DevelopmentalResource>();
            e.Setup(this, type);
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
                if (instance)
                {
                    instance.transform.position = randomPosition;
                    instance.transform.rotation = Quaternion.identity;
                }
            }
            // ReSharper disable once IteratorNeverReturns
        }

        private void SetLockdown(ResourceType type, float time)
        {
            if (_lockdownCoroutine != null)
            {
                StopCoroutine(_lockdownCoroutine);
            }
            _lockdownCoroutine = StartCoroutine(Lockdown(type, time));
        }

        private IEnumerator Lockdown(ResourceType type, float time)
        {
            // tempList.Clear();
            List<ResourceType> temp = new List<ResourceType>();
            foreach (var r in resourceTypes.ToList())
            {
                if (r == type)
                {
                    temp.Add(r);
                    resourceTypes.Remove(r);
                }
            }
            yield return new WaitForSeconds(time);
            resourceTypes.AddRange(temp);
            // tempList.Clear();
        }
        
        public void UpdateEventList()
        {
            switch (gameManager.currentPeriod)
            {
                case CivilPeriod.荒原纪:
                    range = period1Range;
                    break;
                case CivilPeriod.启程纪: 
                    range = period2Range;
                    break;
                case CivilPeriod.黎明纪: 
                    range = period3Range;
                    break;
                case CivilPeriod.星辉纪:
                    range = period4Range;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}