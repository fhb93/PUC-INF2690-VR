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

    private float angleLever = 0;

    // Start is called before the first frame update
    void Start()
    {
        Lever = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //Temp
        if((ControlsHydrogen && Input.GetKeyDown(KeyCode.H)) || (ControlsEngine && Input.GetKeyDown(KeyCode.E)))
        {
            UserInput = true;
        }

        if (UserInput)
        {
            UserInput = false;
                                  
            StartCoroutine(LeverCoroutine());
        }
    }

    [SerializeField]
    private float maxSpinAngle = -70f;

    [SerializeField]
    private float minSpinAngle = 60f;

    private bool RotateLeverAngle(bool isOn)
    {
        if (isOn)
        {
            if (Lever.transform.rotation.z < minSpinAngle)
            {
                angleLever += Time.deltaTime;
            }
            else
            {
                return true;
            }
        }
        else
        {
            if (Lever.transform.rotation.z > maxSpinAngle)
            {
                angleLever -= Time.deltaTime;
            }
            else
            {
                return true;
            }
        }

        Lever.transform.RotateAround(LeverSpinPoint.position, Lever.transform.forward, angleLever);

        return false;
    }

    IEnumerator LeverCoroutine()
    {
        int i = 0;

        angleLever = Lever.transform.rotation.z;

        bool hasMaxRotation = false;

        while (i < 200 && hasMaxRotation == false)
        {
            if(ControlsEngine)
            {
                hasMaxRotation = RotateLeverAngle(engine.EngineOn);
            }

            if (ControlsHydrogen)
            {
                hasMaxRotation = RotateLeverAngle(engine.HydrogenOn);
            }

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
