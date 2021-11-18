using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SO.Events
{
    public class GenericDelegate
    {
        public delegate void Notify();
        public delegate void Notify<T>(T t);        
    }
}