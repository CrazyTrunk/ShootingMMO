using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private InputField nickNamePlayer;
    [SerializeField] private GameObject connecting;

    private string setName = "";
    // Start is called before the first frame update
    void Start()
    {
        connecting.SetActive(false);
    }

    public void UpdateText()
    {
        setName = nickNamePlayer.text;
        PhotonNetwork.LocalPlayer.NickName = setName;
    }
    public void EnterButton()
    {
        if (setName != null)
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
            connecting.SetActive(true);
        }
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    public override void OnConnectedToMaster()
    {
        /*
         OnConnectedToMaster():
        Được gọi khi client (máy của người chơi) đã kết nối thành công với Photon Master Server.
        Đây là bước đầu tiên để tham gia vào trò chơi, và sau khi kết nối, người chơi có thể thực hiện các hành động như tham gia hoặc tạo phòng.
        Thông thường, bạn có thể thêm code trong hàm này để thông báo cho người chơi rằng họ đã kết nối thành công, hoặc để tự động tham gia vào một lobby hoặc tìm kiếm một phòng chơi.
         */
        Debug.Log("I'm connect to server");
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinedRoom()
    {
        //khi game bắt đầu
        PhotonNetwork.LoadLevel("Floor layout");
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        /*
         Được gọi khi việc thử tham gia vào một phòng ngẫu nhiên (thông qua hàm như PhotonNetwork.JoinRandomRoom()) thất bại.
        returnCode và message cung cấp thông tin về lý do tại sao việc tham gia phòng thất bại, có thể là do không có phòng nào khả dụng hoặc do các điều kiện lọc không được đáp ứng.
        Thường được sử dụng để xử lý các trường hợp thất bại như thông báo cho người chơi hoặc tự động tạo phòng mới.
         */
        PhotonNetwork.CreateRoom("Arena1");
    }
}
