
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

using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

public class AndroidPermissionManager
{
    private const string ANDROID_PERMISSION_CLASS_NAME = "androidnativeplugin.unity.akky.androidpermission.AndroidPermission";
	/// <summary>
	/// 実行時パーミッションの対応端末かどうか
	/// </summary>
	/// <returns><c>true</c>, if runtime permission device was ised, <c>false</c> otherwise.</returns>
	public static bool IsRuntimePermissionDevice()
	{
		bool answer = false;
        #if UNITY_ANDROID
        using (AndroidJavaClass plugin = new AndroidJavaClass(ANDROID_PERMISSION_CLASS_NAME))
		{
			answer = plugin.CallStatic<bool>("isRuntimePermissionDevice");
		}
        #endif
		return answer;
	}

	/// <summary>
	/// permissinoStringはAndroidPermissionクラス内のものを用いること。
	/// </summary>
	/// <param name="_permissions">Permission string.</param>
	/// 
	//ToDo:配列に対応する。
	public static void RequestPermissions(IEnumerable<AndroidPermission> _permissions)
	{
        var permissionValue = _permissions.Select(permission => permission.permissionValue).Aggregate((sum,next) => sum+","+next);
		if (!IsRuntimePermissionDevice())
			return;
		using (AndroidJavaClass plugin = new AndroidJavaClass(ANDROID_PERMISSION_CLASS_NAME))
		{
            plugin.CallStatic("requestPermissions", permissionValue);
		}
	}


	/// <summary>
	/// 権限があるかチェックする。ある場合はtrue
	/// </summary>
	/// <returns><c>true</c>, if android permission was checked, <c>false</c> otherwise.</returns>
	/// <param name="permission">Permission string.</param>
	public static bool CheckSelfPermission(AndroidPermission permission)
	{
		bool answer = true;
		#if UNITY_ANDROID
		using (AndroidJavaClass plugin = new AndroidJavaClass(ANDROID_PERMISSION_CLASS_NAME))
		{
			var valueFromAndroidMethod = plugin.CallStatic<int>("checkSelfPermission", permission.permissionValue);
			if (valueFromAndroidMethod != 0)
			{
				answer = false;
			}
		}
		#endif
		return answer;
	}

	/// <summary>
	/// 許可画面で、「二度と表示しない」をユーザーが押したかどうかを調べる。
	/// </summary>
	/// <returns><c>true</c>, if show request permission rationale was shoulded, <c>false</c> otherwise.</returns>
	/// <param name="permission">Permission.</param>
	public static bool ShouldShowRequestPermissionRationale(AndroidPermission permission)
	{
        bool answer = false;
		#if UNITY_ANDROID
		using (AndroidJavaClass plugin = new AndroidJavaClass(ANDROID_PERMISSION_CLASS_NAME))
		{
			answer = plugin.CallStatic<bool>("shouldShowRequestPermissionRationale", permission.permissionValue);
		}
        #endif
        return answer;
	}
}


/// <summary>
/// 下記URLを元に作成
/// https://developer.android.com/reference/android/Manifest.permission.html
/// </summary>
public class AndroidPermission
{
	public string permissionValue { get; private set; }
	private AndroidPermission(string permissionValue)
	{
		this.permissionValue = permissionValue;
	}

