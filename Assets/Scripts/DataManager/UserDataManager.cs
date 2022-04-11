using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using System.IO;

public class UserDataManager
{
    private const string PROGRESS_KEY = "Progress";
    public static UserProgressData Progress;

    public static void Load(){
        if (!PlayerPrefs.HasKey(PROGRESS_KEY)){
            Progress = new UserProgressData();
            Save();
            // Debug.Log("BERHASIL MEMBUAT DATA BARU");
        }
        else{
            string json = PlayerPrefs.GetString(PROGRESS_KEY);
            Progress = JsonUtility.FromJson<UserProgressData>(json);
            // Debug.Log("BERHASIL LOAD DATA");
        }
    }

    public static void Save(){
        string json = JsonUtility.ToJson(Progress, true);
        // File.WriteAllText(Application.dataPath + "/Save.txt", json);
        PlayerPrefs.SetString(PROGRESS_KEY, json);
    }

    public static void Remove(){
        if (PlayerPrefs.HasKey(PROGRESS_KEY)){
            PlayerPrefs.DeleteKey(PROGRESS_KEY);
            // Debug.Log("DELETED");
        }
    }
}
