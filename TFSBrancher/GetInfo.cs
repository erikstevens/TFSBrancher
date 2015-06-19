using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSBrancher
{
    class GetInfo : IInfo
    {
        private Projects _project;
        private string ticketNumber;
        private string featureName;
        private string branchName;
        private string baseFolder;
        private string compileName;
        private string installName;
        private string trunk;

        public GetInfo()
        {
            int response = GetPath();

            switch (response)
            {
                case -1:
                    GetProjectName();
                    GetCompileName();
                    break;
                case 0:
                    GetProjectName();
                    GetTrunk();
                    GetFeatureName();
                    GetBranchName();
                    GetBaseFolder();
                    GetCompileName();
                    GetInstallName();
                    break;
                case 1 - 4:
                    _project = (Projects)response;
                    GetTicketNumber();
                    GetFeatureName();
                    branchName = "DEV-" + ticketNumber + "-" + featureName;
                    compileName = "DevCompile-" + ticketNumber + "-" + featureName;
                    installName = "DevInstall-" + ticketNumber + "-" + featureName;
                    baseFolder = @"$/" + _project + @"/Features/";
                    trunk = @"$/" + _project + @"/DEV";
                    break;
                default:
                    Console.WriteLine("Failure in selection - Try a number that is listed.");
                    System.Threading.Thread.Sleep(5000);
                    System.Environment.Exit(1);
                    break;
            }
        }

        private static int GetPath()
        {
            Console.WriteLine("Please select your path:");
            Console.WriteLine("0. Release/Custom");
            Console.WriteLine("1. Eclipse");
            Console.WriteLine("2. eCapture");
            Console.WriteLine("3. Allegro");
            Console.WriteLine("4. Nucleus");
            string x = Console.ReadLine();
            return Convert.ToInt32(x);
        }

        private void GetBranchName()
        {
            Console.WriteLine("Please enter your branch name:");
            Console.WriteLine("e.g., DEV-12345-MyNewFeature");
            branchName = Console.ReadLine();
        }

        private void GetTicketNumber()
        {
            Console.WriteLine("Please Enter the Ticket Number:");
            ticketNumber = Console.ReadLine();
        }

        private void GetFeatureName()
        {
            Console.WriteLine("Please Enter the Feature Name:");
            featureName = Console.ReadLine();
        }

        private void GetProjectName()
        {
            Console.WriteLine("Please select your project:");
            Console.WriteLine("1. Eclipse");
            Console.WriteLine("2. eCapture");
            Console.WriteLine("3. Allegro");
            Console.WriteLine("4. Nucleus");
            string x = Console.ReadLine();
            Enum.TryParse<Projects>(x, out _project);
        }

        private void GetBaseFolder()
        {
            Console.WriteLine("Please Enter the base folder path ending with a slash:");
            Console.WriteLine("(e.g., $/Eclipse/Releases/)");
            baseFolder = Console.ReadLine();
        }

        private void GetCompileName()
        {
            Console.WriteLine("Please input the Compile Build Name:");
            Console.WriteLine("e.g., 15.1.0_Compile");
            compileName = Console.ReadLine();
        }

        private void GetInstallName()
        {
            Console.WriteLine("Please input the Compile Build Name:");
            Console.WriteLine("e.g., 15.1.0_Install");
            installName = Console.ReadLine();
        }

        private void GetTrunk()
        {
            Console.WriteLine("Please enter the 'Trunk' Branch path:");
            Console.WriteLine("e.g., $/Eclipse/DEV");
            trunk = Console.ReadLine();
        }

        #region Accessors
        public string FeatureName
        {
            get
            {
                return featureName;
            }
        }

        public string BaseFolder
        {
            get
            {
                return baseFolder;
            }
        }

        public string BranchName
        {
            get
            {
                return branchName;
            }
        }

        public string CompileName
        {
            get
            {
                return compileName; 
            }
        }

        public string InstallName
        {
            get
            {
                return installName; 
            }
        }

        public string TrunkFolder
        {
            get
            {
                return trunk;
            }
        }

        public string TicketNumber
        {
            get
            {
                return ticketNumber;
            }
        }

        public string Project
        {
            get
            {
                return _project.ToString();
            }
        }
        #endregion
    }
}
