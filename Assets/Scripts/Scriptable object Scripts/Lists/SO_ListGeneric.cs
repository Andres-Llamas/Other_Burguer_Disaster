using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;

namespace SO.List
{
    public class SO_ListGeneric<T> : ScriptableObject
    {
        public List<T> Items = new List<T>();

		private void OnEnable()
		{
			RemoveAll();
		}
        public void Add(T thing)
        {
            if (!Items.Contains(thing))
                Items.Add(thing);
        }

        public void Remove(T thing)
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