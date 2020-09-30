using System;
using System.Windows.Forms;

namespace Mmc.Winform.IO
{
	public class FileDialog
	{
		public static string ShowOpenFileDialog(string filter)
		{
			string result = string.Empty;
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Filter = filter,
				Multiselect = false
			};
			bool flag = openFileDialog.ShowDialog() == DialogResult.OK;
			if (flag)
			{
				result = openFileDialog.FileName;
			}
			return result;
		}

		public static string[] ShowOpenFilesDialog(string filter)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Filter = filter,
				Multiselect = true
			};
			bool flag = openFileDialog.ShowDialog() == DialogResult.OK;
			string[] result;
			if (flag)
			{
				result = openFileDialog.FileNames;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static string ShowSaveFileDialog(string filter)
		{
			string result = string.Empty;
			SaveFileDialog saveFileDialog = new SaveFileDialog
			{
				Filter = filter
			};
			bool flag = saveFileDialog.ShowDialog() == DialogResult.OK;
			if (flag)
			{
				result = saveFileDialog.FileName;
			}
			return result;
		}

		public static string ShowFolderBrowserDialog(string description = "请选择目录")
		{
			string result = string.Empty;
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog
			{
				Description = description
			};
			bool flag = folderBrowserDialog.ShowDialog() == DialogResult.OK;
			if (flag)
			{
				result = folderBrowserDialog.SelectedPath;
			}
			return result;
		}
	}
}
