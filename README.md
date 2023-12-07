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

## Images of project
### Home page :
![Main page](https://github.com/anna02272/SR46-2021-POP2022-projekat/assets/96575598/6f461430-9828-480c-9809-b5a49f851a5e)
#### for not registered user
![Not registered main page](https://github.com/anna02272/SR46-2021-POP2022-projekat/assets/96575598/2e96fad5-6e9b-40c3-b83e-5374b999822f)
#### for admin
![Admin home window](https://github.com/anna02272/SR46-2021-POP2022-projekat/assets/96575598/6150ffc8-df94-4bfc-a7bb-9d0b2a30f96b)
#### for student
![Student home page](https://github.com/anna02272/SR46-2021-POP2022-projekat/assets/96575598/b990f725-7759-4eb5-857f-a880118de892)
#### for professor
![Professor home page](https://github.com/anna02272/SR46-2021-POP2022-projekat/assets/96575598/b66becc1-a03b-4be2-9f13-e636b4464b46)

### Login and Register
![Login](https://github.com/anna02272/SR46-2021-POP2022-projekat/assets/96575598/db5e0cf1-3bad-4fa0-b1a0-70db79138c5c)
![Register](https://github.com/anna02272/SR46-2021-POP2022-projekat/assets/96575598/1af23bfe-e495-4f86-82d9-a839ca5707d9)

### Users, Students, Professors, Schools, Lessons, Languages, Addresses
![Users](https://github.com/anna02272/SR46-2021-POP2022-projekat/assets/96575598/8ecff2e9-de90-4baa-8225-08ad3c3ef805)
![Students](https://github.com/anna02272/SR46-2021-POP2022-projekat/assets/96575598/9eb1152b-aa6f-47a8-a214-674fa2cbef8e)
![Professors](https://github.com/anna02272/SR46-2021-POP2022-projekat/assets/96575598/c06425cc-382d-46dc-b7e5-37cc5728c701)
![Schools](https://github.com/anna02272/SR46-2021-POP2022-projekat/assets/96575598/d1e29eda-ec3a-4e94-abc4-b411d465b751)
![Lessons](https://github.com/anna02272/SR46-2021-POP2022-projekat/assets/96575598/4a8e3dab-986b-4378-8e6f-d47c34ac5d23)
![Languages](https://github.com/anna02272/SR46-2021-POP2022-projekat/assets/96575598/15f5c1d5-3803-4aa7-b264-c88428be1cf8)
![Addresses](https://github.com/anna02272/SR46-2021-POP2022-projekat/assets/96575598/0d8b01e4-b465-4f94-9549-1bd53cbfef48)

### Search
![Users search](https://github.com/anna02272/SR46-2021-POP2022-projekat/assets/96575598/f107ae4e-b3ed-4eae-a304-531bd634dd0f)

### Add and edit
![Add lesson](https://github.com/anna02272/SR46-2021-POP2022-projekat/assets/96575598/921ab787-d3be-4027-8f0e-5be6179e4924)
![Edit lesson](https://github.com/anna02272/SR46-2021-POP2022-projekat/assets/96575598/e9178ab1-b6c7-4a1f-bdbc-d617f1c759c7)



