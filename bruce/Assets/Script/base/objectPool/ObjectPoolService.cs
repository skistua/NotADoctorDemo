using System.Collections.Generic;
using System;
class ObjectPoolService
{
    private static ObjectPoolService _instance = new ObjectPoolService();

    private IDictionary<Type, ObjectPool> _poolArray;

    private ObjectPoolService()
    {
        _poolArray = new Dictionary
    }

}

