using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffColor : MonoBehaviour
{
    [SerializeField] Light _Light;
    public Color[] _Color;
    int colorIndex;
    void Start()
    {
        StartCoroutine(ChangeForSecondColor());
    }

    private void Update()
    {
       // StartCoroutine(ChangeForSecondColor());
    }
    private IEnumerator ChangeForSecondColor()
    {
        Color newColor = _Color[colorIndex+1];
        Debug.Log(colorIndex);
        _Light.color = Color.Lerp(_Color[colorIndex], newColor, 1f*Time.deltaTime);
        yield return new WaitForSeconds(3f);
        colorIndex++;

        if (colorIndex == 2)
        {
            colorIndex = 0;
        }
        yield return StartCoroutine(ChangeForSecondColor());
    }
}
