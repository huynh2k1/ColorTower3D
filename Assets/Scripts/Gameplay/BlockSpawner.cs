using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using System;
using System.Collections;

public class BlockSpawner : MonoBehaviour
{
    public static BlockSpawner I;
    [SerializeField] GroundCheck _groundCheck;
    [SerializeField] Transform _mainCamera;
    [SerializeField] Block _blockPrefab;
    [SerializeField] List<Block> _listBlocks = new List<Block>();
    int _indexSpawn = 0;
    const float _blockSize = 2f;

    int idBlock = 0;

    private void Awake()
    {
        I = this;
    }

    private void OnEnable()
    {
        _groundCheck.OnCollisionBlock += OnBlockGrounded;
    }

    private void OnDestroy()
    {
        _groundCheck.OnCollisionBlock -= OnBlockGrounded;
    }

    public void Initialize()
    {
        _indexSpawn = 0;
        idBlock = 0;
        _groundCheck.Initialize();
        CameraCtrl.I.Initialize();
        ClearBlocksImmediate();
        SpawnNextBlock();
    }

    public void SpawnNextBlock()
    {
        float posY = (_indexSpawn * _blockSize) + (_blockSize * 4f);
        float start = UnityEngine.Random.value > 0.5f ? -2 : 2;
        Vector3 pos = new Vector3(start, posY, 0);

        Block block = Instantiate(_blockPrefab, pos, Quaternion.identity);
        block.patrolSpeed = 1 + (idBlock / 10);
        block.ID = idBlock;
        block.OnBlockPlaced = OnBlockLanded;
        block.Initialize();
        _listBlocks.Add(block);
        _indexSpawn++;
        idBlock++;
    }

    public void OnBlockLanded()
    {
        ScoreCtrl.I.AddScore(1);
        if(_indexSpawn >= 10)
        {
            SoundManager.I.PlaySoundByType(TypeSound.MERGE10);

            _indexSpawn = 0;
            ClearBlocks();
            CameraCtrl.I.MoveCamera(_indexSpawn, _blockSize, SpawnNextBlock);
            return;
        }
        CameraCtrl.I.MoveCamera(_indexSpawn, _blockSize, SpawnNextBlock);
    }

    public void ClearBlocksImmediate()
    {
        foreach (var block in _listBlocks)
        {
            block.Destroy();
        }
        _listBlocks.Clear();

    }

    public void ClearBlocks()
    {
        StartCoroutine(ClearBlocksRoutine());
    }

    private IEnumerator ClearBlocksRoutine()
    {
        int count = _listBlocks.Count;
        if (count == 0)
        {
            yield break;
        }

        float totalTime = 0.5f;
        float delay = totalTime / count;

        for (int i = count - 1; i >= 0; i--)
        {
            _listBlocks[i].Destroy();
            yield return new WaitForSeconds(delay);
        }

        _listBlocks.Clear();
    }

    public void OnBlockGrounded()
    {
        foreach (Block block in _listBlocks)
            block.SetKinematic();
        CameraCtrl.I.MoveToInit(() =>
        {
            GameCtrl.I.GameLose();
        });
    }
}
