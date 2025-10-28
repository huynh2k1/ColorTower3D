using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float _maxGravity = -35f;
    [SerializeField] float gravityScale = 5f;

    private bool hasStartedFall = false;
    private bool hasLanded = false;

    public Action OnBlockPlaced;
    public Action OnBlockGrounded;

    public void Initialize()
    {
        hasStartedFall = false;
        hasLanded = false;
        rb.isKinematic = true;
    }

    private void Update()
    {
        // Tap để bắt đầu rơi
        if (Input.GetMouseButtonDown(0) && !hasStartedFall)
        {
            hasStartedFall = true;
            rb.isKinematic = false;

            // Tăng gravity cho block để rơi nhanh hơn
            Physics.gravity = new Vector3(0, -9.81f * gravityScale, 0);
        }
    }

    private void FixedUpdate()
    {
        if (!hasStartedFall) return;

        // Giới hạn tốc độ rơi
        if (rb.linearVelocity.y < _maxGravity)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, _maxGravity, rb.linearVelocity.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasLanded) return;

        // Khi chạm tháp hoặc ground thì dừng
        if (collision.gameObject.CompareTag("Block"))
        {
            hasLanded = true;
            Physics.gravity = new Vector3(0, -9.81f, 0);
            rb.linearVelocity = Vector3.zero;
            rb.isKinematic = true;
            rb.isKinematic = false;

            OnBlockGrounded?.Invoke();
        }
    }

    public void SetKinematic()
    {
        rb.isKinematic = true;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
