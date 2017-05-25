using System;
using System.Collections;

namespace Examinator
{
	public enum Brackets
	{
		eNoBrackets   = 1,
		eAllBrackets  = 4,
		eUnknown      = 0
	}

	public enum WhiteSpaces
	{
		eMissed  = 1,
		eOk      = 2,
		eUnknown = 0
	}
	
	public class ExprNode
	{
		public int m_iNumber;
		public int m_iPosition;
		public static int m_iCount;
		public int m_iParent;
		public int m_iLeftChild;
		public int m_iRightChild;
		public ExprNode( int iPar )
		{
			m_iNumber = m_iCount;
			m_iCount++;
			m_iParent = iPar;
			m_iLeftChild = 0;
			m_iRightChild = 0;
		}
		
	}
	
	public class OperationNode: ExprNode
	{
		public string m_strOperation;
		public Brackets m_eBrackets;
		public WhiteSpaces m_eWhiteSpaces;
		public OperationNode(string strOperation, int iPar): base( iPar )
		{
			m_strOperation = strOperation;
			m_eBrackets = Brackets.eAllBrackets;
			m_eWhiteSpaces = WhiteSpaces.eOk;
		}
	}

	public class OperandNode: ExprNode
	{
		public string m_strOperand;
		public OperandNode( string strOperand, int iPar ): base( iPar )
		{
			m_strOperand = strOperand;
	
		}
	}

	public class ExpressionTree
	{
		public ArrayList m_arOperations;
		public ArrayList m_arOperands;
		private string m_strExpr;

 
		public ExpressionTree( string strExpr )
		{
			m_arOperations = new ArrayList();
			m_arOperands = new ArrayList(); 
			m_strExpr = strExpr;
			ExprNode cRoot = new ExprNode( 0 );
			BuildTreeNode( m_strExpr, cRoot );
		}


		public Brackets BracketConsistencyAndOp( ref string strCorrected, ref string strCurrOperation, ref int iPos )
		{
			iPos = 0;
			Brackets eBrackets = Brackets.eNoBrackets;
			if ( strCorrected[0] == '(' && strCorrected[strCorrected.Length-1] == ')' )
			{
				iPos = this.FindOperationAndPos( strCorrected, ref strCurrOperation );
				strCorrected = strCorrected.Remove( 0, 1 );
				strCorrected = strCorrected.Remove( strCorrected.Length - 1, 1 );
				eBrackets = Brackets.eAllBrackets;
				return eBrackets;
			}
			if ( strCorrected[0] != '(' && strCorrected[strCorrected.Length-1] != ')' )
			{
				strCorrected = "(" + strCorrected;
				strCorrected = strCorrected + ")";
				iPos = this.FindOperationAndPos( strCorrected, ref strCurrOperation );
				strCorrected = strCorrected.Remove( 0, 1 );
				strCorrected = strCorrected.Remove( strCorrected.Length - 1, 1 );
				eBrackets = Brackets.eNoBrackets;
				return eBrackets;
			}

			if ( strCorrected[0] != '(' && strCorrected[strCorrected.Length-1] == ')' )
			{
				strCorrected = "(" + strCorrected;
				iPos = this.FindOperationAndPos( strCorrected,  ref strCurrOperation );
				strCorrected = strCorrected.Remove( 0, 1 );
				strCorrected = strCorrected.Remove( strCorrected.Length - 1, 1 );
				eBrackets = Brackets.eNoBrackets;
				return eBrackets;
			}

			if ( strCorrected[0] == '(' && strCorrected[strCorrected.Length-1] != ')' )
			{
				strCorrected = strCorrected + ")";
				iPos = this.FindOperationAndPos( strCorrected, ref strCurrOperation );
				strCorrected = strCorrected.Remove( 0, 1 );
				strCorrected = strCorrected.Remove( strCorrected.Length - 1, 1 );
				eBrackets = Brackets.eNoBrackets;
				return eBrackets;
			}
			eBrackets = Brackets.eUnknown;
			return eBrackets;
		}

