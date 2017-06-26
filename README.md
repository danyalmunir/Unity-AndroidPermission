# UnityAndroidPermission

====

a wrapper of methods for android runtime permission for Unity

## Install
use AndroidPermission.unitypackage

## Usage
```
AndroidPermissionManager.IsRuntimePermissionDevice()
```

- Method for checking if your device need android's runtime permissions


====

	
```
AndroidPermissionManager.RequestPermissions(IEnumerable<AndroidPermission> permissions)
```

- This method is equal to requestPermissions() in android java
- https://developer.android.com/reference/android/support/v4/app/ActivityCompat.html#requestPermissions(android.app.Activity, java.lang.String[], int)


====

```
AndroidPermissionManager.CheckSelfPermission(AndroidPermission permission)
```


- This method is equal to checkSelfPermission() in android java
- https://developer.android.com/reference/android/support/v4/content/ContextCompat.html#checkSelfPermission(android.content.Context, java.lang.String)

====

```
AndroidPermissionManager.ShouldShowRequestPermissionRationale(AndroidPermission permission)
```

- This method is equal to shouldShowRequestPermissionRationale() in android java
- https://developer.android.com/reference/android/support/v4/app/ActivityCompat.html#shouldShowRequestPermissionRationale(android.app.Activity, java.lang.String)

====

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