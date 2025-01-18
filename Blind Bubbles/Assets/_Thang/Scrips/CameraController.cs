using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Transform target;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Transform bulletToFollow;
    public float followSpeed = 5f;
    public float returnSpeed = 2f;
    public Vector3 followOffset = new Vector3(0, 2, -5);
    private bool isFollowingBullet = false;

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        // Đăng ký lắng nghe event từ Bullet
        Bullet.OnBulletHit += OnBulletHitHandler;
    }

    void OnDestroy()
    {
        // Hủy đăng ký event khi destroy
        Bullet.OnBulletHit -= OnBulletHitHandler;
    }

    // Hàm xử lý khi bullet va chạm
    private void OnBulletHitHandler(Vector3 hitPoint)
    {
       
        isFollowingBullet = false;
        // Remove the following line to stop the camera from returning to the original position
        // StartCoroutine(ReturnToOriginalPosition());
    }

    public void StartFollowingBullet(GameObject bullet)
    {
        if (bullet == null) return;
        
        bulletToFollow = bullet.transform;
        isFollowingBullet = true;
        StartCoroutine(FollowBullet());
    }

    private IEnumerator FollowBullet()
    {
        while (isFollowingBullet && bulletToFollow != null)
        {
            Vector3 targetPosition = bulletToFollow.position + followOffset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);
            transform.LookAt(bulletToFollow);
            yield return null;
        }
    }

    private IEnumerator ReturnToOriginalPosition()
    {
        float elapsedTime = 0;
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;

        while (elapsedTime < 1)
        {
            elapsedTime += Time.deltaTime * returnSpeed;
            transform.position = Vector3.Lerp(startPosition, originalPosition, elapsedTime);
            transform.rotation = Quaternion.Lerp(startRotation, originalRotation, elapsedTime);
            yield return null;
        }

        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }
}
