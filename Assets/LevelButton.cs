using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LevelButton : MonoBehaviour
{
    public Button button;
    public TextMeshProUGUI numberText;
    public GameObject starsContainer;
    public GameObject lockIcon;
    public GameObject[] brightStars;

    public event Action<int> Clicked;

    public int LevelNumber { get; private set; }

    private void Awake()
    {
        if (button != null)
            button.onClick.AddListener(OnClick);
    }

    public void Setup(int level, int stars, bool locked)
    {
        LevelNumber = level;

        if (numberText != null)
            numberText.text = level.ToString();

        if (button != null)
        {
            button.interactable = !locked;
            button.enabled = !locked;
        }

        if (starsContainer != null)
            starsContainer.SetActive(!locked);

        if (lockIcon != null)
            lockIcon.SetActive(locked);

        if (locked || brightStars == null)
            return;

        stars = Mathf.Clamp(stars, 0, brightStars.Length);

        for (int i = 0; i < brightStars.Length; i++)
        {
            if (brightStars[i] != null)
                brightStars[i].SetActive(i < stars);
        }
    }

    private void OnClick()
    {
        Clicked?.Invoke(LevelNumber);
    }
}
