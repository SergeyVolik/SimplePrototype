using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestQuestion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var s1 = "a" + "bccba";//string.Format("{0}{1}", "abc", "cba");
        var s3 = "abccba";
        var s2 = "abc" + "cba";
      

        Debug.Log(s1 == s2);

         Debug.Log((object)s1 == (object)s2);
        Debug.Log(s2 == s3);
        Debug.Log((object)s2 == (object)s3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
