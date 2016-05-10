using UnityEngine;
using System.Text;
using System.Net;
using System.Net.Sockets;

class SocketClient
{
    static string ip;
    static int port;
    static Socket clientSocket;
    private static byte[] result = new byte[1024];
    static public void setServer(string ip, int port)
    {
        SocketClient.ip = ip;
        SocketClient.port = port;
    }

    static public string send(string message)
    {
        return send(SocketClient.ip, SocketClient.port, message);
    }

    static public string send(string ip, int port, string message)
    {
        if (clientSocket == null || clientSocket.Connected == false)
        {
            connectServer(ip, port);
        }
        if (clientSocket != null && clientSocket.Connected == true)
        {
            clientSocket.Send(Encoding.ASCII.GetBytes(message));
            int receiveNumber = clientSocket.Receive(result);
            return Encoding.ASCII.GetString(result, 0, receiveNumber);
        }
        return null;
    }

    static void Close()
    {
        if (clientSocket != null) 
        {
            try {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
            catch
            {
                //pass
            }
        }
    }

    static void connectServer(string ip, int port)
    {
        if (clientSocket != null && clientSocket.Connected == true)
        {
            return;
        }
        //设定服务器IP地址  
        IPAddress iPAddress = IPAddress.Parse(ip);
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            clientSocket.Connect(new IPEndPoint(iPAddress, port)); //配置服务器IP与端口  
        }
        catch
        {
            MonoBehaviour.print("连接失败！");
            Close();
        }
    }
}
