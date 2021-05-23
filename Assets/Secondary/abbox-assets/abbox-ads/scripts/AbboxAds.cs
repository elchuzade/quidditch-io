using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.Networking;
using System.Collections;
using static Utilities;

public class AbboxAds : MonoBehaviour
{
    // Video Link
    public class VideoJson
    {
        public string video; // link to video
        public string name; // product title
        public string website; // link to follow on click
    }

    public class Header
    {
        public string deviceId;
        public string deviceOS;
        public string gameId;
    }

    // Id given to you when adding a new game to https://ads.abbox.com
    public string gameId;

    VideoPlayer videoPlayer;

    string abboxAdsApi = "https://ads.abbox.com";
    Header header = new Header();

    void Awake()
    {
        videoPlayer = transform.Find("video-player").GetComponent<VideoPlayer>();
    }

    void Start()
    {
        header.deviceId = SystemInfo.deviceUniqueIdentifier;
        header.deviceOS = SystemInfo.operatingSystem;
        header.gameId = gameId;

        GetVideoLink();
    }

    public void SetAdLink(string url)
    {
        videoPlayer.url = url;
        videoPlayer.Play();
    }

    public void SetAdButton(string url)
    {
        GetComponent<Button>().onClick.AddListener(() => Application.OpenURL(url));
    }

    #region handle response
    // Set error actions of video link from server file
    public void SetVideoLinkError(string response)
    {
        Debug.Log("Error: " + response);
    }

    // Set video link from server file
    public void SetVideoLinkSuccess(VideoJson response)
    {
        SetAdLink(response.video);
        SetAdButton(response.website);
    }
    #endregion

    #region server requests
    public void GetVideoLink()
    {
        string videoUrl = abboxAdsApi + "/api/v1/videos";
        StartCoroutine(GetAdLinkCoroutine(videoUrl));
    }

    private IEnumerator GetAdLinkCoroutine(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            string message = JsonUtility.ToJson(header);
            string headerMessage = BuildHeaders(message);
            webRequest.SetRequestHeader("token", headerMessage);

            // Send request and wait for the desired response.
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(webRequest.downloadHandler.text);
                // Set the error of video link received from the server
                SetVideoLinkError(webRequest.error);
            }
            else
            {
                Debug.Log(webRequest.downloadHandler.text);
                // Parse the response from server to retrieve all data fields
                VideoJson videoInfo = JsonUtility.FromJson<VideoJson>(webRequest.downloadHandler.text);

                // Set the video link received from the server
                SetVideoLinkSuccess(videoInfo);
            }
        }
    }
    #endregion
}
