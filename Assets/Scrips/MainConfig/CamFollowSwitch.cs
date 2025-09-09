using UnityEngine;

public class CamFollowSwitch : MonoBehaviour
{
    public Transform target;
    public Transform targetSeek;
    public Transform targetHide;
    //Switch player

    

    public Vector3 offset = new Vector3(0, 6, -9);

    public float smoothSpeed = 5f;
    public bool lookAtTarget = true;

    public Transform Target => target;


     void Start()
    {   
        
    }
    private void Update()
    {
      

    }
    private void LateUpdate()
    {
        
        

        if (target == null) { return; }

        Vector3 desiredPosition = target.position + offset;


        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed*Time.deltaTime);
        transform.position = smoothPosition;
        if (lookAtTarget) 
        {
            transform.LookAt(target);
        }
    }

    public void SetTarget(Transform newTarget){target = newTarget;}

    public void SetOffset(Vector3 newOffset) {  offset = newOffset; }
}
