using System;
using System.Collections;
using System.Collections.Generic;
using CoreMechanics;
using CoreMechanics.EventScripts;
using Ui;
using UnityEngine;

namespace Utilities
{
    public class TaskManager : MonoBehaviour
    {
        // private List<BaseDevelopmentalEvent> _tasks = new();
        private UiManager _uiManager;
        //
        // public void StartTask(BaseDevelopmentalEvent task, float time)
        // {
        //     _tasks.RemoveAll(t => !task || !task.isActiveAndEnabled);
        //
        //
        //     if (_tasks.Count >= 3)
        //     {
        //         StopCoroutine(_tasks[0].Task());
        //         _uiManager.DestroyEarliestTask();
        //         _tasks.RemoveAt(0);
        //     }
        //     
        //     _tasks.Add(task);
        //     _uiManager.CreateTask(task.eventStruct, time);
        //     StartCoroutine(task.Task());
        // }
        //
        public void SetUiManager(UiManager uiManager)
        {
            _uiManager = uiManager;
        }
    }
}
