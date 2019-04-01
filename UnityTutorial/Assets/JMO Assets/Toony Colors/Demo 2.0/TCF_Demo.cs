// Toony Colors Free
// (c) 2012,2015 Jean Moreno

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ToonyColors
{
	public class TCF_Demo : MonoBehaviour
	{
#if UNITY_4 || UNITY_4_3 || UNITY_4_5 || UNITY_4_6
		const float LightIntensity_TCF = 1.5f;
		const float LightIntensity_Diff = 2f;
#else
		const float LightIntensity_TCF = 3f;
		const float LightIntensity_Diff = 4f;
#endif

		public GUISkin guiSkin;

		public Shader unityDiffuse;

		public Light DirLight;
		public Light PointLight;

		public Material[] Materials;

		//--------------------------------------------------------------------------------------------------
		
		private Dictionary<Material, Shader> OriginalShaders = new Dictionary<Material, Shader>();
		private bool viewUnityShader;
		private bool viewPointLight;
		private bool cameraRotate;
		private float cameraSpeed = 0f;
		private float targetCamSpeed = 1f;
		private float cameraDir = 1f;
		private bool cameraDirInv;

		//--------------------------------------------------------------------------------------------------
		
		void Awake()
		{
			//Ref to base shaders
			foreach(Material m in Materials)
			{
				OriginalShaders.Add(m, m.shader);
			}
		}

		void OnDestroy()
		{
			//Restore base shaders
			foreach(KeyValuePair<Material, Shader> kvp in OriginalShaders)
			{
				kvp.Key.shader = kvp.Value;
			}
		}

		void Update()
		{
			Camera.main.transform.RotateAround(Vector3.zero, Vector3.up, 20 * Time.deltaTime * cameraDir * cameraSpeed);

			if(cameraRotate)
				cameraSpeed = Mathf.Lerp(cameraSpeed, targetCamSpeed, 4 * Time.deltaTime);
			else
			{
				if(cameraSpeed > 0.01f)
					cameraSpeed = Mathf.Lerp(cameraSpeed, 0f, 4 * Time.deltaTime);
				else
					cameraSpeed = 0f;
			}

			Rect uiRect = new Rect(16,16,500,140);
			Vector2 mousePos = Input.mousePosition;
			mousePos.y = Screen.height - mousePos.y;
			if(!uiRect.Contains( mousePos ) && Input.GetMouseButton(0))
				targetCamSpeed = 5f;
			else
				targetCamSpeed = 1f;
				
		}

		//--------------------------------------------------------------------------------------------------
		
		// Interface
		void OnGUI()
		{
			GUI.skin = guiSkin;

			Rect r = new Rect(16,16,500,140);
			GUI.Box(r, "");
			r.xMin += 10;
			r.xMax -= 10;
			r.yMin += 4;
			r.yMax -= 6;
			GUILayout.BeginArea(r);

			GUILayout.Label("Demo Settings:", "Header");

			// Shader
			GUILayout.BeginHorizontal();
			GUILayout.Label("Shader: ");
			GUI.enabled = viewUnityShader;
			if(GUILayout.Button("Toony Colors Free", GUILayout.Width(180)))
			{
				ToonyColorsShader();
				SetLightsIntensity(LightIntensity_TCF);
				viewUnityShader = false;
			}

			GUI.enabled = !viewUnityShader;
			if(GUILayout.Button("Unity Diffuse", GUILayout.Width(180)))
			{
				SetShader(unityDiffuse);
				SetLightsIntensity(LightIntensity_Diff);
				viewUnityShader = true;
			}
			GUILayout.EndHorizontal();

			// Light
			GUILayout.BeginHorizontal();
			GUILayout.Label("Light: ");
			GUI.enabled = !DirLight.gameObject.activeSelf;
			if(GUILayout.Button("Directional Light", GUILayout.Width(180)))
			{
				DirLight.gameObject.SetActive(true);
				PointLight.gameObject.SetActive(false);
			}

			GUI.enabled = DirLight.gameObject.activeSelf;
			if(GUILayout.Button("Point Light", GUILayout.Width(180)))
			{
				PointLight.gameObject.SetActive(true);
				DirLight.gameObject.SetActive(false);
			}
			GUILayout.EndHorizontal();

			// Camera
			GUILayout.BeginHorizontal();
			GUI.enabled = true;
			GUILayout.Label("Camera: ");
			cameraRotate = GUILayout.Toggle(cameraRotate, "Rotation", GUILayout.Width(180));
			GUI.enabled = cameraRotate;
			if(GUILayout.Button("Inverse Direction", GUILayout.Width(180)))
			{
				StopAllCoroutines();
				StartCoroutine("CR_InverseDir");
				cameraDirInv = !cameraDirInv;
			}
			GUI.enabled = true;
			GUILayout.EndHorizontal();

			GUILayout.Label("Hold mouse button to speed up rotation!");

			GUILayout.EndArea();
		}

		//--------------------------------------------------------------------------------------------------

		private IEnumerator CR_InverseDir()
		{
			float initial = cameraDir;
			float target = cameraDirInv ? 1f : -1f;

			float t = 0f;
			float d = 0.75f;
			while(t < d)
			{
				t += Time.deltaTime;
				float delta = Mathf.Clamp01(t/d);
				cameraDir = Mathf.Lerp(initial, target, delta);
				yield return null;
			}
		}

		private void ToonyColorsShader()
		{
			foreach(KeyValuePair<Material, Shader> kvp in OriginalShaders)
			{
				kvp.Key.shader = kvp.Value;
			}
		}

		private void SetShader(Shader shader)
		{
			foreach(Material m in Materials)
			{
				m.shader = shader;
			}
		}

		private void SetLightsIntensity(float intensity)
		{
			PointLight.intensity = intensity;
			DirLight.intensity = intensity;
		}
	}
}