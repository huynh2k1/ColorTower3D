using System;
using System.Linq;
using System.Threading;
using Unity.Android.Gradle;
using UnityEngine;
using UnityEngine.EventSystems;

public class Block : MonoBehaviour
{
    public int ID;
    [SerializeField] ParticleSystem _effect;
    [SerializeField] Rigidbody rb;
    const float _maxGravity = -25f;
    const float _gravityScale = 3f;
    [SerializeField] GameObject[] listModel;

    private bool hasStartedFall = false;
    private bool hasLanded = false;

    public Action OnBlockPlaced;

    [Header("Patrol")]
    public float patrolSpeed = 6f; //
    public const float leftX = -3f;
    public const float rightX = 3f;

    private bool isPatrolling = false;
    Vector3 targetPos;

    public void Initialize(float posY)
    {
        hasStartedFall = false;
        hasLanded = false;
        rb.isKinematic = true;

        InitRandomModel();
        InitPatrol(posY);
    }

    public void InitPatrol(float posY)
    {
        //float randomX = UnityEngine.Random.Range(leftX, rightX);
        float randomX = (UnityEngine.Random.value >= 0.5f) ? rightX : leftX;
        transform.position = new Vector3(randomX, posY, transform.position.z);

        if (UnityEngine.Random.value >= 0.5f)
        {
            targetPos = new Vector3(leftX, transform.position.y, transform.position.z);
        }
        else
        {
            targetPos = new Vector3(rightX, transform.position.y, transform.position.z);
        }

        isPatrolling = true;
    }

    void InitRandomModel()
    {
        int index = UnityEngine.Random.Range(0, listModel.Length);
        listModel[index].SetActive(true);
    }

    private void OnEnable()
    {
        GamePanel.OnClickTapAreaButton += RaiseTapBlock;
    }

    private void OnDisable()
    {
        GamePanel.OnClickTapAreaButton -= RaiseTapBlock;
    }

    public void RaiseTapBlock()
    {
        if (hasStartedFall) return;

        hasStartedFall = true;
        isPatrolling = false; // ❗ STOP PATROL
        rb.isKinematic = false;

        Physics.gravity = new Vector3(0, -9.81f * _gravityScale, 0);
    }

    private void Update()
    {
        if (isPatrolling == false)
            return;
        // Di chuyển tới target
        transform.position = Vector3.MoveTowards(transform.position, targetPos, patrolSpeed * Time.deltaTime);

        // Nếu đến biên thì đổi hướng
        if (Mathf.Abs(transform.position.x - targetPos.x) < 0.05f)
        {
            if (targetPos.x == leftX)
                targetPos = new Vector3(rightX, transform.position.y, transform.position.z);
            else
                targetPos = new Vector3(leftX, transform.position.y, transform.position.z);
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
            if (block != null && ID != block.ID + 1)
            {
                return;
            }
            _effect.Play();
            hasLanded = true;
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(Vector3.down * 10f);

            SoundControl.I.PlaySoundByType(SoundType.BLOCKPLACED);
            OnBlockPlaced?.Invoke();
        }
    }

    public void SetKinematic()
    {
        rb.isKinematic = true;
    }

    public void Destroy()
    {
        OnBlockPlaced = null;
        Destroy(gameObject);
    }
}
