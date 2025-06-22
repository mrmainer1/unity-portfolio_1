using Project.EntenEller.Base.Scripts.Cache;

namespace Project.EntenEller.Base.Scripts.Conditions
{
    public class EEConditionQuestPoint : EECondition
    {
        public string Key;
        public int Value;
    
        public override bool IsFulfill()
        {
            if (EEGlobalCache.Get(Key) == null) return false;
            return (int) EEGlobalCache.Get(Key) == Value;
        }
    }
}
