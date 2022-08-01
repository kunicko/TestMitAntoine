using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManagerScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> _pipes;

    [Header ("Bounds")]
    [SerializeField] private float _minY;
    [SerializeField] private float _maxY;
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    [SerializeField] private float _objectDistanceToBounds;

    [Header("Settings")]
    [SerializeField] private float _pipeDistanceToEachOther;
    [SerializeField] private float _spawnOffset;

    private int _currentPipe = 0;
    private int _furthestPipe = 0;
    private float _despawnX;

    private void Awake()
    {
        _despawnX = transform.position.x - Mathf.Abs(_minX) - _spawnOffset;
        SpawnPipe();
    }
    private void Update()
    {
        if (_pipes[_currentPipe].transform.position.x <= transform.position.x + Mathf.Abs(_maxX) - _pipeDistanceToEachOther + _spawnOffset)
        {
            _currentPipe = _currentPipe < _pipes.Count - 1 ? _currentPipe + 1 : 0;
            SpawnPipe();
        }
        if(_pipes[_furthestPipe].transform.position.x <= _despawnX)
        {
            DespawnPipe();
            _furthestPipe = _furthestPipe < _pipes.Count - 1 ? _furthestPipe + 1 : 0;
        }
    }
    private void SpawnPipe()
    {
        if (_pipes[_currentPipe].activeSelf) return;
        float x = transform.position.x + Mathf.Abs(_maxX) + _spawnOffset;
        float maxY = transform.position.y + Mathf.Abs(_maxY) - _objectDistanceToBounds;
        float minY = transform.position.y - Mathf.Abs(_minY) + _objectDistanceToBounds;
        Debug.Log("min:" + minY + " max:" + maxY);
        float y = Random.Range(minY, maxY);
        _pipes[_currentPipe].transform.position = new Vector3(x, y, transform.position.z);
        _pipes[_currentPipe].SetActive(true);
    }
    private void DespawnPipe()
    {
        _pipes[_furthestPipe].SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        #region Bounds
        //min/max X relativ zum Object
        float minX = transform.position.x - Mathf.Abs(_minX);
        float maxX = transform.position.x + Mathf.Abs(_maxX);
        //min/max Y relativ zum Object
        float minY = transform.position.y - Mathf.Abs(_minY);
        float maxY = transform.position.y + Mathf.Abs(_maxY);
        // Vectoren vor the 4 Corners
        Vector3 topLeft = new Vector3(minX, maxY);
        Vector3 topRight = new Vector3(maxX, maxY);
        Vector3 bottomLeft = new Vector3(minX, minY);
        Vector3 bottomRight = new Vector3(maxX, minY);
        //Draw Cube with four Lines
        Gizmos.color = Color.red;
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topLeft, bottomLeft);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomLeft, bottomRight);

        //ObjectDistanceToBounds
        Vector3 topLeftObject = new Vector3(minX, maxY - _objectDistanceToBounds);
        Vector3 topRightObject = new Vector3(maxX, maxY - _objectDistanceToBounds);
        Vector3 bottomLeftObject = new Vector3(minX, minY + _objectDistanceToBounds);
        Vector3 bottomRightObject = new Vector3(maxX, minY + _objectDistanceToBounds);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(topLeftObject, topRightObject);
        Gizmos.DrawLine(bottomLeftObject, bottomRightObject);
        #endregion

        #region Pipes

        int pipecount = (int)((topRight.x - topLeft.x) / _pipeDistanceToEachOther);
        Gizmos.color = Color.green;
        for (int i = 1; i < pipecount; i++)
        {
            Vector3 top = new Vector3(maxX - (i * _pipeDistanceToEachOther), maxY);
            Vector3 bottom = new Vector3(maxX - (i * _pipeDistanceToEachOther), minY);

            Gizmos.DrawLine(top, bottom);
        }
        #endregion

        #region Spawnpoint
        Vector3 spawnpoint = new Vector3(maxX + _spawnOffset, transform.position.y);
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(spawnpoint, new Vector3(1,1,1));
        #endregion
    }
}
