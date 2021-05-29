using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using static Utilities;

public class Server : MonoBehaviour
{
    public class Header
    {
        public string deviceId;
        public string deviceOS;
    }

    // Each row of leaderboard
    public class LeaderboardValue
    {
        public string playerName;
        public int rank;
        public int xp;
    }

    // Video Link
    public class VideoJson
    {
        public string id; // link id to measure clicks
        public string video; // link to video
        public string name; // product title
        public string website; // link to follow on click
        public string playMarket; // link to follow on click
        public string appStore; // link to follow on click
    }

    public class PlayerData
    {
        public int xp;
        public int currentBallIndex;

        // Clicks
        public List<long> leaderboardClicks;
        public List<long> shopClicks;
        public List<long> challengesClicks;
        public List<long> pushModePlayed;
        public List<long> targetModePlayed;
    }

    // LOCAL TESTING
    //string abboxAdsApi = "http://localhost:5002";
    string quidditchIOApi = "http://localhost:5001/v1/quidditchIO";

    // STAGING
    //string abboxAdsApi = "https://staging.ads.abbox.com";
    //string quidditchIOApi = "https://staging.api.abboxgames.com/quidditchIO";

    // PRODUCTION
    string abboxAdsApi = "https://ads.abbox.com";
    //string quidditchIOApi = "https://api.abboxgames.com/v1/quidditchIO";

    List<LeaderboardValue> top = new List<LeaderboardValue>();
    List<LeaderboardValue> before = new List<LeaderboardValue>();
    List<LeaderboardValue> after = new List<LeaderboardValue>();
    LeaderboardValue you = new LeaderboardValue();

    // To send response to corresponding files
    [SerializeField] MainStatus mainStatus;
    // This is to call the functions in leaderboard scene
    [SerializeField] LeaderboardStatus leaderboardStatus;

    Header header = new Header();

    void Awake()
    {
        header.deviceId = SystemInfo.deviceUniqueIdentifier;
        header.deviceOS = SystemInfo.operatingSystem;
    }

    /* ---------- LOAD SCENE ---------- */

    // CREATE NEW PLAYER
    public void CreatePlayer(Player player)
    {
        string playerUrl = quidditchIOApi + "/player";

        PlayerData playerData = new PlayerData();
        playerData.xp = player.xp;
        playerData.currentBallIndex = player.currentBallIndex;

        // Clicks
        playerData.leaderboardClicks = new List<long>();
        playerData.shopClicks = new List<long>();
        playerData.challengesClicks = new List<long>();
        playerData.pushModePlayed = new List<long>();
        playerData.targetModePlayed = new List<long>();

        player.leaderboardClicks.ForEach(c => { playerData.leaderboardClicks.Add(c); });
        player.shopClicks.ForEach(c => { playerData.shopClicks.Add(c); });
        player.challengesClicks.ForEach(c => { playerData.challengesClicks.Add(c); });
        player.pushModePlayed.ForEach(c => { playerData.pushModePlayed.Add(c); });
        player.targetModePlayed.ForEach(c => { playerData.targetModePlayed.Add(c); });

        string playerDataJson = JsonUtility.ToJson(playerData);

        StartCoroutine(CreatePlayerCoroutine(playerUrl, playerDataJson));
    }

    // This one is called when the game is just launched
    // Either create a new player or move on
    private IEnumerator CreatePlayerCoroutine(string url, string playerData)
    {
        var jsonBinary = System.Text.Encoding.UTF8.GetBytes(playerData);
        DownloadHandlerBuffer downloadHandlerBuffer = new DownloadHandlerBuffer();
        UploadHandlerRaw uploadHandlerRaw = new UploadHandlerRaw(jsonBinary);
        uploadHandlerRaw.contentType = "application/json";

        UnityWebRequest webRequest =
            new UnityWebRequest(url, "POST", downloadHandlerBuffer, uploadHandlerRaw);

        string message = JsonUtility.ToJson(header);
        string headerMessage = BuildHeaders(message);
        webRequest.SetRequestHeader("token", headerMessage);

        yield return webRequest.SendWebRequest();
        if (webRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(webRequest.downloadHandler.text);
            // Set the error received from creating a player
        }
        else
        {
            Debug.Log(webRequest.downloadHandler.text);
            // Make the success actions received from creating a player
            mainStatus.CreatePlayerSuccess();
        }
    }

    /* ---------- MAIN SCENE ---------- */

    // SAVE PLAYER DATA
    public void SavePlayerData(Player player)
    {
        string playerDataUrl = quidditchIOApi + "/data";

        PlayerData playerData = new PlayerData();
        playerData.xp = player.xp;
        playerData.currentBallIndex = player.currentBallIndex;

        // Clicks
        playerData.leaderboardClicks = new List<long>();
        playerData.shopClicks = new List<long>();
        playerData.challengesClicks = new List<long>();
        playerData.pushModePlayed = new List<long>();
        playerData.targetModePlayed = new List<long>();

        player.leaderboardClicks.ForEach(c => { playerData.leaderboardClicks.Add(c); });
        player.shopClicks.ForEach(c => { playerData.shopClicks.Add(c); });
        player.challengesClicks.ForEach(c => { playerData.challengesClicks.Add(c); });
        player.pushModePlayed.ForEach(c => { playerData.pushModePlayed.Add(c); });
        player.targetModePlayed.ForEach(c => { playerData.targetModePlayed.Add(c); });

        string playerDataJson = JsonUtility.ToJson(playerData);

        StartCoroutine(SavePlayerDataCoroutine(playerDataUrl, playerDataJson));
    }

