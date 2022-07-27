using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    
    private void Update()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
    }
}
