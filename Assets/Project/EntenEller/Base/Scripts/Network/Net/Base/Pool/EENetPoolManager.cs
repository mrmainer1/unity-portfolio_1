using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Network.Net.Base.Receiver;
using Project.EntenEller.Base.Scripts.Patterns.Pool;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Network.Net.Base.Pool
{
    public class EENetPoolManager : EEBehaviour, IEENetReceiverClient
    {
        [SerializeField] private List<EEPoolObject> pool;

        public (int, int) GetIndexes(EEPoolObject poolObject)
        {
            var i = pool.IndexOf(poolObject.Origin);
            var j = EEPool.GetIndex(poolObject);
            return (i, j);
        }
        
        public EEPoolObject GetOrigin(int i)
        {
            return pool[i];
        }

        public void ReceiveAsClient(object data)
        {
            var poolObjectData = (EENetPoolObject.EENetPoolObjectData) data;
            print(poolObjectData.J);
        }
    }
}