    private IEnumerator SavePlayerDataCoroutine(string url, string playerData)
    {
        var jsonBinary = System.Text.Encoding.UTF8.GetBytes(playerData);
        DownloadHandlerBuffer downloadHandlerBuffer = new DownloadHandlerBuffer();
        UploadHandlerRaw uploadHandlerRaw = new UploadHandlerRaw(jsonBinary);
        uploadHandlerRaw.contentType = "application/json";

        UnityWebRequest webRequest =
            new UnityWebRequest(url, "POST", downloadHandlerBuffer, uploadHandlerRaw);

        string message = JsonUtility.ToJson(header);
        string headerMessage = BuildHeaders(message);
        webRequest.SetRequestHeader("token", headerMessage);

        yield return webRequest.SendWebRequest();
        if (webRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(webRequest.downloadHandler.text);
            // Set the error received from creating a player
        }
        else
        {
            Debug.Log(webRequest.downloadHandler.text);
            // Make the success actions received from creating a player
        }
    }

    public void SendVideoClick(bool privacy, string videoId, string link)
    {
        string videoUrl = abboxAdsApi + "/api/v1/clicks/" + videoId;
        StartCoroutine(SendClickCoroutine(videoUrl, privacy, link));
    }

    private IEnumerator SendClickCoroutine(string url, bool privacy, string link)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            string message = JsonUtility.ToJson(header);
            string headerMessage = BuildHeaders(message);

            if (privacy)
            {
                webRequest.SetRequestHeader("token", headerMessage);
            }
            webRequest.SetRequestHeader("link", link);

            // Send request and wait for the desired response.
            yield return webRequest.SendWebRequest();
        }
    }

    public void GetVideoLink()
    {
        string videoUrl = abboxAdsApi + "/api/v1/videos";
        StartCoroutine(GetAdLinkCoroutine(videoUrl));
    }

    // This one is for TV in main scene
    // Get the latest video link, for now in general, in future personal based on the DeviceId
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
                //mainStatus.SetVideoLinkError(webRequest.error);
            }
            else
            {
                // Parse the response from server to retrieve all data fields
                VideoJson videoInfo = JsonUtility.FromJson<VideoJson>(webRequest.downloadHandler.text);

                // Set the video link received from the server
                mainStatus.SetVideoLinkSuccess(videoInfo);
            }
        }
    }

    /* ---------- LEADERBOARD SCENE ---------- */

    // GET LEADERBOARD LIST
    public void GetLeaderboard()
    {
        string leaderboardUrl = quidditchIOApi + "/leaderboard";
        StartCoroutine(LeaderboardCoroutine(leaderboardUrl));
    }

    // Get leaderboard data and populate it into the scroll list
    private IEnumerator LeaderboardCoroutine(string url)
    {
        Debug.Log(url);
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            string message = JsonUtility.ToJson(header);
            string headerMessage = BuildHeaders(message);
            webRequest.SetRequestHeader("token", headerMessage);

            // Send request and wait for the desired response.
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                // Set the error of leaderboard data received from the server
                Debug.Log(webRequest.downloadHandler.text);
            }
            else
            {
                Debug.Log(webRequest.downloadHandler.text);
                // Parse the response from server to retrieve all data fields
                PopulateLeaderboardData(webRequest.downloadHandler.text);
            }
        }
    }

    private void PopulateLeaderboardData(string jsonData)
    {
        // Clear the lists incase they already had data in them
        top.Clear();
        before.Clear();
        after.Clear();
        // Extract string arrays of top, before, after and stirng of you data
        string[] topData = JsonHelper.GetJsonObjectArray(jsonData, "top");
        string[] beforeData = JsonHelper.GetJsonObjectArray(jsonData, "before");
        string youData = JsonHelper.GetJsonObject(jsonData, "you");
        string[] afterData = JsonHelper.GetJsonObjectArray(jsonData, "after");

        if (topData != null)
        {
            // Parse top data to leaderboard item to populate the list
            for (int i = 0; i < topData.Length; i++)
            {
                LeaderboardValue item = JsonUtility.FromJson<LeaderboardValue>(topData[i]);
                top.Add(item);
            }
        }

        if (beforeData != null)
        {
            // Parse before data
            for (int i = 0; i < beforeData.Length; i++)
            {
                LeaderboardValue item = JsonUtility.FromJson<LeaderboardValue>(beforeData[i]);
                before.Add(item);
            }
        }

        // Parse you data
        you = JsonUtility.FromJson<LeaderboardValue>(youData);

        if (afterData != null)
        {
            // Parse after data
            for (int i = 0; i < afterData.Length; i++)
            {
                LeaderboardValue item = JsonUtility.FromJson<LeaderboardValue>(afterData[i]);
                after.Add(item);
            }
        }

        // Send leaderboard data to leaderboard scene
        leaderboardStatus.SetLeaderboardData(top, before, you, after);
    }
}

