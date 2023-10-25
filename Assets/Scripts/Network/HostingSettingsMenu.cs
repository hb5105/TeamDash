using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class HostSettingsMenu : MonoBehaviourPunCallbacks
{
    private ExitGames.Client.Photon.Hashtable roomProp = new ExitGames.Client.Photon.Hashtable();

    public TMP_InputField Usernamefield;
    public TMP_InputField Passwordfield;

    public void SetMaxPlayers(int maxPlayersIndex)
    {
        int players = maxPlayersIndex + 2;
        GameSettings.MaxPlayers = players;
        Debug.Log(players);
    }

    public void HostGame()
    {
        GameSettings.Username = Usernamefield.text;
        Debug.Log(GameSettings.Username);
        GameSettings.GamePassword = Passwordfield.text;
        Debug.Log(GameSettings.GamePassword);
        PhotonNetwork.NickName = GameSettings.Username;
        PhotonNetwork.CreateRoom(GameSettings.GamePassword, new RoomOptions { MaxPlayers = (byte)(GameSettings.MaxPlayers + 1) });
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("David Scene");
    }
}
