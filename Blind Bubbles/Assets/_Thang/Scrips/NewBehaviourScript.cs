using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    private bool isHoming = false;
    public float homingSpeed = 15f;
    public float detectionRadius = 10f;
    private GameObject targetBall;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!isHoming)
        {
            // Tìm kiếm vật thể có tag "ball" trong bán kính detectionRadius
            Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("ball"))
                {
                    targetBall = collider.gameObject;
                    isHoming = true;
                    break;
                }
            }
        }
        else if (targetBall != null)
        {
            // Tính hướng đến mục tiêu
            Vector3 directionToTarget = (targetBall.transform.position - transform.position).normalized;
            
            // Điều chỉnh vận tốc của bullet
            rb.velocity = directionToTarget * homingSpeed;
            
            // Xoay bullet theo hướng di chuyển
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Vẽ sphere trong Scene view để debug
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}