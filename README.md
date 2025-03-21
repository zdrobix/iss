# ISS Laboratory project

## Hospital Pharmacy Application

#### Order medication usecase

| **ID and Name** | UC-1: Order medication |
|-----------------|---------------------|
| **Primary Actor** | Hospital staff |
| **Secondary Actors** | Pharmacy center |
| **Description** | The user (hospital staff) views the medication menu, and decides on what drugs shall be ordered. The user (hospital staff) selects the medication, and quantity for each product, and places the order. |
| **Trigger** | The hospital needs to refill the drugs inventory. |
| **Preconditions** | **PRE-1:** The hospital staff is logged in the application. |
| **Postconditions** | **POST-1:** The order is visible in the order table for the pharmacy. |
| **Normal Flow** | **1.0 Order a single drug**  |
| | 1.1 The user checks the menu for available medication. |
| | 1.2 The user selects wanted medication. |
| | 1.3 The user selects wanted quantity for that medication. |
| | 1.4 The user places the command. |
| | 1.5 The command will be visible in the user’s placed commands table, but also the pharmacy command table. |
| **Alternative Flows** | **2.0 Order multiple drugs** |
| | 2.1 The user checks the menu for available medication. |
| | 2.2 The user selects wanted medication. |
| | 2.3 The user selects wanted quantity for that medication. |
| | 2.4 Repeats steps 2.1, 2.2, 2.3 for other drugs. |
| | 2.5 The user places the command. |
| | 2.6 The command will be visible in the user’s placed commands table, but also the pharmacy command table. |
| **Exceptions** | 1. The order is canceled before placing the command. |


---


#### Resolve order usecase
| **ID and Name** | UC-2: Resolve order |
|-----------------|---------------------|
| **Primary Actor** | Pharmacy center |
| **Secondary Actors** | Hospital staff |
| **Description** | The pharmacy staff checks the order table for commands, and selects an order to be completed, based on the quantity of the medication in demand. |
| **Trigger** | An order appeared in the order table. |
| **Preconditions** | **PRE-1:** The pharmacy staff is logged in the application. <br> **PRE-2:** The quantity of the medication in the order is less than the quantity of the medication in the pharmacy inventory. |
| **Postconditions** | **POST-1:** The order is marked as completed, shown in the completed orders table. <br> **POST-2:** The hospital staff will see that the order has been completed. <br> **POST-3:** The order won’t be visible anymore in the placed orders table. |
| **Normal Flow** | **1.0 Resolve an order, marking it as complete afterwards** |
| | 1.1 The pharmacy staff selects an order. |
| | 1.2 The pharmacy staff checks in the inventory if all the drugs from the order are also present in the inventory, in the required amount. |
| | 1.3 If everything is in order, the pharmacy staff completes the delivery information, regarding the date and time. |
| | 1.4 The order is marked as complete. |
| **Alternative Flows** | NaN |
| **Exceptions** | 1. The order is removed before resolving the command. <br> 2. The quantity in the inventory for a certain medication is less than the quantity required in the order. |


---


#### Login usecase
| **ID and Name** | UC-3: Login |
|-----------------|---------------------|
| **Primary Actor** | User |
| **Secondary Actors** | Application |
| **Description** | A user wants to access their account in order to use the application, by providing login credentials. |
| **Trigger** | User navigates to the login page. |
| **Preconditions** | **PRE-1:** The user has registered an account on the platform. <br> **PRE-2:** The user must provide a valid username and password combination. |
| **Postconditions** | **POST-1:** The user is logged in. <br> **POST-2:** The user is redirected to the home page. <br> **POST-3:** Specific features become available. |
| **Normal Flow** | **1.0 User logs in** |
| | 1.1 The user navigates to the login page. |
| | 1.2 The user enters their username and password. |
| | 1.3 The system validates the credentials. |
| | 1.4 Upon success, access is granted. |
| **Alternative Flows** | **2.0 User logs in after being informed that the password is wrong** |
| | 2.1 The user navigates to the login page. |
| | 2.2 The user enters their username and password. |
| | 2.3 The system informs the user that the password is wrong. |
| | 2.4 The user enters now the correct password. |
| | 2.5 The system validates the credentials. |
| | 2.6 Upon success, access is granted. |
| **Exceptions** | 1. The user provides an invalid username. |


---


#### Login usecase
| **ID and Name** | UC-4: Logout |
|-----------------|---------------------|
| **Primary Actor** | User |
| **Secondary Actors** | Application |
| **Description** | A user wants to stop being logged in. |
| **Trigger** | User navigates to the account page, and wants to log out. |
| **Preconditions** | **PRE-1:** The user is logged in. <br> |
| **Postconditions** | **POST-1:** The user is logged out. <br> **POST-2:** The user is redirected to the login page. <br> **POST-3:** Specific features become unavailable. |
| **Normal Flow** | **1.0 User logs out** |
| | 1.1 The user navigates to the account page. |
| | 1.2 The user presses the logout button. |
| **Alternative Flows** | NaN |


---


#### Check the medication menu usecase
| **ID and Name** | UC-5: Check the medication menu |
|-----------------|---------------------|
| **Primary Actor** | Hospital Staff |
| **Secondary Actors** | Application |
| **Description** | A hospital staff member wants to see the available drugs. |
| **Trigger** | User wants to place an order, and needs information on the drugs. |
| **Preconditions** | **PRE-1:** The user is logged in. <br> |
| **Postconditions** | NaN |
| **Normal Flow** | **1.0 User checks the medication menu** |
| | 1.1 The user navigates to the menu page. |
| | 1.2 The drugs are presented in a table, along with the price. |
| **Alternative Flows** | NaN |
| **Exceptions** | NaN. |


---

#### Check the hospital orders usecase
| **ID and Name** | UC-6: Check the hospital orders |
|-----------------|---------------------|
| **Primary Actor** | Pharmacy Staff |
| **Secondary Actors** | Application |
| **Description** | A pharmacy staff member wants to see the placed orders. |
| **Trigger** | User wants to resolve an order. |
| **Preconditions** | **PRE-1:** The user is logged in. <br> |
| **Postconditions** | NaN |
| **Normal Flow** | **1.0 User checks the hospital orders** |
| | 1.1 The user navigates to the orders page. |
| | 1.2 The orders are presented in a table, along with the hospital name, and sorted by the placing date. |
| **Alternative Flows** | NaN |
| **Exceptions** | NaN. |

---

#### Check the storage usecase
| **ID and Name** | UC-7: Check the storage |
|-----------------|---------------------|
| **Primary Actor** | Pharmacy Staff |
| **Secondary Actors** | Application |
| **Description** | A pharmacy staff member wants to see the available drugs, with the quantity. |
| **Trigger** | The user resolves an order and checks the availability of certain drugs. |
| **Preconditions** | **PRE-1:** The user is logged in. <br> |
| **Postconditions** | NaN |
| **Normal Flow** | **1.0 User checks the pharmacy storage** |
| | 1.1 The user navigates to the storage page. |
| | 1.2 The drugs are presented in a table, along the available quantity. |
| **Alternative Flows** | NaN |
| **Exceptions** | NaN. |


