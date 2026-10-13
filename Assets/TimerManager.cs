using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public Image clockImage;
    public float timerDuration = 5f;
    public Button startButton;
    public TextMeshProUGUI launchCounterText;
    
    public Image linearTimerImage;
    public float linearDuration = 2f;
    public TextMeshProUGUI resourceText;
    
    private float currentTime;
    private bool isRunning;
    private int launchCount;
    
    private float currentLinearTime;
    private int resourceCount;
    
    private void Start()
    {
        if (clockImage != null)
        {
            clockImage.type = Image.Type.Filled;
            clockImage.fillMethod = Image.FillMethod.Radial360;
            clockImage.fillAmount = 1f;
            clockImage.fillOrigin = 2;
        }
        
        if (linearTimerImage != null)
        {
            linearTimerImage.type = Image.Type.Filled;
            linearTimerImage.fillMethod = Image.FillMethod.Horizontal;
            linearTimerImage.fillAmount = 0f;
        }
        
        launchCount = 0;
        resourceCount = 0;
        
        UpdateLaunchCounter();
        UpdateResourceText();
        UpdateButton();
        
        currentLinearTime = 0f;
        
        if (startButton != null)
        {
            startButton.onClick.AddListener(StartTimer);
        }
    }
    
    private void Update()
    {
        if (isRunning)
        {
            currentTime -= Time.deltaTime;
            
            if (currentTime <= 0f)
            {
                currentTime = 0f;
                isRunning = false;
                if (clockImage != null)
                    clockImage.fillAmount = 1f;
                launchCount++;
                UpdateLaunchCounter();
                UpdateButton();
            }
            else if (clockImage != null && timerDuration > 0f)
            {
                clockImage.fillAmount = currentTime / timerDuration;
            }
        }
        
        currentLinearTime += Time.deltaTime;
        if (linearDuration > 0f && currentLinearTime >= linearDuration)
        {
            currentLinearTime = 0f;
            resourceCount += 3;
            UpdateResourceText();
            UpdateButton();
        }
        
        if (linearTimerImage != null && linearDuration > 0f)
        {
            linearTimerImage.fillAmount = currentLinearTime / linearDuration;
        }
    }
    
    private void StartTimer()
    {
        if (resourceCount < 5) return;
        
        resourceCount -= 5;
        UpdateResourceText();
        UpdateButton();
        
        currentTime = timerDuration;
        isRunning = true;
        
        if (clockImage != null)
        {
            clockImage.fillAmount = 1f;
        }
    }
    
    private void UpdateLaunchCounter()
    {
        if (launchCounterText != null)
            launchCounterText.text = launchCount.ToString();
    }
    
    private void UpdateResourceText()
    {
        if (resourceText != null)
            resourceText.text = resourceCount.ToString();
    }
    
    private void UpdateButton()
    {
        if (startButton != null)
            startButton.interactable = resourceCount >= 5;
    }
    
    private void OnValidate()
    {
        if (timerDuration < 0.01f)
            timerDuration = 0.01f;
        
        if (linearDuration < 0.01f)
            linearDuration = 0.01f;
    }
}
