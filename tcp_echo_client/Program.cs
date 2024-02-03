// Import necessary namespaces to use TCP client, data encoding and streaming functionalities.
using System;
using System.Text;
using System.IO;
using System.Net.Sockets;

// Define a class named TcpEchoClient.
class TcpEchoClient
{
    // Entry point of the program, the Main method with string array args as parameters for command line arguments.
    static void Main(string[] args)
    {
        // Check if the number of arguments is less than 2 or greater than 3 and throw an exception if true.
        if ((args.Length < 2) || (args.Length > 3))
        {
            throw new ArgumentException("Parameters: <Server> <Word> [<Port>]");
        }

        // Retrieve the server address from the first argument.
        String server = args[0];

        // Convert the second argument (word to be echoed) into a byte array using ASCII encoding.
        byte[] byteBuffer = Encoding.ASCII.GetBytes(args[1]);

        // Check if a third argument (port number) is supplied; if not, default to port number 7.
        int servPort = (args.Length == 3) ? Int32.Parse(args[2]) : 7;

        // Declare variables for the TcpClient and NetworkStream outside the try block to be accessible in the finally block.
        TcpClient client = null;
        NetworkStream netStream = null;

        try
        {
            // Instantiate the TcpClient and connect to the specified server and port.
            client = new TcpClient(server, servPort);
            Console.WriteLine("Connected to server... sending echo string");

            // Get the NetworkStream associated with the TcpClient.
            netStream = client.GetStream();

            // Write the byte buffer to the network stream, sending the data to the server.
            netStream.Write(byteBuffer, 0, byteBuffer.Length);
            Console.WriteLine("Sent {0} bytes to server...", byteBuffer.Length);

            // Initialize a counter for the total number of bytes received.
            int totalBytesRcvd = 0;
            // Initialize a counter for the number of bytes received in the last read operation.
            int bytesRcvd = 0;

            // Loop until all the data sent has been received.
            while (totalBytesRcvd < byteBuffer.Length)
            {
                // Read data from the network stream into the byte buffer.
                if ((bytesRcvd = netStream.Read(byteBuffer, totalBytesRcvd,
                                                byteBuffer.Length - totalBytesRcvd)) == 0)
                {
                    // If no bytes were received, the connection was closed prematurely.
                    Console.WriteLine("Connection closed prematurely.");
                    break;
                }
                // Increment the total bytes received by the number of bytes received in the last operation.
                totalBytesRcvd += bytesRcvd;
            }

            // Output the total number of bytes received and the string that was echoed back from the server.
            Console.WriteLine("Received {0} bytes from server: {1}", totalBytesRcvd,
                              Encoding.ASCII.GetString(byteBuffer, 0, totalBytesRcvd));
        }
        catch (Exception e) // Catch any exceptions that occur during the connection and communication.
        {
            // Output the message associated with the exception to the console.
            Console.WriteLine(e.Message);
        }
        finally // Code in the finally block will execute whether an exception occurred or not.
        {
            // If the network stream was opened, close it.
            if (netStream != null)
                netStream.Close();
            // If the TcpClient was instantiated, close it to free up resources.
            if (client != null)
                client.Close();
        }
    }
}
