# Android 推播測試內容

string templateBodyGCM1 = "{\"data\":{\"message\":\"$(messageParam)\"}}";

 {"data":{"message":"Notification Hub test notification"}}

# iOS 推播測試內容

string templateBodyAPNS = "{\"aps\":{\"alert\":\"$(messageParam)\"}}";

 {"aps":{"alert":"Notification Hub test notification"}}
