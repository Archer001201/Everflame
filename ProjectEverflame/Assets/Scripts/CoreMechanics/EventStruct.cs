using System;
using UnityEngine;

namespace CoreMechanics
{
    public enum EventType
    {
        危机, 机遇, 任务
    }

    public enum EventName
    {
        事件1, 事件2, 事件3
    }
    
    [Serializable]
    public class EventStruct
    {
        public string eventName;
        public EventType eventType;
        [TextArea] public string description;
    }
}