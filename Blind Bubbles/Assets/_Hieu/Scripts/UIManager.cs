using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private RectTransform logo;
    [SerializeField] private RectTransform playBtn;
    [SerializeField] private RectTransform userBtn;
    [SerializeField] private RectTransform mem_1;
    [SerializeField] private RectTransform mem_2;
    [SerializeField] private RectTransform mem_3;
    [SerializeField] private RectTransform mem_4;
    [SerializeField] private RectTransform backBtn;
    [SerializeField] private List<GameObject> memName;
    [SerializeField] Transform circle;
    void Start()
    {
        StartGame();
    }
    public void PlayBtn()
    {
        StartCoroutine(NextSceneEff());
    }
    IEnumerator NextSceneEff() 
    {
        circle.DOScale(30, 1f).SetEase(Ease.InOutQuad);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("LevelMenu");
    }
    private void StartGame()
    {
        logo.DOAnchorPos(new Vector2(0, 345), 0.9f).SetEase(Ease.InOutQuad);
        playBtn.DOAnchorPos(new Vector2(-200,-440),0.75f).SetEase (Ease.InOutQuad);
        userBtn.DOAnchorPos(new Vector2(200,-440),0.75f).SetEase (Ease.InOutQuad);
    }
    private void TurnOffMainFrame()
    {
        logo.DOAnchorPos(new Vector2(0, 1500),0.5f).SetEase(Ease.InOutQuad);
        playBtn.DOAnchorPos(new Vector2(-750, -440), 0.5f).SetEase(Ease.InOutQuad);
        userBtn.DOAnchorPos(new Vector2(750, -440), 0.5f).SetEase(Ease.InOutQuad);
    }
    public void ShowMem()
    {
        TurnOffMainFrame();
        foreach (var txt in memName) txt.SetActive(false);
        SetOrigin(mem_1);
        SetOrigin(mem_2);
        SetOrigin(mem_3);
        SetOrigin(mem_4);
        StartCoroutine(MemCroutine());
    }
    IEnumerator MemCroutine()
    {
        mem_1.DOAnchorPos(new Vector2(-250, 500), 1f).SetEase(Ease.InOutQuad);
        mem_2.DOAnchorPos(new Vector2(250, 500), 1f).SetEase(Ease.InOutQuad);
        mem_3.DOAnchorPos(new Vector2(-250, 0), 1f).SetEase(Ease.InOutQuad);
        mem_4.DOAnchorPos(new Vector2(250, 0), 1f).SetEase(Ease.InOutQuad);
        mem_1.DOScale(1, 1f).SetEase(Ease.Linear);
        mem_2.DOScale(1, 1f).SetEase(Ease.Linear);
        mem_3.DOScale(1, 1f).SetEase(Ease.Linear);
        mem_4.DOScale(1, 1f).SetEase(Ease.Linear);
        backBtn.DOAnchorPos(new Vector2(0,-500),1f).SetEase(Ease.InOutQuad);
        yield return new WaitForSeconds(1f);
        foreach (var txt in memName) txt.SetActive(true);
    }
    private void SetOrigin(Transform value)
    {
        value.gameObject.SetActive(true);
        value.localScale = Vector2.zero;
        value.localPosition = new Vector2(0, 250);
    }
    public void HideMem()
    {
        StartGame();
        mem_1.DOAnchorPos(new Vector2(0, 250), .75f).SetEase(Ease.InOutQuad);
        mem_2.DOAnchorPos(new Vector2(0, 250), .75f).SetEase(Ease.InOutQuad);
        mem_3.DOAnchorPos(new Vector2(0, 250), .75f).SetEase(Ease.InOutQuad);
        mem_4.DOAnchorPos(new Vector2(0, 250), .75f).SetEase(Ease.InOutQuad);
        mem_1.DOScale(0, .75f).SetEase(Ease.Linear);
        mem_2.DOScale(0, .75f).SetEase(Ease.Linear);
        mem_3.DOScale(0, .75f).SetEase(Ease.Linear);
        mem_4.DOScale(0, .75f).SetEase(Ease.Linear);
        backBtn.DOAnchorPos(new Vector2(0, -1300), .75f).SetEase(Ease.InOutQuad);
        foreach (var txt in memName) txt.SetActive(false);
    }
}
