using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ui
{
    public class CreditPanel : MonoBehaviour
    {
        [TextArea]public List<string> content;
        public TextMeshProUGUI textBox;
        
        private void OnEnable()
        {
            StartCoroutine(PlayCredit());
        }

        private IEnumerator PlayCredit()
        {
            foreach (var c in content)
            {
                textBox.text = c;
                yield return new WaitForSecondsRealtime(3);
            }

            SceneManager.LoadSceneAsync("Start");
        }
    }
}
