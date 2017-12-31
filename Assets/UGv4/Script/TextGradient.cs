using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 該效果為 UGUI Text 文字顏色漸層效果. 
/// </summary>
[AddComponentMenu( "UI/Effects/Gradient" )]
public class TextGradient : BaseMeshEffect
{
	public static Color32 topColor = Color.white;
    public Color32 bottomColor = Color.black;

    public override void ModifyMesh( VertexHelper vh )
	{
		float bottomY = -1;
		float topY = -1;

		for ( int i = 0; i < vh.currentVertCount; i++ )
		{
			UIVertex v = new UIVertex();
			vh.PopulateUIVertex( ref v, i );

			if ( bottomY == -1 ) 
				bottomY = v.position.y;
			if ( topY == -1 ) 
				topY = v.position.y;

			if ( v.position.y > topY ) 
				topY = v.position.y;
			else if ( v.position.y < bottomY ) 
				bottomY = v.position.y;
		}


		float uiElementHeight = topY - bottomY;

		for ( int i = 0; i < vh.currentVertCount; i++ )
		{
			UIVertex v = new UIVertex();
			vh.PopulateUIVertex( ref v, i );

			v.color = Color32.Lerp( bottomColor, topColor, (v.position.y - bottomY) / uiElementHeight );
			vh.SetUIVertex( v, i );
		}
	}

}