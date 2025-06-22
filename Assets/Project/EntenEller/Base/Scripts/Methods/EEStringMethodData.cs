using System;
using Project.EntenEller.Base.Scripts.Advanced.Tags;

namespace Project.EntenEller.Base.Scripts.Methods
{
    [Serializable]
    public class EEStringMethodData
    {
        public string EETag;
        public string[] Parameters;

        public object FindAndCall()
        {
            return EETagUtils.FindEETagInScenes(EETag).GetSelf<EEStringMethod>().Call(Parameters);
        }
    }
}
