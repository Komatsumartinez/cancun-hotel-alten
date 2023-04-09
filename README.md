# Hotel Cancun Reservation API

This is an API for managing reservations for the Hotel Cancún, developed using .NET Core 3.1, Layered Architecture, Test-Driven Development (TDD), and Code First approach.

# Technologies

* .NET Core 3.1 
* Entity Framework Core 
* AutoMapper
* XUnit
* Moq

# Getting Started

* Prerequisites

    .NET Core 3.1 SDK

* Installation

    Clone the repository Open the solution in Visual Studio or your preferred IDE Build the solution to restore NuGet packages Run the project

* Usage

    GET /api/reservations - Retrieves a list of all reservations

    GET /api/reservations/{id} - Retrieves a specific reservation by ID

    POST /api/reservations - Creates a new reservation

    PUT /api/reservations/{id} - Updates an existing reservation

    DELETE /api/reservations/{id} - Cancels an existing reservation

* Authorization

    This API does not implement any authorization. It is for testing purposes only.

# Testing

This API was developed using Test Driven Development (TDD) and has unit tests for each layer of the architecture. To run the tests, simply run the test project using your favorite test runner.

# Contributor

Juan Sebastian Martinez Trujillo
