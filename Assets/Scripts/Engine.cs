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
    private float MaxAcc;

    public bool EngineOn;

    public bool HydrogenOn;

    private Vector3 Force;

    private Rigidbody rb;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (HydrogenOn == false)
        {
            if (EngineOn == false)
            {
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
                if (Acc > 1f)
                {
                    Acc -= 0.1f * Time.deltaTime;
                }
                else
                {
                    Acc = 1f;
                                        
                    Force = Vector3.forward;
                }
            }

            
        }
        else
        {
            if (EngineOn == false)
            {
                if (Acc > 0f)
                {
                    Acc -= 0.1f * Time.deltaTime;
                }
                else
                {
                    Acc = 0f;

                    Force = Vector3.up;
                }

                return;
            }
            else
            {
                if (Acc > 1f)
                {
                    Acc -= 0.1f * Time.deltaTime;
                }
                else
                {
                    Acc = 1f;

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
            if (Acc > 1f)
            {
                Acc -= Time.deltaTime;
            }
            else
            {
                Acc = 1f;

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
                    Force += Vector3.left;
                }
                if(i == (int) Dir.BACK){
                    Force += Vector3.back;
                }
                if(i == (int) Dir.RIGHT){
                    Force += Vector3.right;
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
    }
}
