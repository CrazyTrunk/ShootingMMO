using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    TypedLobby killCount = new TypedLobby(Lobby.KILL_COUNT,LobbyType.Default);
    TypedLobby teamBattle = new TypedLobby(Lobby.TEAM_BATTLE, LobbyType.Default);
    TypedLobby noRespawn = new TypedLobby(Lobby.NO_RESPAWN, LobbyType.Default);
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void JoinGameKillCount()
    {
        PhotonNetwork.JoinLobby(killCount);
    }
    public void JoinGameTeamBattle()
    {
        PhotonNetwork.JoinLobby(teamBattle);

    }
    public void JoinGameNoRespawn()
    {
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
}
