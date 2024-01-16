using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LineDrawer : MonoBehaviour
{

    private LineRenderer lineRenderer;
    public PhysicsMaterial2D bounceMaterial;
    private List<int> pressedButtons = new List<int>();
    public List<Transform> points;
    private string sceneName;
    
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        sceneName = SceneManager.GetActiveScene().name;
    }

    private void Update()
    {
        CheckButtons();
    }

    private void CheckButtons()
    {
        // Clear the list of pressed buttons
        pressedButtons.Clear();

        // Check if each button is pressed
        bool button1Pressed = Input.GetKey("1");
        bool button2Pressed = Input.GetKey("2");
        bool button3Pressed = Input.GetKey("3");
        bool button4Pressed = Input.GetKey("4");
        bool button5Pressed = Input.GetKey("5");

        // Add buttons to the list if they are pressed and adjacent
        if (button1Pressed)
        {
            pressedButtons.Add(1);
            if (button2Pressed) pressedButtons.Add(2);
        }
        else if (button2Pressed)
        {
            pressedButtons.Add(2);
            if (button3Pressed) pressedButtons.Add(3);
        }
        else if (button3Pressed)
        {
            pressedButtons.Add(3);
            if (button4Pressed) pressedButtons.Add(4);
        }
        else if (isExpertLevel() && button4Pressed)
        {
            pressedButtons.Add(4);
            if (button5Pressed) pressedButtons.Add(5);
        }

        // Handle the case when exactly two adjacent buttons are pressed
        if (pressedButtons.Count == 2)
        {
            for (int i = 0; i < 2; i++)
            {
                // When button 1 is pressed, we get its value (1) and take it to the array where we have to subtract 1, because it is at position 0
                lineRenderer.SetPosition(i,points[pressedButtons[i] - 1].position);
            }

            EdgeCollider2D collider = GetComponent<EdgeCollider2D>();
            if (collider == null)
            {
                collider = gameObject.AddComponent<EdgeCollider2D>();
            }

            int positionsCount = lineRenderer.positionCount;
            Vector2[] linePoints = new Vector2[positionsCount];

            for (int i = 0; i < positionsCount; i++)
            {
                Vector3 position = lineRenderer.GetPosition(i);
                linePoints[i] = new Vector2(position.x, position.y); // Assuming z-axis is not needed
            }

            collider.points = linePoints;
            collider.sharedMaterial = bounceMaterial;
            Debug.Log("Buttons " + pressedButtons[0] + " and " + pressedButtons[1] + " are pressed and adjacent.");
        }
        else if (pressedButtons.Count > 2)
        {
            // More than two buttons are pressed
            Debug.Log("Error: More than two buttons are pressed.");
        }
    }

    private bool isExpertLevel()
    {
        return sceneName == "ExpertLevel";
    }
}
