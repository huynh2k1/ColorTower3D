using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using System;
using System.Collections;
using Unity.Android.Gradle;

public class BlockSpawner : MonoBehaviour
{
    public static BlockSpawner I;
    [SerializeField] GroundCheck _groundCheck;
    [SerializeField] Transform _mainCamera;
    [SerializeField] Block _blockPrefab;
    [SerializeField] List<Block> _listBlocks = new List<Block>();
    int _indexSpawn = 0;
    const float _blockSize = 2f;

    Block _currentBlock;

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
        CameraCtrl.I.Initialize();
        ClearBlocksImmediate();
        SpawnNextBlock();
    }

    public void SpawnNextBlock()
    {
        float posY = (_indexSpawn * _blockSize) + (_blockSize * 5f);
        Vector3 pos = new Vector3(0, posY, 0);

        Block block = Instantiate(_blockPrefab, pos, Quaternion.identity);
        block.OnBlockGrounded = OnBlockLanded;
        block.Initialize();
        _listBlocks.Add(block);
        _indexSpawn++;
    }

    public void OnBlockLanded()
    {
        ScoreCtrl.I.AddScore(1);
        if(_indexSpawn >= 10)
        {
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
