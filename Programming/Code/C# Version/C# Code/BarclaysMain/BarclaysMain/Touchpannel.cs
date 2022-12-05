using Crestron.SimplSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using WebsocketServer;

namespace BarclaysMain
{

    public class Touchpannel
    {
        public ControlSystem cs;
        SecurityCheck securityCheck;
        Scheduler scheduler;
        CorioMaster corioMaster;
        SamsungSnowbox[] videoWalls;

        private static Timer aTimer;

        private WebsocketSrvr CommsServer;
        private bool _clientConnected;

        private List<string> _backlog;
        bool isPinging = false;

        public Touchpannel(int port, SecurityCheck sc, Scheduler sch, CorioMaster vm, SamsungSnowbox[] vws)
        {
            try
            {
                securityCheck = sc;
                scheduler = sch;
                corioMaster = vm;
                videoWalls = vws;

                CommsServer = new WebsocketSrvr();
                CommsServer.Initialize(port);
                CommsServer.OnClientConnectedChange += OnClientConnected;
                CommsServer.OnStringSignalChange += OnReceivingMessage;

                _backlog = new List<string>();

                _clientConnected = false;

                aTimer = new Timer();
                aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                aTimer.Interval = 59000;
                aTimer.Enabled = true;


                corioMaster.CorioMAsterChanged += OnCorioMasterChangeReceived;
                videoWalls[0].SnowboxChanged += OnSnowboxChangeReceived;
                videoWalls[1].SnowboxChanged += OnSnowboxChangeReceived;
                videoWalls[2].SnowboxChanged += OnSnowboxChangeReceived;
                videoWalls[3].SnowboxChanged += OnSnowboxChangeReceived;
                scheduler.SchedulerDataChanged += OnSchedulerDataChanged;
            }
            catch (Exception e)
            {
                cs.logger.WriteLine("TP Constructor issue: \n" + e.ToString());
            }
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (!isPinging)
            {
                Stop();
                Start();
            }
            isPinging = false;
        }

        public void Start()
        {
            CommsServer.StartServer();
        }

        public void Stop()
        {
            CommsServer.StopServer();
        }

        public void WriteLine(string msg, params object[] args)
        {
            var text = String.Format(msg, args) + "\n";

            if (_clientConnected)
            {
                CommsServer.SetIndirectTextSignal(1, text);
            }
            else
            {
                _backlog.Add(text);
            }
        }

        private void OnClientConnected(ushort state)
        {
            if (state == 0)
            {
                // Disconnected
                _clientConnected = false;
            }
            else
            {
                // Connected
                _clientConnected = true;
                CommsServer.SetIndirectTextSignal(1, "\n-- CONNECTED --\n");
                CommsServer.SetIndirectTextSignal(1, "DaysData " + scheduler.GetSchedulerData());

                if (_backlog.Count > 0)
                {
                    foreach (var msg in _backlog)
                    {
                        CommsServer.SetIndirectTextSignal(1, msg);
                    }
                }

                _backlog.Clear();
            }
        }

        private void OnReceivingMessage(ushort state, SimplSharpString value)
        {
            cs.logger.WriteLine(value.ToString());
            if (value.ToString() == "__ping__")
            {
                isPinging = true;
                // _logger.WriteLine("panel is pinging server, isPinging = "+isPinging.ToString());
                CommsServer.SetIndirectTextSignal(1, "__pong__");
            }
            else
            {
                evaluateString(value.ToString());
            }
        }

        public void evaluateString(string incomingRequest)
        {
            string[] requestSorted = incomingRequest.Split(':');
            try
            {
                if (requestSorted[0].Equals("Login"))
                {
                    if (securityCheck.EvaluatePIN(int.Parse(requestSorted[1])))
                    {
                        CommsServer.SetIndirectTextSignal(1, "Login:Success");
                    }
                    else
                    {
                        CommsServer.SetIndirectTextSignal(1, "Login:Failed");
                    }
                }
                else if (requestSorted[0].Equals("PINChange"))
                {
                    securityCheck.ChangePIN(requestSorted[1]);
                }
                else if (requestSorted[0] == "Scheduler")
                {
                    int dayIndex = int.Parse(requestSorted[2]);

                    if (requestSorted[1] == "DayState")
                    {
                        bool dayState;

                        if (requestSorted[3] == "true")
                            dayState = true;
                        else
                            dayState = false;

                        scheduler.ChangeDayState(dayIndex, dayState);
                    }
                    else if(requestSorted[1] == "OnTime")
                    {
                        int onTime = int.Parse(requestSorted[3]);
                        scheduler.ChangeDayOnTime(dayIndex, onTime);
                    }
                    else if (requestSorted[1] == "OffTime")
                    {
                        int offTime = int.Parse(requestSorted[3]);
                        scheduler.ChangeDayOffTime(dayIndex, offTime);
                    }
                }
                else if (requestSorted[0] == "VideoMatrix")
                {
                    if(requestSorted[1] == "Connect")
                    {
                        corioMaster.Connect();
                        videoWalls[0].Connect();
                        videoWalls[1].Connect();
                        videoWalls[2].Connect();
                        videoWalls[3].Connect();
                    }

                    else if(requestSorted[1] == "ChangeInput")
                    {
                        corioMaster.RouteInputToOutput(int.Parse(requestSorted[2]), int.Parse(requestSorted[3]));
                    }
                    else if (requestSorted[1] == "Brightness")
                    {
                        if(requestSorted[3] == "Up")
                            videoWalls[int.Parse(requestSorted[2]) - 1].BrightnessUp();
                        if (requestSorted[3] == "Down")
                            videoWalls[int.Parse(requestSorted[2]) - 1].BrightnessDown();
                    }
                    else if(requestSorted[1] == "BlankOutputs")
                    {
                        corioMaster.OutputPower(1, "Off");
                        corioMaster.OutputPower(2, "Off");
                        corioMaster.OutputPower(3, "Off");
                        corioMaster.OutputPower(4, "Off");
                        corioMaster.OutputPower(5, "Off");
                    }

                    else if (requestSorted[1] == "Disconnect")
                    {
                        corioMaster.Disconnect();
                    }
                }
            }catch(Exception ex)
            {
                cs.logger.WriteLine(ex.ToString());
            }
        }

        public void OnCorioMasterChangeReceived(object source, CorioMAsterChangeEventArgs args)
        {
            CommsServer.SetIndirectTextSignal(1, args.change);
        }

        public void OnSnowboxChangeReceived(object source, SnowboxChangeEventArgs args)
        {
            CommsServer.SetIndirectTextSignal(1, args.change);
        }
        
        public void OnSchedulerDataChanged(object source, EventArgs args)
        {
            CommsServer.SetIndirectTextSignal(1, "DaysData " + scheduler.GetSchedulerData());
        }
    }
}
