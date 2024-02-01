// provide FQDN as CLA it then resolves for that FQDN
// host name, canonical name, ip addresses
// taken + adapted from "TCP/IP Sockets in C#" by Makofske et al. (2004)

using System; // For String and Console
using System.Net; // For Dns, IPHostEntry, IPAddress
using System.Net.Sockets; // For SocketException

class IPAddressExample
{

    static void PrintHostInfo(String host)
    {

        try
        {
            IPHostEntry hostInfo;

            // Attempt to resolve DNS for given host or address
            hostInfo = Dns.GetHostEntry(host);

            // Display the primary host name
            Console.WriteLine("\tCanonical Name: " + hostInfo.HostName);

            // Display list of IP addresses for this host
            Console.Write("\tIP Addresses: ");
            foreach (IPAddress ipaddr in hostInfo.AddressList)
            {
                Console.Write(ipaddr.ToString() + " ");
            }
            Console.WriteLine();

            // Display list of alias names for this host
            Console.Write("\tAliases: ");
            foreach (String alias in hostInfo.Aliases)
            {
                Console.Write(alias + " ");
            }
            Console.WriteLine("\n");
        }
        catch (Exception)
        {
            Console.WriteLine("\tUnable to resolve host: " + host + "\n");
        }
    }

    static void Main(string[] args)
    {
        // Check if at least one command-line argument is provided
        if (args.Length > 0)
        {
            // Get and print info for host provided as command-line argument
            foreach (String arg in args)
            {
                Console.WriteLine(arg + ":");
                PrintHostInfo(arg);
            }
        }
        else
        {
            Console.WriteLine("Please provide a fully qualified domain name as an argument.");
        }
    }



}