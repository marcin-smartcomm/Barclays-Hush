using Newtonsoft.Json;
using System;
using System.Timers;

namespace BarclaysMain
{
    public class Scheduler
    {
        private static Timer aTimer;
        FileOperations fileOps;
        CorioMaster videoMatrix;
        DaysData daysData;

        bool schedulerCommandSend;

        public delegate void SchedulerDataChangedEventHandler(object source, EventArgs args);
        public event SchedulerDataChangedEventHandler SchedulerDataChanged;

        public Scheduler(string[] days, CorioMaster vm, FileOperations fileOps)
        {
            schedulerCommandSend = false;
            videoMatrix = vm;
            this.fileOps = fileOps;
            string existingData = fileOps.LoadSchedulerData();
            daysData = new DaysData();

            if (existingData == "")
            {
                daysData.dayNames = new string[days.Length];
                daysData.states = new bool[days.Length];
                daysData.onTimes = new int[days.Length];
                daysData.offTimes = new int[days.Length];

                for (int i = 0; i < days.Length; i++)
                {
                    daysData.dayNames[i] = days[i];
                    daysData.states[i] = false;
                    daysData.onTimes[i] = 7;
                    daysData.offTimes[i] = 18;
                }


                SaveInFile();
            }
            else
            {
                daysData = JsonConvert.DeserializeObject<DaysData>(existingData);
            }

            aTimer = new Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            //aTimer.Interval = 60000; This is every minute
            aTimer.Interval = 1000;
            aTimer.Enabled = true;
        }

        public string ChangeDayState(int dayIndex, bool dayState)
        {
            daysData.states[dayIndex] = dayState;

            SaveInFile();

            OnSchedulerDataChanged();
            return fileOps.LoadSchedulerData();
        }

        public string ChangeDayOnTime(int dayIndex, int dayOnTime)
        {
            daysData.onTimes[dayIndex] = dayOnTime;

            SaveInFile();

            OnSchedulerDataChanged();
            return fileOps.LoadSchedulerData();
        }

        public string ChangeDayOffTime(int dayIndex, int dayOffTime)
        {
            daysData.offTimes[dayIndex] = dayOffTime;

            SaveInFile();

            OnSchedulerDataChanged();
            return fileOps.LoadSchedulerData();
        }

        void SaveInFile()
        {
            string serializedData = JsonConvert.SerializeObject(daysData);
            fileOps.WriteSchedulerData(serializedData);
        }

        public string GetSchedulerData()
        {
            return fileOps.LoadSchedulerData();
        }

        protected virtual void OnSchedulerDataChanged()
        {
            if (SchedulerDataChanged != null)
                SchedulerDataChanged(this, new EventArgs() { });
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            DateTime now = DateTime.Now;

            if (now.Minute > 0)
                return;

            if (now.Second > 10)
                return;

            if (now.Second == 10)
            {
                videoMatrix.Disconnect();
                fileOps._controlSystem.logger.WriteLine("Disconnecting from Matrix");
                schedulerCommandSend = false;
                return;
            }

            if (schedulerCommandSend)
                return;

            for (int i = 0; i < daysData.dayNames.Length; i++)
            {
                if(now.DayOfWeek.ToString().Contains(daysData.dayNames[i]))
                {
                    if(daysData.states[i] == true)
                    {
                        if (now.Hour == daysData.onTimes[i] - 1)
                        {
                            try
                            {
                                videoMatrix.GetConnectionState();

                                fileOps._controlSystem.logger.WriteLine("VideoMatrix connected, sending On Commands");
                                videoMatrix.OutputPower(1, "On");
                                videoMatrix.OutputPower(2, "On");
                                videoMatrix.OutputPower(3, "On");
                                videoMatrix.OutputPower(4, "On");
                                videoMatrix.OutputPower(5, "On");
                                schedulerCommandSend = true;
                            }
                            catch(Exception ex)
                            {
                                fileOps._controlSystem.logger.WriteLine("VideoMatrix not connected, attempting to connect before sending commands\n"+ex.Message);
                                videoMatrix.Connect();
                            }
                        }
                        if (now.Hour == daysData.offTimes[i] - 1)
                        {
                            try
                            {
                                videoMatrix.GetConnectionState();

                                fileOps._controlSystem.logger.WriteLine("VideoMatrix connected, sending Off Commands");
                                videoMatrix.OutputPower(1, "Off");
                                videoMatrix.OutputPower(2, "Off");
                                videoMatrix.OutputPower(3, "Off");
                                videoMatrix.OutputPower(4, "Off");
                                videoMatrix.OutputPower(5, "Off");
                                schedulerCommandSend = true;
                            }
                            catch (Exception ex)
                            {
                                fileOps._controlSystem.logger.WriteLine("VideoMatrix not connected, attempting to connect before sending commands\n" + ex.Message);
                                videoMatrix.Connect();
                            }
                        }
                    }
                }
            }
            
        }
    }

    public class DaysData
    {
        public string[] dayNames { get; set; }
        public bool[] states { get; set; }
        public int[] onTimes { get; set; }
        public int[] offTimes { get; set; }
    }
}
