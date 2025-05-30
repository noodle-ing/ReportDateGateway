# ReportDateGateway

**ReportDateGateway** is a RESTful API built with **ASP.NET Core** that acts as a gateway to a gRPC-based reporting date calculation service. It receives HTTP POST requests, validates input, and forwards them to the gRPC backend, returning the calculated next report date.

## ✨ Features

- Accepts user input via RESTful POST requests
- Forwards requests to a gRPC microservice
- Validates input using FluentValidation
- Handles gRPC exceptions and maps them to appropriate HTTP responses

## 📦 Prerequisites

Before running the project, ensure you have:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- A running instance of the gRPC ReportDateService
- Proper `GrpcSettings:ServiceUrl` configured in `appsettings.json`

## 📁 Project Structure
```
ReportDateGateway/
├── Controllers/
│ └── ReportDateController.cs
├── Models/
│ └── ReportDateHttpRequest.cs
├── Services/
│ └── DateService/
│ ├── IReportDateService.cs
│ └── ReportDateService.cs
├── Validators/
│ └── ReportDateHttpRequestValidator.cs
├── Program.cs
├── appsettings.json
├── appsettings.Development.json
```
## Install required NuGet packages
```
dotnet add package Grpc.Net.Client
dotnet add package Grpc.Tools
dotnet add package Google.Protobuf
dotnet add package FluentValidation.AspNetCore
```
## 🔒 Error Handling
The gateway translates gRPC errors to HTTP responses:

| gRPC Status Code | HTTP Code | Description                |
| ---------------- | --------- | -------------------------- |
| InvalidArgument  | 400 Bad   | Input validation error     |
| NotFound         | 404       | Resource not found         |
| Unavailable      | 503       | gRPC service not reachable |
| Others           | 500       | General internal error     |

## 🛠 Technologies Used
* ASP.NET Core 8

* gRPC

* FluentValidation

* Grpc.Net.Client

* Google.Protobuf



