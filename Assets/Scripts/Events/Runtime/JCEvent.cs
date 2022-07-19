// Author:  Joseph Crump
// Date:    07/18/22

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace JC.Events
{
    [System.Serializable]
    public class JCEvent : JCEventBase
    {
        [SerializeField]
        private List<SerializedAction> _actions = new();
    }
}
