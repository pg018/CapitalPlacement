### Properties For Each Tab

#### Tab 1 => Program Details 
##### (POST/PUT)
string id => required
object ProgramInfo => required
object AdditionalProgramInfo => required
##### (GET)
Input => id as query
Output => Same as the POST/PUT input

#### Tab 2 => Application Form 
##### (PUT)
string id => required
string CoverImage => required
object PersonalInfo => required
object ProfileInfo => required
list AdditionalQuestions => optional
##### (GET)
Input => id as query
Output => Same as the PUT Input

#### Tab 3 => Workflow
##### (PUT)
string id => required
object StageItem => required
##### (GET)
Input => id as query
Output => list of stages

#### Tab 4 => Preview
##### (GET)
Input => id as query
Output => all the data

Program.cs is the root file..

Endpoints Are:
http://localhost:5000/programdetails (GET/POST/PUT)
Http://localhost:5000/applicationform (GET/PUT)
http://localhost:5000/workflow (GET/PUT)
http://localhost:5000/preview (GET)

### Commit Number 3 => First Task Done, Second Task In Progress...

In the first task, I set up the directory structure which included controllers, core level, infrastructure as top level
directories...
Within the controllers are all the controllers....
Within the core level are the services, enums, custom validators and models...
Within the infrastructure is the files for cosmos document structure...

The architecture I have followed is CLEAN architecture...

For each incoming request, there is a dto (Data Transfer Objects) which brings the data to controller..
The data undergoes validation processess for each api and then the required operation is performed...

### Commit Number 5 => Task 2 Complete

In the second task, their were four main components => Cover Image, Personal Information, Profile Information and Additional Questions.
Each of the information sections had an option of additional questions...

Initially during get operation, a list of id and names is sent along with rest of the data..
The id and names list is for the question type...
During the put operation, we get the question, id and its related properties...
We extract the properties of each of the question, validate the object.
We check for the document id in database if it exists..
If does not exists, then resource not found error
If exists, we combine the incoming data and data from database into a new json object and replace it in database.

During get operation, the incoming document id is mapped in database..
If the id does not exist in database, we send a resource not found error..
If found, we extract the data that is required for "application form" only and send the data in json format