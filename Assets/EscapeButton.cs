using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FirstGearGames.LobbyAndWorld.Extensions;
using FirstGearGames.LobbyAndWorld.Lobbies;
public class EscapeButton : MonoBehaviour
{
    public void OnClick_Leave(Animator anim)
    {
        //Hide current room.
        anim.GetComponent<Animator>().SetBool("show", false);
        LobbyNetwork.LeaveRoom();
    }
}
