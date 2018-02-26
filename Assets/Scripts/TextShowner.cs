using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class TextShowner : MonoBehaviour
{
	public string text = "Object";
	public int textSize = 14;
	public Font textFont;
	public Color textColor = Color.white;
	public Vector3 offset = new Vector3(0, 0.5f, 0.5f);
	private Vector3 shadowOffset = new Vector3(1, 1, 1);

	void OnGUI()
	{
		GUI.depth = 9999;

		GUIStyle style = new GUIStyle();
		style.fontSize = textSize;
		style.richText = true;
		if (textFont) style.font = textFont;
		style.normal.textColor = textColor;
		style.alignment = TextAnchor.MiddleCenter;

		GUIStyle shadow = new GUIStyle();
		shadow.fontSize = textSize;
		shadow.richText = true;
		if (textFont) shadow.font = textFont;
		shadow.normal.textColor = Color.black;
		shadow.alignment = TextAnchor.MiddleCenter;

		Vector3 worldPosition = transform.position + offset;
		Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPosition);
		screenPos.y = Screen.height - screenPos.y;

		GUI.Label(new Rect(screenPos.x + shadowOffset.x, screenPos.y + shadowOffset.y, 0, 0), text, shadow);
		GUI.Label(new Rect(screenPos.x, screenPos.y, 0, 0), text, style);
	}
}
