

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
        [SerializeField] [SyncVar] private int _warriorIndex;
        [SerializeField] private WarriorObject[] _allWarriors;
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

    }

}
