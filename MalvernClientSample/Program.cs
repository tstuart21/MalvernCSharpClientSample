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
            //Get Order (009)
            "0,\"009\"1,\"126\"99,\"\"",
            //Rate Single Method (002)
            "0,\"002\"1,\"12345\"12,\"Contact\"11,\"Company\"13,\"40 Lloyd Ave\"14,\"Suite 103\"15,\"Malvern\"16,\"PA\"17,\"19355\"50,\"\"18,\"6101112233\"21,\"10\"19,\"UPS\"22,\"GND\"24,\"20211108\"6001,\"11082021112233\"99,\"\"",
            //Ship Single Package (001)
            "0,\"001\"1,\"12345\"12,\"Contact\"11,\"Company\"13,\"40 Lloyd Ave\"14,\"Suite 103\"15,\"Malvern\"16,\"PA\"17,\"19355\"50,\"\"18,\"6101112233\"21,\"10\"19,\"UPS\"22,\"GND\"24,\"20211108\"6001,\"11082021112233\"99,\"\""
        };  

    }



   
}