using System.Collections;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;
using UnityEngine.Networking;

namespace Project.EntenEller.Base.Scripts.Network.API.Telegram
{
    public class EETelegramGroup : EEBehaviour
    {
        public string ID;

        public void SendImage(Texture2D image)
        {
            var url = $"https://api.telegram.org/bot{GetSelf<EETelegramManager>().Key}/sendPhoto?chat_id={ID}";
            var imageBytes = image.EncodeToPNG();
            var form = new WWWForm();
            form.AddBinaryData("photo", imageBytes, "image.png", "image/png");
            StartCoroutine(Send());
            
            IEnumerator Send()
            {
                using var request = UnityWebRequest.Post(url, form);
                yield return request.SendWebRequest();
            
                if (request.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log("Image sent successfully");
                }
                else
                {
                    Debug.Log(request.error);
                }
            }
        }
    }
}
