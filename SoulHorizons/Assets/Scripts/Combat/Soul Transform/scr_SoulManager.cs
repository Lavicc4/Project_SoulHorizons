using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Update the UI, take in user input, and manage information on the available soul transforms
/// </summary>
public class scr_SoulManager : MonoBehaviour {

    public List<SoulTransform> soulTransforms = new List<SoulTransform>(); //make sure that there are the same number of transforms and buttons in the inspector
    public  List<Button> buttons = new List<Button>();
    private SoulTransform currentTransform = null; //a reference to whatever transformation the player is currently in
    private bool transformed = false;
    private scr_Entity player; // a referenct to the player

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<scr_Entity>();
    }

	void Start () {
		//Set all the button sprites according to the soul transforms given

        //add an on click to the button that triggers the transform
        //Example: btn2.onClick.AddListener(delegate {TaskWithParameters("Hello"); });
        int i = 0;
        foreach (SoulTransform item in soulTransforms)
        {
            buttons[i].onClick.AddListener(delegate {Transformation(item); });
        }

        //when this is done, each button should have an on click event which triggers one of the transforms
	}
	
	// Update is called once per frame
	void Update () {
		UserInput();
	}

    /// <summary>
    /// Get keyboard input
    /// </summary>
    private void UserInput()
    {   
        //this is used only for keyboard input
        //TODO: need to take charge into account once we start charging these
        switch (scr_InputManager.K_SoulFusion())
        {
            case 1:
                buttons[0].onClick.Invoke();
                break;
            case 2:
                buttons[1].onClick.Invoke();
                break;
            case 3:
                buttons[2].onClick.Invoke();
                break;
            default:
                break; //returned 0, so none were pressed
        }
    }

    /// <summary>
    /// Replace the default list of transforms with passed-in arguments. Use this on start when we start using save data to set the transforms
    /// </summary>
    /// <param name="transforms"></param>
    public void SetSoulTransforms(params SoulTransform[] transforms)
    {
        soulTransforms = new List<SoulTransform>(transforms.Length);


    }

    /// <summary>
    /// This should be added to the corresponding button to listen for the onClick event.
    /// This method performs the transformation process with the given argument
    /// </summary>
    /// <param name="soul"></param>
    private void Transformation(SoulTransform soul)
    {
        //TODO: do we need to take into account the possibility that the player is already in a different transform?

        //disable the player attack and movement

        //add the transform's attack and movement
        //QUESTION: Add and remove components or enable and disable components as needed? In that case, we would need to add the components for all of the transforms to the player on Start

        currentTransform = soul;
        transformed = true;
        StartCoroutine(ShieldDrain());
    }

    /// <summary>
    /// Called when the player's shield runs out and they revert back to their default state
    /// </summary>
    private void EndTransformation()
    {
        //remove or disable the current transform's components on the player

        //enable the player default attack and movement

        transformed = false;
    }

    /// <summary>
    /// Started when the player transforms, continues as long as the player is transformed and still has a shield
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShieldDrain()
    {
        while (transformed)
        {
            yield return new WaitForSeconds(1f);
            //decrement the shield using currentTransform
            player._health.temp_hp -= currentTransform.getShieldDrainRate();

            //end the transformation if the shield hits 0
            if (player._health.temp_hp <= 0)
            {
                player._health.temp_hp = 0;
                EndTransformation();
            }
        }
    }

}
