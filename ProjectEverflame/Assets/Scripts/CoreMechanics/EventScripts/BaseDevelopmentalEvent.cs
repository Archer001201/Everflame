using System.Collections;
using System.Collections.Generic;
using Player;
using TMPro;
using Ui;
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
        private UiManager _uiManager;
        private GameObject _player;
        private MeshRenderer _meshRenderer;
        private TextMeshProUGUI _nameTmp;
        
        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _nameTmp = GetComponentInChildren<TextMeshProUGUI>();
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            _uiManager = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<UiManager>();
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

        protected virtual void Pickup()
        {
            objectPool.ReturnToPool(gameObject);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        protected void ChangePlayerSpeed(float rate, float time)
        {
            _player.GetComponent<MoveController>().StartChangeSpeed(rate, time);
        }

        protected void StartTask(TaskType type, float time)
        {
            // _taskManager.StartTask(this, time);
            _uiManager.CreateTask(eventStruct, type, time);
        }

        protected void StartDestroyResources(ResourceType type, float time)
        {
            EventHandler.DestroyResource(type, time);
        }

        protected void ActivateMagnet(List<ResourceType> types, float time)
        {
            _player.GetComponentInChildren<Magnet>().StartMagnet(types, time);
        }

        // private IEnumerator DestroyResources(ResourceType type, float time)
        // {
        //     EventHandler.DestroyResource(type, true);
        //     yield return new WaitForSeconds(time);
        //     Debug.Log("end coroutine");
        //     EventHandler.DestroyResource(type, false);
        // }
    }
}