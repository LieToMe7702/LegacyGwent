using System.Collections.Generic;

namespace Assets.Script.Utility
{
    public class ObjectPool<T> : IObjectPool<T>
    {
        private Stack<T> _pool = new Stack<T>();
        public T Get()
        {
            if(_pool.Count == 0)
            {
                T t = default(T);
                return t;
            }
            return _pool.Pop();
        }
        public void Push(T obj)
        {
            _pool.Push(obj);
        }
    }
}
