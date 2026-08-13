using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class CapsuleController : MonoBehaviour
{

    //TODO:
    // FIX THE DISTANCE SLIDER
    // work on docking detection
    // do the roll and yaw controls
    // do Mathf.Movetowards() for all movement to make it smooth
    // if you crash into the station, the company loses money?
    // add random failures to the capsule
    // add a 1% chance of equipment having a catastrphic falure when used, like you increase the throttle and the thruster explodes, or you use RCS down and it breaks and will go until out of fuel and you have to fix that with counter-thrust and the control panel
    // add a control panel, when on click, you open up a menu where you can deactiate components, like RCS left, or throttle down:
        //
        //                                                  THE CAPSULE 
        //                                                        /\
        //                                                       |  | <------- RCS: view below (later it is the menu below the "RCS:") (Button(Deactivate) means there is a button titled deactivate there)
        //                                   FUEL (42%) -------> |  |
        //                                              [][][][]-    -[][][][]
        //                                                       |  |  <- ANGLE CONTROLS (later the angle controller menu)
        //     MAIN THRUSTER BACK [DEACTIVE] Button(Activate) ->  \/   <- MAIN THRUSTER FORWARD [ACTIVE] Button(Deactivate)
        //                                                        ||
        //
        //           /=========RCS CONTROLER==========\
        //          | LEFT [ACTIVE] Button(Deactivate) |
        //          | RIGHT [ACTIVE] Button(Deactivate)|
        //          | UP [ACTIVE] Button(Deactivate)   |
        //          | DOWN [ACTIVE] Button(Deactivate) |
        //          \==================================/        
        //          
        //          /========ANGLE CONTROLLER=========\
        //          | ROLL[ACTIVE] Button(Deactivate)  |
        //          | PITCH [ACTIVE] Button(Deactivate)|
        //          | YAW [ACTIVE] Button(Deactivate)  |
        //          \==================================/
    // change the random placement, make it more speratic, and add random rotations when pitch/yaw/roll is added
    // make it so the marker can't leave the screen, and make it so you can't see above/below it (make it always show you the same face)
    // make the rigidbody work and you can't faze through the docking station, maybe scrap the ai prefab and make the "capsule" be another marker?
    // get the start button working
    // make a todo list vscode plugin?
    // add an ETA (distance is in meters and speed is m/s, so it will be easy)
    // add the starting text like: 
        // "The launch required more fuel than expected; starting fuel = 75" 
        // or "The launch required less fuel than expected; starting fuel = 250"
        // or "The angle when exiting the atmoshere was not ideal, fox the orientation of the capsule to dock with the station"
        // or "The main thruster is not operational, make sure you don't get too slow or you won't be able to reach the station"


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
        distanceSlider.maxValue = 1;
        Startdist = Vector3.Distance(transform.position, target.transform.position);
        fuel = Random.Range(75, 250); // random fuel, no launch is the same
        fuelSlider.maxValue = fuel;
        setDistanceSlider();
        setFuelSlider();
    }
    void Update()
    {
        dist = Vector3.Distance(transform.position, target.transform.position);
        speedText.text = "Speed: " + fwdSpeed.ToString("F2") + " m/s";
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
    void setRandomLoc()
    {
        transform.position += new Vector3(Random.Range(-250, 250), Random.Range(-250, 250), 0);
        // add the random rotations later
    }
}