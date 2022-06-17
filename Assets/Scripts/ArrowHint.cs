using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHint : MonoBehaviour
{
    float offset = 0;

    float pingPong = 0;

    public bool IsEnabled;

    // Start is called before the first frame update
    void Start()
    {
        IsEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsEnabled)
        {
            offset = Mathf.PingPong(pingPong, 16) - 8;

            Vector3 newPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + offset, gameObject.transform.position.z);

            gameObject.transform.Translate(newPos);
        }
    }
}
