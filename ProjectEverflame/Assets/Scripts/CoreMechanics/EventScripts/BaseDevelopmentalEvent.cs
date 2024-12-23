using System.Collections;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

namespace CoreMechanics.EventScripts
{
    public class BaseDevelopmentalEvent : MonoBehaviour
    {
        public UnityEvent onPickupEvent = new();
        public EventGenerator objectPool;
        public float duration = 20f;
        public EventStruct eventStruct;
        
        protected GameManager gameManager;
        private TaskManager _taskManager;
        private GameObject _player;
        private MeshRenderer _meshRenderer;
        private TextMeshProUGUI _nameTmp;
        
        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _nameTmp = GetComponentInChildren<TextMeshProUGUI>();
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            _taskManager = GameObject.FindGameObjectWithTag("TaskManager").GetComponent<TaskManager>();
        }
        
        private void OnEnable()
        {
            StartCoroutine(ReturnObjectAfterTime(duration));
        }

        private void OnDisable()
        {
            onPickupEvent.RemoveAllListeners();
            StopAllCoroutines();
            objectPool = null;
            Destroy(this);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            _player = other.gameObject;
            onPickupEvent?.Invoke();
            objectPool.ReturnToPool(gameObject);
        }
        
        private IEnumerator ReturnObjectAfterTime(float time)
        {
            yield return new WaitForSeconds(time);
            objectPool.ReturnToPool(gameObject);
        }
        
        public void Setup(EventGenerator pool, EventStruct eStruct)
        {
            objectPool = pool;
            eventStruct = eStruct;
            onPickupEvent.AddListener(Pickup);
            _nameTmp.text = eStruct.eventName;
        }

        public void SetMaterial(Material material)
        {
            _meshRenderer.material = material;
        }

        protected virtual void Pickup(){}

        // ReSharper disable Unity.PerformanceAnalysis
        protected void ChangePlayerSpeed(float rate, float time)
        {
            _player.GetComponent<MoveController>().StartChangeSpeed(rate, time);
        }

        protected void StartTask(IEnumerator task)
        {
            _taskManager.StartTask(task);
        }

        protected virtual IEnumerator Task()
        {
            yield return null;
        }
    }
}