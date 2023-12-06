# Language School Appointment System
This standalone GUI .NET application, developed using WPF technology, and using relational database for data storage aims to facilitate the scheduling and management of individual language school classes. 
The application serves both registered and unregistered users, including administrators, professors, and students. Below are the entities and functionalities of the system.

## Entities
1. **Address**
   - Code
   - Street
   - Number
   - City
   - Country

2. **School**
   - Code
   - Name
   - Address
   - List of languages offered

3. **Registered User**
   - Name
   - Surname
   - JMBG (unique)
   - Gender
   - Address
   - Email
   - Password
   - User type (administrator, professor, student)

4. **Professor**
   - Registered user
   - School where employed
   - List of languages taught
   - List of classes (reserved and/or available)

5. **Student**
   - Registered user
   - List of reserved classes

6. **Class**
   - Code
   - Assigned professor
   - Date
   - Start time
   - Duration (in minutes)
   - Status (available or reserved)
   - Student who booked the class

## Functionality

1. **Unregistered User View**
   - View all schools based on selected location and languages.
   - View all professors working in a selected school.
   - No class scheduling capabilities.

2. **Student Registration**
   - Unregistered users (students) can register on the system.
   - During registration, enter name, surname, JMBG, gender, address, email, and password.
   - A new user gets an automatically generated empty list of classes.

3. **User Authentication**
   - Users can log in and log out.
   - During login, users enter JMBG and the password used during registration.

4. **User Profile Management**
   - All registered users can view and update their personal data.

5. **Administrator Functions**
   - Administrators are loaded from the database at application startup and cannot be added later.
   - Administrators can view all entities and perform add, edit, and logical delete operations.
   - Initial creation of AVAILABLE classes for professors.
   - Delete classes irrespective of their status (RESERVED or AVAILABLE).
   - Only administrators can register professors, and during registration, they also enter the languages the professors will teach.

6. **Professor Functions**
   - Professors can view their classes (reserved and available) for a selected date.
   - Professors can create and delete their available classes.
   - Constraints: Professors cannot create classes in the past; deletion is allowed only for AVAILABLE classes.

7. **Student Functions**
   - Students can view available classes for selected professors.
   - Students can view their list of reserved classes.
   - Students can schedule AVAILABLE classes and cancel their previously RESERVED class bookings.
   - Graphical representation of all available classes for selected professors.

8. **Entity Search**
   - Users (registered and unregistered) can search entities based on specific criteria.
   - Search options:
     - Registered Users: name, surname, email, address, and user type (available to administrators).
     - Professors: name, surname, address, email, and languages taught (available to registered/unregistered users).
     - Schools: name, address, and languages offered (available to students/unregistered users).
   
