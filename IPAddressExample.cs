using System; // For String and Console
using System.Net; // For Dns, IPHostEntry, IPAddress
using System.Net.Sockets; // For SocketException

class IPAddressExample {

static void PrintHostInfo(String host) {

try {
IPHostEntry hostInfo;

 // Attempt to resolve DNS for given host or address
hostInfo = Dns.Resolve(host);

 // Display the primary host name
Console.WriteLine("\tCanonical Name: " + hostInfo.HostName);

// Display list of IP addresses for this host
Console.Write("\tIP Addresses: ");
foreach (IPAddress ipaddr in hostInfo.AddressList) {
Console.Write(ipaddr.ToString() + " ");
}
Console.WriteLine();

 // Display list of alias names for this host
Console.Write("\tAliases: ");
foreach (String alias in hostInfo.Aliases) {
	Console.Write(alias + " ");
}
Console.WriteLine("\n");
 } catch (Exception) {
31 Console.WriteLine("\tUnable to resolve host: " + host + "\n");
}
}

static void Main(string[] args) {

// Get and print local host info
try {
Console.WriteLine("Local Host:");
String localHostName = Dns.GetHostName();
Console.WriteLine("\tHost Name: " + localHostName);

PrintHostInfo(localHostName);
} catch (Exception) {
Console.WriteLine("Unable to resolve local host\n");
}

// Get and print info for hosts given on command line
foreach (String arg in args) {
Console.WriteLine(arg + ":");
PrintHostInfo(arg);
}
}
}