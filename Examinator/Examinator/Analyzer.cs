/*------------------------------------------------------------------------------------------------
// File:        Analyzer.cs
// Description: The file contains definition of Analyzer class
// Author:      Kirill V. Sorokin; saron312@yandex.ru
// Copyright:   (c) SPSU, MM 2005
//------------------------------------------------------------------------------------------------
// Modification history:
//----------------------------------------------------------------------------------------------*/
using System;
using System.Collections;
namespace Examinator
{
	// This enumeration describes types of possible mistakes
	public enum Mistake
	{
		eOperation = 1,
		eOperand = 2,
		eBrackets = 3,
		eFatalError = 4,
		eWhiteSpaces = 5,
		eNoMistake = 6,
		eUnknown = 0
	}
	
	
	public class Analyzer
	{
		// Length of operation string
		private const int OP_LENGTH = 4;
		private KnowledgeBaseManager m_cKBaseManager;
		private Settings m_cSettings;
		private ArrayList m_arMistakes;
		public Analyzer( Settings cCurrSettings )
		{
			m_arMistakes = new ArrayList();
			m_cSettings = cCurrSettings;
			m_cKBaseManager = new KnowledgeBaseManager( m_cSettings );
		}
		
		// This function analyzes just an expression
		public Mark AnalyzeSingleExpression ( string strInputParams, string strOutputParam, string strUserExpr )
		{
			m_arMistakes.Clear();
			string strBaseExpr;
			Mark cCurrMark = Mark.eExcellent;
			
			m_cKBaseManager.SetInputData( strInputParams + ';' + strOutputParam );
			m_cKBaseManager.OpenBase();
			strBaseExpr =  m_cKBaseManager.GetOutputData();
			strBaseExpr = GetTopExpr( strBaseExpr );
			
			cCurrMark = this.AnalyzeExpression( strBaseExpr, strUserExpr );	
			return cCurrMark;
		}

		// This function analyzes sequence of expressions 
		public Mark AnalyzeMultiExpression( string strInputParams, string strOutputParam, string strUserExpr )
		{
			ArrayList arBaseExprs = new ArrayList();
			ArrayList arUserExprs = new ArrayList();

			ArrayList arMarks = new ArrayList();
			string strBaseExpr;
			Mark cCurrMark = Mark.eExcellent;
			
			m_cKBaseManager.SetInputData( strInputParams + ';' + strOutputParam );
			m_cKBaseManager.OpenBase();
			strBaseExpr =  m_cKBaseManager.GetOutputData();

			this.GetSingleExprList( ref arBaseExprs, strBaseExpr );
			this.GetSingleExprList( ref arUserExprs, strUserExpr );

			arBaseExprs.Reverse();

			if ( arBaseExprs.Count != arUserExprs.Count )
			{	
				cCurrMark = Mark.eUnsatisfactory;
				return cCurrMark;
			}
		
			for( int i=0; i<arBaseExprs.Count; i++)			
			{
				string strBase = arBaseExprs[i] as string;
				string strUser = arUserExprs[i] as string;
				cCurrMark = this.AnalyzeExpression( strBase, strUser );
				arMarks.Add ( cCurrMark );
			}
			
			arMarks.Sort();
			cCurrMark = (Mark)arMarks[0];
			return cCurrMark;
		}

