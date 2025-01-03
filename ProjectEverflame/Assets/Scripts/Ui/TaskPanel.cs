using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public enum TaskType
    {
        自然采集初级, 自然采集中级, 追逐珍宝初级, 追逐珍宝中级, 追逐珍宝高级, 科技采集中级, 科技采集高级, 科技不采集, 出圈中级
    }
    
    public class TaskPanel : MonoBehaviour
    {
        public TextMeshProUGUI taskText;
        public TextMeshProUGUI timerText;
        public Image progressBar;
        public float progressValue;
        // public float timerValue;

        private void Awake()
        {
            progressBar.fillAmount = 0;
        }

        private void Update()
        {
            progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, progressValue, 5f * Time.deltaTime);
        }

        public void UpdateTaskText(string task)
        { 
            taskText.text = task;
        }

        public void StartTask(float time)
        {
            // timerValue = time;
            StartCoroutine(CountDown(time));
        }

        private IEnumerator CountDown(float time)
        {
            var timer = time;
            while (timer > 0)
            {
                timerText.text = "剩余时间：" + timer + "s";
                yield return new WaitForSeconds(1);
                timer -= 1;
            }
            Destroy(gameObject);
        }

        public void UpdateTaskProgress(float progress)
        {
            progressValue = progress;
        }
    }
}
