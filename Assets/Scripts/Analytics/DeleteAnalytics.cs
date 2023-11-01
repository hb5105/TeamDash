using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class DeleteAnalytics : MonoBehaviour
{
    private const string databaseUrl = "https://csci-526-powerups-default-rtdb.firebaseio.com/";
   
    void Start()
    {
            StartCoroutine(DeleteAllDataCoroutine());
    }

    IEnumerator DeleteAllDataCoroutine()
    {
        // Create a URL to send a DELETE request to the Firebase Realtime Database
        string url = $"{databaseUrl}.json";

        using (UnityWebRequest www = UnityWebRequest.Delete(url))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error deleting data: " + www.error);
            }
            else
            {
                Debug.Log("Data deleted successfully.");
            }
        }
    }
}
