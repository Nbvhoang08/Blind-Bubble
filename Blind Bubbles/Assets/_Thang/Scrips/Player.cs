using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator ani;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float throwForce = 20f;
    public float arcHeight = 5f;
    [SerializeField] private bool isWind = false; // Thêm biến kiểm tra có gió hay không
    [SerializeField] private Vector3 windDirection = Vector3.right; // Hướng gió (mặc định là sang phải)
    [SerializeField] private float windStrength = 5f; // Độ mạnh của gió

    private Vector3 targetPosition;
    private bool hasTarget = false;
    [SerializeField] private bool isThrowing = false;
    [SerializeField] private bool isTurning = false;

    void Start()
    {
        ani = GetComponent<Animator>();
        isThrowing = false;
        isTurning = false;
    }

    void Update()
    {
        ani.SetBool("throw", isThrowing);
        ani.SetBool("turn", isTurning);

        // Nhấn chuột trái để bắt đầu
        if (Input.GetMouseButtonDown(0) && !isThrowing && !isTurning)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                SetTarget(hit.point);
                StartTurn();
            }
        }

        // Toggle wind với phím Space (để test)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isWind = !isWind;
            Debug.Log("Wind is " + (isWind ? "enabled" : "disabled"));
        }
    }

    public void SetTarget(Vector3 position)
    {
        targetPosition = position;
        hasTarget = true;
    }

    public void CompleteThrow()
    {
        Debug.Log("completeThrow");
        isThrowing = false;
        isTurning = true;
        StartCoroutine(RotateOverTime(180));
    }

    public void StartTurn()
    {
        isTurning = true;
        isThrowing = true;
        StartCoroutine(RotateOverTime(180));
    }

    public float turnSpeed = 1.0f;

    public void CompleteTurn()
    {
        if (isTurning) return;
    }

    private System.Collections.IEnumerator RotateOverTime(float angle)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, transform.eulerAngles.y + angle, 0);

        float elapsedTime = 0f;
        float duration = Mathf.Abs(angle / turnSpeed);
        Debug.Log(startRotation + " " + targetRotation);
        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
        isTurning = false;
    }

    [SerializeField] private CameraController cameraController;

    void SpawnBullet()
{
    if (!isThrowing || !hasTarget) return;

    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    Debug.Log("Bullet spawned at: " + bullet.transform.position);
    
    Rigidbody rb = bullet.GetComponent<Rigidbody>();
    
    // Thêm HomingBullet component
    bullet.AddComponent<HomingBullet>();

    Vector3 throwVelocity = CalculateThrowVelocity(firePoint.position, targetPosition, arcHeight, throwForce);
    rb.velocity = throwVelocity;

    if (cameraController != null)
    {
        Debug.Log("Starting camera follow");
        cameraController.StartFollowingBullet(bullet);
    }
    else
    {
        Debug.LogError("Camera Controller is not assigned!");
    }

    Destroy(bullet, 3f);
}

    private Vector3 CalculateThrowVelocity(Vector3 start, Vector3 target, float height, float speed)
    {
        Vector3 direction = target - start;
        Vector3 directionXZ = new Vector3(direction.x, 0, direction.z);

        float distance = directionXZ.magnitude;
        float yOffset = direction.y;

        float vX = directionXZ.normalized.x * speed;
        float vZ = directionXZ.normalized.z * speed;
        float vY = (yOffset + height) / distance * speed;

        return new Vector3(vX, vY, vZ);
    }

    void OnMouseDown()
    {
        if (!isThrowing && !isTurning)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                SetTarget(hit.point);
                StartTurn();
            }
        }
    }
}

// Thêm class mới để xử lý hiệu ứng gió cho bullet
