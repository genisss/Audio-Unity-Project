using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Canvas))]
public class CanvasMoleController: MonoBehaviour
{

    public float RotationLerpSpeed = 5f;

    private Animator canvasAnim;
    public GameObject pointsPanel, moreTimePanel, lessTimePanel;
    public TMPro.TextMeshProUGUI timeText, pointsText;


    // The canvas that is attached to this object.
    private Canvas _canvas;

    // The camera this object will be in front of.
    public Camera _camera;

    void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _camera = Camera.main;
        canvasAnim = GetComponent<Animator>();

        // Disable this component if
        // it failed to initialize properly.
        if (_canvas == null)
        {
            Debug.LogError("Error: HeadposeCanvas._canvas is not set, disabling script.");
            enabled = false;
            return;
        }
        if (_camera == null)
        {
            Debug.LogError("Error: HeadposeCanvas._camera is not set, disabling script.");
            enabled = false;
            return;
        }
    }

    void Update()
    {
       
        // Rotate the object to face the camera.
        float rotSpeed = Time.deltaTime * RotationLerpSpeed;
        Quaternion rotTo = Quaternion.LookRotation(_camera.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotTo, rotSpeed);
    }

    public void openDecreaseTimePanel()
    {

        pointsPanel.SetActive(false);
        moreTimePanel.SetActive(false);
        lessTimePanel.SetActive(true);
        canvasAnim.SetTrigger("open");

    }
 
    public void openPointsPanel(int coins)
    {
        pointsText.SetText("+" + coins);
        pointsPanel.SetActive(true);
        moreTimePanel.SetActive(false);
        lessTimePanel.SetActive(false);

        canvasAnim.SetTrigger("open");

    }

    public void closeAllPanels()
    {
        pointsPanel.SetActive(false);
        moreTimePanel.SetActive(false);
        lessTimePanel.SetActive(false);
    }
}

