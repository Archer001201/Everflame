using System;
using System.Collections;
using System.Collections.Generic;
using CoreMechanics.EventScripts;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace CoreMechanics
{
    public class EventGenerator : MonoBehaviour
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
        public List<EventStruct> period1Events;
        public List<EventStruct> period2Events;
        public List<EventStruct> period3Events;
        public List<EventStruct> period4Events;
        public List<EventStruct> currentEvents;
        public Material matCrisis;
        public Material matTask;
        public Material matOpportunity;
        
        private Queue<GameObject> _objectPool;

        private void Awake()
        {
            gameManager.SetEventGenerator(this);
            UpdateEventList();
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
                SetupObject(obj, currentEvents[Random.Range(0, currentEvents.Count)]);
                return obj;
            }
            else
            {
                var obj = Instantiate(prefab, transform);
                SetupObject(obj, currentEvents[Random.Range(0, currentEvents.Count)]);
                return obj;
            }
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

        public void UpdateEventList()
        {
            // currentEvents.Clear();
            switch (gameManager.currentPeriod)
            {
                case CivilPeriod.荒原纪:
                    currentEvents = period1Events; 
                    range = period1Range;
                    break;
                case CivilPeriod.启程纪: 
                    currentEvents = period2Events; 
                    range = period2Range;
                    break;
                case CivilPeriod.黎明纪: 
                    currentEvents = period3Events; 
                    range = period3Range;
                    break;
                case CivilPeriod.星辉纪:
                    currentEvents = period4Events; 
                    range = period4Range;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void SetupObject(GameObject obj, EventStruct eventStruct)
        {
            BaseDevelopmentalEvent e = eventStruct.eventName switch
            {
                "逐影灾厄" => obj.AddComponent<逐影灾厄>(),
                "幻菇迷狂" => obj.AddComponent<幻菇迷狂>(),
                "草药寻踪" => obj.AddComponent<草药寻踪>(),
                "流火珍宝" => obj.AddComponent<流火珍宝>(),
                "风羽赠礼" => obj.AddComponent<风羽赠礼>(),
                "雷卵奇迹" => obj.AddComponent<雷卵奇迹>(),
                "星果盛宴" => obj.AddComponent<星果盛宴>(),
                _ => null
            };

            if (e == null) return;
            
            e.Setup(this, eventStruct);
            switch (eventStruct.eventType)
            {
                case EventType.危机: e.SetMaterial(matCrisis); break;
                case EventType.任务: e.SetMaterial(matTask); break;
                case EventType.机遇: e.SetMaterial(matOpportunity); break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
