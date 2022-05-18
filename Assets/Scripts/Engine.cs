using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.InputManagerEntry;

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

    [SerializeField]
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

    private bool IsFirstTimeEngineOn = true;

    private static Quaternion forwardDirection = Quaternion.identity;
    private static Quaternion rightDirection = Quaternion.FromToRotation(Vector3.forward, Vector3.right);
    private static Quaternion leftDirection = Quaternion.FromToRotation(Vector3.forward, Vector3.left);
    private static Quaternion backDirection = Quaternion.FromToRotation(Vector3.forward, Vector3.back);

    private Vector3 backPosition;

    public Vector3 RotationForce;

    private float maxTimer = 0.4f;

    public enum Dir{
        FRONT,
        LEFT,
        BACK,
        RIGHT,
        UP,
        //DOWN,
    }

    [Range(-1, 1)]
    public float[] InputDirection = new float[4];

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.mass = Mass;

        audioSource = GetComponent<AudioSource>();

        m_EulerAngleVelocity = new Vector3(0, 1, 0);

        backPosition = GameObject.Find("ForcesTarget").transform.position;
        
        for(int i = 0; i < InputDirection.Length; i++)
        {
            InputDirection[i] = 0f;
        }

        //Time.timeScale = 10f;
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
                if(IsFirstTimeEngineOn == true)
                {
                    IsFirstTimeEngineOn = false;

                    Force += Vector3.forward * MaxAcc;
                }

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
                                        
                    //Force = Vector3.forward;
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
                if (IsFirstTimeEngineOn == true)
                {
                    IsFirstTimeEngineOn = false;

                    //Force += Vector3.forward * MaxAcc;
                }

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
                    
                    //Force = Vector3.forward * Acc +

                    //Force = Force.normalized;
                }
            }
        }

        InputDirection[(int) Dir.FRONT] = Mathf.Clamp(Input.GetAxis("Vertical"), 0, 1);
        
        InputDirection[(int) Dir.LEFT] = Mathf.Clamp(Input.GetAxis("Horizontal"), -1, 0);

        InputDirection[(int) Dir.BACK] = Mathf.Clamp(Input.GetAxis("Vertical"), -1, 0);

        InputDirection[(int) Dir.RIGHT] = Mathf.Clamp(Input.GetAxis("Horizontal"), 0, 1);

        bool noInput = true;

        for(int i = 0; i < 4; i++)
        {
            if(i != 0 && InputDirection[i] != 0f)
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

                //Force += Vector3.forward;

                //Force = Force.normalized;
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
                Force = transform.up * RateUp;

                return;
            }
            else
            {
                Force = transform.forward + transform.up * RateUp;
            }
        }
        else
        {
            if (EngineOn == true)
            {
                Force = transform.forward * constant2;
            }
        }

        rb.AddForce(Force * Acc);
        


        for (int i = 0; i < 4; i++)
        {
            if (InputDirection[i] != 0f)
            {
                if (Acc < MaxAcc)
                {
                    Acc += 2f * Time.deltaTime;
                }
                else
                {
                    Acc = MaxAcc;
                }
            }

            if(InputDirection[i] > 0f)
            {
                if (i == (int)Dir.FRONT)
                {
                    //TODO usar transform.Forward
                }
                else if (i == (int)Dir.RIGHT)
                {
                    
                    Rotation(Vector3.right);
                }
            }
            else if(InputDirection[i] < 0f)
            {
                if (i == (int)Dir.LEFT)
                {
                    Rotation(Vector3.left);
                }
                else if (i == (int)Dir.BACK)
                {

                    Force = Vector3.back;
                }
            }
        }


    }

    public float constant = 0f;
    public float constant2 = 0f;

    void Rotation(Vector3 dir)
    {
        //timer += Time.fixedDeltaTime;

        //if(timer > maxTimer)
        //{
        //    return;
        //}
        //RotationForce = Vector3.zero;

        if (dir.x > 0)
        {
            //Multiplicar por uma constante e testar no inspector
            RotationForce = Vector3.left;
        }
        else
        {
            //Multiplicar por uma constante e testar no inspector
            RotationForce = Vector3.right;
        }

        //rb.AddTorque(RotationForce * Acc * 10000); //.rotation = Quaternion.Euler(0, RotationForce.x, 0);

       // rb.AddTorque(Vector3.right * 100000 * Input.GetAxis("Horizontal"));

        //multiplicar pela constante do inspector
        rb.AddForceAtPosition(RotationForce * constant, backPosition);

        Force = rb.velocity;
    }
}
