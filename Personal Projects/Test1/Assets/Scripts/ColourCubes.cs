using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class ColourCubes : MonoBehaviour
{
    public InputActionReference interact;
    public Material mat;
    public GameObject player;
    [SerializeField] private Image _loadingBarSprite;


    private void Start()
    {
        UpdateLoadingBar(0f);
        mat = GetComponent<Renderer>().material;
    }


    public void UpdateLoadingBar(float fillAmount)
    {
        _loadingBarSprite.fillAmount = Mathf.Clamp01(fillAmount);
    }

    private void OnTriggerStay(Collider collider)
    {
        if (interact.action.triggered)
        {
            Debug.Log("Interaction triggered with " + collider.gameObject.name);
            StartCoroutine(ColourChange(2f));
        }
    }

    IEnumerator ColourChange(float holdTime)
    {
        float elapsedTime = 0f;
        while (elapsedTime < holdTime)
        {
            if (interact.action.IsPressed())
            {
                elapsedTime += Time.deltaTime;
                UpdateLoadingBar(elapsedTime / holdTime);
            }
            else
            {
                elapsedTime = 0f; // Reset if the button is released
                UpdateLoadingBar(0f);
                yield break; // Exit the coroutine if the button is released
            }
            yield return null;
        }
        Debug.Log("Hold time reached, changing color");
        mat.color = player.GetComponent<Renderer>().material.color;
        UpdateLoadingBar(0f); 
 
    }
    

}
