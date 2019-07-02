using System;
using UnityEngine;


namespace CustomTools.UnityEvents {

    [Serializable] public class UnityEventVector2 : UnityEngine.Events.UnityEvent<Vector2> { }

    [Serializable] public class UnityEventVector3 : UnityEngine.Events.UnityEvent<Vector3> { }

    [Serializable] public class UnityEventVector4 : UnityEngine.Events.UnityEvent<Vector4> { }
   
    [Serializable] public class UnityEventColor : UnityEngine.Events.UnityEvent<Color> { }

    [Serializable] public class UnityEventTransform : UnityEngine.Events.UnityEvent<Transform> { }

    [Serializable] public class UnityEventInt : UnityEngine.Events.UnityEvent<int> { }

    [Serializable] public class UnityEventLong : UnityEngine.Events.UnityEvent<long> { }

    [Serializable] public class UnityEventFloat : UnityEngine.Events.UnityEvent<float> { }

    [Serializable] public class UnityEventDouble : UnityEngine.Events.UnityEvent<double> { }

    [Serializable] public class UnityEventEnum : UnityEngine.Events.UnityEvent<Enum> { }

    [Serializable] public class UnityEventBool : UnityEngine.Events.UnityEvent<bool> { }

    [Serializable] public class UnityEvent<T> : UnityEngine.Events.UnityEvent<T> { }
    
    [Serializable] public class UnityEvent<T, U> : UnityEngine.Events.UnityEvent<T, U> { }
}