using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    TypedLobby killCount = new TypedLobby(Lobby.KILL_COUNT, LobbyType.Default);
    TypedLobby teamBattle = new TypedLobby(Lobby.TEAM_BATTLE, LobbyType.Default);
    TypedLobby noRespawn = new TypedLobby(Lobby.NO_RESPAWN, LobbyType.Default);
    [SerializeField] private GameObject roomNumber;
    private string levelName = "";
    private void Start()
    {
        roomNumber.SetActive(false);
    }
    public void BackToMenu()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("MainMenu");
    }
    public void JoinGameKillCount()
    {
        levelName = "KillCount";
        PhotonNetwork.JoinLobby(killCount);
    }
    public void JoinGameTeamBattle()
    {
        levelName = "Floor layout";

        PhotonNetwork.JoinLobby(teamBattle);

    }
    public void JoinGameNoRespawn()
    {
        levelName = "Floor layout";

        PhotonNetwork.JoinLobby(noRespawn);
    }
    public override void OnJoinedLobby()
    {
        /*
         Được gọi khi người chơi tham gia thành công vào một lobby.
        Sau khi tham gia vào lobby, người chơi có thể bắt đầu tìm kiếm các phòng để tham gia, hoặc xem các thông tin và trạng thái của các phòng hiện có.
        Bạn có thể sử dụng hàm này để cập nhật giao diện người dùng hoặc hiển thị danh sách các phòng có sẵn trong lobby.
         */
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 6;
        PhotonNetwork.CreateRoom("Arena" + Random.Range(1, 100), roomOptions);
    }
    public override void OnJoinedRoom()
    {
        roomNumber.SetActive(true);
        PhotonNetwork.LoadLevel(levelName);
    }
}
