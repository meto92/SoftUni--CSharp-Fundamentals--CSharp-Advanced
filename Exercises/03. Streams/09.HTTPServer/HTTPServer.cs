using System;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Globalization;

class HTTPServer
{
    const int PortNumber = 12345;
    const int BufferSize = 4096;
    const string NewLine = "\r\n";
    const string IndexPageTitle = "Home Page";
    const string IndexPageBody = @"
        <h1>Welcome to our test page.</h1>
	    <h4>You can check the server information <a href=""/info"">here</a></h4>
	    <h5>Congratulations for creating your first web app :) </h5>";
    const string InfoPageTitle = "Info Page";
    const string InfoPageBody = @"
        <h2>Current Time: {0}</h2>
	    <h2>Logical Processors: {1}<h2>";
    const string ErrorPageTitle = "Error Page";
    const string ErrorPageBody = @"
        <h2 style=""color:red"">Error! Try going to the <a href=""/"">home page</a></h2>";
    const string HtmlLayout = @"
        <!DOCTYPE html>
        <html>
        <head>
	        <title>{0}</title>
        </head>
        <body>
	        {1}
        </body>
        </html>";

    static void WriteResponse(NetworkStream stream, string responseBody)
    {
        string response = string.Concat(
            "HTTP/1.0 200 OK",
            NewLine,
            "Content-Type: text/html",
            NewLine,
            $"Content-Length: {responseBody.Length}",
            NewLine,
            NewLine,
            responseBody);

        byte[] responseBytes = Encoding.UTF8.GetBytes(response);

        stream.Write(responseBytes, 0, responseBytes.Length);
    }

    static void Main(string[] args)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        Regex pattern = new Regex("(?<=GET) (.*?)(?= HTTP)");
        TcpListener tcpListener = new TcpListener(IPAddress.Any, PortNumber);

        tcpListener.Start();

        Console.WriteLine("Listening on port {0}...", PortNumber);

        while (true)
        {
            using (NetworkStream stream = tcpListener.AcceptTcpClient().GetStream())
            {
                byte[] buffer = new byte[BufferSize];
                int readBytes = stream.Read(buffer, 0, buffer.Length);

                string body = string.Empty;
                string title = string.Empty;
                string requestText = Encoding.UTF8.GetString(buffer, 0, readBytes);
                string address = pattern.Match(requestText).Value.Trim();

                Console.WriteLine(requestText);

                switch (address)
                {
                    case "/":
                        body = IndexPageBody;
                        title = IndexPageTitle;
                        break;
                    case "/info":
                        body = string.Format(
                            InfoPageBody,
                            DateTime.Now,
                            Environment.ProcessorCount);
                        title = InfoPageTitle;
                        break;
                    default:
                        body = ErrorPageBody;
                        title = ErrorPageTitle;
                        break;
                }

                string responseBody = string.Format(HtmlLayout, title, body);

                WriteResponse(stream, responseBody);
            }
        }
    }
}