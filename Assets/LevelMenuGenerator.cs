using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenuGenerator : MonoBehaviour
{
    public Button playButton;
    public GameObject menuPanel;
    public Button backButton;

    public LevelButton levelPrefab;
    public Transform container;
    public int levelCount = 20;
    public int firstLockedLevel = 16;
    public int[] customStarCounts;

    private readonly List<LevelButton> spawned = new List<LevelButton>();

    private void Start()
    {
        if (playButton != null)
            playButton.onClick.AddListener(OpenMenu);

        if (backButton != null)
            backButton.onClick.AddListener(CloseMenu);

        if (menuPanel != null)
            menuPanel.SetActive(false);

        Generate();
    }

    public void OpenMenu()
    {
        if (playButton != null)
            playButton.gameObject.SetActive(false);

        if (menuPanel != null)
            menuPanel.SetActive(true);
    }

    public void CloseMenu()
    {
        if (menuPanel != null)
            menuPanel.SetActive(false);

        if (playButton != null)
            playButton.gameObject.SetActive(true);
    }

    public void Generate()
    {
        if (levelPrefab == null || container == null)
            return;

        for (int i = container.childCount - 1; i >= 0; i--)
        {
            GameObject child = container.GetChild(i).gameObject;
            child.SetActive(false);
            Destroy(child);
        }

        spawned.Clear();

        int lockedFrom = firstLockedLevel <= 0 ? levelCount + 1 : Mathf.Min(firstLockedLevel, levelCount);

        for (int level = 1; level <= levelCount; level++)
        {
            LevelButton item = Instantiate(levelPrefab, container);
            item.name = "Level_" + level;

            bool locked = level >= lockedFrom;
            int stars = locked ? 0 : StarsFor(level);

            item.Setup(level, stars, locked);
            spawned.Add(item);
        }
    }

    private int StarsFor(int level)
    {
        if (customStarCounts != null && level - 1 < customStarCounts.Length && customStarCounts[level - 1] >= 0)
            return Mathf.Clamp(customStarCounts[level - 1], 0, 3);

        return Random.Range(0, 4);
    }
}
