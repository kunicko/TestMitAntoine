using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMovementScript : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void Update()
    {
        Move();
    }
    public void Move()
    {
        transform.Translate(_speed * Time.deltaTime * Vector2.left);
    }
}
