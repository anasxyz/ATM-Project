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

namespace ATM
{
    public partial class ATMForm : Form
    {
        private bool raceCondition; // race condition setting for each ATM
        private string phase = "number"; // represents what phase the user is currently on in their use of the ATM
        private string accountNumber = ""; // account number for this ATM thread
        private string PIN = ""; // PIN for this ATM thread
        private Account activeAccount; // the active account for this ATM thread

        private int[] amounts = { 10, 20, 30, 50, 100 }; // amounts of money to be used to depositing and withdrawing

        Semaphore semaphore = new Semaphore(1, 1); // semaphore for preventing multi thread access to a shared account between two or more threads

        // constructor receives the race condition setting set in the main window as a parameter
        public ATMForm(bool raceCondition)
        {
            InitializeComponent();

            this.raceCondition = raceCondition; // sets this ATM thread's race condition to the original race condition before thread launch
        }

        // hides the button choices when the form loads, preparing for login first
        private void Form2_Load(object sender, EventArgs e)
        {
            hideChoices();
        }

        // one function for all the numpad buttons for efficiency
        private void btn_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            switch (phase)
            {
                // if user is entering account number then prevent more than 6 digits
                case "number":
                    if (lblInformation.Text.Length < 6)
                    {
                        lblInformation.Text += button.Text;
                        accountNumber += button.Text;
                    }

                    break;
                // if user is entering PIN then prevent more than 4 digits
                case "PIN":
                    if (lblInformation.Text.Length < 4)
                    {
                        lblInformation.Text += "•"; // hides PIN digits
                        PIN += button.Text;
                    }
                    break;
                default:
                    break;
            }


        }

        // very important and useful function, sets the phase of ATM use
        // this is important for buttons that have more than one function to know when to do what, depending on the phase
        //
        // receives the phase to be set as a parameter
        //
        // "number" : user is entering account number
        // "PIN" : user is entering PIN
        // "menu" : main ATM menu (deposit, balance, or withdraw)
        // "deposit" : user is in the deposit screen
        // "withdraw" : user is in the withdraw screen
        // "balance" : user is viewing their balance
        private void setPhase(string phaseToSet)
        {
            phase = phaseToSet; // sets the current phase to the desired phase

            // if phase is account number then show/hide required labels
            if (phase == "number")
            {
                lblTitle.Visible = true;
                lblInformation.Visible = true;

                hideChoices();

                lblTitle.Text = "Enter Account Number: ";
                lblInformation.Text = "";
            }
            // if phase is PIN then show/hide required labels
            else if (phase == "PIN")
            {
                lblTitle.Visible = true;
                lblInformation.Visible = true;

                hideChoices();

                lblTitle.Text = "        Enter PIN: ";
                lblInformation.Text = "";
            }
            // if phase is main ATM menu then show/hide required labels
            else if (phase == "menu")
            {
                lblTitle.Visible = false;
                lblInformation.Visible = false;

                showMenu();

                showChoices();
            }
            // if phase is either deposit/withdraw then show the money amounts the user is allowed to deposit/withdraw
            else if (phase == "deposit" || phase == "withdraw")
            {
                showAmounts(); // shows amounts of money on the ATM screen
            }
            // if phase is balance then show the user their balance and show/hide required labels
            else if (phase == "balance")
            {
                hideChoices();
                lblChoice6.Visible = true;
                lblChoice6.Text = "Back";

                lblTitle.Visible = true;
                lblInformation.Visible = true;

                lblTitle.Text = "    Your Balance is: ";
                lblInformation.Text = "£" + activeAccount.getBalance().ToString();

                // adds that the user viewed their balance to the logs
                Program.record("Account number " + activeAccount.getAccountNumber() + " viewed their balance");
            }

        }

        // hides all the side labels on the screen
        private void hideChoices()
        {
            lblChoice1.Visible = false;
            lblChoice2.Visible = false;
            lblChoice3.Visible = false;
            lblChoice4.Visible = false;
            lblChoice5.Visible = false;
            lblChoice6.Visible = false;
        }

        // shows all the side labels on the screen
        private void showChoices()
        {
            lblChoice1.Visible = true;
            lblChoice2.Visible = true;
            lblChoice3.Visible = true;
            lblChoice4.Visible = true;
            lblChoice5.Visible = true;
            lblChoice6.Visible = true;
        }

