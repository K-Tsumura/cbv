using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;

namespace ClipboardViewer
{
    static class Program
    {
        //アプリケーション名です
        //メインフォームのテキストだったり通知領域アイコンの名前だったり。
        public static string AppName = "ClipBoardViewer Ver1.3";
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            //フォームが閉じられるときは、現在クリップボードにあるテキストを
            //クリップボードにコピーする。第二引数がtrueなのでこのウインドウを閉じても
            //そのデータは保持されるようになる。
            if (Clipboard.GetText() != "")
            {
                Clipboard.SetDataObject(Clipboard.GetText(), true);
            }


            //終了時にはすべてのプロパティを書き込む
            iniWrite.WriteAllProperty();


            if (property.exitLogSave.ToString() == "true")
            {
                for (int i = 0; i < 8; i++)
                {
                    using (StreamWriter writer = new StreamWriter("./log" + (i + 1).ToString() + ".txt", false))
                    {
                        writer.Write(val.moji[i]);
                        //Console.Write(val.moji[i]+"\n\n");
                    }
                }
            }


        }
    }
}



namespace ClipboardViewer
{
    //iniファイル読み書き用の関数を宣言
    public class iniWrite
    {
        [DllImport("KERNEL32.DLL")]
        public static extern uint WritePrivateProfileString(
          string lpAppName,
          string lpKeyName,
          string lpString,
          string lpFileName);

        [DllImport("KERNEL32.DLL")]
        public static extern uint GetPrivateProfileString(
            string lpAppName,
            string lpKeyName, string lpDefault,
            StringBuilder lpReturnedString, uint nSize,
            string lpFileName);

        //iniファイルにすべての内容を書き込む処理(まとめただけ)
        public static void WriteAllProperty()
        {
            //iniファイルの作成
            using (FileStream fs = File.Create(Form1.iniFileName))
            {
                fs.Close();
            }

            iniWrite.WritePrivateProfileString(
                "SETTINGS", "StartMode",
                property.StartMode.ToString(),
                Form1.iniFileName);

            iniWrite.WritePrivateProfileString(
                "SETTINGS", "CloseButton",
                property.CloseButton.ToString(),
                Form1.iniFileName);

            iniWrite.WritePrivateProfileString(
                "SETTINGS", "TopMost",
                property.TopMost.ToString(),
                Form1.iniFileName);

            iniWrite.WritePrivateProfileString(
                "SETTINGS", "ExitLogSave",
                property.exitLogSave.ToString(),
                Form1.iniFileName);

            iniWrite.WritePrivateProfileString(
                "COLOR", "BackColor",
                property.BackColor.R.ToString("x2") + property.BackColor.G.ToString("x2") + property.BackColor.B.ToString("x2"),
                Form1.iniFileName);

            iniWrite.WritePrivateProfileString(
                "COLOR", "TextBoxBackColor",
                property.TextBoxBackColor.R.ToString("x2") + property.TextBoxBackColor.G.ToString("x2") + property.TextBoxBackColor.B.ToString("x2"),
                Form1.iniFileName);


        }
    }


    public class HotKey{
        // HotKey登録関数
        // 登録に失敗(他のアプリが使用中)の場合は、0が返却されます。
        [DllImport("user32.dll")]
        extern static int RegisterHotKey(IntPtr hWnd, int id, int modKey, int key);

        // HotKey解除関数
        // 解除に失敗した場合は、0が返却されます。
        [DllImport("user32.dll")]
        extern static int UnregisterHotKey(IntPtr HWnd, int ID);

