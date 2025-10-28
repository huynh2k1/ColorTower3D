using System;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

public class Block : MonoBehaviour
{
    public int ID;
    [SerializeField] ParticleSystem _effect;
    [SerializeField] Rigidbody rb;
    [SerializeField] float _maxGravity = -35f;
    [SerializeField] float gravityScale = 5f;
    [SerializeField] GameObject[] listModel;

    private bool hasStartedFall = false;
    private bool hasLanded = false;

    public Action OnBlockPlaced;

    [Header("Patrol")]
    public float patrolSpeed = 1f; //
    [SerializeField] float patrolRange = 2f;
    private bool isPatrolling = false;

    private float leftX = -2f;
    private float rightX = 2f;
    private float timer;

    public void Initialize()
    {
        hasStartedFall = false;
        hasLanded = false;
        rb.isKinematic = true;

        // Spawn ngẫu nhiên tại -2 hoặc 2
        isPatrolling = true;
        timer = transform.position.x == -2 ? 0 : patrolRange; // Đảm bảo hướng đi đúng ngay từ đầu

        int index = UnityEngine.Random.Range(0, listModel.Length);
        listModel[index].SetActive(true);
    }

    private void OnEnable()
    {
        UIGame.OnClickTapAreaButton += HandleTapBlock;
    }

    private void OnDisable()
    {
        UIGame.OnClickTapAreaButton -= HandleTapBlock;
    }

    public void HandleTapBlock()
    {
        if (hasStartedFall) return;

        hasStartedFall = true;
        isPatrolling = false; // ❗ STOP PATROL
        rb.isKinematic = false;

        Physics.gravity = new Vector3(0, -9.81f * gravityScale, 0);
    }

    private void Update()
    {
        if (isPatrolling)
        {
            timer += Time.deltaTime * patrolSpeed;
            float x = Mathf.Lerp(leftX, rightX, Mathf.PingPong(timer, 1f));
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
    }

    private void FixedUpdate()
    {
        if (!hasStartedFall) return;

        if (rb.linearVelocity.y < _maxGravity)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, _maxGravity, rb.linearVelocity.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasLanded) return;

        if (collision.gameObject.CompareTag("Block"))
        {
            Block block = collision.gameObject.GetComponent<Block>();

            if (block != null && ID != block.ID)
            {
                return;
            }
            _effect.Play();
            hasLanded = true;
            //Physics.gravity = new Vector3(0, -9.81f, 0);
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(Vector3.down * 10f);

            SoundManager.I.PlaySoundByType(TypeSound.BLOCKPLACED);
            OnBlockPlaced?.Invoke();
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
