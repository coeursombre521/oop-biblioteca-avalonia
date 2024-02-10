# OOP_Biblioteca_Avalonia

## Introduction

OOP_Biblioteca_Avalonia is a C# application built with Avalonia.NET, designed to demonstrate the practical application of various design patterns in software development. The app serves as a management system for a library, referred to as "Biblioteca", allowing users to manage members and borrowable items such as books and magazines through a user-friendly interface. The application leverages design patterns like Abstract Factory, Builder, Decorator, Facade, Singleton, Strategy, and Visitor to ensure a robust, maintainable, and extensible codebase.

## Design Patterns Overview

This application implements several design patterns to handle different aspects of library management:

- **Abstract Factory**: Used for creating families of related objects without specifying their concrete classes.
- **Builder**: Separates the construction of a complex object from its representation, allowing the same construction process to create various representations.
- **Decorator**: Dynamically adds additional responsibilities to objects without modifying their structure.
- **Facade**: Provides a simplified interface to a complex subsystem, making it easier for the user to interact with the system.
- **Singleton**: Ensures a class has only one instance and provides a global point of access to it.
- **Strategy**: Defines a family of algorithms, encapsulates each one, and makes them interchangeable.
- **Visitor**: Allows for new operations to be added to a class hierarchy without changing the classes.

## How to Run

To run OOP_Biblioteca_Avalonia, ensure you have the .NET SDK installed on your system. Follow these steps:

1. Clone the repository to your local machine.
2. Navigate to the project directory in your terminal.
3. Run `dotnet build` to build the project.
4. Execute `dotnet run` to start the application.

## App Structure

The application is structured as follows:

- **PAOO.Biblioteca**: Contains the Facade class for the library and the entire implementation behind it
    - **/Builder**: Builder classes
    - **/Collections**: Custom collections for the library
    - **/Decorators**: Decorator classes
    - **/Factories**: Abstract Factory classes
    - **/Interfaces**: Interfaces for the library
    - **/Logger**: Logger classes
    - **/Models**: Model classes for the library
    - **/Strategies**: Strategy classes
    - **/Visitors**: Visitor classes
    - **Biblioteca.cs**: The Facade class for the library
- **PAOO.Main**: Contains the main application logic and user interface
    - **/Assets**: Contains the application's assets, such as images and icons.
    - **/ModelAdapters**: Adapters for the library models
    - **/Models**: Contains the models for the application, including the library items and members.
    - **/Services**: Services for the application, such as the library service and member service.
    - **/ViewModels**: Houses the logic for the user interface, following the MVVM pattern.
    - **/Views**: Includes the AXAML files defining the UI layout and interactions.
    - **Program.cs**: The entry point of the application, setting up the Avalonia application and its configurations.
    - **App.axaml**: Defines global styles and templates for the application.

Each borrowable item in the library, such as books and magazines, is created using a combination of the Abstract Factory, Decorator, and Builder patterns to illustrate their usage in a real-world scenario. Decorators are used to add additional properties to items dynamically.
