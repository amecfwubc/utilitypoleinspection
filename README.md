# utilitypoleinspection

Must be changed:

Steps:

1. Do not use this project. As you will be using the MVVM framework.(I did not use any framework as it is just a simple 3-page project. Any framework makes the codes difficult to understand. Rather gain your own knowledge on MVVM, create your own project, and just use my existing codes to manage a database)

2. Used sqlite.net.async portable class library to access the local SQLite database (implements async database access).

3. Make sure you use asynchronous programming throughout the project. (using async and await, see my code for examples.)

2. Create your own project using an MVVM framework (MVVM light or okra is my suggestion).

3. Use the DataBaseModels to create Models and XAML files to create Views (feel free to change). It also contains a file DBAccess.cs that contains all the codes to manage an SQLite database.

4. Make ViewModels for views. Use databindings to update Views and Commands to act for events. There should be nothing in code behind files.

5. Establish navigation and state management using the MVVM framework. (Navigation between pages, go back, suspension states, etc.)

6. Remember the cross-cutting concerns (research if you don't know about them):

      --Authentication

      --Authorization

      --Configuration

      --Data safety

      --Connectivity

      --Error Handling

      --Logging

      --Validation

7. Start commenting extensively and documenting your code.

Some advice:

  -- Variable names of each control should be meaningful.

  -- Exception handling should be done as higher level as possible.

  -- Log everything.

8. Choose a theme for the app and fix the layout. The layout should be optimized for 8 to 10-inch tablets in landscape mode. However, make sure that it works on all types of screens. Use a menubar on the top of each view. Also, you should leave some space at the bottom when the device is running in the tablet mode or in a windows phone (for the default navigation bar of the Windows 10).

9. Once a basic project is setup and running (Login, Load Tasks, Add Pole Info), we can start adding features. I will be updating the tasks.

10. For github:

        a. Fetch the branch mustbechanged
        b. create a new branch
        c. Whenever you have a satisfactory build, update the remote so that I can checkout and test the build.
        d. Ask for the username and password.


----------In case we need to show a demo in two weeks--------------------

1. You can use my project if you wish.

2. Choose a theme for the app and fix the layout for the tablet you have.

3. just add a feature to capture images from the camera.

4. Add a page to show the uploaded data.


---------After that, Go back to step 1 above.----------------------------------
