using SimpleFileBrowser;
using UnityEngine;

class FileHelper : MonoBehaviour
{

    public static string GetFileData(string filePath)
    {
        string fileData = FileBrowserHelpers.ReadTextFromFile(filePath);

        return fileData;
    }

}
