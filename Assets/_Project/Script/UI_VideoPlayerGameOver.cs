using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class UI_VideoPlayerGameOver : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Drag VideoPlayer component here
    public Button startButton;
    public Button pauseButton;
    public Slider timeSlider;
    public Text currentTimeText;
    public Text durationText;

    private bool isDragging = false;

    void Start()
    {
        // Assign button listeners
        startButton.onClick.AddListener(StartVideo);
        pauseButton.onClick.AddListener(PauseVideo);

        // Set slider values
        timeSlider.minValue = 0;
        timeSlider.maxValue = 1; // Will adjust later based on video duration

        // Add listener for slider drag
        timeSlider.onValueChanged.AddListener(OnSliderValueChanged);

        // Update duration text
        videoPlayer.prepareCompleted += OnVideoPrepared;
        videoPlayer.Prepare();
    }

    void Update()
    {
        // Update slider and time text if video is playing and not being dragged
        if (videoPlayer.isPlaying && !isDragging)
        {
            timeSlider.value = (float)(videoPlayer.time / videoPlayer.length);
            UpdateTimeText();
        }
    }

    void StartVideo()
    {
        videoPlayer.Play();
    }

    void PauseVideo()
    {
        videoPlayer.Pause();
    }

    void OnSliderValueChanged(float value)
    {
        if (isDragging)
        {
            double newTime = value * videoPlayer.length;
            videoPlayer.time = newTime;
            UpdateTimeText();
        }
    }

    public void OnSliderDragStart()
    {
        isDragging = true;
    }

    public void OnSliderDragEnd()
    {
        isDragging = false;
    }

    void OnVideoPrepared(VideoPlayer vp)
    {
        durationText.text = FormatTime(videoPlayer.length);
        timeSlider.maxValue = 1; // Adjust slider to match video length
    }

    void UpdateTimeText()
    {
        currentTimeText.text = FormatTime(videoPlayer.time);
    }

    string FormatTime(double time)
    {
        int minutes = Mathf.FloorToInt((float)time / 60);
        int seconds = Mathf.FloorToInt((float)time % 60);
        return $"{minutes:00}:{seconds:00}";
    }
}
