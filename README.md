This is a .NET 8 Web API that calculates the sum of the angle given the time in hours and minutes based on the hands on a clock.

Installation and Setup: Make sure to have the following installed .NET 8 Visual Studio 2022 (or Visual Studio Code) Git (if cloning repository)

If cloning from Git, use the following command: git clone https://github.com/nguyener14/TimeAngle.git

If you received this project as a .zip, extract it first and then open the solution file (*.sln) from the main directory in Visual Studio.

Running the API Make sure TimeAngle is selected as the startup project and run the program using the start button on the UI or press F5. The Swagger UI should display in the web browser with your localhost as the URL. Note: if there are issues with trusting certificates in the web browser, it can be bypassed by running as http instead of https.

Testing the API In the Swagger UI, click on the drop down and then press "Try it out" and execute the API listed as "calculate-angle" for either type of input. Alternatively, you can test in Postman by making a GET or POST request to the API path: https://localhost:7266/api/timeangle/calculate-angle?hour=3&minute=15 (append URL as needed for different styles of input).
