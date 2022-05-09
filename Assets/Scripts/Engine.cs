using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{

    [SerializeField]
    private float Mass;

    [SerializeField]
    private float Acc;

    [SerializeField]
    [Range(1f, 4f)]
    private float MaxAcc;

    public bool EngineOn;

    public bool HydrogenOn;

    private Vector3 Force;

    private Vector3 AuxForce;

    private Rigidbody rb;

    //Roation Angle
    private float RotationAmount = 0;

    [Range(1f, 4f)]
    public float RateUp = 1f;

    private Vector3 m_EulerAngleVelocity;

    [SerializeField]
    private AudioSource audioSource;

    public enum Dir{
        FRONT,
        LEFT,
        BACK,
        RIGHT,
        UP,
        DOWN,
    }

    public bool[] InputDirection = new bool[6];

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.mass = Mass;

        audioSource = GetComponent<AudioSource>();

        m_EulerAngleVelocity = new Vector3(0, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (HydrogenOn == false)
        {
            if (EngineOn == false)
            {

                if (audioSource.volume > 0f)
                {
                    audioSource.volume -= Time.deltaTime;
                }
                else
                {
                    audioSource.volume = 0f;
                }

                if (Acc > 0f)
                {
                    Acc -= 0.1f * Time.deltaTime;
                }
                else
                {
                    Acc = 0f;

                    Force = Vector3.zero;
                }

                return;
            }
            else
            {
                if (audioSource.volume < 1f)
                {
                    audioSource.volume += Time.deltaTime;
                }
                else
                {
                    audioSource.volume = 1f;
                }

                if (Acc > MaxAcc)
                {
                    Acc -= 0.1f * Time.deltaTime;
                }
                else
                {
                    Acc = MaxAcc;
                                        
                    Force = Vector3.forward;
                }
            }

            
        }
        else
        {
            if (EngineOn == false)
            {
                if(audioSource.volume > 0f)
                {
                    audioSource.volume -= Time.deltaTime;
                }
                else
                {
                    audioSource.volume = 0f;
                }

                if (Acc > 0f)
                {
                    Acc -= 0.1f * Time.deltaTime;
                }
                else
                {
                    Acc = 0f;

                    Force = Vector3.up * RateUp;
                }

                return;
            }
            else
            {
                if (audioSource.volume < 1f)
                {
                    audioSource.volume += Time.deltaTime;
                }
                else
                {
                    audioSource.volume = 1f;
                }

                if (Acc > MaxAcc)
                {
                    
                    Acc -= 0.1f * Time.deltaTime;
                }
                else
                {
                    Acc = MaxAcc;

                    Force = Vector3.forward + 4f * Vector3.up * Acc;

                    Force = Force.normalized;
                }
            }
        }

        InputDirection[(int) Dir.FRONT] = Input.GetKey("up");
        InputDirection[(int) Dir.LEFT] = Input.GetKey("left");
        InputDirection[(int) Dir.BACK] = Input.GetKey("down");
        InputDirection[(int) Dir.RIGHT] = Input.GetKey("right");
     //   InputDirection[(int)Dir.UP] = Input.GetKey(KeyCode.Y);
        InputDirection[(int)Dir.DOWN] = Input.GetKey(KeyCode.U);

        bool noInput = true;

        for(int i = 0; i < 6; i++)
        {
            if(i != 0 && InputDirection[i] == true)
            {
                noInput = false;
                break;
            }
        }

        if(noInput == true)
        {
            if (Acc >= MaxAcc)
            {
                Acc -= Time.deltaTime;
            }
            else
            {
                Acc = MaxAcc;

                Force = Vector3.forward;
            }
        }
    }        

    void FixedUpdate()
    {
        if(EngineOn == false && HydrogenOn == false)
        {
            return;
        }

        if(HydrogenOn == true)
        {
            //Force += 1.2f * Vector3.up;
                        
            rb.velocity = Force;

            if (EngineOn == false)
            {
                return;
            }
        }
        

        for(int i = 0; i < 6; i++)
        {
            if(InputDirection[i] == true)
            {
                if(Acc < MaxAcc)
                {
                    Acc += 2f * Time.deltaTime;
                }
                else{
                    Acc = MaxAcc;
                }

                if(i == (int) Dir.FRONT){
                    Force += Vector3.forward;
                }
                if(i == (int) Dir.LEFT){
                    Rotation(Vector3.left);
                    Force += Vector3.forward;
                    Force += Vector3.left * 0.25f;
                }
                if(i == (int) Dir.BACK){

                    Force += Vector3.back;
                }
                if(i == (int) Dir.RIGHT){
                    Rotation(Vector3.right);
                    Force += Vector3.forward;
                    Force += Vector3.right * 0.25f;
                }

                if (i == (int)Dir.DOWN)
                {
                    Force += Vector3.down;
                }

                Force = Force.normalized;

                Force = Force * Mass * Acc;
            }
        }

        rb.velocity = Force;
       // gameObject.transform.position = rb.position;
    }
    

    void Rotation(Vector3 dir)
    {
        //// Smoothly tilts a transform towards a target rotation.
        //float tiltAroundY = dir.x * tiltAngle * Time.fixedDeltaTime;

        //if (dir.x > 0)
        //{
        //    tiltAngle += 0.01f;
        //}
        //else
        //{
        //    tiltAngle -= 0.01f;
        //}
        //// Rotate the cube by converting the angles into a quaternion.
        //Quaternion target = Quaternion.Euler(0, tiltAroundY, 0);


        //// Dampen towards the target rotation
        ////transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
        if(dir.x > 0)
        {
            RotationAmount = Time.fixedDeltaTime;
        }
        else
        {
            RotationAmount = -Time.fixedDeltaTime;
        }

        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * RotationAmount);

        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}
