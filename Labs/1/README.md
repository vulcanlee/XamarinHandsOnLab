# ![](Icons/fa-bullhorn.png) 關於 Xamarin 在台灣的學習技術資源

歡迎加入 [Xamarin 實驗室 粉絲團](https://www.facebook.com/vulcanlabtw/)，在這裡，將會經常性的貼出各種關於 Xamarin / Visual Studio / .NET 的相關消息、文章、技術開發等文件，讓您可以隨時掌握第一手的 Xamarin 方面消息。

歡迎加入 [Xamarin.Forms @ Taiwan](https://www.facebook.com/groups/149422715582904/)，這是台灣的 Xamarin User Group，若您有任何關於 Xamarin / Visual Studio / .NET 上的問題，都可以在這裡來與各方高手來進行討論、交流。

[Xamarin 實驗室 部落格](http://mylabtw.blogspot.com/) 是作者本身的部落格，這個部落格將會專注於 Xamarin 之跨平台 (Android / iOS / UWP) 方面的各類開技術探討、研究與分享的文章，最重要的是，它是全繁體中文的。

# ![](Icons/fa-heart.png) 最新 Xamarin.Forms 課程資訊  

這裡是作者所開辦的 Xamarin.Forms 教學課程

台中》Xamarin.Forms 跨平台行動應用程式開發實戰 2017-07-01 ~ 2017-07-15 ( 每周星期六、日 ) [http://www.accupass.com/go/DCT_106019](http://www.accupass.com/go/DCT_106019)

《台北》微軟開發平台體驗營 – 企業級行動 App 開發實戰_第五梯次 2017-06-14(Wed) 09:00 ~ 2017-06-14(Wed) 17:00 (GMT+8) [http://www.accupass.com/go/DCT_106015](http://www.accupass.com/go/DCT_106015)

《台北》微軟開發平台體驗營 – 企業級行動 App 開發實戰_第四梯次 2017-05-04(Thu) 09:00 ~ 2017-05-04(Thu) 17:00 (GMT+8) [http://www.accupass.com/go/DCT_106014](http://www.accupass.com/go/DCT_106014)

《台北》微軟開發平台體驗營 – 企業級行動 App 開發實戰_第三梯次 2017-04-12(Wed) 09:00 ~ 2017-04-12(Wed) 17:00 (GMT+8) [http://www.accupass.com/go/DCT_106013](http://www.accupass.com/go/DCT_106013)

《台北》微軟開發平台體驗營 – 企業級行動 App 開發實戰_第二梯次 2017-03-15(Wed) 09:00 ~ 2017-03-15(Wed) 17:00 (GMT+8) [http://www.accupass.com/go/DCT_106012](http://www.accupass.com/go/DCT_106012)

《台北》Xamarin.Forms 跨平台行動應用程式開發實戰(五天) 2017-03-11 ~ 2017-03-25 ( 每周星期六、日 ) [http://www.accupass.com/go/DCT_106011](http://www.accupass.com/go/DCT_106011)

《台北》微軟開發平台體驗營 – 企業級行動 App 開發實戰 2017-02-22(Wed) 09:00 ~ 2017-02-22(Wed) []()

《高雄》Xamarin.Forms 跨平台行動應用程式開發實戰 2016-12-10 09:00 ~ 2017-01-07 17:00 [http://www.accupass.com/go/DCT_105036](http://www.accupass.com/go/DCT_105036)

《台北》Xamarin.Forms 跨平台行動應用程式開發實戰 2016-10-21 09:00 ~ 2016-10-31 17:00 [http://www.accupass.com/go/DCT_105027](http://www.accupass.com/go/DCT_105027)

《台北》Xamarin.Forms 跨平台行動應用程式開發實戰  2016-10-15 09:00 ~ 2016-10-29 17:00 [http://www.accupass.com/go/DCT_105034](http://www.accupass.com/go/DCT_105034)

# Xamarin.Forms 跨平台應用程式開發 實機演練

# ![](Icons/fa-info-circle.png) 本書目的

這是規劃為兩天實作課程的練習電子書，這要的目的是要能夠讓學員，從無到有的開始，使用 Visual Studio 2017 + Xamarin.Forms 開發技術，配合 Azure 行動應用服務，開發出來的一個派工專案 App；在這個 App 內，將會提供底下的功能：

* 開發一個後端 Web API 服務

  * 利用 Azure 行動應用建立後端各類 Web API 服務
  
  * 可以透過 Web API 進行圖片上傳的功能

  * 使用 PostMan，將所有的 Web API 定義起來，並且可以透過 PostMan 來做測試

  * 提供一個可以虛擬打卡的網頁，讓使用者掃描網頁上的 QR Code 來打卡

    [虛擬打卡模擬網頁](https://xamarinhandsonlab.azurewebsites.net/DoTasks?account=user1)

* 開發出一套可以執行於 Android / iOS 的跨平台應用程式

  * 使用者登入與身分驗證檢查

  * 根據登入使用者，下載屬於該使用者的派工單

  * 根據每項工作定義，進行定點簽到(GPS定位,掃 QR Code 條碼)

  * 可以輸入額外資訊到派工單上與進行儲存

  * 使用者可以進行拍照或選取相簿，並且透過網路上傳到後端，之後顯示在手機上

  * 工作完成後，可以上傳通知後台(之後該派工單將無法進行修改)

  * 可以查詢最近7天的歷史派工單資料

  * 完成工單的通知推播(Azure 推播中心設定與App端的註冊與接收)

  * App打包與部署

# ![](Icons/fa-exclamation-triangle.png) 注意事項

* 參加學員必須具備 .NET C# 與 Visual Studio 使用 一年以上的開發經驗

* 最好能夠具有任一行動 App 開發經驗(Android / iOS / UWP)

* 本課程使用 Visual Studio 2017 作為練習開發IDE

* 這個練習的原始碼，將採用 MVVM 架構開發，並且需要搭配 Prism 開發框架來使用

* 開發過程中，將會使用 Android 模擬器做為開發與測試之用

* 若要使用 Android 實體手機來進行開發(QR Code掃描的時候會用到)，請學員自備

* 若要練習 iOS 應用程式的開發與除錯過程，請學員自備 Mac 電腦與 iOS 手機和 Apple 開發者帳號

# 開發環境準備

您可以選擇使用底下兩種環境的任何一種：

* Windows 10 企業版 + Visual Studio 2017

* Windows 10 企業版 + Visual Studio 2015

## Visual Studio 2017

### 教學影片

請參考底下影片，準備您的練習開發環境

[Xamarin 企業級行動化開發平台環境建置攻略 - Visual Studio 2017 安裝與設定](https://youtu.be/MB7piez-oiI)

[Xamarin 企業級行動化開發平台環境建置攻略 - 提升 Xamarin Forms 開發生產力](https://youtu.be/rwOu4Z1SxjI)

### 說明文件

[Xamarin 企業級行動化開發平台環境建置攻略 ( 全新 Visual Studio 2017 )](https://mylabtw.blogspot.tw/2017/03/xamarin-visual-studio-2017.html)

## Visual Studio 2015

### 教學影片

請參考底下影片，準備您的練習開發環境

[Xamarin.Forms 跨平台行動應用程式開發實戰 - Visual Studio 2015 上課環境安裝與設定教學](https://youtu.be/KQB6NuJDkCQ)

# Android APK 下載

[http://bit.ly/2nUjgUq](http://bit.ly/2nUjgUq)
