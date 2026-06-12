using UnityEngine;

public class WindyAreaScript : MonoBehaviour
{
    ///vars
    /// objects
    [SerializeField]private GameObject playerObj; ///need playerObj to test distance
    [SerializeField]private GameObject windControlObj; //need this to check if the player has an active green lantern
    [SerializeField]private Rigidbody playerRb; //Need to get the player's rigidbody
                                                //to blow the player around


    [SerializeField]private float distanceToPlayer;
    [SerializeField]private float windForceX;
    [SerializeField]private float windForceY;
    [SerializeField]private float windForceZ;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Find the playerObj and its rigidbody
        //This is my most common use for Find
        playerObj = GameObject.Find("PlayerObj");

        //store the player's rigidbody in this variable
        https://docs.unity3d.com/6000.4/Documentation/ScriptReference/GameObject.GetComponent.html

        playerRb = playerObj.GetComponent<Rigidbody>();

    }



    // Update is called once per frame
    void Update()
    {
        ///calculate the distance to the player by getting the transform.position of this and the player
        //https://docs.unity3d.com/6000.4/Documentation/ScriptReference/Vector3.Distance.html
        distanceToPlayer = Vector3.Distance(playerObj.transform.position, transform.position);


        ///Check to see if the player is less than 10m away AND has the red lantern out
        /// https://docs.unity3d.com/6000.4/Documentation/ScriptReference/GameObject-activeInHierarchy.html
    

        if((distanceToPlayer < 10) && (windControlObj.activeInHierarchy == false))
        {
            ///If the above conditions are true,
            //Add force to the rigidbody in the chosen directions
            //https://docs.unity3d.com/6000.4/Documentation/ScriptReference/Rigidbody.AddForce.html

            playerRb.AddForce(windForceX, windForceY, windForceZ, ForceMode.Impulse);
        }      
    }
}
