using System.Collections;
using UnityEngine;

namespace NADBase
{
    public enum ResourceStatus
    {
        NULL,
        QUENE,
        LOADING,
        ERROR,
        DONE,
        CACHE
    }
    public abstract class NADResource
    {
        private uint _id = 0;

        public uint Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _type = "null";

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private byte _priority = 0;

        public byte Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }

        private ResourceStatus _status = ResourceStatus.NULL;

        public ResourceStatus Status
        {
            get { return _status; }
            set 
            {
                if (_status == Status) return;
                _status = Status;
                if(_status == ResourceStatus.ERROR && onErro != null)
                {
                    onErro.Invoke(this);
                }
                if(_status == ResourceStatus.DONE && onDone != null )
                {
                    onDone.Invoke(this);
                }
            }
        }

        public delegate void DoneDelegate(NADResource res);
        public DoneDelegate onDone;

        public delegate void ErroDelegate(NADResource res);
        public ErroDelegate onErro;
        
        public NADResource()
        {

        }

        public IEnumerator onDataLoaded(WWW www)
        {
           yield return 0;
        }

        public abstract string getURL();
    }
}
