using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Button> levelBtn; 
    [SerializeField] private Sprite lockedSprite;
    [SerializeField] private Sprite unlockedSprite;
    [SerializeField] private RectTransform title;
    [SerializeField] private Transform circle;
    private List<bool> levelUnlocked = new List<bool>(); 
    public static LevelManager Instance;
    private const int TotalLevels = 20; 
    private void Awake() {
        Instance = this;
    }
    void Start()
    {
        if(title != null)
            StartAnim();
        InitializeLevels();
        if (levelBtn != null && levelBtn.Count > 0)
        {
            UpdateButtonGraphics();
        }
    }

    private void InitializeLevels()
    {
        for (int i = 0; i < TotalLevels; i++)
        {
            bool isUnlocked = PlayerPrefs.GetInt("Level_" + i, i == 0 ? 1 : 0) == 1;
            levelUnlocked.Add(isUnlocked);
        }
    }

    private void UpdateButtonGraphics()
    {
        if (levelBtn == null || levelBtn.Count == 0) return; 

        for (int i = 0; i < levelBtn.Count; i++)
        {
            levelBtn[i].image.sprite = levelUnlocked[i] ? unlockedSprite : lockedSprite;
            int levelIndex = i; 
            levelBtn[i].onClick.RemoveAllListeners(); 
            levelBtn[i].onClick.AddListener(() => PlayLevel(levelIndex));
        }
    }

    public void UnlockNextLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex; 
        Debug.Log(currentLevel);
        int nextLevel = currentLevel + 1;

        if (nextLevel < TotalLevels)
        {
            levelUnlocked[nextLevel] = true;
            PlayerPrefs.SetInt("Level_" + nextLevel, 1); 
            PlayerPrefs.Save();
            Debug.Log("Unlocked Level " + nextLevel);
        }
    }

    public void PlayLevel(int levelIndex)
    {
        StartCoroutine(ChangeSceneEff(levelIndex));
    }

    public void ResetLevels()
    {
        for (int i = 0; i < TotalLevels; i++)
        {
            PlayerPrefs.DeleteKey("Level_" + i);
        }
        PlayerPrefs.Save();

        levelUnlocked.Clear();
        for (int i = 0; i < TotalLevels; i++)
        {
            levelUnlocked.Add(i == 0); 
            PlayerPrefs.SetInt("Level_" + i, i == 0 ? 1 : 0);
        }
        PlayerPrefs.Save();

        if (levelBtn != null && levelBtn.Count > 0)
        {
            UpdateButtonGraphics();
        }
    }

    public void LoadLevelButtons(List<Button> buttons)
    {
        levelBtn = buttons;
        if (levelBtn != null && levelBtn.Count > 0)
        {
            UpdateButtonGraphics();
        }
    }
    public void StartAnim()
    {
        foreach (var button in levelBtn) button.transform.localScale = Vector3.zero;
        StartCoroutine(StartSceneEff());
    }
    IEnumerator StartSceneEff()
    {
        circle.localScale = Vector3.one * 30;
        circle.DOScale(0, 1f).SetEase(Ease.InOutQuad);
        yield return new WaitForSeconds(.5f);
        StartCoroutine(TitleAnim());
        StartCoroutine(LoadBtnCroutine());
    }
    IEnumerator ChangeSceneEff(int levelIndex)
    {
        circle.DOScale(30, .75f).SetEase(Ease.InOutQuad);
        yield return new WaitForSeconds(.75f);
        if (levelIndex < levelUnlocked.Count && levelUnlocked[levelIndex])
        {
            SceneManager.LoadScene("Lv" + (levelIndex +1).ToString());
        }
        else
        {
            Debug.Log("Level " + levelIndex + " is locked!");
        }
    }
    IEnumerator LoadBtnCroutine()
    {
        foreach (var item in levelBtn)
        {
            item.transform.DOScale(1, 0.4f).SetEase(Ease.InOutQuad);
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator TitleAnim()
    {
        title.DOAnchorPos(Vector2.up * 730, 1f).SetEase(Ease.InOutQuad);
        yield return new WaitForSeconds(.5f);
        title.DORotate(new Vector3(0, 0, 10), 0.5f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(0.5f);
        title.DORotate(new Vector3(0, 0, -10), 0.5f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(0.5f);
        title.DORotate(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.Linear);

    }
}
