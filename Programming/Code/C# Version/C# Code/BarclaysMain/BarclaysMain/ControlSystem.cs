using System;
using Crestron.SimplSharp;                          	// For Basic SIMPL# Classes
using Crestron.SimplSharpPro;                       	// For Basic SIMPL#Pro classes
using Crestron.SimplSharpPro.CrestronThread;        	// For Threading
using Crestron.SimplSharpPro.Diagnostics;		    	// For System Monitor Access
using Crestron.SimplSharpPro.DeviceSupport;         	// For Generic Device Support
using Crestron.SimplSharpPro.EthernetCommunication;
using System.Text;

namespace BarclaysMain
{
    public class ControlSystem : CrestronControlSystem
    {
        public ConsoleLogger logger;
        public Touchpannel[] tp;
        public SecurityCheck securityCheck;
        public FileOperations fileOps;
        public Scheduler scheduler;
        public CorioMaster corioMaster;
        public SamsungSnowbox Level7Wall;
        public SamsungSnowbox Level8Wall;
        public SamsungSnowbox Level9Wall;
        public SamsungSnowbox Level10Wall;
        public AsyncTCPServer ServerForSIMPL;

        public ControlSystem()
            : base()
        {
            try
            {
                Thread.MaxNumberOfUserThreads = 20;

                //Subscribe to the controller events (System, Program, and Ethernet)
                CrestronEnvironment.SystemEventHandler += new SystemEventHandler(_ControllerSystemEventHandler);
                CrestronEnvironment.ProgramStatusEventHandler += new ProgramStatusEventHandler(_ControllerProgramEventHandler);
                CrestronEnvironment.EthernetEventHandler += new EthernetEventHandler(_ControllerEthernetEventHandler);

                if (this.SupportsEthernet)
                {
                    logger = new ConsoleLogger(55555);

                    const int TOUCHPANNEL_COUNT = 2;
                    const ushort TOUCHPANNEL_START_PORT = 50000;

                    fileOps = new FileOperations(this);
                    securityCheck = new SecurityCheck(fileOps, logger);

                    corioMaster = new CorioMaster("192.168.1.10", 10001, this);
                    Level7Wall = new SamsungSnowbox("10.10.10.33", 1515, 1, this);
                    Level8Wall = new SamsungSnowbox("10.10.10.32", 1515, 2, this);
                    Level9Wall = new SamsungSnowbox("10.10.10.31", 1515, 3, this);
                    Level10Wall = new SamsungSnowbox("10.10.10.30", 1515, 4, this);
                    scheduler = new Scheduler(new string[] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" }, corioMaster, fileOps);


                    tp = new Touchpannel[TOUCHPANNEL_COUNT];

                    for (int i = 0; i < TOUCHPANNEL_COUNT; i++)
                    {
                        tp[i] = new Touchpannel(TOUCHPANNEL_START_PORT + i, securityCheck, scheduler, corioMaster, new SamsungSnowbox[] { Level7Wall, Level8Wall, Level9Wall, Level10Wall });
                        tp[i].cs = this;
                    }

                    ServerForSIMPL = new AsyncTCPServer(55554, this);
                    ServerForSIMPL.MessageReceived += OnMessageReceived;
                    ServerForSIMPL.ClientConnected += OnClientConnected;
                    scheduler.SchedulerDataChanged += OnSchedulerDataChanged;
                    corioMaster.CorioMAsterChanged += OnCorioMasterChangeReceived;
                }
            }
            catch (Exception e)
            {
                ErrorLog.Error("Error in the constructor: {0}", e.Message);
            }
        }

        void EvaluateMessage(string incomingRequest)
        {
            string[] requestSorted = incomingRequest.Split(':');
            try
            {
                if (requestSorted[0].Equals("Login"))
                {
                    if (securityCheck.EvaluatePIN(int.Parse(requestSorted[1])))
                    {
                        ServerForSIMPL.SendMessage("Login:Success");
                    }
                    else
                    {
                        ServerForSIMPL.SendMessage("Login:Failed");
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
                    else if (requestSorted[1] == "OnTime")
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
                    if (requestSorted[1] == "Connect")
                    {
                        corioMaster.Connect();
                        Level7Wall.Connect();
                        Level8Wall.Connect();
                        Level9Wall.Connect();
                        Level10Wall.Connect();
                    }

                    else if (requestSorted[1] == "ChangeInput")
                    {
                        corioMaster.RouteInputToOutput(int.Parse(requestSorted[2]), int.Parse(requestSorted[3]));
                    }
                    else if (requestSorted[1] == "Brightness")
                    {
                        if (requestSorted[3] == "Up")
                        {
                            if(int.Parse(requestSorted[2]) - 1 == 0)
                            {
                                Level7Wall.BrightnessUp();
                            }
                            if (int.Parse(requestSorted[2]) - 1 == 1)
                            {
                                Level8Wall.BrightnessUp();
                            }
                            if (int.Parse(requestSorted[2]) - 1 == 2)
                            {
                                Level9Wall.BrightnessUp();
                            }
                            if (int.Parse(requestSorted[2]) - 1 == 3)
                            {
                                Level10Wall.BrightnessUp();
                            }
                        }
                        if (requestSorted[3] == "Down")
                        {
                            if (int.Parse(requestSorted[2]) - 1 == 0)
                            {
                                Level7Wall.BrightnessDown();
                            }
                            if (int.Parse(requestSorted[2]) - 1 == 1)
                            {
                                Level8Wall.BrightnessDown();
                            }
                            if (int.Parse(requestSorted[2]) - 1 == 2)
                            {
                                Level9Wall.BrightnessDown();
                            }
                            if (int.Parse(requestSorted[2]) - 1 == 3)
                            {
                                Level10Wall.BrightnessDown();
                            }
                        }
                    }

                    else if (requestSorted[1] == "Disconnect")
                    {
                        corioMaster.Disconnect();
                    }
                }
            }
            catch (Exception e)
            {
                ErrorLog.Error("Error in ControlSystem.EvaluateMessage(): {0}", e.Message);
            }
        }

        void OnMessageReceived(object source, MessageReceivedEventArgs args)
        {
            EvaluateMessage(Encoding.ASCII.GetString(args.message));
        }

        void OnClientConnected(object source, EventArgs args)
        {
            ServerForSIMPL.SendMessage(scheduler.GetSchedulerData());
        }

        public void OnSchedulerDataChanged(object source, EventArgs args)
        {
            ServerForSIMPL.SendMessage(scheduler.GetSchedulerData());
        }

        public void OnCorioMasterChangeReceived(object source, CorioMAsterChangeEventArgs args)
        {
            ServerForSIMPL.SendMessage(args.change);
        }


        /// <summary>
        /// InitializeSystem - this method gets called after the constructor 
        /// has finished. 
        /// 
        /// Use InitializeSystem to:
        /// * Start threads
        /// * Configure ports, such as serial and verisports
        /// * Start and initialize socket connections
        /// Send initial device configurations
        /// 
        /// Please be aware that InitializeSystem needs to exit quickly also; 
        /// if it doesn't exit in time, the SIMPL#Pro program will exit.
        /// </summary>
        public override void InitializeSystem()
        {
            try
            {
                for (int i = 0; i < tp.Length - 1; i++)
                {
                    tp[i].Start();
                }
                logger.Start();
            }
            catch (Exception e)
            {
                logger.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// Event Handler for Ethernet events: Link Up and Link Down. 
        /// Use these events to close / re-open sockets, etc. 
        /// </summary>
        /// <param name="ethernetEventArgs">This parameter holds the values 
        /// such as whether it's a Link Up or Link Down event. It will also indicate 
        /// wich Ethernet adapter this event belongs to.
        /// </param>
        void _ControllerEthernetEventHandler(EthernetEventArgs ethernetEventArgs)
        {
            switch (ethernetEventArgs.EthernetEventType)
            {//Determine the event type Link Up or Link Down
                case (eEthernetEventType.LinkDown):
                    //Next need to determine which adapter the event is for. 
                    //LAN is the adapter is the port connected to external networks.
                    if (ethernetEventArgs.EthernetAdapter == EthernetAdapterType.EthernetLANAdapter)
                    {
                        //
                    }
                    break;
                case (eEthernetEventType.LinkUp):
                    if (ethernetEventArgs.EthernetAdapter == EthernetAdapterType.EthernetLANAdapter)
                    {

                    }
                    break;
            }
        }

        /// <summary>
        /// Event Handler for Programmatic events: Stop, Pause, Resume.
        /// Use this event to clean up when a program is stopping, pausing, and resuming.
        /// This event only applies to this SIMPL#Pro program, it doesn't receive events
        /// for other programs stopping
        /// </summary>
        /// <param name="programStatusEventType"></param>
        void _ControllerProgramEventHandler(eProgramStatusEventType programStatusEventType)
        {
            switch (programStatusEventType)
            {
                case (eProgramStatusEventType.Paused):
                    //The program has been paused.  Pause all user threads/timers as needed.
                    break;
                case (eProgramStatusEventType.Resumed):
                    //The program has been resumed. Resume all the user threads/timers as needed.
                    break;
                case (eProgramStatusEventType.Stopping):
                    //The program has been stopped.
                    //Close all threads. 
                    //Shutdown all Client/Servers in the system.
                    //General cleanup.
                    //Unsubscribe to all System Monitor events
                    break;
            }

        }

        /// <summary>
        /// Event Handler for system events, Disk Inserted/Ejected, and Reboot
        /// Use this event to clean up when someone types in reboot, or when your SD /USB
        /// removable media is ejected / re-inserted.
        /// </summary>
        /// <param name="systemEventType"></param>
        void _ControllerSystemEventHandler(eSystemEventType systemEventType)
        {
            switch (systemEventType)
            {
                case (eSystemEventType.DiskInserted):
                    //Removable media was detected on the system
                    break;
                case (eSystemEventType.DiskRemoved):
                    //Removable media was detached from the system
                    break;
                case (eSystemEventType.Rebooting):
                    //The system is rebooting. 
                    //Very limited time to preform clean up and save any settings to disk.
                    break;
            }

        }
    }
}