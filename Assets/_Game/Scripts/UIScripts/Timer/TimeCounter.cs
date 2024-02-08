using Photon.Pun;
using TMPro;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI minutesText;
    [SerializeField] private TextMeshProUGUI secondsText;
    [SerializeField] private PhotonView photonView;

    [SerializeField] private int minutes = 4;
    [SerializeField] private int seconds = 59;
    [SerializeField] private KillCount killCount;
    private bool timeStop = false;

    public bool TimeStop { get => timeStop; set => timeStop = value; }

    public void BeginTimer()
    {
        
        minutesText.text = minutes.ToString();
        secondsText.text = seconds.ToString();
        photonView.RPC(nameof(Count), RpcTarget.AllBuffered);
    }
    [PunRPC]
    public void Count()
    {
        CancelInvoke(nameof(CountDown));
        InvokeRepeating(nameof(CountDown), 1, 1);
    }

    private void CountDown()
    {
        if (seconds > 10)
        {
            seconds -= 1;
            secondsText.text = seconds.ToString();
        }
        else if (seconds > 0 && seconds < 11)
        {
            seconds -= 1;
            secondsText.text = "0" + seconds.ToString();
        }
        else if (seconds == 0 && minutes > 0)
        {
            secondsText.text = "0" + seconds.ToString();
            minutes -= 1;
            seconds = 59;
            secondsText.text = seconds.ToString();

        }
        if(seconds == 0 && minutes <= 0)
        {
            killCount.countDown = false;
            killCount.TimeOver();
            TimeStop = true;
        }
    }
}
