using BestHTTP;
using Project.EntenEller.Base.Scripts.Advanced.Textures;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Network.HTTP
{
    public class EEHTTPTexture2D : EEHTTPByteReceiver
    {
        protected override void Success(HTTPResponse result)
        {
            base.Success(result);
            GetSelf<EETextureChanger>().Change(result.DataAsTexture2D);
        }
    }
}
