using UnityEngine;

namespace Utils {
	public class stringUtils : MonoBehaviour {
		/// 转全角的函数(SBC case)
		///
		///任意字符串
		///全角字符串
		///
		///全角空格为12288，半角空格为32
		///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
		///
		public static string ToSBC(string input) {
			// 半角转全角：
			char[] c = input.ToCharArray();
			for (int i = 0; i < c.Length; i++) {
				if (c[i] == 32) {
					c[i] = (char)12288;
					continue;
				}
				if (c[i] < 127)
					c[i] = (char)(c[i] + 65248);
			}
			return new string(c);
		}

		/**/
		// /
		// / 转半角的函数(DBC case)
		// /
		// /任意字符串
		// /半角字符串
		// /
		// /全角空格为12288，半角空格为32
		// /其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
		// /
		public static string ToDBC(string input) {
			char[] c = input.ToCharArray();
			for (int i = 0; i < c.Length; i++) {
				if (c[i] == 12288) {
					c[i] = (char)32;
					continue;
				}
				if (c[i] > 65280 && c[i] < 65375)
					c[i] = (char)(c[i] - 65248);
			}
			return new string(c);
		}


		/// <summary>
		/// GB2312转换成UTF8
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static string gb2312_utf8(string text) {
			//声明字符集   
			System.Text.Encoding utf8, gb2312;
			//gb2312   
			gb2312 = System.Text.Encoding.GetEncoding("gb2312");
			//utf8   
			utf8 = System.Text.Encoding.GetEncoding("utf-8");
			byte[] gb;
			gb = gb2312.GetBytes(text);
			gb = System.Text.Encoding.Convert(gb2312, utf8, gb);
			//返回转换后的字符   
			return utf8.GetString(gb);
		}

		/// <summary>
		/// UTF8转换成GB2312
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static string utf8_gb2312(string text) {
			//声明字符集   
			System.Text.Encoding utf8, gb2312;
			//utf8   
			utf8 = System.Text.Encoding.GetEncoding("utf-8");
			//gb2312   
			gb2312 = System.Text.Encoding.GetEncoding("gb2312");
			byte[] utf;
			utf = utf8.GetBytes(text);
			utf = System.Text.Encoding.Convert(utf8, gb2312, utf);
			//返回转换后的字符   
			return gb2312.GetString(utf);
		}
	}
}