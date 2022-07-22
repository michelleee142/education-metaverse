using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class CompInteractor : MonoBehaviour
{
    [SerializeField] GameObject homeScreen;
    [SerializeField] GameObject testScreen;
    [SerializeField] TMP_Dropdown materialsList;
    [SerializeField] TMP_Text selectedMaterial;
    [SerializeField] GameObject sample;

    MeshRenderer sampleRenderer;

    private int materialIndex = 0;

    private string[] materials = new string[] { "Original Steel", "Normalized Steel", "Quenched Steel"};

    // Start is called before the first frame update
    void Start()
    {
        // set home screen
        homeScreen.SetActive(true);
        testScreen.SetActive(false);

        // get renderer of sample
        sampleRenderer = sample.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Perform test
    public void run_test()
    {
        // switch to test screen
        homeScreen.SetActive(false);
        testScreen.SetActive(true);

        // save selected material
        materialIndex = materialsList.value;

        // set material text to selected material
        selectedMaterial.text = "Selected material: " + materials[materialIndex];

        // change material color on machine to selected material
        switch (materialIndex)
        {
            // original steel - blue
            case 0:
                sampleRenderer.material.color = Color.blue;
                break;

            // normalized steel - green
            case 1:
                sampleRenderer.material.color = Color.green;
                break;

            // quenched steel - red
            case 2:
                sampleRenderer.material.color = Color.red;
                break;

            // default - return to blue
            default:
                sampleRenderer.material.color = Color.blue;
                break;
        }

        // perform tests

    }

    // Return home after test completion
    public void return_home()
    {
        // switch to home screen
        testScreen.SetActive(false);
        homeScreen.SetActive(true);

        // return machine back to starting state
    }

}
