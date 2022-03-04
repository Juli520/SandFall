using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public static LobbyManager Instance;
    [Header("Game Handling")] 
    [Range(1, 4)]
    public int minPlayers = 1;
    [Header("Scene Handling")]
    public string sceneLevelName;
    public string sceneMenuName;
    [Header("Error Handling")]
    public GameObject errorText;
    [Header("Connection Handling")]
    public GameObject connectingMenu;
    public GameObject selectionMenu;
    public GameObject controlsMenu;
    public GameObject mainMenu;
    
    private string _roomName = string.Empty;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            PhotonNetwork.Destroy(gameObject);
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
            return;
        
        ConnectToServer();
        OnConnected();
    }

    public void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void DisconnectFromServer()
    {
        PhotonNetwork.Disconnect();
    }

    public void LeaveLobby()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void JoinOrCreateRoom()
    {
        if (_roomName == string.Empty || _roomName == "" ||
            PhotonNetwork.LocalPlayer.NickName == "" ||
            PhotonNetwork.LocalPlayer.NickName == string.Empty)
            return;
        
        RoomOptions options = new RoomOptions
        {
            MaxPlayers = 4,
            IsOpen = true,
            IsVisible = true
        };

        PhotonNetwork.JoinOrCreateRoom(_roomName, options, TypedLobby.Default);
    }
    
    public void ChangeRoomName(string serverName)
    {
        _roomName = serverName;
    }

    public void ChangeNickName(string nickName)
    {
        PhotonNetwork.LocalPlayer.NickName = nickName;
    }

    public string GetRoomName()
    {
        return _roomName;
    }

    public Photon.Realtime.Player[] GetPlayerList()
    {
        return PhotonNetwork.PlayerList;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void CheckPlayerCount()
    {
        if (PhotonNetwork.PlayerList.Length == minPlayers)
            photonView.RPC("StartGame", RpcTarget.All);
        else
            errorText.SetActive(true);
    }
    
    [PunRPC]
    public void StartGame()
    {
        if (sceneLevelName == string.Empty)
            return;

        PhotonNetwork.LoadLevel(sceneLevelName);
    }

    public override void OnConnected()
    {
        CheckConnection();
    }

    public void CheckConnection()
    {
        if (PhotonNetwork.IsConnected && !mainMenu.activeSelf && !controlsMenu.activeSelf)
        {
            connectingMenu.SetActive(false);
            selectionMenu.SetActive(true);
        }   
    }
    
    public void MainMenu()
    {
        PhotonNetwork.Disconnect();
        PhotonNetwork.LoadLevel(sceneMenuName);
    }
}
