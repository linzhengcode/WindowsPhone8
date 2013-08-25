using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PushNotificationServerDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label4.Text = "";
            string msg = textBox2.Text;
            if (radioButton1.Checked)//raw通知
            {
                byte[] strBytes = new UTF8Encoding().GetBytes(msg);
                sendNotificationType(strBytes, notificationType.raw);
            }
            else if (radioButton2.Checked)
            {
                // Tile通知
                string tileMessage = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                    "<wp:Notification xmlns:wp=\"WPNotification\">" +
                       "<wp:Tile>" +
                          "<wp:BackgroundImage>/Images/test.png</wp:BackgroundImage>" +
                          "<wp:Count>3</wp:Count>" +
                          "<wp:Title>" + textBox2.Text + "</wp:Title>" +
                       "</wp:Tile> " +
                    "</wp:Notification>";
                byte[] strBytes = new UTF8Encoding().GetBytes(tileMessage);
                sendNotificationType(strBytes, notificationType.tokens);  
            }
            else if (radioButton3.Checked)
            {
                // toast通知
                string toastMessage = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                        "<wp:Notification xmlns:wp=\"WPNotification\">" +
                           "<wp:Toast>" +
                              "<wp:Text1>test</wp:Text1>" +
                              "<wp:Text2>" + msg + "</wp:Text2>" +
                           "</wp:Toast>" +
                        "</wp:Notification>";
                byte[] strBytes = new UTF8Encoding().GetBytes(toastMessage);
                sendNotificationType(strBytes, notificationType.toast);
            }
        }

        void sendNotificationType(byte[] payLoad, notificationType type)
        {
            //通过Http Post方式发送消息
            HttpWebRequest sendNotificationRequest = (HttpWebRequest)WebRequest.Create(textBox1.Text);
            sendNotificationRequest.Method = WebRequestMethods.Http.Post;

            //X-MessageID头必须是一个唯一的字符床
            sendNotificationRequest.Headers["X-MessageID"] = Guid.NewGuid().ToString();

            if (type == notificationType.raw)
            {

                // 设置raw通知
                sendNotificationRequest.ContentType = "text/xml; charset=utf-8";
                sendNotificationRequest.Headers.Add("X-NotificationClass", "3");
                // 3: 表示马上发送
                // 13:表示450秒内发送
                // 23:表示900秒内发送
            }
            else if (type == notificationType.tokens)
            {
                // 设置Tile通知
                sendNotificationRequest.ContentType = "text/xml; charset=utf-8";
                sendNotificationRequest.Headers.Add("X-WindowsPhone-Target", "token");
                sendNotificationRequest.Headers.Add("X-NotificationClass", "1");
                // 1: 表示马上发送
                // 11:表示450秒内发送
                // 21:表示900秒内发送
            }
            else if (type == notificationType.toast)
            {
                // 设置toast通知
                sendNotificationRequest.ContentType = "text/xml; charset=utf-8";
                sendNotificationRequest.Headers.Add("X-WindowsPhone-Target", "toast");
                sendNotificationRequest.Headers.Add("X-NotificationClass", "2");
                // 2:表示马上发送
                // 12:表示450秒内发送
                // 22:表示900秒内发送
            }
            sendNotificationRequest.ContentLength = payLoad.Length;
            byte[] notificationMessage = payLoad;
            // 发送通知
            using (Stream requestStream = sendNotificationRequest.GetRequestStream())
            {
                requestStream.Write(notificationMessage, 0, notificationMessage.Length);
            }
            // 获取推送通知的响应状态
            HttpWebResponse response = (HttpWebResponse)sendNotificationRequest.GetResponse();
            string notificationStatus = response.Headers["X-NotificationStatus"];
            string notificationChannelStatus = response.Headers["X-SubscriptionStatus"];
            string deviceConnectionStatus = response.Headers["X-DeviceConnectionStatus"];
            label4.Text = String.Format("通知状态：{0}，管道状态：{1}，设备状态：{2}",
                notificationStatus, notificationChannelStatus, deviceConnectionStatus);

        }
    }

    public enum notificationType
    {
        raw,
        toast,
        tokens
    }
}
