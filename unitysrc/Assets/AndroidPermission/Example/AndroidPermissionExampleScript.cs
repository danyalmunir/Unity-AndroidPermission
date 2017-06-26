using UnityEngine;
using System.Collections;

public class AndroidPermissionExampleScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //※AndroidManifest.xmlで、<uses-permission android:name="android.permission.CAMERA" />を含めないと正しく動作しません
        if (AndroidPermissionManager.IsRuntimePermissionDevice())
            AndroidPermissionManager.RequestPermissions(new AndroidPermission[] { AndroidPermission.CAMERA });
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
