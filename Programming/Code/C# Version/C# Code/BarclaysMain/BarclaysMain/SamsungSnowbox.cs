using System;

namespace BarclaysMain
{
    public class SnowboxChangeEventArgs : EventArgs
    {
        public string change { get; set; }
    }
    public class SamsungSnowbox
    {
        ControlSystem cs;

        bool ipControl;

        //IP Control Variables
        AsyncTCPClient IPComms;
        string IPADDRESS;
        int PORT;

        //General Variables
        int boxID;
        int dataLength;
        int commandByte;
        char boxIDChr;
        char dataLengthByteChr;
        int currentBrightness;

        public delegate void SnowboxChangeEventHandler(object source, SnowboxChangeEventArgs args);
        public event SnowboxChangeEventHandler SnowboxChanged;

        public SamsungSnowbox(string ipAddr, int port, int boxID,  ControlSystem cont)
        {
            ipControl = true;

            cs = cont;
            IPADDRESS = ipAddr;
            PORT = port;
            this.boxID = boxID;
            currentBrightness = 0;
            IPComms = new AsyncTCPClient(cs, IPADDRESS, PORT, 100);

            boxIDChr = (char)boxID;
            dataLength = 1;
            dataLengthByteChr = (char)dataLength;
            commandByte = 55;

            IPComms.MessageReceived += OnMessageReceived;
        }

        public void Connect()
        {
            if(ipControl)
                IPComms.Connect();
        }

        public void Disconnect()
        {
            if (ipControl)
                IPComms.Disconnect();
        }

        void ChangeBrightness(int level)
        {
            int sum;

            char commandByteChr = (char)commandByte;
            char brightnessChr = (char)level;

            sum = commandByte + boxID + dataLength + level;
            ushort lowSum = (ushort)sum;
            byte lower = (byte)(lowSum & 0xff);
            char checkSum = (char)lower;

            string command = "\xAA" + commandByteChr + boxIDChr + dataLengthByteChr + brightnessChr + checkSum;
            cs.logger.WriteLine(command);
            IPComms.SendMessage(command);
        }

        public void BrightnessUp()
        {
            if(IPComms.GetConnectionStatus())
            {
                if (currentBrightness < 95)
                    ChangeBrightness(currentBrightness + 5);
                else
                    ChangeBrightness(100);
            }
        }

        public void BrightnessDown()
        {
            if (IPComms.GetConnectionStatus())
            {
                if (currentBrightness > 5)
                    ChangeBrightness(currentBrightness - 5);
                else
                    ChangeBrightness(0);
            }
        }

        public void OnMessageReceived(object source, MessageReceivedEventArgs args)
        {
            byte commandByte = 0x37;
            byte[] toCheck = { 0xAA, 0xFF, (byte)boxID, 0x03, 0x41, commandByte };

            if(toCheck[0] == args.message[0] && toCheck[1] == args.message[1] && toCheck[2] == args.message[2] && toCheck[3] == args.message[3] && toCheck[4] == args.message[4] && toCheck[5] == args.message[5])
            {
                cs.logger.WriteLine("New Brightness for for box ID "+boxID+": "+ args.message[6]);
                currentBrightness = args.message[6];
                boxID = boxIDChr;

                OnSnowboxChangeReceived("VideoMatrix:Brightness:" + boxID.ToString() + ":" + currentBrightness.ToString());
            }
        }

        protected virtual void OnSnowboxChangeReceived(string change)
        {
            if (SnowboxChanged != null)
                SnowboxChanged(this, new SnowboxChangeEventArgs() { change = change });
        }
    }
}
