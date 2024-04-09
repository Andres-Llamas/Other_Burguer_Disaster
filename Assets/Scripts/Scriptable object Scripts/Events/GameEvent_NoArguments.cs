using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Z_utilities.EventHandlers;

namespace SO.Events
{
    [CreateAssetMenu(menuName = ("Events/Game Event-No argument"))]
    public class GameEvent_NoArguments : ScriptableObject
    {
        #region Inspector variables ---------------------------------------------------
#if UNITY_EDITOR
        [ResizableTextArea]
        [SerializeField] string description;
#endif
        #endregion

        #region NO_Inspector variables ------------------------------------------------
        public event NotifyVoid Event;// Stablish an event form generi delegate in GenericDelegate script
        #endregion

        #region methods ---------------------------------------------------------------
        [NaughtyAttributes.Button]
        public void Rise()
        {            
            if (Event != null)
                Event.Invoke();
        }        
        #endregion
    }
}