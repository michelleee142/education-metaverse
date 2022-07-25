using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class CompInteractor : MonoBehaviour
{
    [SerializeField] GameObject welcomeScreen;
    [SerializeField] GameObject homeScreen;
    [SerializeField] GameObject testScreen;
    [SerializeField] TMP_Dropdown materialsList;
    [SerializeField] TMP_Text selectedMaterial;

    private string[] materials = new string[] { "Original Steel", "Normalized Steel", "Quenched Steel"};
    private GameObject activeScreen;
    private int materialIndex = 0;

    public InputActionReference compPower = null;

    // Toggle computer screen on when user clicks primary button
    void Awake()
    {
        compPower.action.started += compPowerToggle;
    }

    // Toggle computer screen off when user clicks primary button
    void onDestroy()
    {
        compPower.action.started -= compPowerToggle;
    }

    // Start is called before the first frame update
    void Start()
    {
        // keep labs screens off initially
        homeScreen.SetActive(false);
        testScreen.SetActive(false);

        // set current active screen to home
        activeScreen = homeScreen;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Toggle computer screen
    private void compPowerToggle(InputAction.CallbackContext context)
    {
        bool isActive = !activeScreen.activeSelf;

        activeScreen.SetActive(isActive);
        welcomeScreen.SetActive(!isActive);

    }

    // Perform test
    public void run_test()
    {
        // switch to test screen
        homeScreen.SetActive(false);
        testScreen.SetActive(true);
        activeScreen = testScreen;

        // save selected material
        materialIndex = materialsList.value;

        // set material text to selected material
        selectedMaterial.text = "Selected material: " + materials[materialIndex];

        // perform tests

    }

    // Return home after test completion
    public void return_home()
    {
        // switch to home screen
        testScreen.SetActive(false);
        homeScreen.SetActive(true);
        activeScreen = homeScreen;

        // return machine back to starting state
    }

}
