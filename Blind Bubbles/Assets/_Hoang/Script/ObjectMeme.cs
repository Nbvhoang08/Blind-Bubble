using System;
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
        JumpScare
    }

    [System.Serializable]
    public class MemeData
    {
        public MemeType type;
        public Sprite memeImage; // Ảnh meme
        public VideoClip memeVideo; // Video meme
        public AudioClip ImgSound;
     }

    public MemeData memeData;
   void OnTriggerEnter(Collider other)
   {    
        if(other.CompareTag("Bullet"))
        {
            Subject.NotifyObservers("BublePop",memeData);
        }
   }    
    

}
