using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnim : MonoBehaviour
{
    public bool XAnim = true;
    public bool YAnim = false;
    public Vector3 TargetPos;
    public float speed = 1;
    // Start is called before the first frame update
   void Update()
    {
       if(XAnim)
       {
           if(transform.localPosition.x<TargetPos.x)
           {
               transform.Translate(-Vector3.right * speed);
           }
           else
           {
               transform.localPosition = new Vector3(TargetPos.x, transform.localPosition.y, transform.localPosition.z);
           }
       }
       if (YAnim)
       {
           if (transform.localPosition.y > TargetPos.y)
           {
               transform.Translate(Vector3.up * speed);
           }
           else
           {
               transform.localPosition = new Vector3(transform.localPosition.x, TargetPos.y, transform.localPosition.z);
           }
       }
    }

   
}
