using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Malvern
{
    public class Connection
    {
        private static readonly int malvernPort = 1022;
        private static readonly string malvernServer = "localhost";  // local or remote address/IP of Malvern Server
        
        public static string SendTrans(string tcpRequest)
        {
            var bytes = new byte[1024];
            var tcpResponse = "";
            try
            {
                LogLine("Request: " + tcpRequest);
                var sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    sender.Connect(malvernServer, malvernPort);
                    LogLine("Socket connected to " + sender.RemoteEndPoint.ToString());
                    int bytesRec;
                    byte[] msg = Encoding.ASCII.GetBytes(tcpRequest);
                    int bytesSent = sender.Send(msg);

                    while (tcpResponse.IndexOf("99,\"\"") == -1)
                    {
                        bytesRec = sender.Receive(bytes);
                        if (bytesRec == 0)
                        {
                            // Server closed the connection before sending the 99,"" end-of-record
                            // marker. Without this guard, Receive keeps returning 0 and the loop
                            // spins forever (this is the classic "wrong port / server not ready" hang).
                            throw new IOException("Connection closed by the server before the end-of-record marker (99,\"\") was received.");
                        }
                        tcpResponse += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    }

                    LogLine("Response: " + tcpResponse);
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();
                }
                catch (ArgumentNullException ane)
                {
                    LogLine("ArgumentNullException: " + ane.Message);
                    return "ArgumentNullException: " + ane.Message;
                }
                catch (SocketException se)
                {
                    LogLine("SocketException: " + se.Message);
                    return "SocketException: " + se.Message;
                }
                catch (Exception e)
                {
                    LogLine("Exception: " + e.Message);
                    return "Exception: " + e.Message;
                }
            }
            catch (Exception e)
            {
                LogLine("Exception: " + e.Message);
                return "Exception: " + e.Message;
            }
            return tcpResponse;
        }
        private static void LogLine(string message)
        {
            try
            {
                Directory.CreateDirectory("c:\\malvern");
                Directory.CreateDirectory("c:\\malvern\\logs");
                File.AppendAllText(Path.Combine("c:\\malvern\\logs",$"malvern_connection_{DateTime.Now.ToString("yyyyMMdd")}.txt"), DateTime.Now.ToString() + " " + message +"\r\n");
            }
            catch (Exception) { }
        }
    }
}