        // changes the side labels to money amounts, more efficient because it's
        // reusing the side labels instead of creating entirely new ones
        private void showAmounts()
        {
            lblChoice1.Text = "£" + amounts[0].ToString();
            lblChoice2.Text = "£" + amounts[1].ToString();
            lblChoice3.Text = "£" + amounts[2].ToString();
            lblChoice4.Text = "£" + amounts[3].ToString();
            lblChoice5.Text = "£" + amounts[4].ToString();
            lblChoice6.Text = "Back";
        }

        // changes the side label to the main menu choices, again efficient because
        // it's reusing the side labels instead of creating separate ones for choices and amounts
        private void showMenu()
        {
            lblChoice1.Text = "Deposit";
            lblChoice2.Text = "Balance";
            lblChoice3.Text = "Withdraw";
            lblChoice4.Text = "";
            lblChoice5.Text = "";
            lblChoice6.Text = "";
        }

        // searches and finds an account in the account array form a given account number
        private Account search(string accountNumber)
        {
            int searchNumber = Int32.Parse(accountNumber); // converts the given account number to an integer

            for (int i = 0; i < Program.accounts.Length; i++)
            {
                if (Program.accounts[i].getAccountNumber() == searchNumber)
                {
                    return Program.accounts[i]; // returns the account if found
                }
            }

            return null; // if account is not found then return null
        }

        // validates the users login 
        private void validateLogin()
        {
            activeAccount = search(accountNumber); // sets the current active account on this ATM thread to the account found

            // if account was found, then check PIN validity
            if (activeAccount != null)
            {
                if (validatePIN(activeAccount) == true)
                {
                    MessageBox.Show("Login successful\n\nWelcome, User " + activeAccount.getAccountNumber(), "Welcome");
                    // adds to logs that login was successful
                    Program.record("Login successful for account number " + activeAccount.getAccountNumber());
                    setPhase("menu");
                }
                else
                {
                    MessageBox.Show("Account information is incorrect", "Error");
                    // adds to logs that login was not successful
                    Program.record("PIN entered incorrectly");
                    reset();
                }
            }
            else
            {
                reset();
                MessageBox.Show("Account information is incorrect", "Error");
                // adds to logs that login was not successful
                Program.record("Account information entered incorrectly");
            }
        }

        // checks if PIN matches given account's PIN
        private bool validatePIN(Account account)
        {
            int PIN2 = Int32.Parse(PIN);

            if (PIN2 == account.getPIN()) return true;
            else return false;
        }

        // clears the text being entered on the screen, whether it be account number or PIN
        // also clears the corresponding variables for the current phase
        private void clear()
        {
            lblInformation.Text = "";

            if (phase == "number") accountNumber = "";
            else if (phase == "PIN") PIN = "";
        }

        // resets the ATM to it's default state
        private void reset()
        {
            // sets phase back to account number
            setPhase("number");

            // resets variables
            accountNumber = "";
            PIN = "";
        }

