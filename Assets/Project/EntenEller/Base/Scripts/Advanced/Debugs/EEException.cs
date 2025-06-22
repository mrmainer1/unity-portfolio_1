using System;

namespace Project.EntenEller.Base.Scripts.Advanced.Debugs
{
    public static class EEException
    {
        public static void Call(object script, string message)
        {
            throw new Exception("[" + script + "] : " + message);
        }
    }
}