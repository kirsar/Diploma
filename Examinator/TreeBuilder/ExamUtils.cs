/*------------------------------------------------------------------------------------------------
// File:        ExanUtils.cs
// Description: The file contains definition of ExamTree class and some classes working with it.
// Author:      Kirill V. Sorokin; saron312@yandex.ru
// Copyright:   (c) SPSU, MM 2005
//------------------------------------------------------------------------------------------------
// Modification history:
//----------------------------------------------------------------------------------------------*/
using System;
using System.Collections;

namespace TreeBuilder
{
	public enum Mark
	{
		eExcellent      = 5,
		eGood           = 4,
		eSatisfactory   = 3,
		eUnsatisfactory = 2,
		eUnknown        = 0
	}
	
	public class TreeItem
	{
		private int m_iNumber;
		private int m_iExChild;
		private int m_iGoodChild;
		private int m_iSatChild;
		private int m_iUnsatChild;
		private string strQuestionItem;
		private string strInput;
		private string strOutput;
		private bool m_bMultiExpr;
		private Mark m_cMark;
		private int m_iNextChild;
		
		private static int s_iCount;
		
		public TreeItem()
		{
			s_iCount++;
			m_iNumber = s_iCount;
			m_cMark = new Mark();
		}
		#region Public Properties
		public int Number
		{
			get 
			{
				return m_iNumber;
			}
			set
			{
				m_iNumber = value;
			}
		}

		public string QuestionItem
		{
			get 
			{
				return strQuestionItem;
			}
			set
			{
				strQuestionItem = value;
			}
		}
		
		public string Input
		{
			get 
			{
				return strInput;
			}
			set
			{
				strInput = value;
			}
		}

		public string Output
		{
			get 
			{
				return strOutput;
			}
			set
			{
				strOutput = value;
			}
		}

		public bool MultiExpr
		{
			get
			{
				return m_bMultiExpr;
			}
			set
			{
				m_bMultiExpr = value;
			}
		}

		public int ExChild
		{
			get 
			{
				return m_iExChild;
			}
			set
			{
				m_iExChild = value;
			}
		}

		public int SatChild
		{
			get 
			{
				return m_iSatChild;
			}
			set
			{
				m_iSatChild = value;
			}
		}

		public int UnsatChild
		{
			get 
			{
				return m_iUnsatChild;
			}
			set
			{
				m_iUnsatChild = value;
			}
		}
		public int GoodChild
		{
			get 
			{
				return m_iGoodChild;
			}
			set
			{
				m_iGoodChild = value;
			}
		}


		public int NextChild
		{
			get 
			{
				return m_iNextChild;
			}
			set
			{
				m_iNextChild = value;
			}
		}

		public Mark Mark
		{	
			get
			{
				return m_cMark;
			}
			set
			{
				m_cMark = value;
			}
		}
		#endregion

	}

	public class ExamTree
	{
		public TreeItems m_cTreeItems;
		public Marks     m_cMarks;
		public int       m_iCurrItem;
		public ExamTree()
		{
			m_cTreeItems = new TreeItems();
			m_cMarks     = new Marks();
		}
	
		
		#region Public Methods 
		
		public TreeItem GetNextItem()
		{
			
			if ( m_iCurrItem == 0 )
				m_iCurrItem = 1;
			else
				m_iCurrItem = GetItemByNumber( m_iCurrItem ).NextChild;
			return GetItemByNumber( m_iCurrItem );
		}

		public TreeItem GetCurrItem()
		{
			return GetItemByNumber(m_iCurrItem);
		}

		

		public void SetItemChild ( Mark cMark )
		{
			int iCurrIndex = GetIndexByNumber( m_iCurrItem );
			switch ( cMark )
			{
				case ( Mark.eExcellent ):
					m_cTreeItems[iCurrIndex].NextChild = m_cTreeItems[iCurrIndex].ExChild;
					break;
				case ( Mark.eGood ):
					m_cTreeItems[m_iCurrItem].NextChild = m_cTreeItems[iCurrIndex].GoodChild;
					break;
				case (Mark.eSatisfactory ):
					m_cTreeItems[m_iCurrItem].NextChild = m_cTreeItems[m_iCurrItem].SatChild;
					break;
				case ( Mark.eUnsatisfactory ):
					m_cTreeItems[m_iCurrItem].NextChild = m_cTreeItems[m_iCurrItem].UnsatChild;
					break;
			}
		}

		
		public int SetNextItem ( int iCurrTreeItem, int iCurrMark )
		{
			return 0;
		}
		public int GetIndexByNumber ( int iNumber )
		{
			for (int i=0; i<m_cTreeItems.Count; i++ )
				if ( m_cTreeItems[i].Number == iNumber )
					return i;
			return -1;
		}
        #endregion

		private TreeItem GetItemByNumber( int iNumber )
		{
			foreach ( TreeItem cItem in m_cTreeItems )
				if ( cItem.Number == iNumber )
					return cItem;
			return null;
		}



	}
	public class TreeItems: ICollection
	{
		
		public string CollectionName;
		private ArrayList arTreeItems = new ArrayList(); 
		public TreeItem this[int index]
		{
			get
			{
				return (TreeItem) arTreeItems[index];
			}
		}
    
		public int Count
		{
			get
			{
				return arTreeItems.Count;
			}
		}

		public object SyncRoot
		{
			get
			{
				return this;
			}
		}
		
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		public void CopyTo(Array arr, int index)
		{
			arTreeItems.CopyTo(arr, index);
		}
		
		public IEnumerator GetEnumerator()
		{
			return arTreeItems.GetEnumerator();
		}

		public void Add(TreeItem newTreeItem)
		{
			arTreeItems.Add(newTreeItem);
		}
		public void RemoveAt( int index)
		{
			arTreeItems.RemoveAt( index );		
		}
	}
	
	public class Marks: ICollection
	{
		public string CollectionName;
		private ArrayList arMarks = new ArrayList(); 

		public Mark this[int index]
		{
			get
			{
				return (Mark) arMarks[index];
			}
		}
    
		public int Count
		{
			get
			{
				return arMarks.Count;
			}
		}
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}
		
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		public void CopyTo(Array arr, int index)
		{
			arMarks.CopyTo(arr, index);
		}
		
		public IEnumerator GetEnumerator()
		{
			return arMarks.GetEnumerator();
		}

		public void Add(Mark newMark)
		{
			arMarks.Add(newMark);
		}
	}
}

