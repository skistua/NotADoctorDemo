using System.Collections;
using System.Collections.Generic;
using System;
/// <summary>
/// 资源管理器 从本地或者远端服务器加载 Assetbundle资源
/// </summary>

namespace NADBase
{
    public class NADResourceManager {

	    private static NADResourceManager _instance= null;

        private Dictionary<string, Dictionary<uint, NADResource>> resourceStore;

        private Dictionary<string, Type> resourceTypeDic;

        public NADResourceManager Instance()
        {
            if(null == _instance)
            {
                _instance = new NADResourceManager();
            }

            return _instance;
        }

        private NADResourceManager()
        {
            resourceStore = new Dictionary<string, Dictionary<uint, NADResource>>();
            resourceTypeDic = new Dictionary<string, Type>();
        }

        public void requireRes(string type,uint id)
        {
            NADResource res = getStoredRes(type, id);
            if(null == res)
            {
                res = newResource(type);
            }
        }

        public void releaseRes(NADResource res)
        {

        }

        public void registerResource(String type,Type _class)
        {
            if(typeof(NADResource).IsAssignableFrom(_class))
            {
                resourceTypeDic[type] = _class;
            }
        }

        private NADResource newResource(string type)
        {
            if (resourceTypeDic.ContainsKey(type))
            {
                return ObjectPoolService.Instance().borrowObject(resourceTypeDic[type]) as NADResource;
            }
            else
            {
                return null;
            }
        }

        private NADResource getStoredRes(String type, uint id)
        {
            if(resourceStore.ContainsKey(type))
            {
                Dictionary<uint,NADResource> idDic = resourceStore[type];
                if(idDic.ContainsKey(id))
                {
                    return idDic[id];
                }
            }
            return null;
        }

        private NADResource removeRes(String type, uint id)
        {
            NADResource res = null;

            if (resourceStore.ContainsKey(type))
            {
                Dictionary<uint, NADResource> idDic = resourceStore[type];
                if (idDic.ContainsKey(id))
                {
                    res  = idDic[id];
                    idDic.Remove(id);
                }
            }
            return res;
        }
    }
}

