using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class InitNetworkSceneManager : MonoBehaviourPunCallbacks
{
    #region Unity Methods
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        ConnectToPhotonServer();
    }
    #endregion

    #region Public Methods
    public void ConnectToPhotonServer()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    #endregion

    #region Photon Callbacks
    public override void OnConnectedToMaster()
    {
        //if(PhotonNetwork.NickName == "")
        //{
        PhotonNetwork.NickName = "Hhe " + Random.Range(1, 10);
        //}
        Debug.Log(PhotonNetwork.NickName + " Connected to photon server.");
        JoinRandomRoom();
    }

    public override void OnConnected()
    {
        Debug.Log("Connected to Internet.");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log(message);
        CreateAndJoinRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + " joined to" + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LoadLevel("1_GameScene");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " joined to" + PhotonNetwork.CurrentRoom.Name + " " + PhotonNetwork.CurrentRoom.PlayerCount);
    }
    #endregion

    #region Private methods
    void CreateAndJoinRoom()
    {
        string randomRoomName = "Room " + Random.Range(0, 10000);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 4;

        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);
    }
    #endregion
}
