using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class CapsuleController : MonoBehaviour
{

    //TODO:
    // do the sliders + text for fuel and distance to target
    // do the text for speed left
    // work on docking detection
    // do the fuel functionality
    // do the roll and yaw controls
    // do Mathf.Movetowards() for all movement to make it smooth
    // FIX THE DISTANCE SLIDER

    [Header("Text")]
    public TMP_Text rotationText;
    public TMP_Text speedText;


    [Header("Sliders")]
    public Slider fuelSlider;
    public Slider distanceSlider;


    [Header("Misc. Vars")]
    public float fuel = 100f; // fuel amount
    public float Startdist; // starting distance to target
    public float dist; // distance to target
    public GameObject target; // where the capsule docks at
    public InputSystem_Actions input;
    public float rotation = 0f; // rotation of the capsule
    public float speed = 600f; // speed of the capsule movement
    public float fwdSpeed = 0f; // speed of the capsule forward movement
    
    [Header("-Movement Controls-")]
        [Header("RCS controls")]
        public InputAction Leh; // left
        public InputAction Rih; // right
        public InputAction Uhp; // up
        public InputAction Doh; // down

        [Header("Thruster controls")] // ADD THESE
        public InputAction Fwd; // forward -  e
        public InputAction Bck; // backward - q

        [Header("Roll Controls")] // ADD THESE
        public InputAction RolLf; // roll left - left arrow
        public InputAction RolRt; // roll right - right arrow

        [Header("Yaw Controls")] // ADD THESE
        public InputAction YawLf; // yaw left - , (<)
        public InputAction YawRt; // yaw right - . (>)
        public InputAction YawUp; // yaw up - up arrow
        public InputAction YawDn; // yaw down - down arrow


    public void Awake()
            {
                input = new InputSystem_Actions();
                Leh = input.Player.Left;
                Rih = input.Player.Right;
                Uhp = input.Player.Up;
                Doh = input.Player.Down;
                Fwd = input.Player.Forward;
                Bck = input.Player.Back;
                RolLf = input.Player.RollLeft;
                RolRt = input.Player.RollRight;
                YawLf = input.Player.YawLeft;
                YawRt = input.Player.YawRight;
                YawUp = input.Player.YawUp;
                YawDn = input.Player.YawDown;
            }

            public void OnEnable()
            {
                Leh.Enable();
                Rih.Enable();
                Uhp.Enable();
                Doh.Enable();
                Fwd.Enable();
                Bck.Enable();
                RolLf.Enable();
                RolRt.Enable();
                YawLf.Enable();
                YawRt.Enable();
                YawUp.Enable();
                YawDn.Enable();
            }
            public void OnDisable()
            {
                Leh.Disable();
                Rih.Disable();
                Uhp.Disable();
                Doh.Disable();
                Fwd.Disable();
                Bck.Disable();
                RolLf.Disable();
                RolRt.Disable();
                YawLf.Disable();
                YawRt.Disable();
                YawUp.Disable();
                YawDn.Disable();
            }

    // Update is called once per frame
    void Start()
    {
        fuelSlider.maxValue = 100;
        distanceSlider.maxValue = 1;
        //distanceSlider.maxValue = 100;
        Startdist = Vector3.Distance(transform.position, target.transform.position);
        fuel = Random.Range(75, 250); // random fuel, no launch is the same
    }
    void Update()
    {
        dist = Vector3.Distance(transform.position, target.transform.position);
        speedText.text = "Speed: " + fwdSpeed.ToString("F2");
        rotationText.text = "Rotation: " + rotation.ToString("F2");
        setFuelSlider();
        setDistanceSlider();
        transform.position += new Vector3(0,0,1) * fwdSpeed * Time.deltaTime;
        if(Leh.IsPressed())
        {
            if(fuel > 0)
            {
                transform.position += new Vector3(-100, 0, 0) * Time.deltaTime;
                fuel -= 0.01f;
            }
        }
        if(Rih.IsPressed())
        {
            if(fuel > 0)
            {
                transform.position += new Vector3(100, 0, 0) * Time.deltaTime;
                fuel -= 0.01f;
            }
        }
        if(Uhp.IsPressed())
        {
            if(fuel > 0)
            {
                transform.position += new Vector3(0, 100, 0) * Time.deltaTime;
                fuel -= 0.01f;
            }
        }
        if(Doh.IsPressed())
        {
            if(fuel > 0)
            {
                transform.position += new Vector3(0, -100, 0) * Time.deltaTime;
                fuel -= 0.01f;
            }
        }
        if(Fwd.IsPressed())
        {
            if(fuel > 0)
            {
                fwdSpeed = Mathf.MoveTowards(fwdSpeed, fwdSpeed + 1, Time.deltaTime);
                fuel -= 4 * Time.deltaTime;
            }
        }
        if(Bck.IsPressed())
        {
            if(fuel > 0)
            {
                fwdSpeed = Mathf.MoveTowards(fwdSpeed, fwdSpeed - 1, Time.deltaTime);
                fuel -= 4 * Time.deltaTime;
            }
        }
        // later add the roll and yaw controls here
    }

    void setFuelSlider()
    {
        fuelSlider.value = fuel;
    }
    void setDistanceSlider()
    {
        distanceSlider.value = Startdist / dist; // get the % of distance to target, then make it into a slider value
    }
}
