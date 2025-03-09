using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.globalVar;

public class Bullet : MonoBehaviour
{
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private float _progress;
    [SerializeField] private float _speed = 40f;
    public GlobalData globalVar;
    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
        // gameObject.SetActive(false);
        // anim = gameObject.GetComponent<Animator>();  
    }


    // Update is called once per frame
    void Update()
    {
        _progress += Time.deltaTime * _speed;
        transform.position = Vector3.Lerp(_startPosition, _targetPosition, _progress);
    }

    public void SetTragetPosition(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
    }
}
