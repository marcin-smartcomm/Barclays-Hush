using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BarclaysMain
{
    public class MessageReceivedEventArgs: EventArgs
    {
        public byte[] message { get; set; }
    }

    public class AsyncTCPClient
    {
        ControlSystem cs;

        Socket _clientSocket;
        string _IPADDRESS;
        int _PORT;
        byte[] _buffer;
        int _bufferSize;

        public delegate void MessageReceivedEventHandler(object source, MessageReceivedEventArgs args);
        public event MessageReceivedEventHandler MessageReceived;

        public AsyncTCPClient(ControlSystem controlSys, string IPAddr, int port, int bufferSize)
        {
            cs = controlSys;
            _IPADDRESS = IPAddr;
            _PORT = port;
            _bufferSize = bufferSize;
        }

        public void SendMessage(string message)
        {
            try
            {
                byte[] buffer = Encoding.ASCII.GetBytes(message);
                _clientSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
            }
            catch (SocketException) { }
            catch (Exception ex)
            {
                cs.logger.WriteLine("Exception in CorioMAster.SendMessage() " + ex.ToString());
            }
        }

        void SendCallback(IAsyncResult AR)
        {
            try
            {
                _clientSocket.EndSend(AR);
            }
            catch (Exception ex)
            {
                cs.logger.WriteLine("Exception in CorioMAster.SendCallback() " + ex.ToString());
            }
        }

        public void ReceiveCallback(IAsyncResult AR)
        {
            try
            {
                int received = _clientSocket.EndReceive(AR);
                Array.Resize(ref _buffer, received);
                string text = Encoding.ASCII.GetString(_buffer);

                OnMessageReceived(_buffer);

                Array.Resize(ref _buffer, _bufferSize);
                _clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
            }
            catch (ObjectDisposedException) { cs.logger.WriteLine("TCP Client Socket for: " + _IPADDRESS + " Disposed"); }
            catch (Exception ex)
            {
                cs.logger.WriteLine("Exception in CorioMAster.ReceiveCallback() " + ex.ToString());
            }
        }

        public void Connect()
        {
            try
            {
                IPHostEntry connectToAddress = Dns.GetHostEntry(_IPADDRESS);
                _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _clientSocket.BeginConnect(new IPEndPoint(connectToAddress.AddressList[0], _PORT), new AsyncCallback(ConnectCallback), null);
                _buffer = new byte[_bufferSize];
                _clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
            }
            catch (Exception ex)
            {
                cs.logger.WriteLine("Exception in CorioMAster.Connect() " + ex.ToString());
            }
        }

        void ConnectCallback(IAsyncResult AR)
        {
            try
            {
                _clientSocket.EndConnect(AR);
            }
            catch (Exception ex)
            {
                cs.logger.WriteLine("Exception in CorioMAster.ConnectCallback() " + ex.ToString());
            }
        }

        public void Disconnect()
        {
            if (_clientSocket == null)
                return;

            if (_clientSocket.Connected)
            {
                _clientSocket.Dispose();
                cs.logger.WriteLine("TCP Client Socket for: " + _IPADDRESS + " Disposed");
                _clientSocket = null;
            }
        }

        public void DisconnectOnly()
        {
            _clientSocket.Disconnect(false);
        }

        public bool GetConnectionStatus()
        {
            return _clientSocket.Connected;
        }

        protected virtual void OnMessageReceived(byte[] message)
        {
            if (MessageReceived != null)
                MessageReceived(this, new MessageReceivedEventArgs() { message = message });
        }
    }
}
