using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Advanced.Tags;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.UI.Toggles;
using Sirenix.Utilities;

namespace Project.Scripts.Builder.Builder
{
    public class BuilderManager : EEBehaviour
    {
        public Building.Building CurrentBuilding;
        public bool IsDrag;
        private HashSet<EETagHolder> builders;

        protected override void EEAwake()
        {
            base.EEAwake();
            builders = EETagUtils.FindEETagsInScenes("builder");
        }

        public void SetActiveAllBuilder()
        {
            builders.ForEach(n => n.GetChild<EEToggle>().Enable());
        }
        public void SetBuilding(Building.Building building, bool isDrag = false)
        {
            CurrentBuilding = building;
            IsDrag = isDrag;
        }

        public void ClearBuilding()
        {
            CurrentBuilding = null;
            IsDrag = false;
        }
    }
}
