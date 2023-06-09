using UnityEngine;
using UnityEngine.UI;
using Firebase.RemoteConfig;
using System.Collections;
using System;

public class ButtonColor : MonoBehaviour
{
	private string buttonColor = "green";

	private void Start()
	{
		StartCoroutine(GetButtonColor());
	}

	IEnumerator GetButtonColor()
	{
		FirebaseRemoteConfig remoteConfig = FirebaseRemoteConfig.DefaultInstance;
		var fetchTask = remoteConfig.FetchAsync(TimeSpan.Zero);

		yield return new WaitUntil(() => fetchTask.IsCompleted);

		if (fetchTask.Exception != null)
		{
			Debug.LogError($"Failed to fetch remote config: {fetchTask.Exception}");
			yield break;
		}


		remoteConfig.ActivateAsync();

		buttonColor = FirebaseRemoteConfig.DefaultInstance.GetValue("button_color").StringValue;
		string check = "red";
		Debug.Log("Button color: " + buttonColor);
		Debug.Log("???:" + (buttonColor == check));
		if (buttonColor == "\"red\"")
		{
			Debug.Log("in if: " + buttonColor);
			GetComponent<UnityEngine.UI.Button>().image.color = Color.red;
		}
		else if (buttonColor == "\"blue\"")
		{
			Debug.Log("in if: " + buttonColor);
			GetComponent<UnityEngine.UI.Button>().image.color = Color.blue;
		}
	}
}
