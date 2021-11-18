using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace SO.List
{    
    [CreateAssetMenu(fileName = "Lists/Game Object", menuName = "Lists/Game Object")]
    public class SO_List_GameObject : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string Description;
#endif
        public bool clearInOnEnable = false;
        public List<GameObject> Items = new List<GameObject>();        

        private void OnEnable()
        {
            if(clearInOnEnable)
                RemoveAll();
        }
        public void Add(GameObject thing)
        {
            if (!Items.Contains(thing))
                Items.Add(thing);
        }

        public void Remove(GameObject thing)
        {
            if (Items.Contains(thing))
                Items.Remove(thing);
        }
        
        public void RemoveAll()
        {
            Items.Clear();
        }
    }
}