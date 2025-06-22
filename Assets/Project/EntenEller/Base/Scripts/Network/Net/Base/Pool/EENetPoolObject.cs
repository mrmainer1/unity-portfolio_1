using System;
using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Advanced.Variables;
using Project.EntenEller.Base.Scripts.Network.Net.Base.Events;
using Project.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data;
using Project.EntenEller.Base.Scripts.Patterns.Pool;
using Project.EntenEller.Base.Scripts.Patterns.Singleton;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Network.Net.Base.Pool
{
    public class EENetPoolObject : EEPoolObject
    {
        protected override void EEAwake()
        {
            base.EEAwake();
            if (!EESingleton.Get<EENetServer>().IsActive) return;
            EESingleton.Get<EENetServer>().ClientConnectedEvent += ClientConnected;
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            EESingleton.Get<EENetServer>().ClientConnectedEvent -= ClientConnected;
        }

        private void ClientConnected(EEPeer peer)
        {
            var (i, j) = EESingleton.Get<EENetPoolManager>().GetIndexes(this);
            var data = new EENetPoolObjectData
            {
                I = i,
                J = j,
                State = IsActive,
                Position = EEVectorUtils.V3ToList(transform.position),
                Rotation = EEAngleUtils.QuaternionToList(transform.rotation),
                Scale = EEVectorUtils.V3ToList(transform.localScale)
            };
            EEPacketToClient.Send(peer, typeof(EENetPoolManager), data);
        }

        [Serializable]
        public class EENetPoolObjectData
        {
            public int I;
            public int J;
            public List<float> Position;
            public List<float> Rotation;
            public List<float> Scale;
            public bool State;
        }
    }
}
