using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinControlLever : MonoBehaviour
{
    public GameObject Lever { get; private set; }

    [SerializeField]
    private Transform LeverSpinPoint;

    public bool UserInput;

    [SerializeField]
    private Engine engine;

    [SerializeField]
    private bool ControlsHydrogen;

    [SerializeField]
    private bool ControlsEngine;

    // Start is called before the first frame update
    void Start()
    {
        Lever = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (UserInput)
        {
            UserInput = false;
                                  
            StartCoroutine(LeverCoroutine());
        }
    }

    IEnumerator LeverCoroutine()
    {
        int i = 0;

        float angleLever = 0;

        while (i < 120)
        {
            if(ControlsEngine)
            {
                if(engine.EngineOn)
                {
                    angleLever += Time.deltaTime;
                }
                else
                {
                    angleLever -=  Time.deltaTime;
                }
            }

            if (ControlsHydrogen)
            {
                if (engine.HydrogenOn)
                {
                    angleLever += Time.deltaTime;
                }
                else
                {
                    angleLever -= Time.deltaTime;
                }
            }

            Lever.transform.RotateAround(LeverSpinPoint.position, Lever.transform.forward, angleLever);

            i++;

            yield return new WaitForSeconds(0.01f);
        }

        if(ControlsHydrogen)
        {
            engine.HydrogenOn = !engine.HydrogenOn;
        }

        if(ControlsEngine)
        {
            engine.EngineOn = !engine.EngineOn;
        }
    }
}