		// This function converts expression from prefix to infix form
		private string ParseSingleExpression ( string strExpr )
		{
			int iCurrPosition;
			int iCurrCommaPosition;
			string strCurrOperation;
			int iBracketCount = 0;
			char chCurrLetter;
			for (iCurrPosition=0; iCurrPosition<strExpr.Length; iCurrPosition++ )
			{	
				if ( iCurrPosition < strExpr.Length - OP_LENGTH )
				{	
					strCurrOperation = strExpr.Substring(iCurrPosition, OP_LENGTH);
					if ( strCurrOperation == "sqrt" )
						iCurrPosition += OP_LENGTH;
				
					if ( strCurrOperation == "mult" || strCurrOperation == "divd" ||
						strCurrOperation == "summ" || strCurrOperation == "subt" ||
						strCurrOperation == "symb" || strCurrOperation == "flot")
					{
						strExpr = strExpr.Remove( iCurrPosition, OP_LENGTH );
				
						iBracketCount = 0;
						if ( strCurrOperation == "mult" || strCurrOperation == "divd" ||
							 strCurrOperation == "summ" || strCurrOperation == "subt" )
						{	
							for (iCurrCommaPosition=iCurrPosition; iCurrCommaPosition<strExpr.Length; iCurrCommaPosition++)
							{
								chCurrLetter = strExpr[iCurrCommaPosition]; 
								if ( chCurrLetter == ',' )
									if ( iBracketCount == 1)
									{
										strExpr = strExpr.Remove( iCurrCommaPosition, 1 );
										switch ( strCurrOperation )
										{
											case ("mult"):
												strExpr = strExpr.Insert( iCurrCommaPosition, " * " );
												break;
											case ("divd"):
												strExpr = strExpr.Insert( iCurrCommaPosition, " / " );
												break;
											case ("summ"):
												strExpr = strExpr.Insert( iCurrCommaPosition, " + " );
												break;
											case ("subt"):
												strExpr = strExpr.Insert( iCurrCommaPosition, " - " );
												break;
										}
										break;
									}
								if ( chCurrLetter == '(' )
									iBracketCount++;
								if ( chCurrLetter == ')' )
									iBracketCount--;
							}
						}
					
						iBracketCount = 0;
						if ( strCurrOperation == "symb" || strCurrOperation == "flot" ) 
							for ( int iCurrBracketPos=iCurrPosition; iCurrBracketPos<strExpr.Length; iCurrBracketPos++ )
							{	
								chCurrLetter = strExpr[iCurrBracketPos];
								if ( chCurrLetter == '(' )
								{	
									if ( iBracketCount == 0 )
									{	
										strExpr = strExpr.Remove( iCurrBracketPos, 1);
										iBracketCount++;
									}
									else
										iBracketCount++;
								}
								if ( chCurrLetter == ')' )
								{
									if ( iBracketCount == 1 )
									{
										strExpr = strExpr.Remove( iCurrBracketPos, 1);
										iCurrPosition--;
										break;
									}
									else
										iBracketCount--;
								}
							}
					}	
				}
			}
			for (iCurrPosition = 0; iCurrPosition<strExpr.Length; iCurrPosition++ )
			{
				chCurrLetter = strExpr[iCurrPosition];
				if ( chCurrLetter == 'p' )
					if ( strExpr[iCurrPosition + 1] == '2' )
					{	
						strExpr = strExpr.Remove( iCurrPosition, 1);
						strExpr = strExpr.Insert( iCurrPosition, "^" );
					}
			}
			
			strExpr = strExpr.Replace( '"'.ToString(), ""  );
			
			return strExpr;
		}
		
		// This function gets last expression from prolog-base output buffer 
		private string GetTopExpr( string strCurrExpr )
		{
			int iSeparatorPos = strCurrExpr.IndexOf(";");
			strCurrExpr = strCurrExpr.Substring( 0, iSeparatorPos );
			return strCurrExpr;
		}

		// This function gets mark depends on mistakes
		private Mark GetMark()
		{
			Mark eMark = Mark.eExcellent;
			int iMark = 5;
			foreach ( Mistake eMistake in m_arMistakes )
			{
				if ( eMistake == Mistake.eBrackets )
					iMark -=1;
				if ( eMistake == Mistake.eWhiteSpaces )
					iMark -= 1;
				if ( eMistake == Mistake.eOperand )
					iMark -= 2;
				if (eMistake == Mistake.eOperation )
					iMark -= 2;
				if (eMistake == Mistake.eFatalError )
					iMark -= 3;
			}
			if ( 1 < iMark && iMark < 6 )
				eMark = (Mark)iMark;
			else 
				eMark = Mark.eUnsatisfactory;
			return eMark;
		}
		
		// This function converts prolog-base output string to list of strings
		public void GetSingleExprList( ref ArrayList arExprs, string strExpr ) 
		{
			int iLastSepPos = 0;
			string strCurrExpr;
			for (int i=0; i<strExpr.Length; i++)
			{
				if ( strExpr[i] == ';' )
				{			
					strCurrExpr = strExpr.Substring( iLastSepPos, i - iLastSepPos ); 
					iLastSepPos = i+1;
					if ( strCurrExpr.Length != 0 )
						arExprs.Add( strCurrExpr );
				}
			}
		
		}
		
