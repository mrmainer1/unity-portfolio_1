using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BestHTTP;
using MEC;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Advanced.Events;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Cache;

namespace Project.EntenEller.Base.Scripts.Network.HTTP
{
    public class EEHTTP : EEBehaviour
    {
        public List<string> URLList;
        private string urlParameters;
        [SerializeField] private bool isRepeatOnError;
        [SerializeField] private HTTPMethods Method = HTTPMethods.Get;
        [SerializeField] private bool isCached;
        
        private HTTPRequest request;
        
        public EENotifier BeginNotifier;
        public EENotifier RepeatNotifier;
        public EENotifier SuccessNotifier;
        public EENotifier FailNotifier;
        public EENotifier EndNotifier;
        
        public Action<HTTPResponse> SuccessEvent;
        public Action<HTTPRequest> NewRequestEvent;

        private static bool isInit;
        private static readonly List<CompletedHTTP> completed = new();
        
        protected override void EEAwake()
        {
            base.EEAwake();
            if (!isInit)
            {
                isInit = true;
                HTTPManager.Setup();
                Timing.RunCoroutineSingleton(Loop(), ComponentID, SingletonBehavior.Overwrite);
            }

            static IEnumerator<float> Loop()
            {
                while (true)
                {
                    yield return Timing.WaitForOneFrame;
                    while (completed.Count > 0)
                    {
                        var a = completed[0];
                        if (a.Response != null)
                        {
                            a.Source.SuccessEvent.Call(a.Response);
                            a.Source.SuccessNotifier.Notify();
                        }
                        else
                        {
                            a.Source.FailNotifier.Notify();
                            if (a.Source.isRepeatOnError)
                            {
                                a.Source.RepeatNotifier.Notify();
                                a.Source.Call();
                            }
                        }
                        a.Source.EndNotifier.Notify();
                        completed.RemoveAt(0);
                    }
                }
            }
        }

        public void SetParameters(Dictionary<string, string> parameters)
        {
            urlParameters = "?";
            foreach (var kv in parameters)
            {
                urlParameters += kv.Key + "=" + kv.Value + "&";
            }
        }

        public void Call()
        {
            var uri = new Uri(URLList.First());
            request = new HTTPRequest(uri, Method);
            
            if (isCached)
            {
                var result = EEGlobalCache.Get(ComponentID);
                if (result != null)
                {
                    completed.Add((CompletedHTTP) result);
                    return;
                }
            }
            
            NewRequestEvent.Call(request);
            request.Uri = new Uri(URLList.Aggregate(string.Empty, (current, url) => current + url) + urlParameters);

            BeginNotifier.Notify();
            request.Callback = AddData;
            request.Send();
        }


        private void AddData(HTTPRequest _, HTTPResponse response)
        {
            var completedHTTP = new CompletedHTTP
            {
                Source = this,
                Response = response
            };
            completed.Add(completedHTTP);
            if (isCached && response != null) EEGlobalCache.Set(ComponentID, completedHTTP);
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            Stop();
        }

        public void Stop()
        {
            if (request == null) return;
            request.Abort();
            request.Dispose();
        }

        public void SetURL(string url, int index = 0)
        {
            if (URLList.Count == index) URLList.Add(url);
            else URLList[index] = url;
        }

        private struct CompletedHTTP
        {
            public EEHTTP Source;
            public HTTPResponse Response;
        }
    }
}
