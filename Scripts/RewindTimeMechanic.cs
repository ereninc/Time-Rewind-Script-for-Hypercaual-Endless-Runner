using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackinTimeMechanics : MonoBehaviour
{

    [SerializeField] private bool isRewinding = false;
    List<PointInTime> pointsInTime;
    Rigidbody rb;
    [SerializeField] private float recordTime =  3f;
    [SerializeField] private bool isAdRetryButtonPressed = false;

    void Start()
    {
        pointsInTime = new List<PointInTime>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            isAdRetryButtonPressed = true;
            StartRewind();
        }
        if(Input.GetKeyUp(KeyCode.Return))
        {
            StopRewind();
        }
    }

    void FixedUpdate() 
    {
        if(isRewinding)
        {
            Rewind();
        }

        else
            Record();
    }

    void Rewind()
    {
        if(pointsInTime.Count>0)
        {
            PointInTime pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            pointsInTime.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }

    void Record()
    {
        if(pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }
        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
    }

    public void StartRewind()
    {
        isRewinding = true;
        rb.isKinematic = true;
    }

    public void StopRewind()
    {
        isRewinding = false;
        rb.isKinematic = false;
    }
}