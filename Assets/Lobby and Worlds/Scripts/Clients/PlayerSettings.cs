

using FishNet.Object;
using FishNet.Object.Synchronizing;
using System;
using UnityEngine;

namespace FirstGearGames.LobbyAndWorld.Clients
{

    public class PlayerSettings : NetworkBehaviour
    {

        #region Private.
        /// <summary>
        /// Username for this client.
        /// </summary>
        [SyncVar]
        private string _username;
        [SerializeField] [SyncVar] private int _warriorIndex, _gunIndex;
        [SerializeField] private WarriorObject[] _allWarriors;
        [SerializeField] private GunObject[] _allGuns;
        #endregion

        /// <summary>
        /// Sets Username.
        /// </summary>
        /// <param name="value"></param>
        public void SetUsername(string value)
        {
            _username = value;
        }
        public void ChooseWarrior(int value)
        {
            _warriorIndex = value;
        }
        /// <summary>
        /// Returns Username.
        /// </summary>
        /// <returns></returns>
        public string GetUsername() 
        {
            return _username; 
        }

        public int GetWarriorIndex() 
        {
            return _warriorIndex; 
        }
        public void SetWarriorIndex(int value) 
        {
            _warriorIndex = value;
        }
        public WarriorObject[] GetAllWarriors() 
        {
            return _allWarriors; 
        }

        public int GetGunIndex() 
        {
            return _gunIndex; 
        }
        public void SetGunIndex(int value) 
        {
            _gunIndex = value;
        }
        public GunObject[] GetAllGuns() 
        {
            return _allGuns; 
        }

    }

}
