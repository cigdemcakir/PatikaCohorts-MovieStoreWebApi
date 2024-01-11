<h2 align="center">Movie Store</h2>


### :small_blue_diamond: Introduction

This document provides a comprehensive overview of the Movie Store project, a sophisticated backend system designed to manage the functionalities of a Movie Store application. The project encapsulates a broad spectrum of services including the management of movies, actors, directors, and customer interactions.

<br/>


### :small_blue_diamond: Project Structure and Features

----

✓ **Movies Management** 

The Movies module is developed to administer various aspects of a movie entity, including its name, year of release, genre, associated director, actors involved, and pricing details.

Distinct management of directors and actors is a key feature, recognizing the potential dual roles individuals may hold.

✓ **Actors Module**

This module focuses on the storage and retrieval of actor-specific information, including their names and filmography.

It supports a many-to-many relationship between actors and movies, allowing flexible data association.

✓ **Directors Module**

Directors are cataloged with their names and the films they have directed, mirroring the structure of the Actors module.

✓ **Customer Profiles**

Central to the application, this module handles customer data, including personal details, purchase history, and preferred genres.

It allows for the repetition of genre purchases and supports the storage of multiple favorite genres per customer.

✓ **Authentication Mechanism**

To safeguard transactions and user interactions, a robust customer authentication system has been implemented.

✓ **Purchase Records**
  
A comprehensive recording system tracks each transaction, documenting relevant details such as the customer, movie, price, and date of purchase.

<br/>

### :small_blue_diamond: Application Functionalities

---

- **Movie Operations**

Functionalities for adding, deleting, updating, and listing movies have been integrated.

- **Customer Management**

This includes capabilities to add and delete customer profiles.

- **Actor Management**

Encompasses adding, deleting, updating, and listing actors.

- **Director Management**

Similar functionalities as actors, tailored for directors.

- **Movie Purchases**

A feature enabling customers to purchase movies through the application.

- **Customer Purchase History**

A tracking system for customer purchases, designed to maintain records irrespective of movie availability in the database.

<br/>

### :small_blue_diamond: Technical Specifications

---

✓ **Entity Objects**

Direct usage of entity objects for input/output operations is avoided.  


✓ **Models and DTOs**

The application leverages models and DTOs, with Automapper facilitating object transformations.


✓ **Service Layer Architecture**

Business logic is encapsulated within a dedicated service layer, streamlining controller operations.


✓ **Validation Framework**

Each service is equipped with a validation class, ensuring comprehensive data integrity.


✓ **Exception Handling and Logging**

Implemented via middleware, this system ensures coherent logging of activities and errors.


✓ **Dependency Injection (DI)**

The architecture employs DI containers for a decoupled and maintainable codebase.


✓ **Authentication and Authorization**

The project features a foundational security layer, particularly for transactional endpoints.

✓ **Unit Testing**

Rigorous unit tests are in place to guarantee the reliability and accuracy of the services.
