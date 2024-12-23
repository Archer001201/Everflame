using System.Collections;
using UnityEngine;

namespace Utilities
{
    public class TaskManager : MonoBehaviour
    {
        private Coroutine[] _tasks = new Coroutine[3];

        public void StartTask(IEnumerator task)
        {
            var replaceIndex = -1;

            // 检查空槽位
            for (var i = 0; i < _tasks.Length; i++)
            {
                if (_tasks[i] == null)
                {
                    replaceIndex = i; // 找到空位
                    break;
                }
            }

            // 如果没有空位，顶替最早任务
            if (replaceIndex == -1)
            {
                replaceIndex = 0; // 顶替第一个任务
                StopCoroutine(_tasks[replaceIndex]); // 停止旧任务
            }

            // 启动新任务
            _tasks[replaceIndex] = StartCoroutine(task);
        }
    }
}
