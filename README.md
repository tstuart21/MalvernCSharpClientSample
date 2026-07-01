# Malvern Shipping Server — C# Sample Client

A minimal C# / .NET sample showing how to talk to the **Malvern Shipping Server** over a
TCP socket: open a connection, write a transaction, and read the reply up to the `99,""`
end-of-record marker.

It is intentionally small — a starting point for a proof of concept and a concrete
reference for the socket handling, not a production client.

## What it does

`Program.cs` sends a series of hardcoded sample transactions and prints each request and
reply to the console, pausing between them:

| Transaction | Code | What it does |
|---|---|---|
| Ping Server | `081` | Confirms the server is reachable and returns its version |
| Rate | `002` | Rates a single package with one carrier/service |
| Rate Shop | `003` | Rates several carriers/services in one call |
| Ship | `001` | Creates a shipping label |
| Validate Address | `057` | Validates and standardizes a US address |

The socket work lives in `MalvernComm/Connection.cs` (`Malvern.Connection.SendTrans`).

## Prerequisites

- Windows with the .NET Framework (the project targets 4.7.2) and Visual Studio, or MSBuild.
- Network access to a running Malvern Shipping Server (host/IP and TCP port).

## Configure

The server address and port are set at the top of `MalvernComm/Connection.cs`:

```csharp
private static readonly int malvernPort = 1022;         // your server's TCP port (default 1022)
private static readonly string malvernServer = "localhost";  // your server's host name or IP
```

Change `malvernServer` (and `malvernPort` if your administrator configured a non-default
port) to point at your server before running.

## Build & run

Open `MalvernClientSample.sln` in Visual Studio and press F5, or from the command line:

```
msbuild MalvernClientSample.sln /p:Configuration=Debug
MalvernClientSample\bin\Debug\MalvernClientSample.exe
```

Each request is printed before it is sent; press **Enter** to send it and see the reply.

## Notes

- Requests and replies are flat strings of `field,"value"` pairs with no separators,
  terminated by `99,""`. See the field and transaction references linked below.
- Connection activity is logged to `C:\malvern\logs\` for troubleshooting.
- This sample is synchronous and single-threaded. A production client should add timeout
  handling and, for high volume, fan transactions across multiple concurrent sockets
  (one socket per outstanding transaction).

## Full documentation

Detailed integration docs are in the Malvern customer portal:

- **TCP Integration Overview** — `/help/tcp-integration`
- **Building a Transaction** — `/help/tcp-integration/building-a-transaction`
- **Transaction Examples** — `/help/tcp-integration/examples`
- **TCP Field Reference** — `/help/api-reference/fields`

Contact your Malvern technician or call (800) 296-9642 for portal access or help getting started.
