using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SQLServerDemo
{
    public class DataHelper
    {
        public static void MoveReferenceDatabase()
        {
            IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication();
            // 读取安装包的数据库文件
            using (Stream input = Application.GetResourceStream(new Uri("MyDataContext.sdf", UriKind.Relative)).Stream)
            {
                // 再写入到本地的存储里面
                using (IsolatedStorageFileStream output = iso.CreateFile("MyDataContext.sdf"))
                {
                    byte[] readBuffer = new byte[4096];
                    int bytesRead = -1;
                    // 复制数据
                    while ((bytesRead = input.Read(readBuffer, 0, readBuffer.Length)) > 0)
                    {
                        output.Write(readBuffer, 0, bytesRead);
                    }
                }
            }
        }

    }
}
