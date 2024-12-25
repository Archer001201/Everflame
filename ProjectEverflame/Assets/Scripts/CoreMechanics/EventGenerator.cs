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
                //荒原纪
                //危机
                "逐影灾厄" => obj.AddComponent<逐影灾厄>(),
                "幻菇迷狂" => obj.AddComponent<幻菇迷狂>(),
                //任务
                "电光石火" => obj.AddComponent<电光石火>(),
                "夙愿之痕" => obj.AddComponent<夙愿之痕>(),
                //机遇
                "风羽赠礼" => obj.AddComponent<风羽赠礼>(),
                "雷卵奇迹" => obj.AddComponent<雷卵奇迹>(),
                "星果盛宴" => obj.AddComponent<星果盛宴>(),
                
                //启程纪
                //危机
                "铁炬动乱" => obj.AddComponent<铁炬动乱>(),
                "学馆纷争" => obj.AddComponent<学馆纷争>(),
                "丹毒事件" => obj.AddComponent<丹毒事件>(),
                //机遇
                "风引琉沙" => obj.AddComponent<风引琉沙>(),
                "烛林秘光" => obj.AddComponent<烛林秘光>(),
                "火藤余焰" => obj.AddComponent<火藤余焰>(),
                
                //黎明纪
                //危机
                "蒸汽工潮" => obj.AddComponent<蒸汽工潮>(),
                "污染之乱" => obj.AddComponent<污染之乱>(),
                //机遇
                "合金" => obj.AddComponent<合金>(),
                "奇妙微生物" => obj.AddComponent<奇妙微生物>(),
                "蒸汽猎鹰" => obj.AddComponent<蒸汽猎鹰>(),
                "铁路献礼" => obj.AddComponent<铁路献礼>(),
                
                //星辉纪
                //危机
                "实验失控" => obj.AddComponent<实验失控>(),
                "星尘爆炸" => obj.AddComponent<星尘爆炸>(),
                //机遇
                "空轨系统通车" => obj.AddComponent<空轨系统通车>(),
                "能源矩阵" => obj.AddComponent<能源矩阵>(),
                "星尘驱动实验成功" => obj.AddComponent<星尘驱动实验成功>(),
                "星环通信复苏" => obj.AddComponent<星环通信复苏>(),
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
