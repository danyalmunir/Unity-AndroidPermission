# UnityAndroidPermission

a wrapper of methods for android runtime permission for Unity

## Install
Use AndroidPermission.unitypackage

Before you use this, you must install
 - AndroidSDK(ver25 or more)
 - AndroidBuildTools(ver25.03 or more)

You can know how to setup these tools at https://docs.unity3d.com/Manual/android-sdksetup.html
## Usage
```
AndroidPermissionManager.IsRuntimePermissionDevice()
```

- Method for checking if your device need android's runtime permissions


-------------------------------

	
```
AndroidPermissionManager.RequestPermissions(IEnumerable<AndroidPermission> permissions)
```

- This method is equal to requestPermissions() in android java
- https://developer.android.com/reference/android/support/v4/app/ActivityCompat.html
-------------------------------

```
AndroidPermissionManager.CheckSelfPermission(AndroidPermission permission)
```


- This method is equal to checkSelfPermission() in android java
- https://developer.android.com/reference/android/support/v4/content/ContextCompat.html


-------------------------------

```
AndroidPermissionManager.ShouldShowRequestPermissionRationale(AndroidPermission permission)
```

- This method is equal to shouldShowRequestPermissionRationale() in android java
- https://developer.android.com/reference/android/support/v4/app/ActivityCompat.html

-------------------------------

```
AndroidPermission.XXX
//ex)AndroidPermission.GET_ACCOUNTS
```

- XXX is wrapper of member of Manifest.permission
- https://developer.android.com/reference/android/Manifest.permission.html



## Example

```
//request camera permission
AndroidPermissionManager.RequestPermissions(new AndroidPermission[] { AndroidPermission.CAMERA });
```

## Licence

MIT
