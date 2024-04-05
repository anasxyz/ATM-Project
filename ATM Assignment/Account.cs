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

namespace ATM
{
    class Account
    {
        // account attributes
        private int accountNumber; // holds account number
        private int accountBalance; // holds account balance
        private int accountPIN; // holds account PIN

        // account object constructor
        public Account(int accountBalance, int accountPIN, int accountNumber)
        {
            this.accountBalance = accountBalance;
            this.accountPIN = accountPIN;
            this.accountNumber = accountNumber;
        }

        // getter methods for the attributes
        public int getAccountNumber()
        {
            return accountNumber;
        }

        public int getBalance()
        {
            return accountBalance;
        }

        public int getPIN()
        {
            return accountPIN;
        }

        // setter method for balance
        public void setBalance(int newBalance)
        {
            accountBalance = newBalance;
        }

        // method that withdraws money from the accounts balance
        public bool withdraw(int amount)
        {
            if (this.accountBalance >= amount)
            {
                accountBalance -= amount;
                return true;
            }
            else
            {
                MessageBox.Show("Balance not enough", "Error");
                return false;
            }
        }

        // method that deposits money in the accounts balance
        public void deposit(int amount)
        {
            this.accountBalance += amount;
        }
    }
}
