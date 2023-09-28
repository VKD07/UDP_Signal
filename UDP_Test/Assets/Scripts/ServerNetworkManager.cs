using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerNetworkManager : MonoBehaviour
{
    [Header("Objs")]
    [SerializeField] UDPReceive m_receiver;
    [SerializeField] UDPSend m_sender;

    private void Awake()
    {
        m_receiver.receiveUDP += OnReceiveSensorData;
    }

    void Start()
    {
        //m_selfieIdleMessage = ConfigManager.GetInstance().GetStringValue("PLAY_SELFIE_IDLE_MESSAGE");
        //m_selfieDataKeyword = ConfigManager.GetInstance().GetStringValue("SELFIE_REQUEST_MESSAGE");
        //m_playScreenSaverKeyword = ConfigManager.GetInstance().GetStringValue("SCREENSAVER_REQUEST_MESSAGE");
        //m_connectionCheckKeyword = ConfigManager.GetInstance().GetStringValue("CONNECTION_REQUEST_MESSAGE");
    }

    public void OnReceiveSensorData(string p_msge)
    {
        //On Receive Message.
        //Data Sample: Selfie:Michon Bojador,Visitor, I am a resident, mbojador@gmail.com,ABCD-AS123123
        // Selfie:Fajel,Resident, Love to Live,fajel@sparkslab.me,9c3dca0c-de73-4577-a207-a4d6b8ca8a4c

        //if (p_msge.StartsWith(m_selfieDataKeyword))
        //{
        //    var splitMessage = p_msge.Split(':');
        //    m_photoManager.ReceiveData(splitMessage[1]);
        //}
        //else if (p_msge.Equals(m_playScreenSaverKeyword))
        //{
        //    m_videoManager.PlayVideo();
        //}
        //else if (p_msge.Equals(m_selfieIdleMessage))
        //{
        //    m_photoManager.StopPhotoReveal(false);
        //    m_videoManager.PlaySelfieIdle();
        //}
        //else if (p_msge.Equals(m_connectionCheckKeyword))
        //{
        //    isTabletConnected = true;
        //}
    }

    //public void CheckTabletConnection()
    //{
    //    m_sender.DelayedSendUDP("STATUS");
    //    isTabletConnected = false;
    //}

}