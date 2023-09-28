using UnityEngine;
using System.Collections;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPSend : MonoBehaviour
{
    private static UDPSend _instance;
    public static UDPSend GetInstance()
    {
        return _instance;
    }
    
    [SerializeField] private string m_ip;
    [SerializeField] int m_port;
    [SerializeField] static float m_sendLogDelay = 0.3f;

    IPEndPoint remoteEndPoint;
    UdpClient client;
    bool sendingLog = false;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this);
    }

    public void Start()
    {
        sendingLog = false;
        m_port = ConfigManager.GetInstance().GetValue("UDP_SEND_PORT");
        m_sendLogDelay = ConfigManager.GetInstance().GetFloatValue("SEND_LOG_DELAY");
        client = new UdpClient();
    }

    public void SetPort(int p_port)
    {
        m_port = p_port;
    }

    // Send Data
    public void SendUDPMsg(string message, string p_ip = "")
    {
        //Debug.LogAssertion($"Sending Message: {message}");
        try
        {
            if (p_ip == "")
                remoteEndPoint = new IPEndPoint(IPAddress.Broadcast, m_port);
            else
                remoteEndPoint = new IPEndPoint(IPAddress.Parse(p_ip), m_port);
            byte[] data = Encoding.UTF8.GetBytes(message);
            client.Send(data, data.Length, remoteEndPoint);
            Debug.LogAssertion($"Details: port {m_port} ip{p_ip ?? null: empty} " +
                $"\n Sent Message : {message}");
        }
        catch (Exception err)
        {
            Debug.LogAssertion($"Data not Sent: {message}");
            Debug.LogAssertion(err.ToString());
        }
    }

    public void SendUDPMsg(string message, int p_port, string p_ip = "")
    {
        Debug.LogAssertion($"Sending Message: {message}");
        try
        {
            if (p_ip == "")
                remoteEndPoint = new IPEndPoint(IPAddress.Broadcast, p_port);
            else
                remoteEndPoint = new IPEndPoint(IPAddress.Parse(p_ip), p_port);
            Debug.LogAssertion($"Details: port {p_port} ip{p_ip ?? null: empty} \n Message : {message}");
            byte[] data = Encoding.UTF8.GetBytes(message);
            client.Send(data, data.Length, remoteEndPoint);
        }
        catch (Exception err)
        {
            Debug.LogAssertion("Data not Sent");
            print(err.ToString());
        }
    }

    public void DelayedSendUDP(string p_sendData)
    {
        StartCoroutine(DelayedSendLog(p_sendData));
    }

    public IEnumerator DelayedSendLog(string p_message)
    {
        sendingLog = true;
        yield return new WaitForSeconds(m_sendLogDelay);
        SendUDPMsg(p_message);
        sendingLog = false;
    }
}