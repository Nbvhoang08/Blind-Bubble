using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ProcessSystem : MonoBehaviour, IObserver 
{
    void Awake()
    {
        Subject.RegisterObserver(this);
    }
    void OnDestroy()
    {
        Subject.UnregisterObserver(this); 
    }
    public RawImage VideoDisPlay;
    public VideoPlayer videoPlayer;
    public Image ImgDisPlay;    
    public List<GameObject> DisPlayItem;

    public void OnNotify(string eventName, object eventData)
    {
        if(eventName =="BublePop")
        {
             if (eventData is ObjectMeme.MemeData data) // Kiểm tra và ép kiểu
            {
                ProcessEvent(data); // Gửi data vào hàm xử lý
            } 
        }
    }          
     public void ProcessEvent(ObjectMeme.MemeData data)
    {
        switch (data.type)
        {
            case ObjectMeme.MemeType.Image:
                ShowMemeImage(data.memeImage, data.ImgSound);
                break;

            case ObjectMeme.MemeType.Video:
                PlayMemeVideo(data.memeVideo);
                break;
        }
    }

    private void PlayMemeVideo(VideoClip memeVideo)
{
    if (memeVideo == null)
    {
        return;
    }

    ActiveDisPlayItem(0);

    // Gán clip video
    videoPlayer.clip = memeVideo;
    videoPlayer.renderMode = VideoRenderMode.RenderTexture;
    videoPlayer.isLooping = false;

    // Phát video
    videoPlayer.Play();

    // Lấy thời gian của video và bắt đầu coroutine với thời gian chờ
    StartCoroutine(CloseAllItems(memeVideo.length));
}

private IEnumerator CloseAllItems(double delay)
{
    // Chờ thời gian tương ứng với độ dài video
    yield return new WaitForSeconds((float)delay);

    // Thực hiện hành động đóng các item
    // Đặt logic của bạn ở đây
    Debug.Log("Đã đóng tất cả các items sau thời gian video chạy.");
}

   private void ShowMemeImage(Sprite memeImg ,AudioClip ImgSound)                                                                                                                                                                
    {

        if (ImgDisPlay != null)
        {
            ActiveDisPlayItem(1);
            ImgDisPlay.sprite = memeImg;

        }
        if(ImgSound != null )
        {
            SoundManager.Instance.PlayVFXSound(ImgSound);
        }
        StartCoroutine(CloseAllItems());
    }
    private void TriggerJumpScare()
    {
        // if (jumpScarePrefab != null)
        // {
        //     Instantiate(memeData.jumpScarePrefab, transform.position, Quaternion.identity);
        // }
    }

    private void ActivateGameEvent()
    {
        // if (memeData.gameEventObject != null)
        // {
        //     memeData.gameEventObject.SetActive(true);
        // }
    }

    public void ActiveDisPlayItem(int index)
    {
        for(int i =  0 ; i <= DisPlayItem.Count-1;i++)
        {
            if(i == index)
            {
                DisPlayItem[i].SetActive(true);
            }else
            {
                DisPlayItem[i].SetActive(false);
            }
        }
    }
    IEnumerator  CloseAllItems()
    {
        yield return new WaitForSeconds(10);
        //CloseAll();
    }
    public void CloseAll()
    {
        for(int i =  0 ; i <= DisPlayItem.Count-1;i++)
        {
            DisPlayItem[i].SetActive(false);    
        }
    }
}
     