		public WhiteSpaces CheckWhiteSpacesConsistency ( ref string strCorrected )
		{
			char chCurrLetter;
			WhiteSpaces eWhiteSpaces = WhiteSpaces.eOk;
			for (int i=0; i<strCorrected.Length; i++)
			{
				chCurrLetter = strCorrected[i];
				if ( chCurrLetter == '+' || chCurrLetter == '-' ||
					chCurrLetter == '/' || chCurrLetter == '*' )
				{	
					if ( strCorrected[i-1] != ' ')
					{
						eWhiteSpaces = WhiteSpaces.eMissed; 
						strCorrected = strCorrected.Insert( i-1, " " );		
					}
					if ( strCorrected[i+1] != ' ')
					{
						eWhiteSpaces = WhiteSpaces.eMissed; 
						strCorrected = strCorrected.Insert( i+1, " " );		
					}
				}
			}
			return eWhiteSpaces;
		}

		public void BuildTreeNode( string strExpr, ExprNode cParentNode )
		{
			if ( CheckWhetherSqrt( strExpr ) )
			{
				int iPos = 0;
				string strOperation = "sqrt";
				int iParNum = cParentNode.m_iNumber;
				OperationNode cNode = new OperationNode( "sqrt", iParNum );
				string strSqrtParams = strExpr.Substring( 4, strExpr.Length - 4 );
				Brackets eBrackets = this.BracketConsistencyAndOp( ref strSqrtParams, ref strOperation, ref iPos );
				cNode.m_eBrackets = eBrackets;
				m_arOperations.Add ( cNode );
				BuildTreeNode( strSqrtParams, cNode );
				return;
			}
			if ( this.CheckWhetherOperation( strExpr ) )
			{
				int iPos = 0;
				string strOperation = "";
				WhiteSpaces eWhiteSpaces = this.CheckWhiteSpacesConsistency( ref strExpr );
				Brackets eBrackets = this.BracketConsistencyAndOp( ref strExpr, ref strOperation, ref iPos );
				string strRightExpr = strExpr.Substring( iPos + 1 );
				string strLeftExpr  = strExpr.Substring( 0, iPos - 2);
				OperationNode cNode = new OperationNode( strOperation, cParentNode.m_iNumber );
				cNode.m_eBrackets = eBrackets;
				cNode.m_eWhiteSpaces = eWhiteSpaces;
				m_arOperations.Add( cNode );
				if ( strLeftExpr.Length != 0)
					BuildTreeNode( strLeftExpr, cNode );
				if ( strRightExpr.Length != 0)
					BuildTreeNode( strRightExpr, cNode );
				return;
			}
			if ( !this.CheckWhetherOperation ( strExpr ) )
			{
				OperandNode cNode = new OperandNode( strExpr, cParentNode.m_iNumber );
				m_arOperands.Add ( cNode );
				return;
			}
		}

		public int FindOperationAndPos( string strExpr, ref string strOperation )
		{
			int iBracketCount = 0;
			char chCurrLetter;
			for (int i=0; i<strExpr.Length; i++)
			{
				chCurrLetter = strExpr[i];
				if ( chCurrLetter == '(' )
					iBracketCount++;
				if ( chCurrLetter == ')' )
					iBracketCount--;
				if ( chCurrLetter == '+' || chCurrLetter == '-' ||
					chCurrLetter == '/' || chCurrLetter == '*' )
					if ( iBracketCount == 1)
					{	
						strOperation = chCurrLetter.ToString();
						return i;
					}
			}
			return -1;
		}

		public bool CheckWhetherOperation ( string strExpr )
		{
			for (int i=0; i<strExpr.Length; i++) 
				if ( strExpr[i] == '+' || strExpr[i] == '-' ||
					 strExpr[i] == '/' || strExpr[i] == '*' )
					return true;
			return false;
		}


	
		public bool CheckWhetherSqrt( string strExpr )
		{
			if ( strExpr.Length < 4 )
				return false;
			string strCurrOp = strExpr.Substring(0, 4);
			if ( strCurrOp == "sqrt" )
				return true;
			return false;
		}
		
	}
}
