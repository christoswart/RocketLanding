Implement OOP Principles:

Separation of Concerns (SoC): Moved the logic for displaying and updating the rocket’s display to the Rocket class, which now handles all interactions related to the rocket’s visual representation. The countdown functionality has been separated into the CountdownTimer class, responsible for managing the countdown process, clearing the console, and displaying the landing message.

Encapsulation: The rocket’s display string is now encapsulated within the Rocket class. External classes, like CountdownTimer, interact with the rocket using methods like Display and UpdateDisplay, ensuring better data hiding and cohesion.

Reusability: The Rocket and CountdownTimer classes are now self-contained and can be reused and extended throughout the application without impacting the core logic.

Single Responsibility Principle (SRP): Each class now has a single, well-defined responsibility: Rocket handles display logic, while CountdownTimer manages the countdown and associated actions, improving code clarity and maintainability.
