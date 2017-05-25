/*------------------------------------------------------------------------------------------------
// File:        XMLManager.cs
// Description: The file contains definition of XMLManager class.
// Author:      Kirill V. Sorokin; saron312@yandex.ru
// Copyright:   (c) SPSU, MM 2005
//------------------------------------------------------------------------------------------------
// Modification history:
//----------------------------------------------------------------------------------------------*/
using System;
using System.Xml.Serialization;
using System.IO;
using System.Diagnostics;
namespace Examinator
{
	/// <summary>
	/// Summary description for XMLManager.
	/// </summary>
	public class XMLManager
	{
		
		public XMLManager()
		{
		}

		public object GetSettingsFromXML( string strSettingsFile )
		{
			
			Settings cCurrSettings = new Settings();
			XmlSerializer Serializer = new XmlSerializer( typeof(Settings) );
			try
			{
				TextReader Reader = new StreamReader( strSettingsFile );
				cCurrSettings = (Settings)Serializer.Deserialize( Reader );
				Reader.Close();
				return cCurrSettings;
			}
			catch ( Exception ex)
			{
				Debug.Assert( false, ex.Message );
				throw ex;
			}
		}

		public ExamTree GetTreeFromXML( string strTreeFile )
		{
			XmlSerializer Serializer = new XmlSerializer( typeof(TreeItems) );
			ExamTree cTree = new ExamTree();
			try
			{
				TextReader Reader = new StreamReader( strTreeFile );
				cTree.m_cTreeItems = (TreeItems)Serializer.Deserialize( Reader );
				Reader.Close();
				return cTree;
			}
			catch ( Exception ex)
			{
				Debug.Assert( false, ex.Message );
				throw ex;
			}
		}


	}
}