using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KeyBoardMouseHookWFDemo;

internal static class MouseActionSimulator {
	[DllImport("user32.dll", SetLastError = true)]
	private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

	[StructLayout(LayoutKind.Sequential)]
	public struct MOUSEINPUT {
		public int dx;
		public int dy;
		public uint mouseData;
		public uint dwFlags;
		public uint time;
		public IntPtr dwExtraInfo;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct INPUT {
		public uint type;
		public MOUSEINPUT mi;
	}

	private const uint MOUSEEVENTF_ABSOLUTE = 0x8000;
	private const uint MOUSEEVENTF_MOVE = 0x0001;
	private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
	private const uint MOUSEEVENTF_LEFTUP = 0x0004;
	private const int INPUT_MOUSE = 0;

	public static void Click(int x, int y) {
		// 获取屏幕分辨率
		int screenWidth = Screen.PrimaryScreen.Bounds.Width;
		int screenHeight = Screen.PrimaryScreen.Bounds.Height;

		// 将屏幕坐标转换为相对坐标（0-65535）
		int absoluteX = (int)((double)x / screenWidth * 65535);
		int absoluteY = (int)((double)y / screenHeight * 65535);

		INPUT[] inputs = new INPUT[3];

		inputs[0].type = INPUT_MOUSE;
		inputs[0].mi.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE;
		inputs[0].mi.dx = absoluteX;
		inputs[0].mi.dy = absoluteY;

		inputs[1].type = INPUT_MOUSE;
		inputs[1].mi.dwFlags = MOUSEEVENTF_LEFTDOWN;

		inputs[2].type = INPUT_MOUSE;
		inputs[2].mi.dwFlags = MOUSEEVENTF_LEFTUP;

		SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
	}

	public static void Move(int x, int y) {
		// 获取屏幕分辨率
		int screenWidth = Screen.PrimaryScreen.Bounds.Width;
		int screenHeight = Screen.PrimaryScreen.Bounds.Height;

		// 将屏幕坐标转换为相对坐标（0-65535）
		int absoluteX = (int)((double)x / screenWidth * 65535);
		int absoluteY = (int)((double)y / screenHeight * 65535);

		INPUT[] inputs = new INPUT[1];

		inputs[0].type = INPUT_MOUSE;
		inputs[0].mi.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE;
		inputs[0].mi.dx = absoluteX;
		inputs[0].mi.dy = absoluteY;

		SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
	}
}
