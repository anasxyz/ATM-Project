/*
 * TEAM 4:
 * Ahmed Youssef (2507690)
 * Ramin Hashemi (2508573)
 * Anas Saad (2510059)
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ATM
{
    public partial class MainForm : Form
    {
        private int numberOfATMs = 1; // default number of ATMs
        private bool raceCondition = true; // default race condition setting

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // sets the number of ATMs to the value of the text of the button sender
        // more efficient than hardcoding differen functions for each button
        private void btn_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            numberOfATMs = Int32.Parse(button.Text);

            lblNoATMs.Text = "Number of ATMs: " + numberOfATMs;
        }

        // switches the race condition setting on/off
        private void switchRaceCondition(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (button.Text == "On") raceCondition = true;
            else raceCondition = false;

            lblRaceCondition.Text = "Race Condition: " + button.Text;
        }

        // starts a new thread and opens the ATM form on the new thread
        private void btnStart_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < numberOfATMs; i++)
            {
                // create new thread
                Thread newThread = new Thread(() =>
                {
                    runNewATM(); // runs the function that opens a new form on that thread
                });

                newThread.Start(); // starts the thread
            }
        }

        // opens new ATM form
        private void runNewATM()
        {
            Application.Run(new ATMForm(raceCondition));
        }

        // displays the about us section
        private void btnAboutUs_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Multi-threaded, multiple ATM system designed to showcase the inconsistent balance resulting from a data race," +
                            " and the correct balance results of no data race, following a withdrawal OR a deposit.\n\n" +
                            "Created by:\n\n- Ahmed Youssef\n- Ramin Hashemi\n- Anas Saad", "About Us");
        }

        // button that allows access to the logs
        // hides and shows the necessary elements for the logs UI
        private void btnLogs_Click(object sender, EventArgs e)
        {
            listLogs.Visible = true;
            btnBack.Visible = true;

            lblTitle.Text = "               Logs";
            lblNoATMs.Visible = false;
            lblRaceCondition.Visible = false;

            btnAboutUs.Visible = false;
            btnLogs.Visible = false;
            btnStart.Visible = false;

            readFromFile();
        }

        // button that backs from the logs area into the main screen
        // hids the logs and shows the main screen
        private void btnBack_Click(object sender, EventArgs e)
        {
            listLogs.Visible = false;
            btnBack.Visible = false;

            lblTitle.Text = "Central Bank System";
            lblNoATMs.Visible = true;
            lblRaceCondition.Visible = true;

            btnAboutUs.Visible = true;
            btnLogs.Visible = true;
            btnStart.Visible = true;
        }

        // reads logs from file
        private void readFromFile()
        {
            listLogs.Items.Clear(); // clear the listview before adding new logs

            string[] logs = File.ReadAllLines(@"../../logs.txt");

            foreach (string log in logs)
            {
                ListViewItem item = new ListViewItem(log);
                listLogs.Items.Add(item);
            }
        }
    }
}
