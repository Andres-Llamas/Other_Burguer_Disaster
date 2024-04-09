using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Z_utilities.EventHandlers;

namespace SO.Events
{
    [CreateAssetMenu(menuName = ("Events/Game Event-String"))]
    public class GameEvent_String : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        [SerializeField] string description;
#endif
        // I did not found a better way to do a generic event system        
        public event NotifyVoid<string> Event;// Stablish an event form generi delegate in GenericDelegate script
        public string parameter; // this is the parameter of the event 

        public void RiseWIthArgument(string s)
        {
            parameter = s;
            OnEventRised();
        }

        [NaughtyAttributes.Button]
        public void OnEventRised()
        {
            Event.Invoke(parameter);
        }
    }
}