        // the clear button, clears text on the screen
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (phase == "number" || phase == "PIN") clear();
        }

        // the cancel button, takes user's card out of the machine and resets the machine
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (!(phase == "number"))
            {
                MessageBox.Show("Taking card out...", "Cancel");
                // adds to logs that user took their card out
                if (activeAccount != null)
                {
                    Program.record("Account number " + activeAccount.getAccountNumber() + " taking card out");
                }
                reset();
            }
        }

        // enter button, submits current information being entered depending on phase
        private void btnEnter_Click(object sender, EventArgs e)
        {
            switch (phase)
            {
                case "number":
                    if (!(accountNumber == ""))
                    {
                        setPhase("PIN");
                    }
                    break;
                case "PIN":
                    // doesn't submit PIN unless it's 4 digits
                    if (PIN.Length == 4)
                    {
                        validateLogin();
                    }
                    break;
                case "menu":
                    break;
                default:
                    break;
            }
        }

        // withdraw function for withdrwaing a specifiied amount passed in as a parameter
        // into the current active account
        //
        // this is the race condition implenetation for a withdraw from two accounts on
        // two different ATMs, done at the same time
        //
        // sets the active accounts balance to the stored balance added to the specified amount
        // thus negating any changes to the balance that might have happened during the thread's delay
        // and sucessfully simulating two SIMULTANEOUS withdrawals since it's impossible to do two
        // simultaneous withdrawals at the same time using a user interface
        //
        // example, if £10 is withdrawn on two ATMs, both threads sleep for 10 seconds
        // if thread 1 changes the balance first, then balance changes to £290, so when thread 2 comes to change it 
        // it should change it to £280, but the withdraw is based on the temporary balance that was stored BEFORE the thread sleep
        // so it would withdraw £10 from £300 and not £290, thus demonstrating the inconsistency in the balance as a result of a data race
        private void withdraw(int amount)
        {
            if (activeAccount.getBalance() < amount)
            {
                MessageBox.Show("Balance not enough to withdraw £" + amount, "Error");
                // adds to logs failed withdrawal attempt and amount
                Program.record(activeAccount.getAccountNumber() + "attempted to withdraw more than available balance");
            }
            else
            {
                // if account contains enough balance for withdrawal, then withdraw specified amount
                if (activeAccount.getBalance() >= amount)
                {
                    int tempBalance = activeAccount.getBalance(); // creates and stores a temporary balance

                    Thread.Sleep(10000); // thread delays for 10 seconds

                    activeAccount.setBalance(tempBalance - amount); // does actual withdrawal

                }

                MessageBox.Show("Withdrew £" + amount + " from account number " + activeAccount.getAccountNumber(), "Withdrawal");
                // adds to logs withdrawal and amount
                Program.record("Withdrew £" + amount + " from account number " + activeAccount.getAccountNumber());
            }
        }
        
        // this is the no race condition implementation of the withdraw function
        // using a semaphore to prevent more than one thread accessing the same account by
        // waiting for the semaphore to release access
        private void withdrawNoRaceCondition(int amount)
        {
            bool withdrawalCheck; // holds the value of success of the withdrawal attempt

            semaphore.WaitOne(); // locks
            Thread.Sleep(10000); // thread sleeps for 10 seconds
            withdrawalCheck = activeAccount.withdraw(amount); // does actual withdrawal attempt
            semaphore.Release(); // releases access

            // if withdrawal was successful then announce successful withdrawal
            if (withdrawalCheck == true)
            {
                MessageBox.Show("Withdrawing £" + amount + " from account number " + activeAccount.getAccountNumber(), "Withdrawal");
                // adds to logs withdrawal and amount
                Program.record("Withdrew £" + amount + " from account number " + activeAccount.getAccountNumber());
            }
            else
            {
                MessageBox.Show("Balance not enough to withdraw £" + amount, "Error");
                // adds to logs failed withdrawal attempt
                Program.record(activeAccount.getAccountNumber() + "attempted to withdraw more than available balance");
            }
        }

        // deposit function for depositing a specifiied amount passed in as a parameter
        // into the current active account
        //
        // this is the race condition simulation for a deposit by two accounts on
        // two different ATMs, done at the same time
        //
        // same explanation and example as the withdraw function above ^
        private void deposit(int amount)
        {
            int tempBalance = activeAccount.getBalance(); // creates and stores a temporary balance

            Thread.Sleep(10000); // thread delays for 10 seconds

            activeAccount.setBalance(tempBalance + amount); // does actual deposits

            // since there's no condition for the deposit's validity, it instantly announces successful deposit
            MessageBox.Show("Deposited £" + amount + " into account number " + activeAccount.getAccountNumber(), "Deposit");
            // adds to logs deposit and amount
            Program.record("Deposited £" + amount + " into account number " + activeAccount.getAccountNumber());
        }

        // this is the no race condition implementation of the deposit function
        // using a semaphore to prevent more than one thread accessing the same account by
        // waiting for the semaphore to release access
        private void despositNoRaceCondition(int amount)
        {
            semaphore.WaitOne(); // locks
            Thread.Sleep(10000); // thread sleeps for 10 seconds
            activeAccount.deposit(amount); // does actual deposit
            semaphore.Release(); // releases access

            MessageBox.Show("Deposited £" + amount + " into account number " + activeAccount.getAccountNumber(), "Deposit");
            // adds to logs deposit and amount
            Program.record("Deposited £" + amount + " into account number " + activeAccount.getAccountNumber());
        }

        // implenetaion of top left ATM button funcionality depending on current phase
        private void btnChoice1_Click(object sender, EventArgs e)
        {
            if (phase == "menu")
            {
                setPhase("deposit");
            }
            else if (phase == "deposit")
            {
                // use correct function depending on the state of the race condition setting
                if (raceCondition == true)
                {
                    deposit(amounts[0]); 
                }
                else
                {
                    despositNoRaceCondition(amounts[0]);
                }
                
                setPhase("menu");
            }
            else if (phase == "withdraw")
            {
                // use correct function depending on the state of the race condition setting
                if (raceCondition == true)
                {
                    withdraw(amounts[0]);
                } 
                else
                {
                    withdrawNoRaceCondition(amounts[0]);
                }
                
                setPhase("menu");
            }

        }

        // implenetaion of middle left ATM button funcionality depending on current phase
        private void btnChoice2_Click(object sender, EventArgs e)
        {
            if (phase == "menu")
            {
                setPhase("balance");
            }
            else if (phase == "deposit")
            {
                // use correct function depending on the state of the race condition setting
                if (raceCondition == true)
                {
                    deposit(amounts[1]);
                }
                else
                {
                    despositNoRaceCondition(amounts[1]);
                }
                setPhase("menu");
            }
            else if (phase == "withdraw")
            {
                // use correct function depending on the state of the race condition setting
                if (raceCondition == true)
                {
                    withdraw(amounts[1]);
                }
                else
                {
                    withdrawNoRaceCondition(amounts[1]);
                }
                setPhase("menu");
            }

        }

        // implenetaion of bottom left ATM button funcionality depending on current phase
        private void btnChoice3_Click(object sender, EventArgs e)
        {
            if (phase == "menu")
            {
                setPhase("withdraw");
            }
            else if (phase == "deposit")
            {
                // use correct function depending on the state of the race condition setting
                if (raceCondition == true)
                {
                    deposit(amounts[2]);
                }
                else
                {
                    despositNoRaceCondition(amounts[2]);
                }
                setPhase("menu");
            }
            else if (phase == "withdraw")
            {
                // use correct function depending on the state of the race condition setting
                if (raceCondition == true)
                {
                    withdraw(amounts[2]);
                }
                else
                {
                    withdrawNoRaceCondition(amounts[2]);
                }
                setPhase("menu");
            }
        }

        // implenetaion of top right ATM button funcionality depending on current phase
        private void btnChoice4_Click(object sender, EventArgs e)
        {
            if (phase == "deposit")
            {
                // use correct function depending on the state of the race condition setting
                if (raceCondition == true)
                {
                    deposit(amounts[3]);
                }
                else
                {
                    despositNoRaceCondition(amounts[3]);
                }
                setPhase("menu");
            }
            else if (phase == "withdraw")
            {
                // use correct function depending on the state of the race condition setting
                if (raceCondition == true)
                {
                    withdraw(amounts[3]);
                }
                else
                {
                    withdrawNoRaceCondition(amounts[3]);
                }
                setPhase("menu");
            }
        }

        // implenetaion of middle right ATM button funcionality depending on current phase
        private void btnChoice5_Click(object sender, EventArgs e)
        {
            if (phase == "deposit")
            {
                // use correct function depending on the state of the race condition setting
                if (raceCondition == true)
                {
                    deposit(amounts[4]);
                }
                else
                {
                    despositNoRaceCondition(amounts[4]);
                }
                setPhase("menu");
            }
            else if (phase == "withdraw")
            {
                // use correct function depending on the state of the race condition setting
                if (raceCondition == true)
                {
                    withdraw(amounts[4]);
                }
                else
                {
                    withdrawNoRaceCondition(amounts[4]);
                }
                setPhase("menu");
            }
        }

        // implenetaion of bottom right ATM button funcionality depending on current phase
        // also serves as a back button from all phases
        private void btnChoice6_Click(object sender, EventArgs e)
        {
            if (phase == "deposit" || phase == "withdraw" || phase == "balance")
            {
                setPhase("menu");
            }

        }
    }
}
