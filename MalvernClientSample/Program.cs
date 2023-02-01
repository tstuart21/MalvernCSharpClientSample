using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Malvern;

public class MalvernClientSample
{

    public static void Main(String[] args)
    {
        var testCases = GetTestCases();
        foreach (var request in testCases)
        {
            Console.WriteLine($"Request:\r\n{request}");
            Console.WriteLine("\r\nPress [Enter] to process transaction...\r\n");
            Console.ReadLine();
            
            var reply = Malvern.Connection.SendTrans(request);
            Console.WriteLine($"Response:\r\n{reply}");
            Console.WriteLine("\r\nPress [Enter] to continue...\r\n");
            Console.ReadLine();
        }
        Console.WriteLine("\r\nDone");
        Console.ReadLine();
    }

    private static List<string> GetTestCases()
    {
        return new List<string>()
        {
            // Ping Server
            "0,\"081\"99,\"\"",            
            // Domestic Rate
            "0,\"002\"1,\"12345\"11,\"Malvern Systems\"12,\"Dev Team\"13,\"1 N Bacton Hill Rd Ste 101\"15,\"Malvern\"16,\"PA\"17,\"19355\"19,\"UPS\"21,“2.5\"22,\"GND\"57,\"12\"58,\"8\"59,\"5\"99,\"\"",
            // Domestic Rate Shop
            "0,\"003\"1,\"12345\"11,\"Malvern Systems\"12,\"Dev Team\"13,\"1 N Bacton Hill Rd Ste 101\"15,\"Malvern\"16,\"PA\"17,\"19355\"21,“2.5\"57,\"12\"58,\"8\"59,\"5\"1033,\"USP 1CL,USP PRT,UPS GND,FDX GND,FDX HOM\"99,\"\"", 
            // Domestic Ship
            "0,\"001\"1,\"12345\"11,\"Malvern Systems\"12,\"Dev Team\"13,\"1 N Bacton Hill Rd Ste 101\"15,\"Malvern\"16,\"PA\"17,\"19355\"19,\"UPS\"21,“2.5\"22,\"GND\"57,\"12\"58,\"8\"59,\"5\"99,\"\"",
            // Domestic Address Validate
            "0,\"057\"13,\"1 N Bacton Hill Road\"14,\"Suite 101\"15,\"Malvern\"16,\"PA\"17,\"19355\"99,\"\""
        };  

    }



   
}