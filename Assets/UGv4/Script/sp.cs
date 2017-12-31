using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sp : MonoBehaviour {
    public Transform thumb_bri;
    bool dragging_bri=false;
    float inputData;
    HSBColor toShowColor;
    public GameObject show;
    float brightness;
    float br_tmp=1;
    void Update () {

        if (Input.GetMouseButton(0) && dragging_bri) 
        {
            inputData = Input.mousePosition.x;
            float scale = this.transform.localScale.y;
            float loc = this.transform.position.x;
            float distWithCanvas = 27.5f;
            float sliderWidth = 30;

            float point = Mathf.Clamp(inputData, (loc-distWithCanvas)-sliderWidth*(scale-1), (loc+distWithCanvas)+sliderWidth*(scale-1));       //266, 321
            thumb_bri.position =new Vector2(point, thumb_bri.position.y);

            brightness = Map(inputData, (loc-distWithCanvas)-sliderWidth*(scale-1), (loc+distWithCanvas)+sliderWidth*(scale-1), 1, 0);
            br_tmp = Mathf.Clamp(brightness, 0, 1);
            // print("brightness " + br_tmp);
        }

        //print("rgb " + uiPanel.forAll);
        toShowColor = HSBColor.FromColor(uiPanel.forAll);
        //print("hsb " + toShowColor.ToString());

        toShowColor.b = br_tmp;
        show.GetComponent<Image>().color = toShowColor.ToColor();
    }

   public void isClicked()
    {
        dragging_bri = true;
    }
    public void release()
    {
        dragging_bri = false;
    }

    public float Map(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
}
