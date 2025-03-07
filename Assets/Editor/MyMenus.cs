using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class MyMenus : Editor
{
    [MenuItem("V.R.Tools/BuildSettings/Set KeyStore Password %#E")]
    public static void SetKeyStorePassword()
    {
        var editorData = Resources.Load("EditorData") as EditorData;

        if (editorData != null)
        {
            PlayerSettings.Android.keystorePass = editorData.keystorePassword;
            
            PlayerSettings.Android.keyaliasName = editorData.keystoreAlias;
            PlayerSettings.Android.keyaliasPass = editorData.keyAliasPassword;
            Debug.Log("Password Set.");
        }
    }
    
    [MenuItem("V.R.Tools/BuildSettings/Increase Bundle Version")]
    public static void IncreaseVersion()
    {
        PlayerSettings.Android.bundleVersionCode += 1;
        var firstDigits = PlayerSettings.bundleVersion.Split(".")[0];
        var lastDigits = PlayerSettings.bundleVersion.Split(".")[1];
        var currentVersion = float.Parse(lastDigits);
        PlayerSettings.bundleVersion = firstDigits +"." + (currentVersion + 1f).ToString(CultureInfo.InvariantCulture);
    }

    
    [MenuItem("V.R.Tools/FTP/Build And Upload APK %#W")]
    public static void BuildAndCopyAPK()
    {
        SetKeyStorePassword();
        
        var buildPlayerOptions = new BuildPlayerOptions
        {
            locationPathName = GetApkName(),
            target = BuildTarget.Android,
            options = BuildOptions.None,
            scenes = GetAllSceneNames()
        };

        var report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        var summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded ");
            UploadFile();
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }
    }
    
    [MenuItem("V.R.Tools/FTP/Upload APK")]
    public static void UploadFile()
    {
        var editorData = Resources.Load("EditorData") as EditorData;
        if (editorData == null) 
            return;
        
        var ftpHost = editorData.ftpHost;
        var ftpUserName = editorData.ftpUserName;
        var ftpPassword = editorData.ftpPassword;
        var filePath = Application.dataPath + "/" + GetApkName() ;
            
        filePath = filePath.Replace("/Assets", "");
        Debug.Log("FTP Path is " + filePath);
            
        var client = new WebClient();
        var uri = new Uri(ftpHost + new FileInfo(filePath).Name);
            
        client.UploadFileCompleted += OnFileUploadCompleted;
        client.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
        client.UploadFileAsync(uri, "STOR", filePath);
    }
    
    [MenuItem("V.R.Tools/Build/Build Android Export")]
    public static void BuildAndroidExport()
    {
        Debug.Log("Building Android Export");

        ExtraSettingsForAndroidExport();
        EditorUserBuildSettings.exportAsGoogleAndroidProject = true;
        
        var buildPlayerOptions = new BuildPlayerOptions
        {
            locationPathName = "Build/"+GetExportName("ANDROID"),
            target = BuildTarget.Android,
            options = BuildOptions.None,
            scenes = GetAllSceneNames()
        };

        var report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        var summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded ");
            ExportToZip(GetExportName("ANDROID"));
            FinishCallbackForAndroidExport();
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }
    }
    
    [MenuItem("V.R.Tools/Build/Build MAC Build %#m")]
    public static void BuildMacBuild()
    {
        Debug.Log("Building Mac Build");

        // Code to generate Asset Bundles.
        
        // AssetBundleBrowser.AssetBundleModel.Model.DataSource.BuildAssetBundles(new ABBuildInfo()
        // {
        //     outputDirectory = "Assets/StreamingAssets",
        //     buildTarget = BuildTarget.Android
        // });
        
        ExtraSettingsForMacBuild();
        // EditorUserBuildSettings.exportAsGoogleAndroidProject = true;
        
        var buildPlayerOptions = new BuildPlayerOptions
        {
            locationPathName = "Build/"+GetExportName("MAC"),
            target = BuildTarget.StandaloneOSX,
            options = BuildOptions.None,
            scenes = GetAllSceneNames()
        };

        var report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        var summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded ");
            // ExportToZip(GetExportName("ANDROID"));
            FinishCallbackForMacBuild();
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }
    }

    [MenuItem("V.R.Tools/Build/Open MAC Build #2")]
    private static void OpenMacBuildTwoInstances()
    {
        var basePath = Application.dataPath.Replace("/Assets", "") + "/Build/";
        
        CopyDirectory(basePath + "MAC_EXPORT_Khelo_Jeeto.app",basePath+"MAC_EXPORT_Khelo_Jeeto_Copy.app");
        // OpenFolder("MAC_EXPORT_Khelo_Jeeto.app"); // Unable to focus on Mac build therefore focusing on android apk.
        
        TerminalCommand("open","MAC_EXPORT_Khelo_Jeeto.app");
        TerminalCommand("open","MAC_EXPORT_Khelo_Jeeto_Copy.app");
    }
    
    [MenuItem("V.R.Tools/Build/Open MAC Build Four Instances #4")]
    private static void OpenMacBuildFourInstances()
    {
        var basePath = Application.dataPath.Replace("/Assets", "") + "/Build/";
        
        CopyDirectory(basePath + "MAC_EXPORT_Khelo_Jeeto.app",basePath+"MAC_EXPORT_Khelo_Jeeto_Instance_Two.app");
        CopyDirectory(basePath + "MAC_EXPORT_Khelo_Jeeto.app",basePath+"MAC_EXPORT_Khelo_Jeeto_Instance_Three.app");
        CopyDirectory(basePath + "MAC_EXPORT_Khelo_Jeeto.app",basePath+"MAC_EXPORT_Khelo_Jeeto_Instance_Four.app");
        // OpenFolder("MAC_EXPORT_Khelo_Jeeto.app"); // Unable to focus on Mac build therefore focusing on android apk.
        
        TerminalCommand("open","MAC_EXPORT_Khelo_Jeeto.app");
        TerminalCommand("open","MAC_EXPORT_Khelo_Jeeto_Instance_Two.app");
        TerminalCommand("open","MAC_EXPORT_Khelo_Jeeto_Instance_Three.app");
        TerminalCommand("open","MAC_EXPORT_Khelo_Jeeto_Instance_Four.app");
    }
    
    [MenuItem("V.R.Tools/Build/Close All Mac Instances #B")]
    private static void CloseAllMacInstances()
    {
        TerminalCommand("pkill","Khelo Jeeto");
    }
    
    
    [MenuItem("V.R.Tools/Build/Build IOS Export")]
    public static void BuildIOSExport()
    {
        Debug.Log("Building IOS Export");
        
        ExtraSettingsForIOSExport();
        EditorUserBuildSettings.exportAsGoogleAndroidProject = true;
        
        var buildPlayerOptions = new BuildPlayerOptions
        {
            locationPathName = "Build/"+GetExportName("IOS"),
            target = BuildTarget.iOS,
            options = BuildOptions.None,
            scenes = GetAllSceneNames()
        };

        var report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        var summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded ");
            //OpenFolder(GetExportName("IOS"));
            ExportToZip(GetExportName("IOS"));
            FinishCallbackForIOSExport();
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }
    }
    
    [MenuItem("V.R.Tools/Build/Build Test APK %#R")]
    public static void BuildTestApk()
    {
        Debug.Log("Building Test Apk.");

        ExtraSettingsForTestAPK();
        EditorUserBuildSettings.exportAsGoogleAndroidProject = false;
        
        var buildPlayerOptions = new BuildPlayerOptions
        {
            locationPathName = "Build/"+ "Test.apk",
            target = BuildTarget.Android,
            options = BuildOptions.None,
            scenes = GetAllSceneNames()
        };

        var report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        var summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded ");
            OpenFolder( $"Test.apk");
            FinishCallbackForTestAPK();
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }
    }
    
    [MenuItem("V.R.Tools/Build/Open Release Link")]
    public static void OpenReleaseLink()
    {
        var editorData = Resources.Load("EditorData") as EditorData;
        if (editorData != null)
        {
            if(editorData.testAPKUploadLink != "")
                Application.OpenURL(editorData.testAPKUploadLink);
        }
    }
    
    [MenuItem("V.R.Tools/Build/Create Release Build %#T")]
    public static void CreateReleaseBuild()
    {
        Debug.Log("Building Release Apk.");

        ExtraSettingsForTestAPK();
        EditorUserBuildSettings.exportAsGoogleAndroidProject = false;
        EditorUserBuildSettings.buildAppBundle = true;
        
        var buildPlayerOptions = new BuildPlayerOptions
        {
            locationPathName = "Build/"+ $"{Application.productName}.aab",
            target = BuildTarget.Android,
            options = BuildOptions.None,
            scenes = GetAllSceneNames()
        };

        var report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        var summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded ");
            OpenFolder( $"{Application.productName}.aab");
            FinishCallbackForReleaseBuild();
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }
    }

    private static void ExportToZip(string directoryName)
        {
            var pathToSave = Application.dataPath.Replace("/Assets", "") + "/Build/" + directoryName;
     
            //choosing zip name and path
            var  zipFullPath =  pathToSave + ".zip";

            if (File.Exists(zipFullPath))
            {
                // Removing existing file if zip file already exists.
                
                var existingZipFile = new FileInfo(pathToSave + ".zip");
                existingZipFile.Delete();
            }
            
            var filesToZip = Directory.EnumerateFiles(pathToSave, "*.*", SearchOption.AllDirectories).ToList();//.Where(d => exceptionList.All(e => !d.StartsWith(e))).ToList();
            var filesToDelete = new DirectoryInfo(pathToSave);
           
            //adding files to the archive
            bool hasBeenCompleted;
            using (ZipArchive zip = ZipFile.Open(zipFullPath, ZipArchiveMode.Create))
            {
                hasBeenCompleted = AddFilesToZip(zip, filesToZip);
                zip.Dispose(); //prevents a bug where the process kept control of the file
            }
            if (hasBeenCompleted == false)
            {
                Debug.LogError($"The compression has been cancelled before completion.");
            }
            filesToDelete.Delete(true); // Deleting Old Folder.
        }
     
         static private bool AddFilesToZip(ZipArchive zip, List<string> fileList)
        {
            var projectPath = System.IO.Directory.GetCurrentDirectory();

            int fileCount = fileList.Count();
            string details = "";
            for (int i = 0; i < fileCount; i++)
            {
                string file = fileList[i];
                string fileRelativePath = Path.GetRelativePath(projectPath + "/Build", file);
                float ratio = (i + 1f) / fileCount;
                details = $"Zipping file {i + 1} of {fileCount} ({(int)(ratio * 100)}%)... [{fileRelativePath}]";
             //   PauseForProgressBarRefresh(file);
                if (EditorUtility.DisplayCancelableProgressBar("Compressing files", details, ratio))
                {
                    //user has pressed the cancel button on the ProgressBar
                    EditorUtility.ClearProgressBar();
                    EditorUtility.FocusProjectWindow(); //prevents editor from losing focus when cancel button is used
                    return false;
                }
                try
                {
                    var combinedPath = FixPathForMac(fileRelativePath);
                    zip.CreateEntryFromFile(file, combinedPath);
                }
                catch (IOException exception)
                {
                    Debug.LogError($"An error occurred while adding the file to the zip archive: {exception.Message}\nThe project was not exported.");
                    return false;
                }
                catch (Exception exception)
                {
                    Debug.LogError($"An unknown error occurred: {exception.Message}\nThe project was not exported.");
                    return false;
                }
            }
            EditorUtility.ClearProgressBar();
            return true;
        }
     
     static private string GetFolderFullPath(string folderName)
     {
         string folderFullPath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), folderName, "X"); //add a separator at the end to avoid matching a file starting with the same name
         folderFullPath = folderFullPath.Substring(0, folderFullPath.Length - 1); //remove last X but keep the separator
         return folderFullPath;
     }
    
     static private string FixPathForMac(string path)
     {
         return path.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
     }
     
    [MenuItem("V.R.Tools/Open Build Path")]
    public static void OpenFolder()
    {
        var path = Application.dataPath.Replace("/Assets","") + "/Build/.";
        Debug.Log($"Final Path is {path}");
        EditorUtility.RevealInFinder(path);
    }
    
    private static void OpenFolder(string path)
    {
        var finalPath = Application.dataPath.Replace("/Assets","") +"/Build/"+ path ;
        Debug.Log($"Final Path is {finalPath}");
        EditorUtility.RevealInFinder(finalPath);
    }
    
  

    [MenuItem("V.R.Tools/Settings")]
    public static void Settings()
    {
        var editorData = AssetDatabase.LoadMainAssetAtPath("Assets/Resources/EditorData.asset");

        if (editorData == false)
        {
            var asset = CreateInstance<EditorData>();

            AssetDatabase.CreateAsset(asset, "Assets/Resources/EditorData.asset");
            AssetDatabase.SaveAssets();
            
            editorData = AssetDatabase.LoadMainAssetAtPath("Assets/Resources/EditorData.asset");
        }
        
        Selection.activeObject = editorData;
        EditorGUIUtility.PingObject(editorData);
    }

    private static void ExtraSettingsForTestAPK()
    {
        // Put your extra settings for Test APK here.
    }

    private static void ExtraSettingsForAndroidExport()
    {
        // Put your extra settings for Android export here.
    }
    
    private static void ExtraSettingsForMacBuild()
    {
        // Put your extra settings for Android export here.
        
        // var gameConfig = Resources.Load<GameConfig>("GameConfig");
        // gameConfig.IsStatic = false;
    }

    private static void ExtraSettingsForIOSExport()
    {
        // Put your extra settings for IOS export here.
    }

    private static void FinishCallbackForAndroidExport()
    {
        var editorData = Resources.Load("EditorData") as EditorData;
        if (editorData != null)
        {
            if(editorData.androidExportUploadLink != "")
                Application.OpenURL(editorData.androidExportUploadLink);
        }
        
        OpenFolder(GetExportName(("ANDROID"))+ ".zip");
    }
    
    private static void FinishCallbackForMacBuild()
    {
        OpenMacBuildTwoInstances();
    }
    
    private static void TerminalCommand(string command,string argument)
    {
        var basePath = Application.dataPath.Replace("/Assets", "") + "/Build/";
        string appPath = basePath + argument;
        
        var process = new Process();
        process.StartInfo.FileName = command;
        process.StartInfo.Arguments = appPath; // Add any arguments here
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.CreateNoWindow = true;
        
        // Start the process
        try
        {
            process.Start();
        
            // Read the output (optional)
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
        
            process.WaitForExit();
        
            Console.WriteLine("Output: " + output);
            Console.WriteLine("Error: " + error);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception: " + ex.Message);
        }
        
    }
    
    static void CopyDirectory(string sourceDir, string destDir)
    {
        // Get the subdirectories for the specified directory.
        DirectoryInfo dir = new DirectoryInfo(sourceDir);

        if (!dir.Exists)
        {
            throw new DirectoryNotFoundException($"Source directory does not exist or could not be found: {sourceDir}");
        }

        if (Directory.Exists(destDir))
        {
            Directory.Delete(destDir, true);
        }

        DirectoryInfo[] dirs = dir.GetDirectories();
        // If the destination directory doesn't exist, create it.
        Directory.CreateDirectory(destDir);

        // Get the files in the directory and copy them to the new location.
        FileInfo[] files = dir.GetFiles();
        foreach (FileInfo file in files)
        {
            string tempPath = Path.Combine(destDir, file.Name);
            file.CopyTo(tempPath, false);
        }

        // Copy subdirectories and their contents to new location.
        foreach (DirectoryInfo subdir in dirs)
        {
            string tempPath = Path.Combine(destDir, subdir.Name);
            CopyDirectory(subdir.FullName, tempPath);
        }
    }
    
    private static void FinishCallbackForIOSExport()
    {
        var editorData = Resources.Load("EditorData") as EditorData;
        if (editorData != null)
        {
            if(editorData.iosExportUploadLink != "")
                Application.OpenURL(editorData.iosExportUploadLink);
        }
        
        OpenFolder(GetExportName(("IOS")) + ".zip");
    }
    
    private static void FinishCallbackForTestAPK()
    {
        var editorData = Resources.Load("EditorData") as EditorData;
        if (editorData != null)
        {
            if(editorData.testAPKUploadLink != "")
                Application.OpenURL(editorData.testAPKUploadLink);
        }
    }
    
    private static void FinishCallbackForReleaseBuild()
    {
        var editorData = Resources.Load("EditorData") as EditorData;
        if (editorData != null)
        {
            if(editorData.releaseAPKUploadLink != "")
                Application.OpenURL(editorData.releaseAPKUploadLink);
        }
    }

    private static void OnFileUploadCompleted(object sender, UploadFileCompletedEventArgs e)
    {
        Debug.Log("File Uploaded");
    }

    private static string[] GetAllSceneNames()
    {
        return EditorBuildSettings.scenes.ToList().Select(x => x.path).ToArray();
    }

    private static string GetExportName(string type)
    {
        return  type + "_EXPORT_"  + "Khelo_Jeeto";
    }

    private static string GetApkName()
    {
        return "Khelo_Jeeto" + "_FTP.apk";
    }
}
