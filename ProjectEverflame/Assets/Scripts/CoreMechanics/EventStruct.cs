using System;
using UnityEngine;

namespace CoreMechanics
{
    public enum EventType
    {
        危机, 机遇, 任务
    }
    
    [Serializable]
    public class EventStruct
    {
        public string eventName;
        public EventType eventType;
        [TextArea] public string description;
        [TextArea] public string effect;
    }
}