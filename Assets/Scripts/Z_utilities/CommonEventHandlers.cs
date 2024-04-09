using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z_utilities.EventHandlers
{
	public delegate void NotifyVoid();
	public delegate void NotifyVoid<T>(T t);	
}