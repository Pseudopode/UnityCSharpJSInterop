using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ChangeMat : MonoBehaviour
{
    public Material redMat;
    public Material greenMat;

    public GameObject go;

    public Text textToBrowser;
    public Text textFromBrowser;
    public Text textSentFromJS;

    public RawImage rawImage;

    public string url;

    public string uInfoFromBrowser ="";

    public string uInfoInsideUnity = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void uSetRedMat()
    {
        Material[] mats = go.GetComponent<Renderer>().materials;
        mats[0] = redMat;
        go.GetComponent<Renderer>().materials = mats;
    }

    public void uSetGreenMat()
    {
        Material[] mats = go.GetComponent<Renderer>().materials;
        mats[0] = greenMat;
        go.GetComponent<Renderer>().materials = mats;
    }

    [DllImport("__Internal")]
    private static extern string jsStringReturnValueFunction();

    [DllImport("__Internal")]
    private static extern string jsReturnInfoToBrowser();

    public void uGetInfoFromBrowser()
    {
        string uStr = jsStringReturnValueFunction();
        Debug.Log("Unity.Debug.Log(): Info retrieved from browser, on Unity action: " + uStr);
        textFromBrowser.text = uStr;
    }

    [DllImport("__Internal")]
    private static extern void jsUnitySendInfoToBrowser(string str);

    public void uSendInfoToBrowser()
    {
        string str = textToBrowser.text;
        Debug.Log("Unity.Debug.Log(): Sending info to browser, on Unity action: " + str);
        jsUnitySendInfoToBrowser(str);
    }

    [DllImport("__Internal")]
    private static extern void jsHello();

    public void uSendWindowAlertToBrowser()
    {
        jsHello();
    }

    public void uInfoCalledFromJS()
    {
        string str = uInfoInsideUnity;
        Debug.Log("Unity.Debug.Log(): Sending info to browser, on Browser JS action: " + str);
        jsUnitySendInfoToBrowser(str);
    }

    public void uInfoSentFromJS(string str)
    {
        Debug.Log("Unity.Debug.Log(): Received info from browser, on Browser JS action: " + str);
        textSentFromJS.text = "Text sent from JS: " + str;
    }

    public void downloadIMGFromWeb()
    {
        StartCoroutine(DownloadImage(url));
    }

    IEnumerator DownloadImage(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
            rawImage.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
    }
}
