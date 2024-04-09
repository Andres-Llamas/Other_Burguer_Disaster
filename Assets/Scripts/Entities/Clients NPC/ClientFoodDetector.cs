using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Z_utilities.EventHandlers;

namespace Entities.Client
{
	public class ClientFoodDetector : MonoBehaviour
	{
		#region Inspector variables =======================================================================
		[SerializeField][Tag] string foodTag;
		#endregion

		#region No Inspector Variables ====================================================================
		public event NotifyVoid<GameObject> foodDetected;
		public event NotifyVoid foodStoppedBeingDetected;
		#endregion

		#region Methods ===================================================================================
		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.tag.Equals(foodTag))
			{				
				foodDetected.Invoke(other.gameObject);
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.gameObject.tag.Equals(foodTag))
			{
				if (foodStoppedBeingDetected != null)
					foodStoppedBeingDetected.Invoke();
			}
		}
		#endregion
	}
}