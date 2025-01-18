using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video; // Thêm để hỗ trợ phát video

public class ObjectMeme : MonoBehaviour
{
    public enum MemeType
    {
        Image,
        Video,
        JumpScare,
        GameEvent
    }

    [System.Serializable]
    public class MemeData
    {
        public MemeType type;
        public Sprite memeImage; // Ảnh meme
        public VideoClip memeVideo; // Video meme
        public GameObject jumpScarePrefab; // Prefab jump scare
        public GameObject gameEventObject; // Event trong game
    }

    public MemeData memeData;


    // Hàm này sẽ được gọi khi click vào object
    private void OnMouseDown()
    {
        Subject.NotifyObservers("BublePop",memeData);
        
    }
   
    private void TriggerJumpScare()
    {
        if (memeData.jumpScarePrefab != null)
        {
            Instantiate(memeData.jumpScarePrefab, transform.position, Quaternion.identity);
        }
    }

    private void ActivateGameEvent()
    {
        if (memeData.gameEventObject != null)
        {
            memeData.gameEventObject.SetActive(true);
        }
    }

    // private IEnumerator ResetMeme()
    // {
    //     yield return new WaitForSeconds(5f);
    //     isMemeActive = false;
    // }
}
