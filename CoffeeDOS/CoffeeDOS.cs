using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeCore;
using VendorCore;

namespace CoffeeDispenser.FrontEnd
{

    /// <summary>
    /// Runs a coffee program. 
    /// Does not have a function to quit from the console, must be exited by Windows or CTRLC,
    /// as a coffee vending machine should not allow the user to shut itself down.
    /// </summary>
    public class CoffeeDOS
    {
        private static ICoffeeVendor _vendingMachine;

        public static void Main(string[] args)
        {
            //Generate the coffee vendor program
            _vendingMachine = new CoffeeVendor();
            Action nextAction;
            
            while (true)
            {
                //Reset the next action.
                nextAction = null;

                //Set the next action to what the user selects in the main menu
                nextAction = RunMainMenuToConsole();
                if (nextAction != null)
                    nextAction.Invoke();
            }
        }

        #region Menu Controllers
        /*
         * This region handles inputs from user, and displays proper menus to console.
         */
        private static Action RunMainMenuToConsole()
        {
            Action nextActionToTake = null;
            string input = String.Empty;
            bool leaveMainMenu = false;

            while (!leaveMainMenu)
            {
                Console.Clear();
                Console.WriteLine(GetMainMenuOutput());
                Console.Write("Your selection: ");

                switch (Console.ReadLine().Trim())
                {
                    case "1":
                        nextActionToTake = RunSizeMenuToConsole;
                        leaveMainMenu = true;
                        break;
                    case "2":
                        nextActionToTake = RunPaymentMenuToConsole;
                        leaveMainMenu = true;
                        break;
                    case "3":
                        _vendingMachine.ClearOrders();
                        Console.WriteLine("Orders cleared.");
                        leaveMainMenu = false;
                        break;
                    case "4":
                        nextActionToTake = RunDispenseMenuToConsole;
                        leaveMainMenu = true;
                        break;
                    case "5":
                        Console.WriteLine(GetEndTransactionOutput());
                        leaveMainMenu = false;
                        break;
                    default:
                        leaveMainMenu = false;
                        Console.WriteLine("Invalid input. Please try again.");
                        break;
                }

                //If we aren't leaving the main menu, give time for the user to read the last prompt by
                //waiting for input,
                if (!leaveMainMenu)
                {
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
            return nextActionToTake;
        }
        private static void RunSizeMenuToConsole()
        {
            ICoffeeSize size = null;
            bool leaveSizeMenu = false;

            while (!leaveSizeMenu)
            {
                Console.Clear();
                Console.WriteLine(GetSizeMenuOuptut());
                Console.Write("Your choice: ");
                switch (Console.ReadLine().Trim())
                {
                    case "1":
                        size = new SmallSize();
                        leaveSizeMenu = true;
                        break;
                    case "2":
                        size = new MediumSize();
                        leaveSizeMenu = true;
                        break;
                    case "3":
                        size = new LargeSize();
                        leaveSizeMenu = true;
                        break;
                    case "0":
                        leaveSizeMenu = true;
                        break;
                    default:
                        Console.WriteLine("Invalid entry. Please try again.");
                        break;
                }

                if (!leaveSizeMenu)
                {
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }

            if (size != null)
                RunCreamerSugarMenuToConsole(new Coffee(size));

        }
        private static void RunCreamerSugarMenuToConsole(Coffee order)
        {
            bool leaveCreamerSugarMenu = false;

            while (!leaveCreamerSugarMenu)
            {
                Console.Clear();
                Console.WriteLine(GetCreamAndSugarMenuOutput(order));
                Console.Write("Your choice: ");
                switch (Console.ReadLine().Trim())
                {
                    case "1":
                        order.AddedCreamer.AddQuantity(1);
                        break;
                    case "2":
                        order.AddedCreamer.RemoveQuantity(1);
                        break;
                    case "3":
                        order.AddedSugar.AddQuantity(1);
                        break;
                    case "4":
                        order.AddedSugar.RemoveQuantity(1);
                        break;
                    case "5":
                        _vendingMachine.AddCoffeeOrder(order);
                        leaveCreamerSugarMenu = true;
                        break;
                    case "6":
                        leaveCreamerSugarMenu = true;
                        break;
                    default:
                        Console.WriteLine("Did not recognize input. Please try again, and press any key to continue.");
                        Console.ReadKey();
                        break;
                }
            }
        }
        private static void RunPaymentMenuToConsole()
        {
            bool leavePaymentMenu = false;
            while (!leavePaymentMenu)
            {
                Console.Clear();
                Console.WriteLine(GetPaymentMenuOutput());
                Console.Write("Make a selection: ");
                string response = Console.ReadLine().Trim();

                try
                {
                    short responseNumber = Convert.ToInt16(response);
                    if (responseNumber > 0 && responseNumber <= AcceptedPayments.Payments.Count())
                        _vendingMachine.MakePayment(AcceptedPayments.Payments[responseNumber - 1]);
                    else if (responseNumber == AcceptedPayments.Payments.Count() + 1)
                        leavePaymentMenu = true;
                    else
                    {
                        Console.WriteLine("Invalid entry. Please try again, and press any key to continue...");
                        Console.ReadKey();
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid entry. Please try again, and press any key to continue...");
                    Console.ReadKey();
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Invalid entry. Please try again, and press any key to continue...");
                    Console.ReadKey();
                }
                catch (ArgumentException)
                {
                    //In case someone tries to put in a custom number here...
                    Console.WriteLine("Invalid entry. Please try again, and press any key to continue...");
                    Console.ReadKey();
                }

            }
        }
        private static void RunDispenseMenuToConsole()
        {
            Console.WriteLine(GetDispenseMenuOutput());
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        #endregion

        #region Console Output Menus
        /*
         * This region handles all console output menus
         */
        private static string GetMainMenuOutput()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(GetOrderOverviewOutput());
            sb.AppendLine("--------------------------------------");
            sb.AppendLine("Please select from the options below:");
            sb.AppendLine("  1) Add a coffee to your order");
            sb.AppendLine("  2) Make a payment to your order");
            sb.AppendLine("  3) Clear your order");
            sb.AppendLine("  4) Finish and dispense");
            sb.AppendLine("  5) Cancel and receive change");
            sb.AppendLine();
            return sb.ToString();
        }
        private static string GetSizeMenuOuptut()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("What size would you like your coffee to be?");
            sb.AppendLine("Please select from the choices below:");
            sb.AppendLine("  1) Small");
            sb.AppendLine("  2) Medium");
            sb.AppendLine("  3) Large");
            sb.AppendLine("  0) Go back");
            sb.AppendLine();
            return sb.ToString();
        }
        private static string GetCreamAndSugarMenuOutput(Coffee currentOrder)
        {
            int lineCount = 1;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Would you like to add Creamer or Sugar?");
            sb.AppendFormat("   1) Add Creamer ($.50 ea) ({0} of {1} max)",
                                currentOrder.AddedCreamer.Quantity,
                                currentOrder.AddedCreamer.MaxQuantity);
            sb.AppendLine();
            sb.AppendLine("   2) Remove Creamer");
            sb.AppendLine();

            sb.AppendFormat("   3) Add Sugar ($.25 ea) ({1} of {2} max)",
                                lineCount,
                                currentOrder.AddedSugar.Quantity,
                                currentOrder.AddedSugar.MaxQuantity);
            sb.AppendLine();
            sb.AppendLine("   4) Remove Sugar");
            sb.AppendLine();

            sb.AppendLine("   5) Finish Order");
            sb.AppendLine("   6) Cancel Order");

            return sb.ToString();
        }
        private static string GetPaymentMenuOutput()
        {
            int menuItem = 1;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Current Balance: {0:C}", _vendingMachine.BalanceDue);
            sb.AppendLine();
            sb.AppendFormat("Total paid: {0:C}", _vendingMachine.TenderPaid);
            sb.AppendLine();

            sb.AppendLine("Select payment: ");

            for (int i = 0; i < AcceptedPayments.Payments.Count(); i++)
            {
                sb.AppendFormat("   {0}: {1:C}", menuItem, AcceptedPayments.Payments[i]);
                sb.AppendLine();
                menuItem += 1;
            }
            sb.AppendFormat("   {0}: Finish payment", menuItem);


            return sb.ToString();
        }
        private static string GetOrderOverviewOutput()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Current Order:");
            if (_vendingMachine.CoffeeOrders.Count > 0)
            {
                //Display each order, with number of sugars and creamers in the order.
                for (int i = 0; i < _vendingMachine.CoffeeOrders.Count; i++)
                {
                    int sugarCount = _vendingMachine.CoffeeOrders[i].AddedSugar.Quantity;
                    int creamerCount = _vendingMachine.CoffeeOrders[i].AddedCreamer.Quantity;

                    sb.AppendLine(i + 1 + ") " + _vendingMachine.CoffeeOrders[i].Size.Description + " Coffee");
                    if (sugarCount > 0)
                        sb.AppendLine("     + Add " + sugarCount + " sugar packets.");
                    if (creamerCount > 0)
                        sb.AppendLine("     + Add " + creamerCount + " creamer cups.");
                }

                sb.AppendLine();
                //Show user balance remaining or change.
                if (_vendingMachine.BalanceDue > 0)
                    sb.Append("Balance due: ");
                else
                    sb.Append("Change due: ");

                sb.AppendFormat("{0:C}", _vendingMachine.BalanceDue);
                sb.AppendLine();
            }
            else
            {
                sb.AppendLine("     NO ORDER YET MADE");
            }

            sb.AppendFormat("Payment inserted: {0:C}", _vendingMachine.TenderPaid);


            return sb.ToString();
        }
        private static string GetDispenseMenuOutput()
        {
            decimal change = 0.00m;
            Coffee[] coffeeOrders = null;
            StringBuilder sb = new StringBuilder();

            try
            {
                coffeeOrders = _vendingMachine.Dispense(out change);

                if (coffeeOrders != null)
                {
                    foreach (Coffee order in coffeeOrders)
                    {
                        sb.AppendFormat("Dispensing {0} Coffee ({1} Creamers and {2} Sugars)...",
                                        order.Size.Description,
                                        order.AddedCreamer.Quantity,
                                        order.AddedSugar.Quantity);
                        sb.AppendLine();
                    }
                    sb.AppendLine();
                    sb.AppendLine("Thank you! Have a nice day.");
                    sb.AppendFormat("Change received: {0:C}", change);
                    sb.AppendLine();
                }
            }
            catch (InvalidOperationException ex)
            {
                sb.AppendLine("Could not dispense order. " + ex.Message);
                sb.AppendLine("Please finish payment and try again.");
            }

            return sb.ToString();
        }
        private static string GetEndTransactionOutput()
        {
            StringBuilder sb = new StringBuilder();

            decimal change = _vendingMachine.EndTransaction();

            sb.AppendLine("Thank you! Have a nice day.");
            sb.AppendFormat("Change received: {0:C}", change);
            sb.AppendLine();

            return sb.ToString();
        }
        #endregion
    }
}
