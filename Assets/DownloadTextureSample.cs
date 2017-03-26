using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DownloadTextureSample : MonoBehaviour {

    public GameObject PhotoView;
    public string Uri;

    bool initialized = false;

	IEnumerator Start () {
        yield return StartCoroutine(download());
        initialized = true;
	}

    IEnumerator download() {
        var request = UnityWebRequest.GetTexture(Uri);
        yield return request.Send();
        if (request.isError) {
            Debug.Log(request.error);
        } else {
            var dlHandler = (DownloadHandlerTexture)request.downloadHandler;
            var tex = dlHandler.texture;
            Debug.Log(string.Format("{0}, {1}", tex.width, tex.height));
            var obj = Instantiate(PhotoView);
            obj.GetComponent<Renderer>().material.mainTexture = tex;
            Debug.Log("PhotoView initialized");
        }
    }
	
	void Update () {
        if (!initialized) {
            return;
        }
        Debug.Log("Update");
	}
}
