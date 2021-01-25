using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace LoadGame
{
    
    public class LoadGameScript : MonoBehaviour
    {

        [SerializeField]
        private LoadingBar loadingBar; // lower loading bar
        [SerializeField]
        private GameObject videoPanel; // panel with background and image to play video
        [SerializeField]
        private VideoPlayer videoPlayer; // without comments
        [SerializeField]
        private RawImage videoImage; // image when video player streaming video

        public string urlServer = "https://testingrostik.000webhostapp.com/VideoUrls.php"; // url for download JSON video url and play market link
        private UrlsFromServer urlsFromServer; // class for saving downloaded urls

        void Awake()
        {
            StartCoroutine(LoadAsyncIE()); // just load play scene with waiting load bar
            StartCoroutine(GetUlrs()); // post request on server to get urls
        }

        public IEnumerator LoadAsyncIE()
        {
            yield return new WaitForEndOfFrame(); // without this string not working

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("PlayScene"); // start load scene in the background
            asyncOperation.allowSceneActivation = false; // dont show when finish loading

            while ((asyncOperation.progress < 0.9f || loadingBar.Value < 0.9f) && !videoPlayer.isPrepared) // wait when loading bar == 90 or 
                // or load scene and if video is loading - show it
            {
                loadingBar.Value += 0.005f; // add value
                yield return null; 
            }

            if (videoPlayer.isPrepared) // if loading bar == 90 and video not downloaded - skipped and load play scene
                // else play video
            {
                while (videoPlayer.isPlaying) // wait when video playing, after continue added value to loading bar
                    yield return null;
            }

            while (loadingBar.Value < 1f) // added after closed video or if video not downloaded
            {
                loadingBar.Value += 0.01f;
                yield return null;
            }
            asyncOperation.allowSceneActivation = true; // show scene
        }

        private IEnumerator GetUlrs()
        {
            UnityWebRequest urlsRequest = new UnityWebRequest(urlServer) // reuqest to server
            {
                downloadHandler = new DownloadHandlerBuffer()
            };

            yield return urlsRequest.SendWebRequest(); // wait urls in background

            if (!urlsRequest.isHttpError && !urlsRequest.isNetworkError) // if have no error
            {
                if (urlsRequest.downloadHandler.text == "NULL")
                    yield break;
                urlsFromServer = JsonUtility.FromJson<UrlsFromServer>(urlsRequest.downloadHandler.text);  // converting request to instance
                
                videoPlayer.url = urlsFromServer.videoUrl; // load video url

                videoPlayer.sendFrameReadyEvents = true; // inform if ready to shop

                videoPlayer.prepareCompleted += PrepareCompletedHandle; // call this when prepared
                videoPlayer.frameReady += FrameReadyHandle; // call this when ready to show

                videoPlayer.Prepare(); // start preparing
            }
        }

        private void FrameReadyHandle(VideoPlayer source, long frameidx) // necessarily parameters
        {
            videoPanel.transform.localScale = new Vector3(1, 1, 1); // shop panel
        }

        public void ClickPlayNow() // click on Play Now button or on video
        {
            Application.OpenURL(urlsFromServer.googlePlayUrl); // open downloaded play market url of game
            CloseVideo(); // closed video
        }

        public void CloseVideo()
        {
            videoPlayer.Stop(); // stop play video
            videoPanel.transform.localScale = new Vector3(0f, 0f, 0f); // hide panel with video and background
        }

        private void PrepareCompletedHandle(VideoPlayer video) // n ecessarily parameters
        {
            videoPlayer.Play(); // start play video
        }
    }

    class UrlsFromServer // class to saving urls
    {
        public string videoUrl; // load video url
        public string googlePlayUrl; // play market app url
    }
}
