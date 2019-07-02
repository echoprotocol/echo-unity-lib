using CustomTools.CachedBehaviour;
using CustomTools.Extensions.Core;
using CustomTools.Extensions.Core.Array;
using UnityEngine;


namespace CustomTools.Singleton {

    public interface ISingletonBehaviour {

		bool IsSingletonObject { get; }
	}


    public static class Singleton<T> where T : MonoBehaviour {

		static T instance;
		static int globalInstanceCount = 0;
		static bool awakeSingletonCalled = false;

		internal static GameObject autoCreatePrefab;


        public static T Get( bool throwErrorIfNotFound = true, bool autoCreate = false ) {
			if ( instance.IsNull() ) {
                var component = Resources.FindObjectsOfTypeAll<T>().Find( c => (c as ISingletonBehaviour).IsSingletonObject );
				if ( component.IsNull() ) {
					if ( autoCreate && !autoCreatePrefab.IsNull() ) {
					    Object.Instantiate( autoCreatePrefab ).name = autoCreatePrefab.name;
                        component = Object.FindObjectOfType<T>();
                        if ( component.IsNull() ) {
							Console.Error( "Auto created object does not have component", typeof( T ).Name );
							return null;
						}
					} else {
						if ( throwErrorIfNotFound ) {
							Console.Error( "No singleton component", typeof( T ).Name, "found in the scene." );
						}
						return null;
					}
				}
                AwakeSingleton( instance = component );
            }
			return instance;
		}

        public static bool IsExist {
            get {
                if ( !instance.IsNull() ) {
                    return true;
                }
                var component = Resources.FindObjectsOfTypeAll<T>().Find( c => (c as ISingletonBehaviour).IsSingletonObject );
                if ( !component.IsNull() ) {
                    AwakeSingleton( instance = component );
                    return true;
                }
                return false;
            }
        }

        public static void Awake( T newInstance ) {
			globalInstanceCount++;
			if ( globalInstanceCount > 1 ) {
				Console.Error( "More than one instance of SingletonBehaviour", typeof( T ).Name );
			} else {
				instance = newInstance;
			}
			AwakeSingleton( newInstance );
		}

        public static void Destroy() {
            if ( globalInstanceCount > 0 ) {
				globalInstanceCount--;
				if ( globalInstanceCount == 0 ) {
					awakeSingletonCalled = false;
					instance = null;
				}
			}
		}

		static void AwakeSingleton( T newInstance ) {
			if ( !awakeSingletonCalled ) {
				awakeSingletonCalled = true;
				newInstance.SendMessage( "AwakeSingleton", SendMessageOptions.DontRequireReceiver );
			}
		}
	}


    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour, ISingletonBehaviour where T : MonoBehaviour {

        public static T Instance => Singleton<T>.Get( true, true );

        public static bool IsInstanceExist => Singleton<T>.IsExist;

        public static void ActivateSingletonInstance() => Singleton<T>.Get( true, true );

        public static void SetSingletonAutoCreate( GameObject autoCreatePrefab ) => Singleton<T>.autoCreatePrefab = autoCreatePrefab;

        protected virtual void AwakeSingleton() { }

        protected virtual void Awake() {
            if ( IsSingletonObject ) {
                Singleton<T>.Awake( this as T );
            }
        }

        protected virtual void OnDestroy() {
            if ( IsSingletonObject ) {
                Singleton<T>.Destroy();
            }
        }

        public virtual bool IsSingletonObject => true;

        protected void DontDestroyOnLoad() {
            transform.parent = null;
            DontDestroyOnLoad( gameObject );
        }
    }


    public abstract class SingletonCachedMonoBehaviour<T> : CachedMonoBehaviour, ISingletonBehaviour where T : MonoBehaviour {

        public static T Instance => Singleton<T>.Get( true, true );

        public static bool IsInstanceExist => Singleton<T>.IsExist;

        public static void ActivateSingletonInstance() => Singleton<T>.Get( true, true );

        public static void SetSingletonAutoCreate( GameObject autoCreatePrefab ) => Singleton<T>.autoCreatePrefab = autoCreatePrefab;

        protected virtual void AwakeSingleton() { }

        protected virtual void Awake() {
            if ( IsSingletonObject ) {
                Singleton<T>.Awake( this as T );
            }
        }

        protected virtual void OnDestroy() {
            if ( IsSingletonObject ) {
                Singleton<T>.Destroy();
            }
        }

        public virtual bool IsSingletonObject => true;

        protected void DontDestroyOnLoad() {
            transform.parent = null;
            DontDestroyOnLoad( gameObject );
        }
    }
}