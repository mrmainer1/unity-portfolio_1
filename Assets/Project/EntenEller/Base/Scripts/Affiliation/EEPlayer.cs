using System.Collections.Generic;
using System.Linq;
using Project.EntenEller.Base.Scripts.Advanced.GameObjects;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;

namespace Project.EntenEller.Base.Scripts.Affiliation
{
    public class EEPlayer : EEBehaviour
    {
        public string ID;
        public string IDTeam;

        private static List<EEPlayer> AllPlayers = new List<EEPlayer>();

        public EENotifier ChangedTeamNotifier;
        public EEGameObject EEGameObject;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            EEGameObject = this.GetEEGameObject();
            AllPlayers.Add(this);
        }

        public void OnDestroy()
        {
            AllPlayers.Remove(this);
        }
        
        public void SetIDPlayer(string id)
        {
            ID = id;
        }
        
        public void SetIDTeam(string id)
        {
            if (IDTeam == id) return;
            IDTeam = id;
            ChangedTeamNotifier.Notify();
        }

        public bool IsSamePlayer(EEPlayer eePlayer)
        {
            return ID == eePlayer.ID;
        }
        
        public bool IsSamePlayer(string id)
        {
            return ID == id;
        }
        
        public bool IsSameTeam(EEPlayer eePlayer)
        {
            return IDTeam == eePlayer.IDTeam;
        }
        
        public bool IsSameTeam(string idTeam)
        {
            return IDTeam == idTeam;
        }
        
        public static List<EEPlayer> GetAllPlayersFromTeam(EEPlayer eePlayer)
        {
            return GetAllPlayersFromTeam(eePlayer.IDTeam);
        }
        
        public static List<EEPlayer> GetAllPlayersFromTeam(string idTeam)
        {
            return AllPlayers.Where(a => a.IsSameTeam(idTeam)).ToList();
        }
    }
}
