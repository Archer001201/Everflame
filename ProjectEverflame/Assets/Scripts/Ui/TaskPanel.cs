using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class TaskPanel : MonoBehaviour
    {
        public TextMeshProUGUI taskText;
        public Image progressBar;

        public void UpdateTaskText(string task)
        { 
            taskText.text = task;
        }

        public void StartTask(float time)
        {
            StartCoroutine(UpdateProgressBar(time));
        }

        private IEnumerator UpdateProgressBar(float time)
        {
            var timer = time;
            while (timer > 0)
            {
                progressBar.fillAmount = timer / time;
                yield return new WaitForSeconds(1);
                timer -= 1;
            }
            Destroy(gameObject);
        }
    }
}
