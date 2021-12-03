using System.Collections;
using UnityEngine;
using Vuforia;
using UnityEngine.Networking;

public class DataBase : MonoBehaviour
{
    void Start() {
        StartCoroutine(GetData());
    }


    IEnumerator GetData() {
    gameObject.guiText.text = "Loading...";
    WWW www = new WWW("http://yoururl.com/yourphp.php?table=shoes"); //GET data is sent via the URL

    while(!www.isDone && string.IsNullOrEmpty(www.error)) {
        gameObject.guiText.text = "Loading... " + www.Progress.ToString("0%"); //Show progress
        yield return null;
    }

    if(string.IsNullOrEmpty(www.error)) gameObject.guiText.text = www.text;
    else Debug.LogWarning(www.error);
}
}
