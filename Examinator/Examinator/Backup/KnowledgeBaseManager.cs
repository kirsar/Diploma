/*------------------------------------------------------------------------------------------------
// File:        KnowledgebaseManager.cs
// Description: The file contains definition of KnowledgebaseManager class.
// Author:      Kirill V. Sorokin; saron312@yandex.ru
// Copyright:   (c) SPSU, MM 2005
//------------------------------------------------------------------------------------------------
// Modification history:
//----------------------------------------------------------------------------------------------*/
using System;
using System.IO;
using System.Diagnostics;

namespace Examinator
{
	/// <summary>
	/// Summary description for KnowledgeBaseManager.
	/// </summary>
	public class KnowledgeBaseManager
	{
		private Settings m_cSettings;
		public KnowledgeBaseManager( Settings cCurrSettings )
		{
			m_cSettings = cCurrSettings;
		}

		public void SetInputData ( string strInputParams )
		{
			try
			{
				using ( StreamWriter swWriter = new StreamWriter(m_cSettings.strBaseBufferInFile) ) 
				{
					swWriter.Write( strInputParams + "#" );
				}
			}
			catch ( Exception ex )
			{
				Debug.Assert( false, ex.Message );
				throw ex;
			}
		}
		public string GetOutputData()
		{
			try
			{
				string strOutputParams;
				using ( StreamReader swReader = new StreamReader(m_cSettings.strBaseBufferOutFile) ) 
				{
					strOutputParams = swReader.ReadToEnd();
				}
				return strOutputParams;
			}
			catch ( Exception ex )
			{
				Debug.Assert( false, ex.Message );
				throw ex;
			}
		}

		public void OpenBase ()
		{
			try
			{
				string strFullPath = Path.GetFullPath( m_cSettings.strBaseExecuteFile ); 
				Process cProcess = new Process();
				cProcess.StartInfo.FileName = strFullPath; 
				cProcess.StartInfo.CreateNoWindow = true;
				cProcess.Start();
			}
			catch ( Exception ex )
			{
				Debug.Assert( false, ex.Message );
				throw ex;
			}
		}
	}
}
