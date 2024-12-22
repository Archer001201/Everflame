using System.Collections;
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
        
        private GameManager _gameManager;
        private MeshRenderer _meshRenderer;
        private TextMeshProUGUI _nameTmp;
        
        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _nameTmp = GetComponentInChildren<TextMeshProUGUI>();
            _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
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
    }
}