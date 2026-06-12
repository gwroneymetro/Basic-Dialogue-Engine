using UnityEngine;

public class PuddleRMB2 : MonoBehaviour
{
    ///vars
    


    ///teleporter prefab gameobjects


    ///Store the Game Object that hits this object's Collider
    ///Serialized mostly for debugging purposes
    [SerializeField] private GameObject objToTeleport;

    ///Store the Game Object you're teleporting to
    [SerializeField] private GameObject teleportTarget;

    ///Make a vector3 that stores the teleportTarget's
    /// transform.position
    private Vector3 targPos;


    ///var for this gameobject's collider, which we will have to toggle on and
    /// off to prevent teleport loops
    private Collider thisCollider;


    private Collider targetCollider;    

    ///timer variable to keep track of how long it's been since the last teleport
    /// and if we should turn the collider back on, serialized for debugging
    [SerializeField] private float timer;


    private void Awake()
    {
        thisCollider = GetComponent<Collider>(); //assign thisCollider to the GameObject's collider
    }


    // Update is called once per frame
    void Update()
    {
        ///find the exit portal GameObject 
        //if there is one and store it as teleportTarget
        //Only returns active game objects
        //It's usually a bad idea to use Find in update, because it's resource-intensive,
        //but used in this limited manner in a scene that doesn't have tons and tons of
        //game objects in it shouldn't be too bad
        //still, be careful doing this
        //relevant documentation:
        ///https://docs.unity3d.com/6000.4/Documentation/ScriptReference/GameObject.Find.html

        teleportTarget = GameObject.Find("PuddleRMB1(Clone)"); 
        targPos = teleportTarget.transform.position; //store the transform.position of the out location

        TurnColliderBackOnAfterTeleport(); //turns the collider back on after 3 seconds
    }

    private void OnTriggerEnter(Collider other)
    {
        //If the player bumped into our collider and PuddleRMB2 exists,
        if((other.tag == "Player") && (teleportTarget.name == "PuddleRMB1(Clone)"))
        {



            ///Without this line, the player would teleport back and forth between the two puddles
            /// every frame, because it will instantly hit the other collider and trigger this
            /// script on the other puddle. So we get the collider with Getcomponent, 
            /// store it into the targetCollider variable, 
            /// then disable the collider on the destination puddle
            /// 
            /// https://docs.unity3d.com/6000.4/Documentation/ScriptReference/GameObject.GetComponent.html
            targetCollider = teleportTarget.GetComponent<Collider>();
            targetCollider.enabled = false;
            

            //Get the other collider's GameObject and store it as ObjToTeleport
            objToTeleport = other.gameObject; 

            //Teleport the object you just collided with to the out location
            objToTeleport.transform.position = targPos;
        }
    }

    private void TurnColliderBackOnAfterTeleport()
    {
        ///Check to see if the collider is disabled
        if(thisCollider.enabled == false)
        {
        ///this is how you make a timer
            //every frame increase the timer time by Time.deltaTime
            //https://docs.unity3d.com/6000.4/Documentation/ScriptReference/Time-deltaTime.html
        timer += Time.deltaTime;

            if(timer>=3)
            {
                //Turn the collider back on and reset the timer to zero
                //https://docs.unity3d.com/6000.4/Documentation/ScriptReference/Collider-enabled.html
                thisCollider.enabled = true;
                timer = 0;
            }
        }
    }
}
