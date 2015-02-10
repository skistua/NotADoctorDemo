using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace NADBase
{
    public class NADResRequireQuene : MonoBehaviour
    {
        private List<NADResource> quene;

        private const uint MaxLoadResNum = 3;
                                           
        private uint currentLoadResNum = 0;

        public NADResRequireQuene()
        {
            quene = new List<NADResource>();
        }

        public void addToQuene(NADResource res)
        {
            if (!quene.Contains(res))
            {
               if(currentLoadResNum < MaxLoadResNum)
               {
                   loadNextRes(res);
               }
               else
               {
                   for (int i = 0 ; i < quene.Count  ; i++)
                   {
                       if(res.Priority > quene[i].Priority)
                       {
                           quene.Insert(i, res);
                           res.Status = ResourceStatus.QUENE;
                       }
                   }
               }
            }
        }

        public void removeFromQuene(NADResource res)
        {

        }

        private void loadNextRes(NADResource res)
        {
            if (currentLoadResNum < MaxLoadResNum)
            {
                if (null == res && quene.Count > 0)
                {
                    res = quene[0];
                    quene.RemoveAt(0);
                }

                if(res != null)
                {
                    currentLoadResNum++;
                    StartCoroutine(loaderCoroutine(res));
                }
            }
        }

        private IEnumerator loaderCoroutine(NADResource res)
        {
            res.Status = ResourceStatus.LOADING;
            WWW www = new WWW(res.getURL());
            yield return www;
            if (string.IsNullOrEmpty(www.error))
            {
                if(res.onErro != null)
                {
                    res.Status = ResourceStatus.ERROR;
                }
            }
            else
            {
                yield return res.onDataLoaded(www);
            }
            currentLoadResNum--;
            loadNextRes(null);
        }


    }    
}

