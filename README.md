# Cash Register REST API

* [Guidelines](#guidelines)
* [Technologies](#technologies)
* [Cash Register REST API](#cash-register-rest-api)
  * [Deposit](#deposit)
  * [Get Available Cash Amount](#get-available-cash-amount)
  * [Withdraw](#withdraw)
  * [Calculate Change](#calculate-change)


## Guidelines

This document provides guidelines and examples for the Cash Register REST API, encouraging consistency, maintainability, and best practices across applications. Cash Register APIs aims to balance a truly RESTful API interface with a positive developer experience. Full information on each available request could be found below.


## Technologies
* C#
* .Net Core 3.1
* ASP.NET Web API
* Entity Framework Core
* MS Test, Moq
* Swagger UI

## Cash Register REST API
    
### Deposit

#### Request

`POST /api/cashregister/deposit`
#### Request Body

`
  {
    "fifty": 5,
    "twenty": 5,
    "ten": 5,
    "five": 5,
    "two": 5,
    "one": 5
  }
`

#### Curl
    curl -X POST "https://localhost:44325/api/cashregister/deposit" -H "accept: text/plain" -H "Content-Type: application/json" -d "{\"fifty\":5,\"twenty\":5,\"ten\":5,\"five\":5,\"two\":5,\"one\":5}"


#### Response

    content-type: application/json; charset=utf-8 
    date: Sun, 22 Mar 2020 21:06:02 GMT 
    server: Microsoft-IIS/10.0 
    status: 200 
    x-powered-by: ASP.NET 

    Deposit successful!

### Get Available Cash Amount

#### Request

`GET /api/cashregister/amountavailable`

#### Curl
    curl -X GET "https://localhost:44325/api/cashregister/amountAvailable" -H "accept: application/json"

#### Response

    content-type: application/json; charset=utf-8 
    date: Sun, 22 Mar 2020 21:06:02 GMT 
    status: 200 
    
    {
      "fifty": 5,
      "twenty": 5,
      "ten": 5,
      "five": 5,
      "two": 5,
      "one": 5
    }

### Withdraw

#### Request

`POST /api/cashregister/withdraw?withdrawAmount=5`

#### Curl
    curl -X POST "https://localhost:44325/api/cashregister/withdraw?withdrawAmount=5" -H "accept: application/json"

#### Response

    content-type: application/json; charset=utf-8 
    date: Sun, 22 Mar 2020 21:23:40 GMT 
    status: 200
    
    {
      "fifty": 0,
      "twenty": 0,
      "ten": 0,
      "five": 1,
      "two": 0,
      "one": 0
    }
    
### Calculate Change

#### Request

`POST /api/cashregister/calculatechange?price=5&sum=10`

#### Curl
    curl -X POST "https://localhost:44325/api/cashregister/calculatechange?price=5&sum=10" -H "accept: application/json"

#### Response

    content-type: application/json; charset=utf-8 
     date: Sun, 22 Mar 2020 21:23:40 GMT
     status: 200
    
    {
      "fifty": 0,
      "twenty": 0,
      "ten": 0,
      "five": 1,
      "two": 0,
      "one": 0
    }
