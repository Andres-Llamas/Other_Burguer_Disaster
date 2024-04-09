using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Client
{
	public class ClientPlayerDetector : MonoBehaviour
	{
		#region Inspector variables =======================================================================		
		#endregion

		#region No Inspector Variables ====================================================================
		public event Z_utilities.EventHandlers.NotifyVoid OnPlayerDetected;
		public event Z_utilities.EventHandlers.NotifyVoid OnPlayerStoppedBeingDetected;
		#endregion

		#region Methods ===================================================================================

		private void OnTriggerEnter(Collider other)
		{
			if(other.gameObject.tag.Equals("Player"))
			{
				if(OnPlayerDetected != null)
					OnPlayerDetected.Invoke();
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if(other.gameObject.tag.Equals("Player"))
			{
				if(OnPlayerStoppedBeingDetected != null)
					OnPlayerStoppedBeingDetected.Invoke();
			}
		}

		#endregion
	}
}