using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace ClipboardViewer
{
    static class Program
    {
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

            //iniファイルの作成
            using (FileStream fs = File.Create(Form1.iniFileName))
            {
                // ファイルストリームを閉じて、変更を確定させる
                // 呼ばなくても using を抜けた時点で Dispose メソッドが呼び出される
                fs.Close();
            }


            iniWrite.WritePrivateProfileString(
                            "SETTINGS",      // セクション名
                            "StartMode",          // キー名
                            property.StartMode.ToString(),     // 書き込む値
                            Form1.iniFileName);   // iniファイル名
            
            iniWrite.WritePrivateProfileString(
                "SETTINGS", "CloseButton",
                property.CloseButton.ToString(),
                Form1.iniFileName);


        }
    }
}



namespace ClipboardViewer
{
    //iniファイル読み書き用の関数を宣言
    class iniWrite
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
      switch (msg.Msg) {

        case WM_DRAWCLIPBOARD:
          if (Clipboard.ContainsText()) {
            // クリップボードの内容がテキストの場合のみ
            if (ClipboardHandler != null) {
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
          if (msg.WParam == nextHandle) {
            nextHandle = (IntPtr)msg.LParam;
          } else if((int)nextHandle != 0)
            SendMessage(
                nextHandle, msg.Msg, msg.WParam, msg.LParam);
          break;
      }
      base.WndProc(ref msg);
    }
  }
}