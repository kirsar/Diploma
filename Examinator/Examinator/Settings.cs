/*------------------------------------------------------------------------------------------------
// File:        Settings.cs
// Description: The file contains definition of Settings structure.
// Author:      Kirill V. Sorokin; saron312@yandex.ru
// Copyright:   (c) SPSU, MM 2005
//------------------------------------------------------------------------------------------------
// Modification history:
//----------------------------------------------------------------------------------------------*/
using System;

namespace Examinator
{
	/// <summary>
	/// Summary description for Settings.
	/// </summary>
	public struct Settings
	{
		public ExamTree m_cTree;
		public string strTreeFile;
		public string strBaseBufferInFile;
		public string strBaseBufferOutFile;
		public string strBaseExecuteFile;
	}
}
