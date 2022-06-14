using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinLever : MonoBehaviour
{
    public Transform Disc { get; private set; }

    public GameObject Lever { get; private set; }

    public Transform LeverSpinPoint { get; private set; }

    [SerializeField]
    private AudioSource audioSource;

    private bool IsTurnedOn;

    public bool UserInput;

    private float angle;

    private float angleLever;


    // Start is called before the first frame update
    void Start()
    {
        Lever = GameObject.Find("Lever3");

        LeverSpinPoint = gameObject.transform;

        Disc = GameObject.Find("Cube_Cube_Disc").transform;

        audioSource = GetComponentInParent<AudioSource>();

        audioSource.loop = false;

        audioSource.playOnAwake = false;
    }

    
   

    IEnumerator LeverCoroutine()
    {
        if(audioSource.isPlaying)
        {
            yield return null;
        }

        int i = 0;

        while(i < 1024)
        {
            angleLever += 0.5f * Time.deltaTime;

            Lever.transform.RotateAround(LeverSpinPoint.position, Lever.transform.forward, angleLever);

            i++;

            yield return new WaitForSeconds(0.01f);
        }

        audioSource.Play();

        IsTurnedOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (UserInput)
        {
            UserInput = false;

            StartCoroutine(LeverCoroutine());
        }

        if(IsTurnedOn)
        {
            angle += 0.5f * Time.deltaTime;

            Disc.RotateAround(Disc.position, Disc.up, angle);

            if(audioSource.isPlaying == false)
            {
                IsTurnedOn = false;
            }
        }
    }
}
