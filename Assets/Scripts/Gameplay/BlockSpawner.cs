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
    }

    public void SpawnNextBlock()
    {
        float posY = (_indexSpawn * _blockSize) + (_blockSize * 3f);

        Block block = Instantiate(_blockPrefab);
        //block.patrolSpeed = 1 + (idBlock / 10);
        block.ID = idBlock;
        block.OnBlockPlaced = OnBlockLanded;
        block.Initialize(posY);
        _listBlocks.Add(block);
        _indexSpawn++;
        idBlock++;
    }

    public void OnBlockLanded()
    {
        ScoreCtrl.I.AddScore(1);
        if(_indexSpawn >= 10)
        {
            SoundControl.I.PlaySoundByType(SoundType.MERGE10);

            _indexSpawn = 0;
            ClearBlocks(SpawnNextBlock);
            CameraCtrl.I.MoveCamera(_indexSpawn, _blockSize);
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

    public void ClearBlocks(Action actionDone = null)
    {
        StartCoroutine(ClearBlocksRoutine(actionDone));
    }

    private IEnumerator ClearBlocksRoutine(Action actionDone = null)
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
        actionDone?.Invoke();
    }

    public void OnBlockGrounded()
    {
        foreach (Block block in _listBlocks)
            block.SetKinematic();
        CameraCtrl.I.MoveToInit(() =>
        {
            GameCtrl.I.Defeat();
        });
    }
}
