using UnityEngine;
using System.Collections.Generic;

public class BubbleMovement : MonoBehaviour
{
    public float floatStrength = 1f; // Độ dao động của bóng
    public float floatSpeed = 1f;    // Tốc độ dao động của bóng
    public float riseSpeed = 2f;     // Tốc độ bay lên của bóng
    public float targetHeight = 5f;  // Độ cao mục tiêu ngẫu nhiên
    public Material[] materials;     // Danh sách các Material

    private Vector3 startPos;
    private bool reachedTargetHeight = false;

    // Danh sách các Material đã được sử dụng
    private static List<Material> usedMaterials = new List<Material>();

    void Start()
    {
        startPos = transform.position;
       
        AssignRandomMaterial(); // Gán Material ngẫu nhiên khi bắt đầu
    }

    void Update()
    {
        if (!reachedTargetHeight)
        {
            // Bay lên đến độ cao mục tiêu
            transform.position += Vector3.up * riseSpeed * Time.deltaTime;
            if (transform.position.y >= startPos.y + targetHeight)
            {
                reachedTargetHeight = true;
                startPos = transform.position; // Thiết lập lại vị trí bắt đầu cho hiệu ứng dao động
            }
        }
        else
        {
            // Hiệu ứng dao động tại chỗ
            transform.position = startPos + new Vector3(0.0f, Mathf.Sin(Time.time * floatSpeed) * floatStrength, 0.0f);
        }
    }

    void AssignRandomMaterial()
    {
        if (materials.Length == 0) return; // Kiểm tra xem danh sách Material có rỗng không
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            Material chosenMaterial = null;

            // Tạo danh sách tạm thời gồm các Material chưa sử dụng
            List<Material> availableMaterials = new List<Material>();
            foreach (Material mat in materials)
            {
                if (!usedMaterials.Contains(mat))
                {
                    availableMaterials.Add(mat);
                }
            }

            // Nếu không còn Material nào chưa sử dụng
            if (availableMaterials.Count == 0)
            {
                Debug.LogError("All materials have been used in other projects!");
                return;
            }

            // Chọn Material ngẫu nhiên từ danh sách tạm thời
            int randomIndex = Random.Range(0, availableMaterials.Count);
            chosenMaterial = availableMaterials[randomIndex];

            // Gán Material ngẫu nhiên cho MeshRenderer
            renderer.material = chosenMaterial;
            // Thêm Material đã sử dụng vào danh sách
            usedMaterials.Add(chosenMaterial);
        }
        else
        {
            Debug.LogError("MeshRenderer component not found!");
        }
    }
}
