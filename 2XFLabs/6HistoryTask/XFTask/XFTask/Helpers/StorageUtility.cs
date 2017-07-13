using PCLStorage;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace XFTask.Helpers
{
    /// <summary>
    /// Storage 相關的 API
    /// http://www.nudoq.org/#!/Packages/PCLStorage/PCLStorage/FileSystem
    /// </summary>
    public class StorageUtility
    {
        /// <summary>
        /// 將所指定的字串寫入到指定目錄的檔案內
        /// </summary>
        /// <param name="folderName">目錄名稱</param>
        /// <param name="filename">檔案名稱</param>
        /// <param name="content">所要寫入的文字內容</param> 
        /// <returns></returns>
        public static async Task WriteToDataFileAsync(string folderPath, string folderName, string filename, string content)
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;

            if (string.IsNullOrEmpty(folderName))
            {
                throw new ArgumentNullException("folderName");
            }

            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentNullException("filename");
            }

            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentNullException("content");
            }

            #region 建立與取得指定路徑內的資料夾
            try
            {
                string[] fooPaths = folderName.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                IFolder folder = rootFolder;
                foreach (var item in fooPaths)
                {
                    folder = await folder.CreateFolderAsync(item, CreationCollisionOption.OpenIfExists);
                }
                IFile file = await folder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
                #endregion

                await file.WriteAllTextAsync(content);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            finally
            {
            }
        }

        /// <summary>
        /// 從指定目錄的檔案內將文字內容讀出
        /// </summary>
        /// <param name="folderName">目錄名稱</param>
        /// <param name="filename">檔案名稱</param>
        /// <returns>文字內容</returns>
        public static async Task<string> ReadFromDataFileAsync(string folderPath, string folderName, string filename)
        {
            string content = "";

            IFolder rootFolder = FileSystem.Current.LocalStorage;

            if (string.IsNullOrEmpty(folderName))
            {
                throw new ArgumentNullException("folderName");
            }

            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentNullException("filename");
            }

            #region 建立與取得指定路徑內的資料夾
            try
            {
                string[] fooPaths = folderName.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                IFolder folder = rootFolder;
                foreach (var item in fooPaths)
                {
                    folder = await folder.CreateFolderAsync(item, CreationCollisionOption.OpenIfExists);
                }
                var fooallf = await folder.GetFilesAsync();
                var fooExist = fooallf.FirstOrDefault(x => x.Name == filename);
                if (fooExist != null)
                {
                    IFile file = await folder.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists);
                    #endregion

                    content = await file.ReadAllTextAsync();
                }
                else
                {
                    content = "";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            finally
            {
            }

            return content.Trim();
        }
    }
}