		// This function analyze expresiion in infix form
		public Mark AnalyzeExpression ( string strBaseExpr, string strUserExpr )
		{
			strBaseExpr = ParseSingleExpression( strBaseExpr );
			strBaseExpr = this.GetLeftExpr( strBaseExpr );
			strUserExpr = this.GetLeftExpr( strUserExpr );
			
			ExpressionTree cBaseTree = new ExpressionTree( strBaseExpr );
			ExpressionTree cUserTree = new ExpressionTree( strUserExpr );
			Mark cCurrMark = new Mark();
			if ( cBaseTree.m_arOperands.Count != cUserTree.m_arOperands.Count ||
				cBaseTree.m_arOperations.Count != cUserTree.m_arOperations.Count )
			{	
				cCurrMark = Mark.eUnsatisfactory;
				return cCurrMark;
			}
	
			for (int i=0; i<cBaseTree.m_arOperations.Count; i++)
			{
				Mistake eMistake = Mistake.eFatalError;
				OperationNode cBaseNode = cBaseTree.m_arOperations[i] as OperationNode;
				OperationNode cUserNode = cUserTree.m_arOperations[i] as OperationNode;
				string strBaseOp = cBaseNode.m_strOperation.Trim();
				string strUserOp = cUserNode.m_strOperation.Trim();
				if ( strUserOp == strBaseOp )
				{
					continue;
				}
				if ( ( strUserOp == "+" && strBaseOp == "-" ) ||
					( strUserOp == "-" && strBaseOp == "+" ) ||
					( strUserOp == "*" && strBaseOp == "/" ) ||
					( strUserOp == "/" && strBaseOp == "*" ) )
				{
					eMistake = Mistake.eOperation;
				}
				
				m_arMistakes.Add( eMistake );
			}

			for (int i=0; i<cBaseTree.m_arOperands.Count; i++)
			{
				Mistake eMistake = Mistake.eFatalError;
				OperandNode cBaseNode = cBaseTree.m_arOperands[i] as OperandNode;
				OperandNode cUserNode = cUserTree.m_arOperands[i] as OperandNode;
				string strBaseOp = cBaseNode.m_strOperand.Trim();
				string strUserOp = cUserNode.m_strOperand.Trim();
				if ( strUserOp == strBaseOp )
				{
					continue;
				}
				if ( ( strUserOp == "cos" && strBaseOp == "sin" ) ||
					( strUserOp == "sin" && strBaseOp == "cos" ) ||
					( strUserOp == "tg" && strBaseOp == "ctg" ) ||
					( strUserOp == "ctg" && strBaseOp == "tg" ) ||
					( strUserOp == "cos2" && strBaseOp == "sin2" ) ||
					( strUserOp == "sin2" && strBaseOp == "cos2" ) ||
					( strUserOp == "tg2" && strBaseOp == "ctg2" ) ||
					( strUserOp == "ctg2" && strBaseOp == "tg2" ) ||
					( strUserOp == "cos^2" && strBaseOp == "sin^2" ) ||
					( strUserOp == "sin^2" && strBaseOp == "cos^2" ) ||
					( strUserOp == "tg^2" && strBaseOp == "ctg^2" ) ||
					( strUserOp == "ctg^2" && strBaseOp == "tg^2" ) ||
					( strUserOp == "cos" && strBaseOp == "cos^2" ) ||
					( strUserOp == "sin" && strBaseOp == "sin^2" ) ||
					( strUserOp == "tg" && strBaseOp == "tg^2" ) ||
					( strUserOp == "ctg" && strBaseOp == "ctg^2" ) ||
					( strUserOp == "cos^2" && strBaseOp == "cos" ) ||
					( strUserOp == "sin^2" && strBaseOp == "sin" ) ||
					( strUserOp == "tg^2" && strBaseOp == "tg2" ) ||
					( strUserOp == "ctg^2" && strBaseOp == "ctg" ) )
				{		
					eMistake = Mistake.eOperand;
				}
				
				m_arMistakes.Add( eMistake );
			}
			foreach ( OperationNode cNode in cUserTree.m_arOperations )
			{	
				if ( cNode.m_eBrackets != Brackets.eAllBrackets )
					m_arMistakes.Add( Mistake.eBrackets );
				if ( cNode.m_eWhiteSpaces != WhiteSpaces.eOk )
					m_arMistakes.Add ( Mistake.eWhiteSpaces );
			}
			cCurrMark = this.GetMark();
			return cCurrMark;
		}

		// This function gets string on the right side from "=" sign
		public string GetLeftExpr( string strExpr )
		{
			int iRightExprBeg = strExpr.IndexOf( "=" );
			iRightExprBeg = iRightExprBeg + 2;
			strExpr = strExpr.Substring( iRightExprBeg );
			return strExpr;
		}

	}

}