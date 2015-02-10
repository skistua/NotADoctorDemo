using System.Collections.Generic;
using System;

namespace NADBase
{
    class ObjectPoolService
    {
        private static ObjectPoolService _instance = new ObjectPoolService();

        private IDictionary<Type, ObjectPool> _poolArray;

        private ObjectPoolService()
        {
            _poolArray = new Dictionary<Type, ObjectPool>();
        }

        public static ObjectPoolService Instance()
        {
            return _instance;
        }

        public Object borrowObject(Type type)
        {
            ObjectPool pool = null;

            if (!_poolArray.ContainsKey(type))
            {
                _poolArray[type] = pool = new ObjectPool(type);
            }
            else
            {
                pool = _poolArray[type];
            }

            return pool.borrowObject();
        }

        public void returnObject(Object obj)
        {
            Type type = obj.GetType();

            if (_poolArray.ContainsKey(type))
            {
                _poolArray[type].returnObject(obj);
            }
            else
            {
                ObjectPool pool = new ObjectPool(type);
                pool.returnObject(obj);
                _poolArray[type] = pool;
            }
        }

        public void clearPool(Type type)
        {
            if (_poolArray.ContainsKey(type))
            {
                _poolArray[type].cleanObject();
            }
        }

        public int getPoolNum(Type type)
        {
            if (_poolArray.ContainsKey(type))
            {
                return _poolArray[type].getObjectNum();
            }
            return 0;
        }

        public ObjectPool getObjectPool(Type type)
        {
            if (_poolArray.ContainsKey(type))
            {
                return _poolArray[type];
            }
            else
            {
                ObjectPool pool = new ObjectPool(type);
                _poolArray[type] = pool;
                return pool;
            }
        }

    }
}