	public static readonly AndroidPermission ACCESS_CHECKIN_PROPERTIES = new AndroidPermission("android.permission.ACCESS_CHECKIN_PROPERTIES");
	public static readonly AndroidPermission ACCESS_COARSE_LOCATION = new AndroidPermission("android.permission.ACCESS_COARSE_LOCATION");
	public static readonly AndroidPermission ACCESS_FINE_LOCATION = new AndroidPermission("android.permission.ACCESS_FINE_LOCATION");
	public static readonly AndroidPermission ACCESS_LOCATION_EXTRA_COMMANDS = new AndroidPermission("android.permission.ACCESS_LOCATION_EXTRA_COMMANDS");
	public static readonly AndroidPermission ACCESS_NETWORK_STATE = new AndroidPermission("android.permission.ACCESS_NETWORK_STATE");
	public static readonly AndroidPermission ACCESS_NOTIFICATION_POLICY = new AndroidPermission("android.permission.ACCESS_NOTIFICATION_POLICY");
	public static readonly AndroidPermission ACCESS_WIFI_STATE = new AndroidPermission("android.permission.ACCESS_WIFI_STATE");
	public static readonly AndroidPermission ACCOUNT_MANAGER = new AndroidPermission("android.permission.ACCOUNT_MANAGER");
	public static readonly AndroidPermission ADD_VOICEMAIL = new AndroidPermission("com.android.voicemail.permission.ADD_VOICEMAIL");
	public static readonly AndroidPermission BATTERY_STATS = new AndroidPermission("android.permission.BATTERY_STATS");
	public static readonly AndroidPermission BIND_ACCESSIBILITY_SERVICE = new AndroidPermission("android.permission.BIND_ACCESSIBILITY_SERVICE");
	public static readonly AndroidPermission BIND_APPWIDGET = new AndroidPermission("android.permission.BIND_APPWIDGET");
	[Obsolete("deprecated at AndroidSDK", false)]
	public static readonly AndroidPermission BIND_CARRIER_MESSAGING_SERVICE = new AndroidPermission("android.permission.BIND_CARRIER_MESSAGING_SERVICE");
	public static readonly AndroidPermission BIND_CARRIER_SERVICES = new AndroidPermission("android.permission.BIND_CARRIER_SERVICES");
	public static readonly AndroidPermission BIND_CHOOSER_TARGET_SERVICE = new AndroidPermission("android.permission.BIND_CHOOSER_TARGET_SERVICE");
	public static readonly AndroidPermission BIND_CONDITION_PROVIDER_SERVICE = new AndroidPermission("android.permission.BIND_CONDITION_PROVIDER_SERVICE");
	public static readonly AndroidPermission BIND_DEVICE_ADMIN = new AndroidPermission("android.permission.BIND_DEVICE_ADMIN");
	public static readonly AndroidPermission BIND_DREAM_SERVICE = new AndroidPermission("android.permission.BIND_DREAM_SERVICE");
	public static readonly AndroidPermission BIND_INCALL_SERVICE = new AndroidPermission("android.permission.BIND_INCALL_SERVICE");
	public static readonly AndroidPermission BIND_INPUT_METHOD = new AndroidPermission("android.permission.BIND_INPUT_METHOD");
	public static readonly AndroidPermission BIND_MIDI_DEVICE_SERVICE = new AndroidPermission("android.permission.BIND_MIDI_DEVICE_SERVICE");
	public static readonly AndroidPermission BIND_NFC_SERVICE = new AndroidPermission("android.permission.BIND_NFC_SERVICE");
	public static readonly AndroidPermission BIND_NOTIFICATION_LISTENER_SERVICE = new AndroidPermission("android.permission.BIND_NOTIFICATION_LISTENER_SERVICE");
	public static readonly AndroidPermission BIND_PRINT_SERVICE = new AndroidPermission("android.permission.BIND_PRINT_SERVICE");
	public static readonly AndroidPermission BIND_QUICK_SETTINGS_TILE = new AndroidPermission("android.permission.BIND_QUICK_SETTINGS_TILE");
	public static readonly AndroidPermission BIND_REMOTEVIEWS = new AndroidPermission("android.permission.BIND_REMOTEVIEWS");
	public static readonly AndroidPermission BIND_SCREENING_SERVICE = new AndroidPermission("android.permission.BIND_SCREENING_SERVICE");
	public static readonly AndroidPermission BIND_TELECOM_CONNECTION_SERVICE = new AndroidPermission("android.permission.BIND_TELECOM_CONNECTION_SERVICE");
	public static readonly AndroidPermission BIND_TEXT_SERVICE = new AndroidPermission("android.permission.BIND_TEXT_SERVICE");
	public static readonly AndroidPermission BIND_TV_INPUT = new AndroidPermission("android.permission.BIND_TV_INPUT");
	public static readonly AndroidPermission BIND_VOICE_INTERACTION = new AndroidPermission("android.permission.BIND_VOICE_INTERACTION");
	public static readonly AndroidPermission BIND_VPN_SERVICE = new AndroidPermission("android.permission.BIND_VPN_SERVICE");
	public static readonly AndroidPermission BIND_VR_LISTENER_SERVICE = new AndroidPermission("android.permission.BIND_VR_LISTENER_SERVICE");
	public static readonly AndroidPermission BIND_WALLPAPER = new AndroidPermission("android.permission.BIND_WALLPAPER");
	public static readonly AndroidPermission BLUETOOTH = new AndroidPermission("android.permission.BLUETOOTH");
	public static readonly AndroidPermission BLUETOOTH_ADMIN = new AndroidPermission("android.permission.BLUETOOTH_ADMIN");
	public static readonly AndroidPermission BLUETOOTH_PRIVILEGED = new AndroidPermission("android.permission.BLUETOOTH_PRIVILEGED");
	public static readonly AndroidPermission BODY_SENSORS = new AndroidPermission("android.permission.BODY_SENSORS");
	public static readonly AndroidPermission BROADCAST_PACKAGE_REMOVED = new AndroidPermission("android.permission.BROADCAST_PACKAGE_REMOVED");
	public static readonly AndroidPermission BROADCAST_SMS = new AndroidPermission("android.permission.BROADCAST_SMS");
	public static readonly AndroidPermission BROADCAST_STICKY = new AndroidPermission("android.permission.BROADCAST_STICKY");
	public static readonly AndroidPermission BROADCAST_WAP_PUSH = new AndroidPermission("android.permission.BROADCAST_WAP_PUSH");
	public static readonly AndroidPermission CALL_PHONE = new AndroidPermission("android.permission.CALL_PHONE");
	public static readonly AndroidPermission CALL_PRIVILEGED = new AndroidPermission("android.permission.CALL_PRIVILEGED");
	public static readonly AndroidPermission CAMERA = new AndroidPermission("android.permission.CAMERA");
	public static readonly AndroidPermission CAPTURE_AUDIO_OUTPUT = new AndroidPermission("android.permission.CAPTURE_AUDIO_OUTPUT");
	public static readonly AndroidPermission CAPTURE_SECURE_VIDEO_OUTPUT = new AndroidPermission("android.permission.CAPTURE_SECURE_VIDEO_OUTPUT");
	public static readonly AndroidPermission CAPTURE_VIDEO_OUTPUT = new AndroidPermission("android.permission.CAPTURE_VIDEO_OUTPUT");
	public static readonly AndroidPermission CHANGE_COMPONENT_ENABLED_STATE = new AndroidPermission("android.permission.CHANGE_COMPONENT_ENABLED_STATE");
	public static readonly AndroidPermission CHANGE_CONFIGURATION = new AndroidPermission("android.permission.CHANGE_CONFIGURATION");
	public static readonly AndroidPermission CHANGE_NETWORK_STATE = new AndroidPermission("android.permission.CHANGE_NETWORK_STATE");
	public static readonly AndroidPermission CHANGE_WIFI_MULTICAST_STATE = new AndroidPermission("android.permission.CHANGE_WIFI_MULTICAST_STATE");
	public static readonly AndroidPermission CHANGE_WIFI_STATE = new AndroidPermission("android.permission.CHANGE_WIFI_STATE");
	public static readonly AndroidPermission CLEAR_APP_CACHE = new AndroidPermission("android.permission.CLEAR_APP_CACHE");
	public static readonly AndroidPermission CONTROL_LOCATION_UPDATES = new AndroidPermission("android.permission.CONTROL_LOCATION_UPDATES");
	public static readonly AndroidPermission DELETE_CACHE_FILES = new AndroidPermission("android.permission.DELETE_CACHE_FILES");
	public static readonly AndroidPermission DELETE_PACKAGES = new AndroidPermission("android.permission.DELETE_PACKAGES");
	public static readonly AndroidPermission DIAGNOSTIC = new AndroidPermission("android.permission.DIAGNOSTIC");
	public static readonly AndroidPermission DISABLE_KEYGUARD = new AndroidPermission("android.permission.DISABLE_KEYGUARD");
	public static readonly AndroidPermission DUMP = new AndroidPermission("android.permission.DUMP");
	public static readonly AndroidPermission EXPAND_STATUS_BAR = new AndroidPermission("android.permission.EXPAND_STATUS_BAR");
	public static readonly AndroidPermission FACTORY_TEST = new AndroidPermission("android.permission.FACTORY_TEST");
	public static readonly AndroidPermission GET_ACCOUNTS = new AndroidPermission("android.permission.GET_ACCOUNTS");
	public static readonly AndroidPermission GET_ACCOUNTS_PRIVILEGED = new AndroidPermission("android.permission.GET_ACCOUNTS_PRIVILEGED");
	public static readonly AndroidPermission GET_PACKAGE_SIZE = new AndroidPermission("android.permission.GET_PACKAGE_SIZE");
	[Obsolete("deprecated at AndroidSDK", false)]
	public static readonly AndroidPermission GET_TASKS = new AndroidPermission("android.permission.GET_TASKS");
	public static readonly AndroidPermission GLOBAL_SEARCH = new AndroidPermission("android.permission.GLOBAL_SEARCH");
	public static readonly AndroidPermission INSTALL_LOCATION_PROVIDER = new AndroidPermission("android.permission.INSTALL_LOCATION_PROVIDER");
	public static readonly AndroidPermission INSTALL_PACKAGES = new AndroidPermission("android.permission.INSTALL_PACKAGES");
	public static readonly AndroidPermission INSTALL_SHORTCUT = new AndroidPermission("com.android.launcher.permission.INSTALL_SHORTCUT");
	public static readonly AndroidPermission INTERNET = new AndroidPermission("android.permission.INTERNET");
	public static readonly AndroidPermission KILL_BACKGROUND_PROCESSES = new AndroidPermission("android.permission.KILL_BACKGROUND_PROCESSES");
	public static readonly AndroidPermission LOCATION_HARDWARE = new AndroidPermission("android.permission.LOCATION_HARDWARE");
	public static readonly AndroidPermission MANAGE_DOCUMENTS = new AndroidPermission("android.permission.MANAGE_DOCUMENTS");
	public static readonly AndroidPermission MASTER_CLEAR = new AndroidPermission("android.permission.MASTER_CLEAR");
	public static readonly AndroidPermission MEDIA_CONTENT_CONTROL = new AndroidPermission("android.permission.MEDIA_CONTENT_CONTROL");
	public static readonly AndroidPermission MODIFY_AUDIO_SETTINGS = new AndroidPermission("android.permission.MODIFY_AUDIO_SETTINGS");
	public static readonly AndroidPermission MODIFY_PHONE_STATE = new AndroidPermission("android.permission.MODIFY_PHONE_STATE");
	public static readonly AndroidPermission MOUNT_FORMAT_FILESYSTEMS = new AndroidPermission("android.permission.MOUNT_FORMAT_FILESYSTEMS");
	public static readonly AndroidPermission MOUNT_UNMOUNT_FILESYSTEMS = new AndroidPermission("android.permission.MOUNT_UNMOUNT_FILESYSTEMS");
	public static readonly AndroidPermission NFC = new AndroidPermission("android.permission.NFC");
	public static readonly AndroidPermission PACKAGE_USAGE_STATS = new AndroidPermission("android.permission.PACKAGE_USAGE_STATS");
	[Obsolete("deprecated at AndroidSDK", false)]
	public static readonly AndroidPermission PERSISTENT_ACTIVITY = new AndroidPermission("android.permission.PERSISTENT_ACTIVITY");
	public static readonly AndroidPermission PROCESS_OUTGOING_CALLS = new AndroidPermission("android.permission.PROCESS_OUTGOING_CALLS");
	public static readonly AndroidPermission READ_CALENDAR = new AndroidPermission("android.permission.READ_CALENDAR");
	public static readonly AndroidPermission READ_CALL_LOG = new AndroidPermission("android.permission.READ_CALL_LOG");
	public static readonly AndroidPermission READ_CONTACTS = new AndroidPermission("android.permission.READ_CONTACTS");
	public static readonly AndroidPermission READ_EXTERNAL_STORAGE = new AndroidPermission("android.permission.READ_EXTERNAL_STORAGE");
	public static readonly AndroidPermission READ_FRAME_BUFFER = new AndroidPermission("android.permission.READ_FRAME_BUFFER");
	[Obsolete("deprecated at AndroidSDK", false)]
	public static readonly AndroidPermission READ_INPUT_STATE = new AndroidPermission("android.permission.READ_INPUT_STATE");
	public static readonly AndroidPermission READ_LOGS = new AndroidPermission("android.permission.READ_LOGS");
	public static readonly AndroidPermission READ_PHONE_STATE = new AndroidPermission("android.permission.READ_PHONE_STATE");
	public static readonly AndroidPermission READ_SMS = new AndroidPermission("android.permission.READ_SMS");
	public static readonly AndroidPermission READ_SYNC_SETTINGS = new AndroidPermission("android.permission.READ_SYNC_SETTINGS");
	public static readonly AndroidPermission READ_SYNC_STATS = new AndroidPermission("android.permission.READ_SYNC_STATS");
	public static readonly AndroidPermission READ_VOICEMAIL = new AndroidPermission("com.android.voicemail.permission.READ_VOICEMAIL");
	public static readonly AndroidPermission REBOOT = new AndroidPermission("android.permission.REBOOT");
	public static readonly AndroidPermission RECEIVE_BOOT_COMPLETED = new AndroidPermission("android.permission.RECEIVE_BOOT_COMPLETED");
	public static readonly AndroidPermission RECEIVE_MMS = new AndroidPermission("android.permission.RECEIVE_MMS");
	public static readonly AndroidPermission RECEIVE_SMS = new AndroidPermission("android.permission.RECEIVE_SMS");
	public static readonly AndroidPermission RECEIVE_WAP_PUSH = new AndroidPermission("android.permission.RECEIVE_WAP_PUSH");
	public static readonly AndroidPermission RECORD_AUDIO = new AndroidPermission("android.permission.RECORD_AUDIO");
	public static readonly AndroidPermission REORDER_TASKS = new AndroidPermission("android.permission.REORDER_TASKS");
	public static readonly AndroidPermission REQUEST_IGNORE_BATTERY_OPTIMIZATIONS = new AndroidPermission("android.permission.REQUEST_IGNORE_BATTERY_OPTIMIZATIONS");
	public static readonly AndroidPermission REQUEST_INSTALL_PACKAGES = new AndroidPermission("android.permission.REQUEST_INSTALL_PACKAGES");
	[Obsolete("deprecated at AndroidSDK", false)]
	public static readonly AndroidPermission RESTART_PACKAGES = new AndroidPermission("android.permission.RESTART_PACKAGES");
	public static readonly AndroidPermission SEND_RESPOND_VIA_MESSAGE = new AndroidPermission("android.permission.SEND_RESPOND_VIA_MESSAGE");
	public static readonly AndroidPermission SEND_SMS = new AndroidPermission("android.permission.SEND_SMS");
	public static readonly AndroidPermission SET_ALARM = new AndroidPermission("com.android.alarm.permission.SET_ALARM");
	public static readonly AndroidPermission SET_ALWAYS_FINISH = new AndroidPermission("android.permission.SET_ALWAYS_FINISH");
	public static readonly AndroidPermission SET_ANIMATION_SCALE = new AndroidPermission("android.permission.SET_ANIMATION_SCALE");
	public static readonly AndroidPermission SET_DEBUG_APP = new AndroidPermission("android.permission.SET_DEBUG_APP");
	[Obsolete("deprecated at AndroidSDK", false)]
	public static readonly AndroidPermission SET_PREFERRED_APPLICATIONS = new AndroidPermission("android.permission.SET_PREFERRED_APPLICATIONS");
	public static readonly AndroidPermission SET_PROCESS_LIMIT = new AndroidPermission("android.permission.SET_PROCESS_LIMIT");
	public static readonly AndroidPermission SET_TIME = new AndroidPermission("android.permission.SET_TIME");
	public static readonly AndroidPermission SET_TIME_ZONE = new AndroidPermission("android.permission.SET_TIME_ZONE");
	public static readonly AndroidPermission SET_WALLPAPER = new AndroidPermission("android.permission.SET_WALLPAPER");
	public static readonly AndroidPermission SET_WALLPAPER_HINTS = new AndroidPermission("android.permission.SET_WALLPAPER_HINTS");
	public static readonly AndroidPermission SIGNAL_PERSISTENT_PROCESSES = new AndroidPermission("android.permission.SIGNAL_PERSISTENT_PROCESSES");
	public static readonly AndroidPermission STATUS_BAR = new AndroidPermission("android.permission.STATUS_BAR");
	public static readonly AndroidPermission SYSTEM_ALERT_WINDOW = new AndroidPermission("android.permission.SYSTEM_ALERT_WINDOW");
	public static readonly AndroidPermission TRANSMIT_IR = new AndroidPermission("android.permission.TRANSMIT_IR");
	public static readonly AndroidPermission UNINSTALL_SHORTCUT = new AndroidPermission("com.android.launcher.permission.UNINSTALL_SHORTCUT");
	public static readonly AndroidPermission UPDATE_DEVICE_STATS = new AndroidPermission("android.permission.UPDATE_DEVICE_STATS");
	public static readonly AndroidPermission USE_FINGERPRINT = new AndroidPermission("android.permission.USE_FINGERPRINT");
	public static readonly AndroidPermission USE_SIP = new AndroidPermission("android.permission.USE_SIP");
	public static readonly AndroidPermission VIBRATE = new AndroidPermission("android.permission.VIBRATE");
	public static readonly AndroidPermission WAKE_LOCK = new AndroidPermission("android.permission.WAKE_LOCK");
	public static readonly AndroidPermission WRITE_APN_SETTINGS = new AndroidPermission("android.permission.WRITE_APN_SETTINGS");
	public static readonly AndroidPermission WRITE_CALENDAR = new AndroidPermission("android.permission.WRITE_CALENDAR");
	public static readonly AndroidPermission WRITE_CALL_LOG = new AndroidPermission("android.permission.WRITE_CALL_LOG");
	public static readonly AndroidPermission WRITE_CONTACTS = new AndroidPermission("android.permission.WRITE_CONTACTS");
	public static readonly AndroidPermission WRITE_EXTERNAL_STORAGE = new AndroidPermission("android.permission.WRITE_EXTERNAL_STORAGE");
	public static readonly AndroidPermission WRITE_GSERVICES = new AndroidPermission("android.permission.WRITE_GSERVICES");
	public static readonly AndroidPermission WRITE_SECURE_SETTINGS = new AndroidPermission("android.permission.WRITE_SECURE_SETTINGS");
	public static readonly AndroidPermission WRITE_SETTINGS = new AndroidPermission("android.permission.WRITE_SETTINGS");
	public static readonly AndroidPermission WRITE_SYNC_SETTINGS = new AndroidPermission("android.permission.WRITE_SYNC_SETTINGS");
	public static readonly AndroidPermission WRITE_VOICEMAIL = new AndroidPermission("com.android.voicemail.permission.WRITE_VOICEMAIL");
}