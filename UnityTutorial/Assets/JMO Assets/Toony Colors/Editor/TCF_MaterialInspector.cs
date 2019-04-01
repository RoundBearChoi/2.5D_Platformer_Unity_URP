// Toony Colors Free
// (c) 2012,2016 Jean Moreno

//Enable this to display the default Inspector (in case the custom Inspector is broken)
//(It will also remove the Toony Colors Pro 2 links)
//#define SHOW_DEFAULT_INSPECTOR

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class TCF_MaterialInspector : MaterialEditor
{
	public override void OnInspectorGUI()
	{
		if(!this.isVisible)
		{
			return;
		}

		GUILayout.Label("Toony Colors Free", EditorStyles.boldLabel);

		Separator();
		
		base.OnInspectorGUI();

	#if SHOW_DEFAULT_INSPECTOR
		return;
	#endif

		GUILayout.Space(4);
		SeparatorLine();

		GUILayout.Label("Want more features?", EditorStyles.boldLabel);
		Rect r = GUILayoutUtility.GetRect(new GUIContent("Outlines, bump, specular, transparency, sketch effect, reflection probes, mobile versions..."), EditorStyles.wordWrappedMiniLabel);
		r.y -= 6;
		GUI.Label(r, "Outlines, bump, specular, transparency, sketch effect, reflection probes, mobile versions...", EditorStyles.wordWrappedMiniLabel);
		string color = EditorGUIUtility.isProSkin ? "#27abff" : "#0040dd";
		GUILayout.Label(string.Format("Check out <b><color={0}>Toony Colors Pro+Mobile 2</color></b> and give your game the <i>ultimate stylized look</i>!", color), RichTextLabel);

		GUILayout.Space(4);
		EditorGUILayout.BeginHorizontal();

		if(GUILayout.Button("View Web Demo", EditorStyles.miniButtonLeft))
		{
			Application.OpenURL(@"http://www.jeanmoreno.com/toonycolorspro/?ref=tcf");
		}
		if(GUILayout.Button("View on Asset Store", EditorStyles.miniButtonRight))
		{
			Application.OpenURL("com.unity3d.kharma:content/8105");
		}

		EditorGUILayout.EndHorizontal();
		GUILayout.Space(4);
		Separator();
		GUILayout.Space(8);
	}

	//--------------------------------------------------------------------------------------------------
	// GUI Utilities

	static public GUIStyle _LineStyle;
	static public GUIStyle LineStyle
	{
		get
		{
			if(_LineStyle == null)
			{
				_LineStyle = new GUIStyle();
				_LineStyle.normal.background = EditorGUIUtility.whiteTexture;
				_LineStyle.stretchWidth = true;
			}
			
			return _LineStyle;
		}
	}

	static public GUIStyle _RichTextLabel;
	static public GUIStyle RichTextLabel
	{
		get
		{
			if(_RichTextLabel == null)
			{
				_RichTextLabel = new GUIStyle(EditorStyles.wordWrappedLabel);
				_RichTextLabel.richText = true;
			}
			
			return _RichTextLabel;
		}
	}

	static public void Separator()
	{
		GUILayout.Space(4);
		SeparatorLine();
		GUILayout.Space(4);
	}

	static public void SeparatorLine()
	{
		GUILine(new Color(.3f,.3f,.3f), 1);
		GUILine(new Color(.9f,.9f,.9f), 1);
	}
	
	static public void GUILine(float height = 2f)
	{
		GUILine(Color.black, height);
	}
	static public void GUILine(Color color, float height = 2f)
	{
		Rect position = GUILayoutUtility.GetRect(0f, float.MaxValue, height, height, LineStyle);
		
		if(Event.current.type == EventType.Repaint)
		{
			Color orgColor = GUI.color;
			GUI.color = orgColor * color;
			LineStyle.Draw(position, false, false, false, false);
			GUI.color = orgColor;
		}
	}
}
