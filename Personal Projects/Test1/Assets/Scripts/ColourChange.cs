using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class ColourChange : MonoBehaviour
{

    [SerializeField] GameObject targetObject;
    public InputActionReference changeColourAction;
    public Color previousColor {get; private set; }
    public Material mat { get; private set; }

    public List<Color> colors = new List<Color>
    {
    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mat = targetObject.GetComponent<Renderer>().material;
    }

    public void ChangeColour()
    {
        Color newColor = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        mat.color = newColor;
        colors.Add(newColor);
        
    }

}
