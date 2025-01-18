using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Khai báo event tĩnh để theo dõi va chạm của bullet
    public static System.Action<Vector3> OnBulletHit;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("ball"))
        {
            // Khi chạm vào Ball, kích hoạt event
            if (OnBulletHit != null)
            {
                OnBulletHit.Invoke(transform.position);
            }
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}