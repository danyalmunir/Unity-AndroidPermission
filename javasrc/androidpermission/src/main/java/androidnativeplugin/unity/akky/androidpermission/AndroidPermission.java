/*
The MIT License (MIT)
Copyright (c) 2017 sato0203.
Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

package androidnativeplugin.unity.akky.androidpermission;

import android.Manifest;
import android.support.v4.app.*;
import android.app.*;
import android.os.Build;
import com.unity3d.player.*;

import java.lang.reflect.*;
import java.util.StringTokenizer;

/**
 * Created by Akky on 2017/06/10.
 */
public class AndroidPermission {
    public static final int REQUEST_CODE = 1;

    public static boolean isRuntimePermissionDevice(){
        android.util.Log.d("AndroidPermissionUnity" ,"isRuntimePermissionDeviceCall!");
        return Build.VERSION.SDK_INT >= 23;
    }

    public static void requestPermissions(String _permissionStrings) {
        if (isRuntimePermissionDevice()){

            String[] permissionStrings = _permissionStrings.split(",",0);

            try{
                ActivityCompat.requestPermissions(UnityPlayer.currentActivity, permissionStrings, REQUEST_CODE);
            }catch(Exception e){
                android.util.Log.e("AndroidPermissionUnity" ,"AndroidPermissionPluginError:"+e.toString());
            }
            android.util.Log.d("AndroidPermissionUnity","AndroidPermissionUnityCalled!:"+ permissionStrings);
        }
    }

    public static int checkSelfPermission(String permissionString){
        int answer;
        try
        {
            answer = ActivityCompat.checkSelfPermission(UnityPlayer.currentActivity,permissionString);
        }catch(Exception e){
            android.util.Log.e("AndroidPermissionUnity" ,"AndroidPermissionPluginError:"+e.toString());
            answer = -1;
        }

        return answer;
    }

    public static boolean shouldShowRequestPermissionRationale(String permissionString){
        boolean answer = true;
        try
        {
            answer = ActivityCompat.shouldShowRequestPermissionRationale(UnityPlayer.currentActivity,permissionString);
        }catch(Exception e){
            android.util.Log.e("AndroidPermissionUnity" ,"AndroidPermissionPluginError:"+e.toString());
            answer = false;
        }
        android.util.Log.d("AndroidPermissionUnity" ,"shouldShow:" +permissionString +":"+answer);
        return answer;
    }

    //リフレクションを使って、Manifest.permission内のフィールドを取得、リフレクションを使ってるため危なそうなので、どうしても必要なとき以外は使わない
    @Deprecated
    public static String getPermissionString(String permissionString){
        String answer;
        Class manifestClass = Manifest.permission.class;
        try{
            Field field = manifestClass.getField(permissionString);
            answer = (String) field.get(null);
        }
        catch(Exception e)
        {
            android.util.Log.e("AndroidPermissionUnity" ,"AndroidPermissionPluginError:"+e.toString());
            answer = "error";
        }
        return answer;
    }
}
