using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;

internal class Gastly
{
    private IntPtr window;

    public void _SendMessage(IntPtr handle, int Msg, int wParam, int lParam)
    {
        SendMessage(handle, Msg, wParam, lParam);
    }

    public void ClickWindow(IntPtr hWnd, string button, int x, int y)
    {
        int lParam = this.MakeLParam(x, y);
        int msg = 0;
        int num3 = 0;
        if (button == "left")
        {
            msg = 0x201;
            num3 = 0x202;
        }
        if (button == "right")
        {
            msg = 0x204;
            num3 = 0x205;
        }
        this._SendMessage(hWnd, msg, 0, lParam);
        this._SendMessage(hWnd, num3, 0, lParam);
    }

    [DllImport("user32.dll", SetLastError = true)]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int processId);
    public void KeyPress(string number)
    {
        if (number == "1")
        {
            SendMessage(this.window, 0x100, (IntPtr)0x31, (IntPtr)0x101);
        }
        if (number == "2")
        {
            SendMessage(this.window, 0x100, (IntPtr)50, (IntPtr)0x101);
        }
        if (number == "3")
        {
            SendMessage(this.window, 0x100, (IntPtr)0x33, (IntPtr)0x101);
        }
        if (number == "4")
        {
            SendMessage(this.window, 0x100, (IntPtr)0x34, (IntPtr)0x101);
        }
        if (number == "5")
        {
            SendMessage(this.window, 0x100, (IntPtr)0x35, (IntPtr)0x101);
        }
        if (number == "6")
        {
            SendMessage(this.window, 0x100, (IntPtr)0x36, (IntPtr)0x101);
        }
        if (number == "7")
        {
            SendMessage(this.window, 0x100, (IntPtr)0x37, (IntPtr)0x101);
        }
        if (number == "8")
        {
            SendMessage(this.window, 0x100, (IntPtr)0x38, (IntPtr)0x101);
        }
        if (number == "9")
        {
            SendMessage(this.window, 0x100, (IntPtr)0x39, (IntPtr)0x101);
        }
        if (number == "0")
        {
            SendMessage(this.window, 0x100, (IntPtr)0x30, (IntPtr)0x101);
        }
        if (number == "f1")
        {
            SendMessage(this.window, 0x100, (IntPtr)0x70, (IntPtr)0x101);
        }
        if (number == "f2")
        {
            SendMessage(this.window, 0x100, (IntPtr)0x71, (IntPtr)0x101);
        }
        if (number == "f3")
        {
            SendMessage(this.window, 0x100, (IntPtr)0x72, (IntPtr)0x101);
        }
        if (number == "f4")
        {
            SendMessage(this.window, 0x100, (IntPtr)0x73, (IntPtr)0x101);
        }
        if (number == "f5")
        {
            SendMessage(this.window, 0x100, (IntPtr)0x74, (IntPtr)0x101);
        }
        if (number == "f6")
        {
            SendMessage(this.window, 0x100, (IntPtr)0x75, (IntPtr)0x101);
        }
        if (number == "f7")
        {
            SendMessage(this.window, 0x100, (IntPtr)0x76, (IntPtr)0x101);
        }
        if (number == "f8")
        {
            SendMessage(this.window, 0x100, (IntPtr)0x77, (IntPtr)0x101);
        }
        if (number == "f9")
        {
            SendMessage(this.window, 0x100, (IntPtr)120, (IntPtr)0x101);
        }
        if (number == "f10")
        {
            SendMessage(this.window, 0x100, (IntPtr)0x79, (IntPtr)0x101);
        }
        if (number == "f11")
        {
            SendMessage(this.window, 0x100, (IntPtr)0x80, (IntPtr)0x101);
        }
        if (number == "f12")
        {
            SendMessage(this.window, 0x100, (IntPtr)0x81, (IntPtr)0x101);
        }
        if (number == "tab")
        {
            SendMessage(this.window, 0x100, (IntPtr)9, (IntPtr)0x101);
        }
        if (number == "enter")
        {
            SendMessage(this.window, 0x100, (IntPtr)13, (IntPtr)0x101);
        }
        if (number == "numlock")
        {
            SendMessage(this.window, 0x100, (IntPtr)0x90, (IntPtr)0x101);
        }
    }

    public void Kill()
    {
        int num;
        int windowThreadProcessId = (int)GetWindowThreadProcessId(this.window, out num);
        Process.GetProcessById(num).Kill();
    }

    public void LeftClick(Point point)
    {
        this.ClickWindow(this.window, "left", point.X, point.Y);
    }

    public int MakeLParam(int LoWord, int HiWord) =>
        (LoWord | (HiWord << 0x10));

    public void RightClick(Point point)
    {
        this.ClickWindow(this.window, "right", point.X, point.Y);
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
    public void setWindow(IntPtr window)
    {
        this.window = window;
    }

    public void Wait(int miliseconds)
    {
        Thread.Sleep(miliseconds);
    }

    public enum WMessages
    {
        WM_KEYDOWN = 0x100,
        WM_KEYUP = 0x101,
        WM_LBUTTONDBLCLK = 0x203,
        WM_LBUTTONDOWN = 0x201,
        WM_LBUTTONUP = 0x202,
        WM_RBUTTONDBLCLK = 0x206,
        WM_RBUTTONDOWN = 0x204,
        WM_RBUTTONUP = 0x205
    }
}