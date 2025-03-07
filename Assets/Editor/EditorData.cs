using UnityEngine;

[CreateAssetMenu(fileName = "EditorData", menuName = "EditorData")]
public class EditorData : ScriptableObject
{
    [Header("FTP Data")]
    [Space(5)]
    public string ftpHost;
    public string ftpUserName = "android";
    public string ftpPassword = "android";
   
    [Header("KeyStoreData")]
    [Space(5)]
    public string keystoreAlias;
    public string keystorePassword;
    public string keyAliasPassword;

    [Header("Export UploadLinks")] 
    public string androidExportUploadLink;
    public string iosExportUploadLink;
    public string testAPKUploadLink;
    public string releaseAPKUploadLink;

}