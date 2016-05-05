using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Data.Services.Client;
//using ODataConsoleClient;
using System.IO;
using MS.Dynamics.TestTools.CloudCommonTestUtilities.Authentication;
using Microsoft.OData.Client;
using System.Net;


namespace ODataConsoleClient
{
    class Program
    {

        private static EventHandler<SendingRequest2EventArgs> _ODataResource_SendingRequest = new EventHandler<SendingRequest2EventArgs>(OnSendingRequest);

        /// <summary>
        /// Does all needed initialization to the context object
        /// </summary>
        /// <param name="contextObject">the data context object to initialize</param>
        public static void InitializeContextObject(DataServiceContext contextObject)
        {
            // Make sure it is not null
            if (contextObject == null)
            {
                throw new ArgumentNullException("contextObject");
            }

            // Register to the send request event in order to add the request header
            contextObject.SendingRequest2 += new EventHandler<SendingRequest2EventArgs>(_ODataResource_SendingRequest);
        }


        /// <summary>
        /// Adds to the request the cookie header for the security token
        /// </summary>
        /// <param name="sender">the sender of the event</param>
        /// <param name="e">the args for adding the cookie to</param>
        public static void OnSendingRequest(object sender, SendingRequest2EventArgs e)
        {
            // Add the cookie in the commonUtil to the cookie header to authenticate call to service
            e.RequestMessage.SetHeader(HttpRequestHeader.Cookie.ToString(), AuthenticationUtils.GetCookie(AuthenticatorFactory.AdminAuthenticator));
        }

        
        public static Resources context;

        [STAThread]
        static void Main(string[] args)
        {
            string ODataServiceEndpoint = "https://usncax1aos.cloud.onebox.dynamics.com/data";

            Uri dynODataURI = new Uri(ODataServiceEndpoint, UriKind.Absolute);
            //Creates a DataServiceContext class which encapsulates operations that are supported by the OData endpoint
            context = new Resources(dynODataURI);

            //Authenticate the user
            //AuthenticationHelper.PerformAuthentication(context);
            Program.InitializeContextObject(context);

            CreateExchange();

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


            Customer newCustomer = CreateCustomer();


            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine("Create a new reservation for the customer. Enter to start ...");
            Console.WriteLine("--------------------------------------------------------------------");
            Console.ReadLine();

            CreateReservation(newCustomer);
                        

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
            var query = from customer in context.Customers
                        select customer;

            
            Console.ForegroundColor = ConsoleColor.Cyan;         
            //Display the http request Uri that was sent to fetch all customers
            Console.WriteLine("Request Uri: " + query.ToString());
            Console.ForegroundColor = ConsoleColor.White;            

            Console.WriteLine();

            //After retrieving all the customers, iterate through the collection and print each one.
            foreach (Customer c in query)
            {
                Console.WriteLine("Customer.RecId: " + c.RecId);
                Console.WriteLine("First Name: " + c.FirstName);
                Console.WriteLine("Last Name: " + c.LastName);

                Console.WriteLine("++++++++");
            }
        }


        /// <summary>
        /// This method prompts the user for first name and last name and then uses that to create a new customer by calling the Customer OData entity
        /// </summary>
        /// <returns></returns>
        private static Customer CreateCustomer()
        {

            Console.WriteLine("Enter First Name, and then press Enter");
            string firstName = Console.ReadLine();

            Console.WriteLine("Enter Last Name, and then press Enter ");
            string lastName = Console.ReadLine();

            //Create the Customer object with the first and last names provided by the user
            Customer newCustomer = new Customer()
            {
                FirstName = firstName,
                LastName = lastName,
                CellPhone = "0123456789",
                Email = "test@contoso.com"
            };

            //Add the new customer object to the DataServiceContext object
            context.AddToCustomers(newCustomer);

            //Save the DataServiceContext object and an internally a POST call is made to the OData Customer entity.
            //Record is created in Rainier Database
            OperationResponse response = context.SaveChanges().First();

            Console.ForegroundColor = ConsoleColor.Cyan;         
            if (response.Error == null)
            {
                Console.WriteLine("New customer created: ");
                //Display the URI of the new customer
                Console.WriteLine(response.Headers["Location"]);
            }
            Console.ForegroundColor = ConsoleColor.White;         

            return newCustomer ;
        }

        private static SVC_ODataPersonView_DE CreateExchange()
        {
            DataServiceCollection<SVC_ODataPersonView_DE> svcOdata = new DataServiceCollection<SVC_ODataPersonView_DE>(context);

            var newExchange = new SVC_ODataPersonView_DE();
            svcOdata.Add(newExchange);

            newExchange.Name = "foo bar";
            newExchange.Email  = "kuntal-test";

            context.SaveChanges(SaveChangesOptions.PostOnlySetProperties);

            //context.SaveChanges();

            return newExchange;

        }

        /// <summary>
        /// This method creates a new reservation record for the customer, created by the CreateCustomer method.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        private static Reservation CreateReservation(Customer customer)
        {
            Random rnd = new Random ();
            
            //Create a new Reservation object with the details for new customer.
            Reservation reservation = new Reservation()
            {
                Customer = customer,
                PickupDate = DateTime.Today,
                ReturnDate = DateTime.Today.AddDays (5.0),
                State =0,   //Limitation of OData, no client-side Enum support
                ReservationId = "OData_" + rnd.Next (500).ToString ()
            };

            //Query the vehicle Odata Entity to retrieve the first vehicle from the list
            var vehicle = (from v in context.Vehicles 
                        select v).First();

            
            // Set up the relationship between (a) reservation and vehicle and (b) reservation and customer.

            //Add a link to the customer from the reservation
            reservation.Customer = customer;
            reservation.CustomerId = customer.RecId;
            customer.Reservations.Add(reservation);

            context.AddToReservations(reservation);
            context.AddLink(customer, "Reservations", reservation);

            //add link to the vehicle from the reservation
            reservation.Vehicle = vehicle;
            reservation.VehicleId = vehicle.RecId;
            vehicle.Reservations.Add(reservation);
            context.SetLink(reservation, "Vehicle", vehicle);

            // Now that we have created a new reservation and set the required relationship with customer and vehicle, 
            // we will save these changes. Internally, this will also 
            // perform a POST to the Reservation OData Entity.
            OperationResponse response = context.SaveChanges().First();

            Console.ForegroundColor = ConsoleColor.Cyan;         
            if (response.Error == null)
            {
                Console.WriteLine("New Reservation created");
                //Display the URI of the newly created reservation
                Console.WriteLine(response.Headers["Location"]);
            }
            Console.ForegroundColor = ConsoleColor.White;         
            
            return reservation;            
            
        }


    }
}
