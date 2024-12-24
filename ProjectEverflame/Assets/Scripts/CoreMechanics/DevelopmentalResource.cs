using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Utilities;
using EventHandler = Utilities.EventHandler;
using Random = UnityEngine.Random;

namespace CoreMechanics
{
    public enum ResourceType
    {
        自然, 科技
    }
    public class DevelopmentalResource : MonoBehaviour
    {
        public UnityEvent onPickupEvent;
        public ResourceGenerator objectPool;
        public float duration = 20f;
        public Material matBlue;
        public Material matGreen;
        public ResourceType resourceType;
        // public bool destroyed;
        
        private GameManager _gameManager;
        private MeshRenderer _meshRenderer;
        private TextMeshProUGUI _nameTmp;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _nameTmp = GetComponentInChildren<TextMeshProUGUI>();
            _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }

        // private void Start()
        // {
        //     _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        // }

        private void OnEnable()
        {
            // destroyed = false;
            StartCoroutine(ReturnObjectAfterTime(duration));
            EventHandler.onDestroyResource += DestroyedState;
        }

        private void OnDisable()
        {
            onPickupEvent.RemoveAllListeners();
            objectPool = null;
            EventHandler.onDestroyResource -= DestroyedState;
            StopAllCoroutines();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            onPickupEvent?.Invoke();
            objectPool.ReturnToPool(gameObject);
        }

        public void Setup(ResourceGenerator pool, ResourceType type)
        {
            objectPool = pool;
            resourceType = type;

            switch (type)
            {
                case ResourceType.科技:
                    _meshRenderer.material = matBlue;
                    var valSc = RandomValue();
                    _nameTmp.text = type + " " + valSc;
                    onPickupEvent.AddListener(() => _gameManager.HandleScienceExp(valSc, true));
                    break;
                case ResourceType.自然:
                    _meshRenderer.material = matGreen;
                    var valNa = RandomValue();
                    _nameTmp.text = type + " " + valNa;
                    onPickupEvent.AddListener(() => _gameManager.HandleNatureExp(valNa, true));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private int RandomValue()
        {
            var maxVal = _gameManager.currentPeriod switch
            {
                CivilPeriod.启程纪 => 5,
                CivilPeriod.黎明纪 => 7,
                CivilPeriod.星辉纪 => 9,
                _ => 3
            };
            
            var minVal = _gameManager.currentPeriod switch
            {
                CivilPeriod.黎明纪 => 3,
                CivilPeriod.星辉纪 => 5,
                _ => 1
            };
            
            return Random.Range(minVal, maxVal+1);
        }
        
        private IEnumerator ReturnObjectAfterTime(float time)
        {
            yield return new WaitForSeconds(time);
            objectPool.ReturnToPool(gameObject);
        }

        private void DestroyedState(ResourceType type, float time)
        {
            if (resourceType == type) objectPool.ReturnToPool(gameObject);
        }
    }
}
