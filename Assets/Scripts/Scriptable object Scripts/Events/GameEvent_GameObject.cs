using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Z_utilities.EventHandlers;

namespace SO.Events
{
    [CreateAssetMenu(menuName = ("Events/Game Event-GameObject"))]

    public class GameEvent_GameObject : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        [SerializeField] string description;
#endif
        // I did not found a better way to do a generic event system        
        public event NotifyVoid<GameObject> Event;// Stablish an event form generi delegate in GenericDelegate script
        public GameObject parameter; // this is the parameter of the event 
        public bool enableLogs;

        // this is used for any other script

        /// <summary>
        /// Inoke the event to trigger any subscriber 
        /// <para>
        /// Needs a gameObject parameter 
        /// </para>
        /// </summary>
        /// <param name="game_obj"></param>
        public void Rise(GameObject game_obj)
        {
            parameter = game_obj;
            OnEventRised();
        }

        // this is mostly to test it in the instpector with any argument
        [NaughtyAttributes.Button]
        public void OnEventRised()
        {            
            if(Event != null)
                Event.Invoke(parameter);
            else
                Debug.Log($"Event triggered with {parameter.name} parameter, but not subscriber being found");
        }
    }
}