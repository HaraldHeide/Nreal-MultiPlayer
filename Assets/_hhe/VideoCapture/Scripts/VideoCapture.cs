using NRKernal.Record;
using System.Collections;
using System;
using System.IO;
using System.Linq;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;

[HelpURL("https://developer.nreal.ai/develop/unity/video-capture")]
public class VideoCapture : MonoBehaviour
{
 //   public TMP_Text Message;

    [SerializeField]
    private float SecondsToStart = 1f;
    [SerializeField]
    private float SecondsShootTime = 30f;

    AudioSource audioData;
    public AudioClip[] clips;
    public string VideoSavePath
    {
        get
        {
            string filename = Application.productName + "-" + DateTime.Now.ToString("yyyyMMdd-HHmm") + ".mp4";
            string filepath = Path.Combine("/sdcard/RecordVideos/", filename);
            return filepath;
        }
    }

    NRVideoCapture m_VideoCapture = null;

    private void Awake()
    {
        audioData = GetComponent<AudioSource>();
    }

    IEnumerator  Start()
    {
        //Message.text = "Start";
        yield return new WaitForSeconds(SecondsToStart);
        audioData.clip = clips[0];
        audioData.Play();
        Init();
        yield return new WaitForSeconds(SecondsShootTime);
        StopVideoCapture();
        audioData.clip = clips[1];
        audioData.Play();
        //Message.text = "Stop";
    }
    public void Init()
    {
        CreateVideoCaptureTest();
        StartVideoCapture();
    }

    void CreateVideoCaptureTest()
    {
        NRVideoCapture.CreateAsync(false, delegate (NRVideoCapture videoCapture)
        {
            if (videoCapture != null)
            {
                m_VideoCapture = videoCapture;
            }
        });
    }

    public void StartVideoCapture()
    {
        Resolution cameraResolution = NRVideoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();

        int cameraFramerate = NRVideoCapture.GetSupportedFrameRatesForResolution(cameraResolution).OrderByDescending((fps) => fps).First();

        if (m_VideoCapture != null)
        {
            Debug.Log("Created VideoCapture Instance!");
            CameraParameters cameraParameters = new CameraParameters();
            cameraParameters.hologramOpacity = 0.0f;
            cameraParameters.frameRate = cameraFramerate;
            cameraParameters.cameraResolutionWidth = cameraResolution.width;
            cameraParameters.cameraResolutionHeight = cameraResolution.height;
            cameraParameters.pixelFormat = CapturePixelFormat.BGRA32;
            cameraParameters.blendMode = BlendMode.Blend;

            m_VideoCapture.StartVideoModeAsync(cameraParameters, OnStartedVideoCaptureMode);
        }
    }

    public void StopVideoCapture()
    {
        if (m_VideoCapture == null)
        {
            return;
        }
        m_VideoCapture.StopRecordingAsync(OnStoppedRecordingVideo);
    }

    void OnStartedVideoCaptureMode(NRVideoCapture.VideoCaptureResult result)
    {
        m_VideoCapture.StartRecordingAsync(VideoSavePath, OnStartedRecordingVideo);
    }

    void OnStartedRecordingVideo(NRVideoCapture.VideoCaptureResult result)
    {
    }

    void OnStoppedRecordingVideo(NRVideoCapture.VideoCaptureResult result)
    {
        m_VideoCapture.StopVideoModeAsync(OnStoppedVideoCaptureMode);
    }

    void OnStoppedVideoCaptureMode(NRVideoCapture.VideoCaptureResult result)
    {
    }
}
