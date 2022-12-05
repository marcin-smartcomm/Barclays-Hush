using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BarclaysMain
{
    public class CorioMAsterChangeEventArgs: EventArgs
    {
        public string change { get; set; }
    }

    public class CorioMaster
    {
        public ControlSystem cs;
        AsyncTCPClient comms;

        string[] inputCards;
        string[] outputCards;

        public delegate void CorioMAsterChangeEventHandler(object source, CorioMAsterChangeEventArgs args);
        public event CorioMAsterChangeEventHandler CorioMAsterChanged;

        public CorioMaster(string ipAddr, int port, ControlSystem contsys)
        {
            try
            {
                cs = contsys;
                comms = new AsyncTCPClient(cs, "192.168.1.10", 10001, 2000);
                comms.MessageReceived += OnMessageReceived;

                inputCards = new string[3];
                inputCards[0] = "Slot5";
                inputCards[1] = "Slot8";
                inputCards[2] = "Slot11";

                outputCards = new string[2];
                outputCards[0] = "Slot12";
                outputCards[0] = "Slot15";
            }
            catch (Exception ex)
            {
                cs.logger.WriteLine("Problem in CorioMaster Constructor " + ex.ToString());
            }
        }

        public void Connect()
        {
            comms.Connect();
        }

        public void Disconnect()
        {
            comms.Disconnect();
        }

        public void DisconnectOnly()
        {
            comms.DisconnectOnly();
        }

        public void RouteInputToOutput(int source, int LEDWall)
        {
            string inputSlot;

            if (source < 5)
            {
                inputSlot = inputCards[0];
                comms.SendMessage("Window" + LEDWall.ToString() + ".Input = " + inputSlot + ".In" + source.ToString() + "\x0D");
            }
            else if (source > 4 && source < 9)
            {
                source = source - 4;
                inputSlot = inputCards[1];
                comms.SendMessage("Window" + LEDWall.ToString() + ".Input = " + inputSlot + ".In" + source.ToString() + "\x0D");
            }
            else if (source == 9)
            {
                source = source - 8;
                inputSlot = inputCards[2];
                comms.SendMessage("Window" + LEDWall.ToString() + ".Input = " + inputSlot + ".In" + source.ToString() + "\x0D");
            }
        }

        public void OutputPower(int output, string powerState)
        {
            if(output == 1)
                comms.SendMessage("Slot12.Out1.CutToBlack="+powerState+"\x0D");
            if (output == 2)
                comms.SendMessage("Slot12.Out2.CutToBlack=" + powerState + "\x0D");
            if (output == 3)
                comms.SendMessage("Slot12.Out3.CutToBlack=" + powerState + "\x0D");
            if (output == 4)
                comms.SendMessage("Slot12.Out4.CutToBlack=" + powerState + "\x0D");
            if (output == 5)
                comms.SendMessage("Slot12.Out5.CutToBlack=" + powerState + "\x0D");
        }

        public bool GetConnectionState()
        {
            return comms.GetConnectionStatus();
        }

        string GetOutput(string textToProcess)
        {
            if (textToProcess.Contains("Window1"))
            {
                return "1";
            }
            else if (textToProcess.Contains("Window2"))
            {
                return "2";
            }
            else if (textToProcess.Contains("Window3"))
            {
                return "3";
            }
            else if (textToProcess.Contains("Window4"))
            {
                return "4";
            }
            else if (textToProcess.Contains("Window5"))
            {
                return "5";
            }
            else { return ""; }
        }
        string GetInput(string textToProcess)
        {
            if (textToProcess.Contains("Slot5"))
            {
                if (textToProcess.Contains("In1"))
                {
                    return "1";
                }
                else if (textToProcess.Contains("In2"))
                {
                    return "2";
                }
                else if (textToProcess.Contains("In3"))
                {
                    return "3";
                }
                else if (textToProcess.Contains("In4"))
                {
                    return "4";
                }
            }
            else if (textToProcess.Contains("Slot8"))
            {
                if (textToProcess.Contains("In1"))
                {
                    return "5";
                }
                else if (textToProcess.Contains("In2"))
                {
                    return "6";
                }
                else if (textToProcess.Contains("In3"))
                {
                    return "7";
                }
                else if (textToProcess.Contains("In4"))
                {
                    return "8";
                }
            }
            else if (textToProcess.Contains("Slot11"))
            {
                if (textToProcess.Contains("In1"))
                {
                    return "9";
                }
            }

            return "";
        }

        protected virtual void OnCorioMasterChangeReceived(string change)
        {
            if (CorioMAsterChanged != null)
                CorioMAsterChanged(this, new CorioMAsterChangeEventArgs() { change = change });
        }

        public void OnMessageReceived(object source, MessageReceivedEventArgs e)
        {
            string textToProcess = Encoding.ASCII.GetString(e.message);

            if (textToProcess.Contains("!Done"))
            {
                textToProcess = textToProcess.Replace("!Done ", "");

                if (textToProcess.Contains("Window"))
                {
                    string output = GetOutput(textToProcess);
                    string input = GetInput(textToProcess);

                    OnCorioMasterChangeReceived("VideoMatrix:ChangeInput:" + input + ":" + output);
                }
            }
            else if (textToProcess.Contains("Please login"))
            {
                comms.SendMessage("login(smartcomm,5m@rtc0mm)\x0D");
            }
        }
    }
}
