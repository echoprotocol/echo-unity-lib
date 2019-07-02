using System;
using System.Collections.Generic;


namespace Base.Queues
{
    public class LockedQueue<T> : IDisposable
    {
        private readonly object locker;
        private readonly Queue<T> queue;
        private int count;


        public LockedQueue()
        {
            locker = new object();
            queue = new Queue<T>();
            count = 0;
        }

        public int Count
        {
            get
            {
                lock (locker)
                {
                    return count;
                }
            }
        }

        public bool IsEmpty
        {
            get
            {
                lock (locker)
                {
                    return count == 0;
                }
            }
        }

        public void Clear()
        {
            lock (locker)
            {
                queue?.Clear();
                count = 0;
            }
        }

        public T Dequeue()
        {
            lock (locker)
            {
                if (count > 0)
                {
                    count--;
                    return queue.Dequeue();
                }
            }
            return default(T);
        }

        public void Enqueue(T item)
        {
            lock (locker)
            {
                count++;
                queue.Enqueue(item);
            }
        }

        public bool TryDequeue(out T result)
        {
            lock (locker)
            {
                if (count > 0)
                {
                    count--;
                    result = queue.Dequeue();
                    return true;
                }
            }
            result = default(T);
            return false;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            Clear();
        }
    }
}