const string templateBodyGCM = "{ \"data\" : {\"message\":\"" + description + "\", \"args\":\"" + args + "\"}}";

#  Azure Notification Hub Template (測試傳送)

## 使用於 GcmClient 的推播格式之 C# 語法

### 自行要夾帶的其他內容定義方式

string templateBodyGCM = "{\"data\":{\"message\":\"$(messageParam)\", \"title\":\"$(titleParam)\", \"args\":\"$(argsParam)\"}}";

### 最後的 Json 內容

{"data":{"message":"$(messageParam)", "title":"$(titleParam)", "args":"$(argsParam)"}}

### Azure Notification Hub 內的預設的推播內容格式

string templateBodyGCM1 = "{\"data\":{\"message\":\"$(messageParam)\"}}";

### 最後的 Json 內容

 {"data":{"message":"$(messageParam)"}}



## Azure

### 預設格式

 {"data":{"message":"Notification Hub test notification"}}

### 增加額外 Payload 的格式

 {"data":{"message":"你的應用程式有兩個新紀錄產生", "title":"多奇通知", "args":"Page1"}}
