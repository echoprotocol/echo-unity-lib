using System.Collections.Generic;
using System.Threading;
using UnityEngine;


namespace CustomTools {

    public class MainThreadResolver : MonoBehaviour {

        public class DelayedQueueItem {

            readonly System.Action action;


            public float FireTime { get; private set; }

            public DelayedQueueItem( float fireTime, System.Action action ) {
                FireTime = fireTime;
                this.action = action;
            }

            public void FireAction() => action?.Invoke();
        }


        const int MAX_THREADS = 8;

        static int currentThreadCount;
        static MainThreadResolver instance;
        static bool initialized;

        readonly List<System.Action> actions = new List<System.Action>();
        readonly List<System.Action> currentActions = new List<System.Action>();
        readonly List<DelayedQueueItem> delayed = new List<DelayedQueueItem>();
        readonly List<DelayedQueueItem> currentDelayed = new List<DelayedQueueItem>();


        private static MainThreadResolver Instance {
            get {
                Initialize();
                return instance;
            }
        }

        public static void Initialize() {
            if ( !initialized ) {
                if ( !Application.isPlaying ) {
                    return;
                }
                instance = new GameObject( "ThreadResolver" ).AddComponent<MainThreadResolver>();
                initialized = true;
                DontDestroyOnLoad( instance.gameObject );
            }
        }
        
        public static void QueueOnMainThread( System.Action action )
        {
            lock ( Instance.actions ) {
                Instance.actions.Add( action );
            }
        }

        public static void QueueOnMainThread( System.Action action, float delay ) {
            if ( delay != 0f ) {
                lock ( Instance.delayed ) {
                    Instance.delayed.Add( new DelayedQueueItem( Time.time + delay, action ) );
                }
            } else {
                lock ( Instance.actions ) {
                    Instance.actions.Add( action );
                }
            }
        }

        public static void RunAsync( System.Action action ) {
            Initialize();
            while ( currentThreadCount >= MAX_THREADS ) {
                Thread.Sleep( 1 );
            }
            Interlocked.Increment( ref currentThreadCount );
            ThreadPool.QueueUserWorkItem( state => {
                try {
                    (state as System.Action)?.Invoke();
                } catch {
                } finally {
                    Interlocked.Decrement( ref currentThreadCount );
                }
            }, action );
        }

        private void Awake() {
            if ( instance == null ) {
                instance = this;
                initialized = true;
                DontDestroyOnLoad( gameObject );
            } else
            if ( instance != this ) {
                Destroy( this );
            }
        }

        private void Update() {
            lock ( actions ) {
                currentActions.Clear();
                currentActions.AddRange( actions );
                actions.Clear();
            }
            foreach ( var action in currentActions ) {
                action?.Invoke();
            }
            lock ( delayed ) {
                currentDelayed.Clear();
                var items = delayed.FindAll( item => item.FireTime <= Time.time );
                if ( items.Count > 0 ) {
                    currentDelayed.AddRange( items );
                }
                foreach ( var item in items ) {
                    delayed.Remove( item );
                }
                items.Clear();
            }
            foreach ( var item in currentDelayed ) {
                item.FireAction();
            }
        }
    }
}