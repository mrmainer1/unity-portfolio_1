using System.Collections.Generic;
using LiteNetLib;

namespace Project.EntenEller.Base.Scripts.Network.Net.Base.Sender
{
    public enum EEDeliveryType
    {
        /// <summary>
        /// Unreliable. Packets can be dropped, can be duplicated, can arrive without order.
        /// </summary>
        Unreliable,

        /// <summary>
        /// Reliable. Packets won't be dropped, won't be duplicated, can arrive without order.
        /// </summary>
        ReliableUnordered,

        /// <summary>
        /// Unreliable. Packets can be dropped, won't be duplicated, will arrive in order.
        /// </summary>
        Sequenced,

        /// <summary>
        /// Reliable and ordered. Packets won't be dropped, won't be duplicated, will arrive in order.
        /// </summary>
        ReliableOrdered,

        /// <summary>
        /// Reliable only last packet. Packets can be dropped (except the last one), won't be duplicated, will arrive in order.
        /// </summary>
        ReliableSequenced
    }
}