using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using System.Collections;
using Utils;

public class ReText : MonoBehaviour {

	public TextAsset textAsset;

	public string path;
	public char[] punctuations = { '.', ';', '!', '?', '。', '！', '？', '”', '；' };
	public char[] UpperNum = { '一', '二', '三', '四', '五', '六', '七', '八', '九', '十' };

	public string outpath;

	// Use this for initialization
	void Start() {
		HandleByFile(path);
	}

	void Handle(string text) {
	}

	void HandleByFile(string path) {
		StringBuilder sb = new StringBuilder();
		FileStream fs = new FileStream(path, FileMode.Open);
		if (fs != null) {
			StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("gb2312"));
			string line;
			bool lastLineEndWithPunctuation = false;
			while ((line = sr.ReadLine()) != null) {
				line = stringUtils.ToDBC(line);
				if (line.Length == 0) {
					continue;
				}
				if (line.Contains(' ')) {
					var count = line.Count(character => character.Equals(' '));
					if (line.Length == 0 || count > 0.6 * line.Length) {
						continue;
					}
				}
				if (line.Length < 6 && line.Contains("--")) {
					continue;
				}
				if (line.Trim().Length < 10 && line.Contains("论 神")) {
					continue;
				}
				if (line.Contains('(')) {
					List<int> temp = new List<int>();
					for (int i = 0; i < line.Length; i++) {
						if (line[i] == ('(') && UpperNum.Contains(line[i + 1])) {
							temp.Add(i);
						}
					}
					for (int i = 0; i < temp.Count; i++) {
						line = line.Insert(temp[i] + i, "\r\n");
					}
				}
				if (lastLineEndWithPunctuation) {
					sb.AppendLine(line);
				} else {
					sb.Append(line.TrimStart());
				}
				lastLineEndWithPunctuation = punctuations.Contains(line[line.Length - 1]);
			}
			sr.Close();
		}
		fs.Close();
		FileStream newfs = new FileStream(outpath, FileMode.OpenOrCreate);
		StreamWriter sw = new StreamWriter(newfs);
		sw.Write(sb);
		sw.Close();
		newfs.Close();
		Debug.Log("finish");
	}

	void LineHandle(string text) {

	}

}
