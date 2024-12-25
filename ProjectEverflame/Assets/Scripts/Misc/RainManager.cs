using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Misc
{
    public class RainManager : MonoBehaviour
    {
        public List<GameObject> rains;
        
        private Coroutine _coroutine;

        public void StartRain(int amount, float time)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                foreach (var r in rains)
                {
                    r.SetActive(false);
                }
                _coroutine = null;
            }
            
            _coroutine = StartCoroutine(ActivateRain(amount, time));
        }

        private IEnumerator ActivateRain(int amount, float time)
        {
            for (int i = 0; i < amount; i++)
            {
                rains[i].SetActive(true);
            }
            yield return new WaitForSeconds(time);
            foreach (var r in rains)
            {
                r.SetActive(false);
            }
        }
    }
}
