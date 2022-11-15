using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class CoordData : MonoBehaviour
{
    Thread receiveThread;
    UdpClient Client;
    public int Port = 5052;
    public bool StartReceiving = true;
    public bool PrintToConsole = false;
    public string data;
    // Start is called before the first frame update
    public void Start()
    {
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    // 
    private void ReceiveData()
    {
        Client = new UdpClient(Port);
        while (StartReceiving)
        {
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] dataByte = Client.Receive(ref anyIP);
                data = Encoding.UTF8.GetString(dataByte);

                if (PrintToConsole)
                {
                    print(data);
                }
            }
            catch (Exception err)
            {
                print(err.ToString());
            }
        }
    }
}
