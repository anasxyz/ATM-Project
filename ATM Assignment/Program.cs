/*
 * TEAM 4:
 * Ahmed Youssef (2507690)
 * Ramin Hashemi (2508573)
 * Anas Saad (2510059)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ATM
{
    internal static class Program
    {
        public static Account[] accounts = new Account[3]; // array that holds created accounts

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            initialiseAccounts(); // sets up all the accounts with their information
            initialiseLogs(); // sets up the logs for use

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        // creates all the needed accounts
        private static void initialiseAccounts()
        {
            accounts[0] = new Account(300, 1111, 111111); // creates new account with specified details
            accounts[1] = new Account(750, 2222, 222222); // creates new account with specified details
            accounts[2] = new Account(3000, 3333, 333333); // creates new account with specified details
        }

        // creates the logs file and sets up the logs for use
        private static void initialiseLogs()
        {
            if (File.Exists(@"../../logs.txt"))
            {
                File.WriteAllText(@"../../logs.txt", String.Empty);
            }
            else
            {
                StreamWriter sw = File.CreateText(@"../../logs.txt");
                sw.Close();
            }
        }

        // records all actions done by the user in the log area in the main window
        // all logs get logged in a text file called "logs.txt"
        public static void record(string singleLog)
        {
            using (StreamWriter sw = File.AppendText(@"../../logs.txt"))
            {
                sw.WriteLine(DateTime.Now.ToLongTimeString() + " " + singleLog); // adds date to log for extra information
                sw.Close();
            }
        }
    }
}
