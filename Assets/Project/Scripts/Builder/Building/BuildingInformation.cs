using UnityEngine;

namespace Project.Scripts.Builder.Building
{
    public class BuildingInformation : MonoBehaviour
    {
        public string Username;
        public string CarNumber;

        public void Set(string username, string carNumber)
        {
            Username = username;
            CarNumber = carNumber;
        }
    }
}
