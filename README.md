# LaunchLocker

Launches a file/application and "locks" it for other users.

My inspiration for creating this was Quicken. My wife and I edit the same Quicken file, which is located in a shared folder. We ran into situations many times where were both were editing it, and one person's changes were lost.

## USE CASE

If there's a file on a network share or shared folder (e.g. a DropBox folder), multiple people editing the file at the same time can cause conflicts.

If all potential users of that file use this app to launch the target file, the app will create temporary lock files to prevent the file being opened by more than one user concurrently.

Users will want to use a batch file that calls LaunchLocker with the necessary command line arguments.

## HOW TO USE

It is recommended that you compile LaunchLocker.UI to an EXE. You can do this by creating a publish profile with a self-contained deployment mode, then publishing LaunchLocker.UI.

Once you have it compiled to an EXE, create a shortcut that targets the path of LaunchLocker.UI.exe, followed by the file you want to launch.

It's best if the shortcut has the full path of both LaunchLocker.UI.exe and the file you want to launch.

Example of a shortcut's target:

```
C:\FullPathOfThisExe\LaunchLocker.UI.exe "C:\temp\targetFile.txt"
```

Alternatively, you can run the EXE from a command prompt or batch file:
```
LaunchLocker.UI.exe "C:\temp\targetFile.txt"
```

Or you can run the DLL:

```
dotnet LaunchLocker.UI.dll "C:\temp\targetFile.txt"
```

If you need to pass in additional command line arguments, you can pass them in after the target file name.

You'll know the application is working when you see a .launchlock file in the folder with your target file.

The .launchlock file contains the username of the user who created the lock, and the time the lock was created.

## CAVEATS

If you're using this to open a file as opposed to a program, the file will be opened with the default program.

The file editor may let you close the file without closing the editor. LauchLocker will not be aware when this happens, and therefore will not delete the .launchlock file. Once you close the editor, LauchLocker will then delete the file.