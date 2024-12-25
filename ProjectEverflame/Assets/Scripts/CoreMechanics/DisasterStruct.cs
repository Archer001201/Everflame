using System;
using UnityEngine;
using UnityEngine.UI;

namespace CoreMechanics
{
    public enum DisasterType
    {
        天灾, 人祸
    }
    
    [Serializable]
    public class DisasterStruct
    {
        public string name;
        public DisasterType disasterType;
        [TextArea] public string description;
        public Sprite background;
        public int damageToProsperity;
        public float damageRateToScience;
        public float damageRateToNature;
    }
}
