using System.Threading.Tasks;
using System.Collections;
using System.Text;
using UnityEngine;
using System.IO;

public class UserData
{
    // private const string PROGRESS_KEY = "Progress";
    // public static UserProgressData Progress;

    // public static void LoadFromLocal(){
    //     if(!PlayerPrefs.HasKey(PROGRESS_KEY)){
    //         Save(true);
    //     }
    //     else{
    //         string json = PlayerPrefs.GetString(PROGRESS_KEY);
    //         Progress = JsonUtility.FromJson<UserProgressData>(json);
    //     }
    // }

    // public static IEnumerator LoadFromDatabase(System.Action onComplete){
    //     DatabaseReference reference = GetTargetDatabase();
    //     bool isCompleted = false;
    //     bool isSuccessfull = false;

    //     reference.GetValueAsync().ContinueWith(task => {
    //         if(task.IsCompleted) {
    //             DataSnapshot snapshot = task.Result;
    //             string json = snapshot.GetRawJsonValue();
    //             Progress = JsonUtility.FromJson<UserProgressData>(json);
    //             if(Progress != null){
    //                 isSuccessfull = true;
    //             }
    //         }
    //         isCompleted = true;
    //     });

    //     while(!isCompleted){
    //         yield return null;
    //     }

    //     // Jika sukses mendownload, maka simpan data hasil download
    //     if(isSuccessfull){
    //         Save();
    //     }
    //     else{
    //         LoadFromLocal();
    //     }

    //     onComplete?.Invoke();
    // }

    // private static DatabaseReference GetTargetDatabase (){
    //     // Gunakan Device ID sebagai nama file yang akan disimpan di database
    //     string deviceID = SystemInfo.deviceUniqueIdentifier;
    //     return FirebaseDatabase.DefaultInstance.GetReference("users").Child(deviceID);
    // }

    // public static void Save(bool uploadWithList = false){
    //     if(Progress == null){
    //         Progress = new UserProgressData();
    //     }
    //     string json = JsonUtility.ToJson(Progress, true);

    //     // File.WriteAllText(Application.dataPath + "/Save.txt", json);
    //     PlayerPrefs.SetString(PROGRESS_KEY, json);

    //     if(uploadWithList){
    //         DatabaseReference reference = GetTargetDatabase();
    //         reference.SetRawJsonValueAsync(json);
    //     }
    // }

    // public static void Remove(){
    //     if (PlayerPrefs.HasKey(PROGRESS_KEY)){
    //         PlayerPrefs.DeleteKey(PROGRESS_KEY);
    //     }
    // }
}
