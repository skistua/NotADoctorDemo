using System.Collections;
using System;


namespace NADBase
{
    public class ObjectPool
    {
        private Stack _pool;
        private Type _poolType;
        private int _limitNum = -1;

        public ObjectPool(Type type)
        {
            _pool = new Stack();
            _poolType = type;
        }

        public Object borrowObject()
        {
            if (_pool.Count == 0)
            {
                return Activator.CreateInstance(_poolType);
            }
            else
            {
                return _pool.Pop();
            }
        }

        public void returnObject(Object obj)
        {
            if (_pool.Count < _limitNum)
            {
                _pool.Push(obj);
            }
        }

        public void cleanObject()
        {
            _pool.Clear();
        }

        public int getObjectNum()
        {
            return _pool.Count;
        }

        public void setLimit(int num)
        {
            _limitNum = num;
        }
    }

}
