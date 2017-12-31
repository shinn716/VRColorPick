using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.EventSystems;
public class uiPanel : MonoBehaviour {

	public GameObject SprayTopContent;
	public GameObject DecalsContent;
	bool rightclick_br=false;
	bool leftclick_br=false;
	bool rightclick_de=false;
	bool leftclick_de=false;	
	float value_br=37.4f;
	float value_de=-124;
	float targetPos_br;
	float targetPos_de;	
	float f_smooth = 0.1f;
	public GameObject colorPanel;
	public Image showAim;
	bool _isClicked=false;
	public GameObject decalPanel;
	int brushloc=0;
	int decalsloc=0;

    public GameObject brightPanel;
    public static Color forAll;
	void Start(){
		targetPos_br = value_br;
		targetPos_de = value_de;
	}
	void Update () {

		getPixelByMouseClick();

		if(rightclick_br)
		{
			value_br += ((targetPos_br)-value_br)*f_smooth;
			SprayTopContent.transform.localPosition = new Vector3(value_br, SprayTopContent.transform.localPosition.y, SprayTopContent.transform.localPosition.z);
		}

		if(leftclick_br)
		{
			value_br += ((targetPos_br)-value_br)*f_smooth;
			SprayTopContent.transform.localPosition = new Vector3(value_br, SprayTopContent.transform.localPosition.y, SprayTopContent.transform.localPosition.z);
		}

		if(rightclick_de)
		{
			value_de += ((targetPos_de)-value_de)*f_smooth;
			DecalsContent.transform.localPosition = new Vector3(value_de, DecalsContent.transform.localPosition.y, DecalsContent.transform.localPosition.z);
		}

		if(leftclick_de)
		{
			value_de += ((targetPos_de)-value_de)*f_smooth;
			DecalsContent.transform.localPosition = new Vector3(value_de, DecalsContent.transform.localPosition.y, DecalsContent.transform.localPosition.z);
		}

		if(brushloc==3)
		{
			decalPanel.SetActive(true);
		}
		else{
			decalPanel.SetActive(false);
		}

	}
	// void openDe(){
	// 	decalPanel.SetActive(true);
	// }
	// void otherBtn(){
	// 	decalPanel.SetActive(false);		
	// }
	void getPixelByMouseClick()
	{
		if(Input.GetMouseButton(0) && _isClicked) showImageColor(getImageColor(colorPanel));
	}

	//取得滑鼠點選UI物件的RGBA
	Color getImageColor(GameObject ColorPanel)
	{
		Transform hitUIObject = ColorPanel.transform;
		Vector2 rectPostion = mousePosToImagePos(ColorPanel);
		Sprite _sprite = hitUIObject.GetComponent<UnityEngine.UI.Image>().sprite;
		Rect rect = hitUIObject.GetComponentInParent<RectTransform>().rect;
		Color imageColor = _sprite.texture.GetPixel(Mathf.FloorToInt(rectPostion.x * _sprite.texture.width / (rect.width)), Mathf.FloorToInt(rectPostion.y * _sprite.texture.height / (rect.height)));
		return imageColor;
	}
	// 取得滑鼠座標轉換至圖片座標 
	Vector2 mousePosToImagePos(GameObject colorPanel)
	{
		//fixme: 目前圖片有透過Transform做縮放後索引的色彩會有錯誤，應該可以透過下面的資訊調整...
		//imageScale = hitUIObject.GetComponent<RectTransform>().localScale;
		Transform hitUIObject = colorPanel.transform;
		Vector2 ImagePos = Vector2.zero;
		Rect rect = hitUIObject.GetComponentInParent<RectTransform>().rect;
		ImagePos.x = Input.mousePosition.x - hitUIObject.position.x + rect.width * 0.5f;
		ImagePos.y = Input.mousePosition.y - hitUIObject.position.y + rect.height * 0.5f;
		return ImagePos;
	}
	void showImageColor(Color Color)
	{
		// print (Color.ToString("F2"));
		showAim.color =  new Color(Color.r, Color.g, Color.b, 1);
        TextGradient.topColor = Color;
        forAll = Color;
        //brightPanel.GetComponent<TextGradient>().topColor = Color;
        // showAim.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }

	public void onColorPanel()
	{
		_isClicked=true;
	}
	public void outColorPanel()
	{
		_isClicked=false;
	}
	public void rightArrow_br(){
		if(targetPos_br>=30) { }
		else
		{
			rightclick_br=true;
			targetPos_br += +30;
			brushloc--;
			// print(brushloc);
			// print("right " + rightclick_br + " " + targetPos_br);
		}
	}
	public void leftArrow_br(){	
		if(targetPos_br<-30f) { }
		else{
			leftclick_br=true;
			targetPos_br += -30;
			brushloc++;
			// print("brushloc " + brushloc);	
			// print("left " + leftclick_br + " " + targetPos_br);			
		}

	}
	public void rightArrow_de(){
		if(targetPos_de>=-44.5) { }
		else
		{
			rightclick_de=true;
			targetPos_de += 44.5f;
			decalsloc++;
			// print("decalsloc " + decalsloc);
			// print("de right " + rightclick_de + " " + targetPos_de);
		}
	}

	public void leftArrow_de(){
		if(targetPos_de<=-44.5*4) { }
		else
		{
			leftclick_de=true;
			targetPos_de += -44.5f;
			decalsloc--;
			// print("decalsloc " + decalsloc);
			// print("de left " + leftclick_de + " " + targetPos_de);
		}

	}
}
