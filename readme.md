#Reactive Programming with Commands, Actors and Events - Oh My!

This session will be a mix of presentation and hands-on labs.  If you want to participate in the hands-on portion, we'll be using the following tools and servers:

##Presentation
This session's slide deck can be found on [SlideShare](http://www.slideshare.net/dhoerster/reactive-development-commands-actors-and-events-oh-my).

##Pre-requisites
  - Visual Studio 2015
  - C# 6 (although you can use VS2013 and earlier C# - some scaffolded projects mainly use string interpolation)
  - RethinkDB (to act as a local topic bus)
  - MongoDB (projections store)
  - RoboMongo (a MongoDB GUI tool)
  - EventStore (event store)

There will be a handful of labs that we'll work on.  Most will have a scaffolded solution that contains entities and helper code (along with NuGet packages).  You can either code along or view the solutions in hidden files.  All solutions will be placed in a GitHub repo.

###RethinkDB
Download: http://rethinkdb.com/docs/install/
Please note that the Windows version is a BETA preview.

Download the EXE and place it in a directory of your choice.
Open a command prompt, navigate to your EXE's directory and type:

    rethinkdb.exe --http-port 55000
(The HTTP Port can be of your choosing.  The default port requires you to run the command line as an administrator.)

After running the EXE, open a browser and navigate to

    http://localhost:55000
You should see the RethinkDB dashboard.  Play around with it -- we'll be using it a bit to create new tables and clear out data.


###MongoDB
Download:  https://www.mongodb.org/downloads#production

Download the MSI.  You can either run MongoDB from a command line or install it as a service.
You should also create a directory where you want your database files to be placed.  (I choose C:\DATA\MongoDB.)
To run from the command line, open a command prompt and navigate to where you installed it, and type:

    bin\mongod.exe --dbpath "C:\DATA\MongoDB"

There are a number of other options for the command line.  Check the documentation for more details.

Install RoboMongo next (if you want a GUI tool to view your MongoDB data)


###RoboMongo
Download:  https://robomongo.org/download

I prefer to download the portable EXE (instead of installing it).  I downloaded RoboMongo to C:\Utils\RoboMongo.
Double click the robomongo.exe to launch it.
A dialog will open asking for a MongoDB connection.  Click the create link and type in (this assumes your MongoDB is running under the default port):

  Name:  Local Mongo (or whatever you want to call it)
  Address:  localhost
            27017  (default port)

Click the TEST button and make sure you can connect.  Save the connection.  Go ahead and open it and play around.


###EventStore
Download:  https://geteventstore.com/downloads/

Download the ZIP file and extract it into a directory of your choice.
Open up a command prompt **AS AN ADMINISTRATOR**.
Navigate to where you extracted the ZIP file, and type:

    EventStore.ClusterNode.exe
There are a number of other command line options, so check the documentation for additional details.

Once running, open up a browser and navigate to:

    http://localhost:2113
You should be prompted for credentials.  Type **admin** for the user name and **changeit** for the password.  (You can change these later if you'd like.)
You should then be taken to the EventStore dashboard.
