using Firebase.Storage;
using System.Threading.Tasks;
using System.Collections;
using System.Text;
using UnityEngine;
using System.IO;

public class UserDataManager
{
    private const string PROGRESS_KEY = "Progress";
    public static UserProgressData Progress = new UserProgressData();

    public static void LoadFromLocal(){
        if(!PlayerPrefs.HasKey(PROGRESS_KEY)){
            Save(true);
        }
        else{
            string json = PlayerPrefs.GetString(PROGRESS_KEY);
            Progress = JsonUtility.FromJson<UserProgressData>(json);
        }
    }

    public static IEnumerator LoadFromCloud(System.Action onComplete){
        StorageReference targetStorage = GetTargetCloudStorage();
        bool isCompleted = false;
        bool isSuccessfull = false;
        const long maxAllowedSize = 1024 * 1024; // Sama dengan 1 MB

        targetStorage.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task) => {
            if (!task.IsFaulted){
                string json = Encoding.Default.GetString(task.Result);
                Progress = JsonUtility.FromJson<UserProgressData>(json);
                isSuccessfull = true;
            }
            isCompleted = true;
        });

        while(!isCompleted){
            yield return null;
        }

        // Jika sukses mendownload, maka simpan data hasil download
        if(isSuccessfull){
            Save();
        }
        else{
            // Jika tidak ada data di cloud, maka load data dari local
            LoadFromLocal();
        }

        onComplete?.Invoke();
    }

    private static StorageReference GetTargetCloudStorage (){
        // Gunakan Device ID sebagai nama file yang akan disimpan di cloud
        string deviceID = SystemInfo.deviceUniqueIdentifier;
        FirebaseStorage storage = FirebaseStorage.DefaultInstance;

        return storage.GetReferenceFromUrl($"{storage.RootReference}/{deviceID}");
    }

    public static void Save(bool uploadToCloud = false){
        string json = JsonUtility.ToJson(Progress, true);
        File.WriteAllText(Application.dataPath + "/Save.txt", json);
        PlayerPrefs.SetString(PROGRESS_KEY, json);

        if(uploadToCloud){
            byte[] data = Encoding.Default.GetBytes(json);
            StorageReference targetStorage = GetTargetCloudStorage();
            targetStorage.PutBytesAsync(data);
        }
    }

    public static void Remove(){
        if (PlayerPrefs.HasKey(PROGRESS_KEY)){
            PlayerPrefs.DeleteKey(PROGRESS_KEY);
            // Debug.Log("DELETED");
        }
    }
}
