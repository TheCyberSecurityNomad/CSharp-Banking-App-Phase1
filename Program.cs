

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
 

namespace BankApplication
{
    class BankApp //The Bank Parent Class
    {
        public class Bank // Main Bank Class
        {
            // List to store the accounts
            private List<Account> accounts = new List<Account>();

            // Variable to generate the next account number
            private int nextAccountNumber = 1001;

            // Counter for login attempts
            private int loginAttempts = 0;

            // Maximum allowed login attempts
            private const int maxLoginAttempts = 3;

            // Method to create a new account
            public void CreateAccount()
            {
                //New account condtructor
                Account newAccount = new Account();

                // Loop to get and validate the first name
                while (true)
                {
                    Console.Write("Enter Your First Name: "); // Printed message asking for the users first name.
                    newAccount.FirstName = Console.ReadLine(); // Save the input to a variable for newAccount.
                    if (ValidateName(newAccount.FirstName)) // Run the ValidateName function to check if the input is valid.
                        break; // If the input is valid, break from this loop.
                    else
                        Console.WriteLine("Invalid first name. Please try again."); // Printed message if input is not valid.
                }

                // Loop to get and validate the last name
                while (true)
                {
                    Console.Write("Enter Your Last Name: "); // Printed message asking for the users last name.
                    newAccount.LastName = Console.ReadLine(); // Save the input to a variable for newAccount.
                    if (ValidateName(newAccount.LastName)) // Run the ValidateName function to check if the input is valid.
                        break; // If the input is valid, break from this loop.
                    else
                        Console.WriteLine("Invalid last name. Please try again."); // Printed message if input is not valid.
                }

                // Loop to get and validate the email address
                while (true)
                {
                    Console.Write("Enter Your Email Address: "); // Printed message asking for the users Email.
                    newAccount.Email = Console.ReadLine(); // Save the input to a variable for newAccount.
                    if (ValidateEmail(newAccount.Email)) // Run the ValidateEmail function to check if the input is valid.
                        break; // If the input is valid, break from this loop.
                    else
                        Console.WriteLine("Invalid email address. Please try again."); // Printed message if input is not valid.
                }

                // Printed message asking for the users Address - Did not make a loop validating this.
                Console.Write("Enter Your Address: ");
                newAccount.Address = Console.ReadLine(); //Save the input to a variable for newAccount.


                // Loop to get and validate the date of birth
                while (true)
                {
                    Console.Write("Enter Your Date of Birth (YYYY-MM-DD): "); // Printed message asking for the users DoB.
                    newAccount.Dob = Console.ReadLine(); // Save the users input to a variable
                    if (ValidateDob(newAccount.Dob)) // Run the ValidateDOB function to check if input is valid.
                        break; // If the input is valid, break from this loop. 
                    else
                        Console.WriteLine("Invalid date of birth. Please try again."); // Printed message if input is not valid.
                }

              // Loop to get and validate the phone number
                while (true)
                {
                    Console.Write("Enter Your Phone Number: "); // Printed message asking for the users Phone Number.
                    newAccount.PhoneNumber = Console.ReadLine(); // Save the users input to a variable
                    if (ValidatePhoneNumber(newAccount.PhoneNumber)) // Run the ValidateDOB function to check if input is valid.
                        break; // If the input is valid, break from this loop.
                    else
                        Console.WriteLine("Invalid phone number. Please try again."); // Printed message if input is not valid.
                }

                // Loop to get and validate the starting balance
                while (true)
                {
                    // Printed message asking for how much the user wants to deposit as starting balance.
                    Console.Write("Enter Your Starting Balance: ");  
                    if (decimal.TryParse(Console.ReadLine(), out decimal balance)) // Check input to try and convert it to decimal variable
                    {
                        newAccount.Balance = balance; // Save the users input to a variable
                        break; // If the input is valid, break from this loop.
                    }
                    else
                    {
                        // Printed message if input is not valid.
                        Console.WriteLine("Invalid balance. Please enter a valid decimal number."); 
                    }
                }

               // Loop to get and validate the password.
                while (true)
                {
                    //Printed message asking for user to enter a password.
                    Console.Write("Enter Password (min 6 characters, 1 letter, 1 number, 1 special character): ");
                    newAccount.Password = Console.ReadLine(); // Save the password to a variable
                    if (ValidatePassword(newAccount.Password)) // Sent input through the ValidatePassword function
                    {
                        break; // If the input is valid, break from this loop.
                    }
                    else
                    {
                        // Printed message if input is not valid.
                        Console.WriteLine("Password does not meet requirements. Please try again. \n");
                    }
                }


                // get next account number and save it to a variable
                newAccount.AccountNumber = nextAccountNumber;
                newAccount.Username = nextAccountNumber.ToString(); // Convert nextAccountNumber to string and save as Username.
                // Add new account to the list after all variables have been validated.
                accounts.Add(newAccount);
                nextAccountNumber++; // Increment nextAccountNumber by 1 for when the next account is to be created.

                // Print message letting the user know the account has been created and print their account number to the screen.
                Console.WriteLine($"\nAccount created successfully. Your account number is {newAccount.AccountNumber} \n");
            }

            public void CheckBalance() // Method to check the balance of an account 
            {
                Account account = AuthenticateUser(); // Authenticate user
                if (account != null) // If the user successfully authenticates and account is not invalid or null.
                {
                    // Print balance to screen.
                    Console.WriteLine($"Your balance is: {account.Balance} \n");
                }
            }