        public static void EntryHotKey(IntPtr Handle)
        {
            //(Handle,  Ctrl?Alt?Shift?,  ホットキーID(識別子),  key)
            if(RegisterHotKey(Handle, 0x01, 0x02, (int) Keys.D1) == 0){
                MessageBox.Show("「Ctrl+1」は既に使用されているホットキーです。アプリケーションは動作しますが、このホットキーは無効になります","エラー発生",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            if (RegisterHotKey(Handle, 0x02, 0x02, (int)Keys.D2) == 0)
            {
                MessageBox.Show("「Ctrl+2」は既に使用されているホットキーです。アプリケーションは動作しますが、このホットキーは無効になります", "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (RegisterHotKey(Handle, 0x03, 0x02, (int)Keys.D3) == 0)
            {
                MessageBox.Show("「Ctrl+3」は既に使用されているホットキーです。アプリケーションは動作しますが、このホットキーは無効になります", "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (RegisterHotKey(Handle, 0x04, 0x02, (int)Keys.D4) == 0)
            {
                MessageBox.Show("「Ctrl+4」は既に使用されているホットキーです。アプリケーションは動作しますが、このホットキーは無効になります", "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (RegisterHotKey(Handle, 0x05, 0x02, (int)Keys.D5) == 0)
            {
                MessageBox.Show("「Ctrl+5」は既に使用されているホットキーです。アプリケーションは動作しますが、このホットキーは無効になります", "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (RegisterHotKey(Handle, 0x06, 0x02, (int)Keys.D6) == 0)
            {
                MessageBox.Show("「Ctrl+6」は既に使用されているホットキーです。アプリケーションは動作しますが、このホットキーは無効になります", "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (RegisterHotKey(Handle, 0x07, 0x02, (int)Keys.D7) == 0)
            {
                MessageBox.Show("「Ctrl+7」は既に使用されているホットキーです。アプリケーションは動作しますが、このホットキーは無効になります", "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (RegisterHotKey(Handle, 0x08, 0x02, (int)Keys.D8) == 0)
            {
                MessageBox.Show("「Ctrl+8」は既に使用されているホットキーです。アプリケーションは動作しますが、このホットキーは無効になります", "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}

namespace ClipboardViewer
{
    public class ClipboardEventArgs : EventArgs
    {
        private string text;

        public string Text
        {
            get { return this.text; }
        }

        public ClipboardEventArgs(string str)
        {
            this.text = str;
        }
    }

    public delegate void cbEventHandler(
                            object sender, ClipboardEventArgs ev);

    [System.Security.Permissions.PermissionSet(
          System.Security.Permissions.SecurityAction.Demand,
          Name = "FullTrust")]
    internal class MyClipboardViewer : NativeWindow
    {
        [DllImport("user32")]
        public static extern IntPtr SetClipboardViewer(
                IntPtr hWndNewViewer);

        [DllImport("user32")]
        public static extern bool ChangeClipboardChain(
                IntPtr hWndRemove, IntPtr hWndNewNext);

        [DllImport("user32")]
        public extern static int SendMessage(
                IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        private const int WM_DRAWCLIPBOARD = 0x0308;
        private const int WM_CHANGECBCHAIN = 0x030D;
        private IntPtr nextHandle;

        private Form parent;
        public event cbEventHandler ClipboardHandler;

        public MyClipboardViewer(Form f)
        {
            f.HandleCreated
                        += new EventHandler(this.OnHandleCreated);
            f.HandleDestroyed
                        += new EventHandler(this.OnHandleDestroyed);
            this.parent = f;
        }

        internal void OnHandleCreated(object sender, EventArgs e)
        {
            AssignHandle(((Form)sender).Handle);
            // ビューアを登録
            nextHandle = SetClipboardViewer(this.Handle);
        }

        internal void OnHandleDestroyed(object sender, EventArgs e)
        {
            // ビューアを解除
            bool sts = ChangeClipboardChain(this.Handle, nextHandle);
            ReleaseHandle();
        }

        protected override void WndProc(ref Message msg)
        {
            switch (msg.Msg)
            {

                case WM_DRAWCLIPBOARD:
                    if (Clipboard.ContainsText())
                    {
                        // クリップボードの内容がテキストの場合のみ
                        if (ClipboardHandler != null)
                        {
                            // クリップボードの内容を取得してハンドラを呼び出す
                            ClipboardHandler(this,
                                new ClipboardEventArgs(Clipboard.GetText()));
                        }
                    }
                    if ((int)nextHandle != 0)
                        SendMessage(
                            nextHandle, msg.Msg, msg.WParam, msg.LParam);
                    break;

                // クリップボード・ビューア・チェーンが更新された
                case WM_CHANGECBCHAIN:
                    if (msg.WParam == nextHandle)
                    {
                        nextHandle = (IntPtr)msg.LParam;
                    }
                    else if ((int)nextHandle != 0)
                        SendMessage(
                            nextHandle, msg.Msg, msg.WParam, msg.LParam);
                    break;
            }
            base.WndProc(ref msg);
        }
    }
}