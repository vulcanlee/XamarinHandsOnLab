# 檢視 Azure Mobile App 行動應用服務提供的後端專案

在這個練習中，對於後端 Web API 服務，我們將會使用剛剛下載的後端專案，進行擴建，因此，您需要先了解 ASP.NET 的專案架構與內容，等下才能夠進行擴建；而 對於 Xamarin.Forms 的專案，我們將會使用 `Prism Template Pack` 來建立一個新的 Xamarin.Forms 的專案，並且加入相關要存取 Azure Mobile App 的程式碼。

在這裡，我們先來了解看看，Azure 提供的 ASP.NET 後台專案，了解要如何在 ASP.NET 專案中，存取 Azure Mobile App 服務。

首先，請先使用 Visual Studio 2017，打開剛剛下載的 ASP.NET 後端專案 

* 請開啟這個檔案 `XamarinHandsOnLab.sln`

## 資料模型 (Data Model)

在這個下載的範例專案中，使用的是 Entity Framework 的 Code First 方式開發，若有新的資料表要加入到這個專案內，請在 `DataObjects` 資料夾內加入一個類別。

底下是剛剛操作的代辦事項應用程式的所用到的資料 `TodoItem`

從這裡我們看到，若要能夠透過 Azure Mobile App 架構，存取 SQL 資料庫的資料，您所定義的資料表類別，需要繼承 `EntityData` 這個類別。

```csharp
    public class TodoItem : EntityData
    {
        public string Text { get; set; }

        public bool Complete { get; set; }
    }
```

## 使用 DbContext 增加一個新的資料表

在 ASP.NET 中，若要增加一個資料表，您需要打開 `Models`  資料夾的 `XamarinHandsOnLabContext.cs` 檔案，使用底下方法，宣告 CLR 屬性，並且在 DbSet 泛型中，指定您的資料表的每個欄位定義。

```csharp
        public DbSet<TodoItem> TodoItems { get; set; }
```

當要在 Web API 中進行資料庫存取的時候，可以透過 `XamarinHandsOnLabContext` 物件，來取得特定資料表的紀錄，例如，底下程式碼將會取得 UserTasks 這個資料表中的特定使用者之未完成的工作紀錄出來。

```csharp
private XamarinHandsOnLabContext db = new XamarinHandsOnLabContext();

        public async Task<ActionResult> Index(string account)
        {
            // 查詢該使用的未完成工作有哪些
            var fooToday =new DateTime(1900,1,1).Date;
            return View(await db.UserTasks.Where(x=>DbFunctions.TruncateTime(x.ReportedDatetime) == fooToday
                                  && x.Account == account).ToListAsync());
        }

```


## 建立一個 Web API 服務

若要產生一個新的 Web API 服務，您可以在 `Controllers` 資料夾內，繼承 `ApiController`來建立一個可供 Xamarin.Forms 呼叫的 Web API 服務。

## 同步更新資料庫結構

每次資料表或者欄位有異動的時候，我們需要執行 `add-migration 此次資料庫異動的說明` 命令，並且當佈署到 Azure 後端的時候，會同步更新資料庫的 Schema 結構。