            public void MakeWithdrawal() // Method to make a withdrawal from an account
            {
                Account account = AuthenticateUser(); // Authenticate user before proceeding.
                if (account != null) // If the user successfully authenticates and account is not invalid or null.
                {
                    Console.Write("Enter amount to withdraw: "); // Print message asking the user how much to withdrawl.
                    decimal amount = decimal.Parse(Console.ReadLine()); // Convert it to decimal
                    if (account.Balance >= amount) // Check current balance of the user is sufficient
                    {
                        account.Balance -= amount; // Decrease balance by input amount.
                        //Display new account balance on screen.
                        Console.WriteLine($"Withdrawal successful. New balance: {account.Balance} \n"); 
                    }
                    else
                    {
                        // Print message notifying user they do not have enough funds.
                        Console.WriteLine("Insufficient funds. \n");
                    }
                }
            }

            public void MakeDeposit() // Method to make a deposit into an account
            {
                Account account = AuthenticateUser(); // Authenticate user
                if (account != null) // Check if account is valid.
                {
                    Console.Write("Enter amount to deposit: "); // Ask user how much to deposit.
                    decimal amount = decimal.Parse(Console.ReadLine()); // Convert to decimal
                    account.Balance += amount; // Increase amount by input amount.
                    // Display new balance on screen.
                    Console.WriteLine($"Deposit successful. New balance: {account.Balance} \n");
                }
            }

            private Account AuthenticateUser() // Method to authenticate a user
            {
                if (loginAttempts >= maxLoginAttempts) // Check is max login attempts have been reached. (3)
                {
                    // Print message to notify user that the attempts have been exceeded and program will now end.
                    Console.WriteLine("You have tried too many times, Program will now end. Goodbye!");
                    Environment.Exit(0); // Terminate the program
                }

                Console.Write("Enter Account Number: "); // Print message asking user for their account number.
                string accountNumber = Console.ReadLine(); // Save input to variable.
                Console.Write("Enter Password: "); // Request users password.
                string password = Console.ReadLine(); // Save input to variable.
                foreach (var account in accounts) // Run input through loop
                {
                    // if account number is in list and both account number and password match.
                    if (account.AccountNumber.ToString() == accountNumber && account.Password == password)
                    {
                        loginAttempts = 0; // Reset login attempts after successful authentication
                        return account; // Return the account
                    }
                }
                // If authentication has failed, notify the user.
                Console.WriteLine("Authentication failed. Please try again. \n");
                loginAttempts++; // Increment the login attempts by 1
                return null; // Return null instead of the account.
            }
        }

        private static bool ValidatePassword(string password) // Method to validate the password
        {
            if (password.Length < 6) // Check if new password is at least 6 characters.
                return false; // If less then 6, return false.

            bool hasLetter = false, hasDigit = false, hasSpecialChar = false; // Declaration of bool variables.
            foreach (char c in password) // Loop to check all characters inside the new password
            {
                if (char.IsLetter(c)) hasLetter = true; // If password has at least 1 letter, return true
                if (char.IsDigit(c)) hasDigit = true; // If the new password has at least 1 digit, return true
                if ("!@#$%^&*()<>?,./".Contains(c)) hasSpecialChar = true; // If new password has a Special Character, return true
            }

            return hasLetter && hasDigit && hasSpecialChar; // return all bools (if all true, password is valid, otherwise returns false)
        }

        private static bool ValidateName(string name) // Method to Validate input
        {
            return !string.IsNullOrWhiteSpace(name) && name.All(char.IsLetter); // Checks for null or white space as well as if its all letters.
        }

        private static bool ValidateEmail(string email) // Method to Validate input
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$"; // Checks email for valid input
            return Regex.IsMatch(email, emailPattern); // Returns result
        }

        private static bool ValidateDob(string dob) // Method to Validate input
        {
            DateTime parsedDate; // Pase the date
            return DateTime.TryParseExact(dob, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out parsedDate);
        }

        private static bool ValidatePhoneNumber(string phoneNumber) // Method to Validate input
        {
            string phonePattern = @"^\d{10}$"; // Checks phone number is valid characters
            return Regex.IsMatch(phoneNumber, phonePattern);
        }


        public class Account // Method for the Account objects
        {
            // Gets all variables entered by user and saves them to their appropriate item within list.
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
            public string Dob { get; set; }
            public string PhoneNumber { get; set; }
            public decimal Balance { get; set; }
            public int AccountNumber { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
        }

        static void Main(string[] args) // Main function runs at runtime.
        {
            Bank bank = new Bank(); // Bank Constructor
            bool quit = false; // Bool for quitting program.

            while (!quit) // Loop that waits till user quits program.
            {
                Console.WriteLine("Welcome to abc Bank!\n"); // Introduction
                // Menu Options
                Console.WriteLine("1. Create Account"); 
                Console.WriteLine("2. Check Balance");
                Console.WriteLine("3. Make Withdrawal");
                Console.WriteLine("4. Make Deposit");
                Console.WriteLine("5. Quit Program \n");
                Console.Write("Choose an option: ");
                int choice = int.Parse(Console.ReadLine()); // Takes user input and converts to integer

                switch (choice) // Switch for the users choice.
                {
                    case 1:
                        bank.CreateAccount(); // runs the create account method.
                        break;
                    case 2:
                        bank.CheckBalance(); // Runs the CheckBalance method.
                        break;
                    case 3:
                        bank.MakeWithdrawal(); // Runs the MakeWithdrawl method.
                        break;
                    case 4:
                        bank.MakeDeposit(); // Runs the Make Deposit method.
                        break;
                    case 5:
                        Console.WriteLine("Thank you for using abc Bank!"); // Prints message when user chooses to quit the program.
                        quit = true; // Sets quit bool to true and closes the program.
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again. \n"); // If the input is not one of the options, print error.
                        break;
                }
            }
        }
    }
}