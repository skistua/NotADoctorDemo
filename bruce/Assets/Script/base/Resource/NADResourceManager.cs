using UnityEngine;
using System.Collections;
/// <summary>
/// 资源管理器 从本地或者远端服务器加载 Assetbundle资源
/// </summary>

namespace Resource
{
    public class NADResourceManager {

	    private static NADResourceManager _instance= null;

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

        }

        public void requireResource(string type,uint id)
        {

        }
    }
}

