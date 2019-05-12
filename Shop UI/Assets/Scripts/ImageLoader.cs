using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour
{
    public string url = "http://lorempixel.com/400/200/nature";
    public RawImage rawImage;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetTexture(url, rawImage));
    }

    IEnumerator GetTexture(string url, RawImage image)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            image.texture = myTexture;
        }
    }

}
