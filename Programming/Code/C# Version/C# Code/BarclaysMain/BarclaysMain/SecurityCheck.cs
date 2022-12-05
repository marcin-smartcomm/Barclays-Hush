using System;

namespace BarclaysMain
{
    public class SecurityCheck
    {
        FileOperations fileOps;
        ConsoleLogger cl;
        int PINMaster, PINUser;

        public SecurityCheck(FileOperations fo, ConsoleLogger cl)
        {
            this.cl = cl;
            fileOps = fo;
            PINMaster = 4719;
        }

        public bool EvaluatePIN(int PINEntered)
        {
            try
            {
                PINUser = fileOps.GetUserPINFromFile();
                if (PINEntered == PINMaster || PINEntered == PINUser)
                    return true;
                else
                    return false;
            }
            catch(Exception ex)
            {
                cl.WriteLine(ex.ToString());
                return false;
            }
        }

        public void ChangePIN(string newPIN)
        {
            fileOps.ChangePIN(newPIN);
        }
    }
}
