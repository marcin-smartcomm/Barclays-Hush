using System;
using System.IO;

namespace BarclaysMain
{
    public class FileOperations
    {
        public ControlSystem _controlSystem;
        public FileOperations(ControlSystem cs)
        {
            _controlSystem = cs;
        }

        public int GetUserPINFromFile()
        {
            try
            {
                StreamReader sr = new StreamReader("../Nvram/UserPIN.txt");

                string userPIN = sr.ReadToEnd();
                sr.Close();

                return int.Parse(userPIN);
            }
            catch (Exception ex)
            {
                _controlSystem.logger.WriteLine("issue in fileManager.GetUserPINFromFile\n" + ex.ToString());
                return 0;
            }
        }

        public void ChangePIN(string newPIN)
        {
            File.Delete("../Nvram/UserPIN.txt");
            File.WriteAllText("../Nvram/UserPIN.txt", newPIN);
        }

        public string LoadSchedulerData()
        {
            try
            {
                StreamReader sr = new StreamReader("../Nvram/Scheduler.txt");

                string schedulerData = sr.ReadToEnd();
                sr.Close();

                return schedulerData;
            }
            catch (Exception ex)
            {
                _controlSystem.logger.WriteLine("issue in fileManager.LoadSchedulerData\n" + ex.ToString());
                return "";
            }
        }

        public void WriteSchedulerData(string daysData)
        {
            File.Delete("../Nvram/Scheduler.txt");
            File.WriteAllText("../Nvram/Scheduler.txt", daysData);
        }
    }
}
