using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.EventSystems;
public class uiPanel : MonoBehaviour {

	public GameObject colorPanel;
	public Image show;
	public Transform thumb;
	public bool fixX;
	public bool fixY;
	public GameObject cube;
	public float offZ;
	void Update () {

		if(Input.GetKey("up"))
		{
			cube.transform.position += new Vector3(0,0.01f,0);
		}
		if(Input.GetKey("down"))
		{
			cube.transform.position += new Vector3(0,-0.01f,0);
		}
		if(Input.GetKey("left"))
		{
			cube.transform.position += new Vector3(-0.01f,0,0);
		}
		if(Input.GetKey("right"))
		{
			cube.transform.position += new Vector3(0.01f,0,0);
		}

		RaycastHit hit;
		Ray  ray = new Ray(cube.transform.position, cube.transform.forward);
		if(Physics.Raycast(ray, out hit, 100) )
		{
			// print("hit");
			var point = hit.point;
			SetThumbPosition(point);
		}

	}

	void SetThumbPosition(Vector3 point)
	{
		Vector3 temp = thumb.localPosition;
		thumb.position = point;
		thumb.localPosition = new Vector3(fixX ? temp.x : thumb.localPosition.x, fixY ? temp.y : thumb.localPosition.y, thumb.localPosition.z+offZ);
		getImageColor(thumb.localPosition);
		showImageColor(getImageColor(thumb.localPosition));
	}

	//取得滑鼠點選UI物件的RGBA
	Color getImageColor(Vector2 point)
	{
		// Transform hitUIObject = ColorPanel.transform;
		Vector2 rectPostion = mousePosToImagePos(point);
		Sprite _sprite = colorPanel.GetComponent<UnityEngine.UI.Image>().sprite;
		Rect rect = colorPanel.GetComponentInParent<RectTransform>().rect;
		Color imageColor = _sprite.texture.GetPixel( Mathf.FloorToInt(rectPostion.x * _sprite.texture.width / (rect.width)), 
													 Mathf.FloorToInt(rectPostion.y * _sprite.texture.height / (rect.height)));
		return imageColor;
	}
	// 取得滑鼠座標轉換至圖片座標 
	Vector2 mousePosToImagePos(Vector2 point)
	{
		Vector2 ImagePos = Vector2.zero;
		Rect rect = colorPanel.GetComponentInParent<RectTransform>().rect;
		ImagePos.x = point.x - colorPanel.transform.position.x + rect.width * 0.5f;
		ImagePos.y = point.y - colorPanel.transform.position.y + rect.height * 0.5f;

		return ImagePos;
	}
	void showImageColor(Color Color)
	{
		print (Color.ToString("F2"));
		show.color =  new Color(Color.r, Color.g, Color.b, 1);
	}
}
