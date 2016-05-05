using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODataClient.Microsoft.Dynamics.DataEntities;
using Microsoft.OData.Client;
using System.Net.Http;


namespace Odata4ConsoleApplication
{
    class Program
    {

        public static Resources context;

        [STAThread]
        static void Main(string[] args)
        {
            //the following BaseUri needs to be updated to your org
            string dynamicsBaseUri = "https://usnconeboxax1aos.cloud.onebox.dynamics.com/"; 
            string ODataServiceEndpoint = dynamicsBaseUri + "data";

            Uri dynODataURI = new Uri(ODataServiceEndpoint, UriKind.Absolute);

            //Creates a DataServiceContext class which encapsulates operations that are supported by the OData endpoint
            context = new Resources(dynODataURI);

            //Authenticate the user
            AuthenticationHelper.PerformAuthentication(context,dynamicsBaseUri);

            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine("Query all the customers in Fleet Management. Enter to start ...");
            Console.WriteLine("-------------------------------------------------------------------");
            Console.ReadLine();

            GetAllCustomers();

            Console.WriteLine();
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Create a new customer. Enter to start");
            Console.WriteLine("-------------------------------------");
            Console.ReadLine();
            
            
            FleetCustomer newCustomer = CreateCustomer();

            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine("Create a new reservation for the customer. Enter to start ...");
            Console.WriteLine("--------------------------------------------------------------------");
            Console.ReadLine();

            FleetRental newRental = CreateReservation(newCustomer);

            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine("Call action on the reservation ...");
            Console.WriteLine("--------------------------------------------------------------------");
            Console.ReadLine();

            UriBuilder buildUri = new UriBuilder(ODataServiceEndpoint);
            buildUri.Path = String.Format("data/FleetRentals('{0}')/Microsoft.Dynamics.DataEntities.ReturnRental", newRental.RentalId);
        
            //following code invokes the action by sending an HTTPPost request for the buildUri 
            var returnValue = context.Execute<string>(new Uri(buildUri.ToString()), "POST", true);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Invoked ReturnRental action, response is: \n" + returnValue.Single());

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;

            Console.WriteLine();

            Console.WriteLine("***************************************************");
            Console.WriteLine("*                                                 *");
            Console.WriteLine("You have successfully completed,  \t\t\n 1. Creating a new customer    \t\t\t\n 2. Creating a new reservation for that customer. \n " +
                "Please refer back to Hands on Lab documentation to continue \n Press Enter to close this application \t");
            Console.WriteLine("*                                                 *");
            Console.WriteLine("***************************************************");

            Console.ReadLine();
           
        }

        /// <summary>
        /// This method issues a query URI to retrieve all the Customers from the Customer OData Entity
        /// </summary>
        private static void GetAllCustomers()
        {
            //Using the DataServiceContext class, we can simply perform a LINQ query to retrieve all Customers from the Customer entity. 
            //This internally will issue web request by using the URI that represents OData protocol
            //get all customers
            var query = from customer in context.FleetCustomers
                        select customer;

            Console.ForegroundColor = ConsoleColor.Cyan;
            //Display the http request Uri that was sent to fetch all customers
            Console.WriteLine("Request Uri: " + query.ToString());
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine();

            //After retrieving all the customers, iterate through the collection and print each one.
            foreach (FleetCustomer c in query)
            {
                Console.WriteLine("Customer.Driver License: " + c.DriverLicense);
                Console.WriteLine("First Name: " + c.FirstName);
                Console.WriteLine("Last Name: " + c.LastName);

                Console.WriteLine("++++++++");
            }
        }

        /// <summary>
        /// This method prompts the user for first name and last name and then uses that to create a new customer by calling the Customer OData entity
        /// </summary>
        /// <returns></returns>
        private static FleetCustomer CreateCustomer()
        {

            Console.WriteLine("Enter First Name, and then press Enter");
            string firstName = Console.ReadLine();

            Console.WriteLine("Enter Last Name, and then press Enter ");
            string lastName = Console.ReadLine();

            Random rnd = new Random();

            //Create the Customer object with the first and last names provided by the user
            FleetCustomer newCustomer = new FleetCustomer()
            {
                FirstName = firstName,
                LastName = lastName,
                DriverLicense = "B923-2381-" + rnd.Next(1000,7000).ToString(),
                CellPhone = "0123456789",
                Email = "test@contoso.com",
            };

            //Add the new customer object to the DataServiceContext object
            context.AddToFleetCustomers(newCustomer);

            //Save the DataServiceContext object and an internally a POST call is made to the OData Customer entity.
            //Record is created in Rainier Database
            Microsoft.OData.Client.DataServiceResponse response = null;
            try
            {                 
                response = context.SaveChanges(); 
            }
            catch (DataServiceRequestException e)
            {
                Console.WriteLine("Error occured while saving. Error Details: " + e.InnerException.Message);
            }

            //the following code retrieves the location header which represents the newly created entity
            foreach (ChangeOperationResponse r in response)
            {
                if (r.Headers.ContainsKey("Location"))
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("New customer created: ");
                    Console.WriteLine(r.Headers["Location"]);
                }
            }

            Console.ForegroundColor = ConsoleColor.White;

            return newCustomer;
        }


        /// <summary>
        /// This method creates a new reservation record for the customer, created by the CreateCustomer method.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        private static FleetRental CreateReservation(FleetCustomer customer)
        {
            Random rnd = new Random();

            DataServiceCollection<FleetRental> reservations = new DataServiceCollection<FleetRental>(context);

            FleetRental reservation = new FleetRental();

            reservations.Add(reservation);
            //Create a new Reservation object with the details for new customer.
            
            
            reservation.CustomerFirstName = customer.FirstName;
            reservation.CustomerLastName = customer.LastName;
            reservation.CustomerDriverLicense = customer.DriverLicense;
            reservation.State = 0;   //Limitation of OData, no client-side Enum support
            reservation.RentalId = "OData_" + rnd.Next(500).ToString();
            reservation.Comments = "New customer";
            reservation.VehicleId = "Adatum_Four_2";
            reservation.VehicleVIN = "WAUXL58E15A104563";
            reservation.EndDate = DateTime.Today.AddDays (5);
            reservation.StartDate = DateTime.Today;
            

            Microsoft.OData.Client.DataServiceResponse response = null;
            try
            {
                response = context.SaveChanges(SaveChangesOptions.PostOnlySetProperties); 
            }
            catch (DataServiceRequestException e)
            {
                Console.WriteLine("Error occured while saving. Error Details: " + e.InnerException.Message);
            }


            //the following code retrieves the location header which represents the newly created entity
            Console.ForegroundColor = ConsoleColor.Cyan;         
            foreach (ChangeOperationResponse r in response)
            {
                if (r.Headers.ContainsKey("Location"))
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("New reservation created: ");
                    Console.WriteLine(r.Headers["Location"]);
                }
            }
            Console.ForegroundColor = ConsoleColor.White;    
                        
            return reservation;

        }

    }
}
