using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Reflection;
using System.Globalization;
using System.Drawing;

namespace KBCreator
{
	/// <summary>
	/// Summary description for KBManager.
	/// </summary>
	public class KBManager : IDisposable
	{
		public KBManager()
		{
		    Relations = new RelationsList();
            RelationsTypes = new RelationsList();
            NonKBObjects = new ArrayList();
            Facts = new ArrayList();
            ReadId();
        }

        public void Dispose()
        {
            SaveId();
        }

		public RelationsList Relations;
        public RelationsList RelationsTypes;
        public ArrayList     NonKBObjects;
        public ArrayList     Facts;
     
        public string UniversePath = "Universe.xml";
        public string KBasePath = "KBase.Xml";
        public string EnlargePath = "UniExtents.xml";
        public string PCodePath = "KBase.txt";

        public PGenerationOption PCodeOption = PGenerationOption.eCreateNew;
        private TextWriter pcodeWriter;
   
        private long uniqueid;
        public long Uniqueid { get { return( ++uniqueid ); } } 

        
        #region IdGeneration
        private void ReadId()
        {
            XmlTextReader reader = new XmlTextReader( Path.GetDirectoryName( Assembly.GetEntryAssembly().Location ) + "uniqueid.xml" );
            try
            {
                XmlSerializer serializer = new XmlSerializer( typeof(long ) );
                long Uniqueid = (long)serializer.Deserialize( reader );
            }
            catch ( Exception )
            {
            }
            finally
            {
                reader.Close();
            }
        }

        private void SaveId()
        {
            XmlTextWriter writer = new XmlTextWriter( Path.GetDirectoryName( Assembly.GetEntryAssembly().Location ) + "uniqueid.xml", null );
            try
            {
                XmlSerializer serializer = new XmlSerializer( typeof(long ) );
                serializer.Serialize( writer, Uniqueid );
            }
            catch ( Exception )
            {
            }
            finally
            {
                writer.Close();
            }
        }
        #endregion

        #region Universe Management
        public void SaveUniverse()
        {
            XmlTextWriter writer = new XmlTextWriter( UniversePath, null );
            try
            {
                XmlDocument universeDoc = new XmlDocument();

                XmlElement relationTypes = universeDoc.CreateElement( "reltypes" );
                universeDoc.AppendChild( relationTypes );
           
                foreach ( Relation rel in this.RelationsTypes )
                {
                    rel.Serialize( universeDoc, relationTypes );
                }
                universeDoc.Save( writer ); 
            }
            catch ( Exception )
            {
            }
            finally
            {
                writer.Close();
            }
        }

        public void LoadUniverse()
        {
            XmlTextReader reader = new XmlTextReader( UniversePath );
            try
            {
                XmlDocument universeDoc = new XmlDocument();
                universeDoc.Load( reader );

                XmlNodeList relationTypes = universeDoc.GetElementsByTagName( "relation" );
                foreach( XmlNode relation in relationTypes )
                {
                    this.RelationsTypes.Add( Relation.Deserialize( relation as XmlElement ) );
                }

                foreach( Relation rel in this.RelationsTypes )
                {
                    if ( rel.PairRelation == null )
                        continue;

                    foreach( Relation relPair in RelationsTypes )
                        if ( rel.PairRelation.Name == relPair.Name )
                        {
                            rel.PairRelation = relPair;
                            break;
                        }
                }
            }
            catch ( Exception )
            {
            }
            finally
            {
                reader.Close();
            }
        }
        
        public void EnlargeUniverse()
        {
            XmlTextReader reader = new XmlTextReader( EnlargePath );
            try
            {
                XmlDocument enlargeDoc = new XmlDocument();
                enlargeDoc.Load( reader );

                XmlElement relationTypes = (XmlElement)enlargeDoc.FirstChild;
                XmlNodeList relations = relationTypes.ChildNodes;
                foreach( XmlNode relation in relations )
                {
                    this.RelationsTypes.Add( Relation.Deserialize( relation as XmlElement ) );
                }

                foreach( Relation rel in this.RelationsTypes )
                {
                    if ( rel.PairRelation == null )
                        continue;

                    foreach( Relation relPair in relationTypes )
                        if ( rel.Name == relPair.Name )
                        {
                            rel.PairRelation = relPair;
                            break;
                        }
                }
            }
            catch ( Exception )
            {
            }
            finally
            {
                reader.Close();
            }
        }

        
        
        public void ClearUniverse()
        {
            this.RelationsTypes.Clear();
            this.Relations = new RelationsList();
            this.NonKBObjects.Clear();
        }
        #endregion

        #region Base Management
        public void SaveBase()
        {
            XmlTextWriter writer = new XmlTextWriter( KBasePath, null );
            try
            {
                XmlDocument baseDoc = new XmlDocument();

                XmlElement baseElem = baseDoc.CreateElement( "base" );
                baseDoc.AppendChild( baseElem );

                foreach ( Relation rel in formMain.Manager.Relations )
                {
                    rel.Serialize( baseDoc, baseElem );
                }

                ArrayList objectsList = new ArrayList();
                foreach ( Relation rel in this.Relations )
                {
                    if ( rel.FirstObject is KBObject )
                    {
                        if ( !objectsList.Contains( rel.FirstObject as KBObject ) )
                            objectsList.Add( rel.FirstObject as KBObject );
                    }
                    if ( rel.SecondObject is KBObject )
                    {
                        if ( !objectsList.Contains( rel.SecondObject as KBObject ) )
                             objectsList.Add( rel.SecondObject as KBObject );
                    }
                }

                foreach ( object obj in objectsList )
                {
                    (obj as KBObject).Serialize( baseDoc, baseElem );
                }
                
                baseDoc.Save( writer ); 
            }
            catch ( Exception )
            {
            }
            finally
            {
                writer.Close();
            }

        }

        public void LoadBase()
        {
            XmlTextReader reader = new XmlTextReader( KBasePath );
            try
            {
                XmlDocument baseDoc = new XmlDocument();
                baseDoc.Load( reader );

                XmlNodeList relations = baseDoc.GetElementsByTagName( "relation" );
                foreach( XmlNode relation in relations )
                {
                    this.Relations.Add( Relation.Deserialize( relation as XmlElement ) );
                }

                ArrayList objectsList = new ArrayList();
                XmlNodeList objects = baseDoc.GetElementsByTagName( "object" );
                foreach( XmlNode obj in objects )
                {
                    objectsList.Add( KBObject.Deserialize( obj as XmlElement ) );
                }
                
                foreach( Relation rel in this.Relations )
                {
                    if ( rel.PairRelation == null )
                        continue;

                    foreach( Relation relPair in RelationsTypes )
                        if ( rel.PairRelation.Name == relPair.Name )
                        {
                            rel.PairRelation = relPair;
                            break;
                        }
                }
                
                foreach( Relation rel in this.Relations )
                {
                    if ( rel.FirstObject is KBObject )
                    {
                        foreach( KBObject obj in objectsList )
                        {
                            if ( obj.ObjectId == (rel.FirstObject as KBObject).ObjectId )
                            {
                                rel.FirstObject = obj;
                                break;
                            }
                        }
                    }
                    else if ( rel.FirstObject is Relation )
                    {
                        foreach( Relation connRel in this.Relations )
                        {
                            if ( connRel.RelationId == (rel.FirstObject as Relation).RelationId )
                            {
                                rel.FirstObject = connRel;
                                break;
                            }
                        }
                    }
                    if ( rel.SecondObject is KBObject )
                    {
                        foreach( KBObject obj in objectsList )
                        {
                            if ( obj.ObjectId == (rel.SecondObject as KBObject).ObjectId )
                            {
                                rel.SecondObject = obj;
                                break;
                            }
                        }
                    }
                    else if ( rel.SecondObject is Relation )
                    {
                        foreach( Relation connRel in this.Relations )
                        {
                            if ( connRel.RelationId == (rel.SecondObject as Relation).RelationId )
                            {
                                rel.SecondObject = connRel;
                                break;
                            }
                        }
                    }
                }
            }
            catch ( Exception )
            {
            }
            finally
            {
                reader.Close();
            }
        }

        public void ClearBase()
        {
            Relations = new RelationsList();
            NonKBObjects.Clear();
        }
        #endregion

        #region Validations
        public bool ValidatekNonKB()
        {
            return ( NonKBObjects.Count == 0 );
        }

        public bool ValidateConnections()
        {
            bool bValid = true;
            foreach ( Relation rel in Relations )
            {
                if ( rel.Connected == false )
                {
                    bValid = false;
                    break;
                }
            }
            return bValid;
        }

        public bool ValidatePrimary()
        {
            bool bValid = false;
            foreach( Relation rel in Relations )
            {
                if ( rel.Primary )
                {
                    bValid = true;
                    break;
                }
            }
            return bValid;
        }

        public bool ValidateCycles()
        {
            bool bValid = true;
            return ( bValid );
        }

        public bool GraphBaseIsValid()
        {
            return( ValidateConnections() && 
                    ValidatekNonKB() && 
                    ValidatePrimary() && 
                    ValidateCycles() );
        }

        public bool TextBaseIsValid()
        {
            return ( ValidateBrackets() &&
                     ValidateRelationNames() &&
                     ValidateObjNames() );
        }

        public bool ValidateBrackets ()
        {
            bool bValid = true;
            return ( bValid );
        }

        public bool ValidateRelationNames()
        {
            bool bValid = true;
            return ( bValid );
        }

        public bool ValidateObjNames()
        {
            bool bValid = true;
            return ( bValid );
        }
        public bool ValidateTextFatct( string fact )
        {
            int iCount = 0;
            for (int i=0; i<fact.Length; i++ )
            {
                if ( fact[i] == '(' )
                    iCount++;
                else if ( fact[i] == ')' )
                    iCount--;
            }
            return ( iCount == 0 );
        }
        #endregion

        #region Facts Generation
        public void GenerateFacts()
        {
            this.Facts.Clear();
            //if ( !GraphBaseIsValid() )
            //    return;

            foreach ( KBObject obj in Relations.Objects )
            {
                /*if ( obj.Name == "2" || obj.Name == "1")
                    continue;*/
                RelationsList fact = (RelationsList)this.Relations.Clone();
                fact.MainObject = obj;
                GenerateFact( fact );
                fact.Fact = fact.GetStringFact();
                Facts.Add( fact );
                
                /*RelationsList tmp = this.Relations;
                this.Relations = fact;
                this.KBasePath = "Debug-obj=" + obj.Name + ".xml"; 
                this.SaveBase();
                this.Relations = tmp;*/
            }
        }

        private void GenerateFact( RelationsList fact )
        {
            while ( !fact.IsSimple() )
            {
                SingleGeneration( fact );
            }
            fact.NormalizeByKeyRelation();
        }

        private void SingleGeneration( RelationsList fact )
        {
            Relation objSide = (Relation)fact.DetermineObjectSide( fact.KeyRelation );
            Relation pair = this.RelationsTypes.CreatePair( objSide.PairRelation.Name );
            Object otherSide = null;
            Object objToStay = null;
            if ( fact.KeyRelation.FirstObject != objSide )
                otherSide = fact.KeyRelation.FirstObject;
            else
                otherSide = fact.KeyRelation.SecondObject;
            pair.FirstObject = otherSide;
            if ( objSide.Arity == RelationArity.Unary )
            {
                objToStay = objSide.FirstObject;
            }
            
            else if ( objSide.Arity == RelationArity.Binary )
            {
                if ( !objSide.Commutative )
                {
                    pair.SecondObject = objSide.SecondObject;
                    objToStay = objSide.FirstObject;
                }
                else
                {
                    if ( fact.DetermineObjectSide( objSide ) == objSide.FirstObject )
                    {
                        pair.SecondObject = objSide.SecondObject;
                        objToStay = objSide.FirstObject;
                    }
                    else
                    {
                        pair.SecondObject = objSide.FirstObject;
                        objToStay = objSide.SecondObject;
                    }
                }
            }
            
            if  ( fact.KeyRelation.FirstObject == otherSide )
            {
                fact.KeyRelation.FirstObject = pair;
                fact.KeyRelation.SecondObject = objToStay;
            }
            else
            {    
                fact.KeyRelation.FirstObject = objToStay;
                fact.KeyRelation.SecondObject = pair;
            }
        }
        
        private Object FindTopLevelRel( string fact )
        {
            int iCount = 0;
            for(int i=0; i<fact.Length; i++)
            {
                if ( fact[i] == '(' )
                    iCount++;

                if ( fact[i] == ')' )
                    iCount--;

                if ( fact[i] == '(' || fact[i] == ')')
                    continue;

                if ( iCount == 1 )
                {
                    int iZoneEnd = 0;
                    int iOpenBr =  fact.IndexOf( "(", i );
                    int iCloseBr = fact.IndexOf( ")", i );
                    if ( iOpenBr == - 1 )
                        iZoneEnd = iCloseBr;
                    else if ( iCloseBr == -1 )
                        iZoneEnd = iOpenBr;
                    else
                        iZoneEnd = (iOpenBr <= iCloseBr) ? iOpenBr : iCloseBr;
                    string relName = fact.Substring( i, iZoneEnd - i );
                    string leftPart = fact.Substring( 0, i /*+ iIndex*/ ).Remove( 0, 1 );
                    string rightPart = fact.Substring( i + /*iIndex*/ + relName.Length );
                    rightPart = rightPart.Remove( rightPart.Length -1 , 1);
                    Relation type = this.RelationsTypes.GetRelationByName( relName );
                    if ( type == null )
                        break;
                    Relation rel = new Relation( type );
                    if ( rel.Arity == RelationArity.Unary )
                    {
                        rel.FirstObject = this.FindTopLevelRel( rightPart );
                    }    
                    else if ( rel.Arity == RelationArity.Binary )
                    {
                        rel.FirstObject = this.FindTopLevelRel( leftPart );
                        rel.SecondObject = this.FindTopLevelRel( rightPart );
                    }
                    return ( rel );
                }
            }
            KBObject obj = new KBObject( this.Uniqueid );
            obj.Name = fact.Remove(0,1).Remove(fact.Length - 2, 1); 
            if ( !this.FilterName( obj.Name ) )
                obj.PCodeName = obj.Name;
            else
                obj.PCodeName = "obj" + obj.Name;
            return ( obj );

        }

        public void GeneratePCodeFromText( string fact )
        {
            this.Relations.Clear();
            if ( this.ValidateTextFatct( fact ) )
            {
                this.Relations.Add( FindTopLevelRel( fact ) as Relation );
                this.GenerateFacts();
                //this.GeneratePCode();
            }
        }

        #endregion

        #region PCode Generation
        public enum PGenerationOption
        {
            eUnknown = 0,
            eCreateNew = 1,
            eAddFacts = 2,
            eApplyFactts = 3
        };

       
        public void GeneratePCode()
        {
            //GenerateFacts();
            
            pcodeWriter = File.CreateText( PCodePath );
            
            GenerateDomains();
            
            if ( this.PCodeOption == PGenerationOption.eCreateNew )
                GenerateDatabase();    
            
            GeneratePredicates();
            
            GenerateClauses();
            
            if ( this.PCodeOption == PGenerationOption.eCreateNew )
                GenerateGoal();           

            pcodeWriter.Close();
        }

        private void GenerateDomains()
        {
            pcodeWriter.WriteLine( "domains");
            pcodeWriter.WriteLine( "file  =    datafile" );
            pcodeWriter.WriteLine( "term  =    flot  (real);" );
            pcodeWriter.WriteLine( "symb   (symbol); ");
              
            GenerateKBaseDomains();
            
            pcodeWriter.WriteLine( "datum =    val    (symbol, term)" );
            pcodeWriter.WriteLine( "data  =    datum*" );
            pcodeWriter.WriteLine( "solvs =    symbol*" );
            pcodeWriter.WriteLine( "params=    string*" );
            pcodeWriter.WriteLine( "" );
        }

        private void GenerateKBaseDomains()
        {
            string pCode = "";
            foreach( Relation rel in RelationsTypes )
            {
                if ( rel.Primary )
                    continue;
                if ( pCode.Length != 0 )
                {
                    pCode += ";";
                    pcodeWriter.WriteLine( pCode );
                }
                if ( rel.Arity == RelationArity.Unary)
                    pCode =  rel.PCodeName + "   (term)";
                else if ( rel.Arity == RelationArity.Binary )
                    pCode = rel.PCodeName + "   (term, term)";
            }
            pcodeWriter.WriteLine( pCode );
            pcodeWriter.WriteLine( "" );
        }

        private void GenerateDatabase()
        {
            pcodeWriter.WriteLine( "database" );
            pcodeWriter.WriteLine( "expr   (symbol, term)" );
            pcodeWriter.WriteLine( "internal(symbol, term)" );
            pcodeWriter.WriteLine( "hasVal (symbol)" );
            pcodeWriter.WriteLine( "" );
        }

        private void GeneratePredicates()
        {
            
            if ( this.PCodeOption == PGenerationOption.eCreateNew )
                GenerateAuxilliaryPredicates();

            GenerateKBBasePredicates();
        }
        
        private void GenerateKBBasePredicates()
        {
            ArrayList objects = new ArrayList();
            foreach( KBObject obj in this.Relations.KeyRelation.EnumObjects( objects ) )
            {
                pcodeWriter.WriteLine( obj.PCodeName );
            }
            pcodeWriter.WriteLine( "" );
        }

        private void GenerateAuxilliaryPredicates()
        {
            pcodeWriter.WriteLine( "predicates" );
            pcodeWriter.WriteLine( "p       (symbol)" );
            pcodeWriter.WriteLine( "pp      (symbol)" );
            pcodeWriter.WriteLine( "giveval (symbol, term)" );
            pcodeWriter.WriteLine( "" );
            pcodeWriter.WriteLine( "findval (symbol)" );
            pcodeWriter.WriteLine( "find    (solvs)" );
            pcodeWriter.WriteLine( "given   (data)" );
            pcodeWriter.WriteLine( "giveInternalVal(symbol, term)" );
            pcodeWriter.WriteLine( "" );    
            pcodeWriter.WriteLine( "getResult()" );
            pcodeWriter.WriteLine( "showTerms(symbol)" );
            pcodeWriter.WriteLine( "operateParams(params, string)" );
            pcodeWriter.WriteLine( "operateInpChar(params, string, char)" );
            pcodeWriter.WriteLine( "fullfillInput(params)" );
            pcodeWriter.WriteLine( "append(params, params, params)" );
            pcodeWriter.WriteLine( "" );
        }
        
        private void GenerateClauses()
        {
            if ( this.PCodeOption == PGenerationOption.eCreateNew )
                GenerateAuxilliaryClauses();
   
            GenerateKBaseClauses();
        }
        
        private void GenerateKBaseClauses()
        {
            ArrayList objects = new ArrayList();
            foreach( KBObject obj in this.Relations.KeyRelation.EnumObjects( objects ) )
            {
                pcodeWriter.WriteLine( "findVal(" + obj.PCodeName + ")  :- " + obj.PCodeName  +".");
            }
            
            pcodeWriter.WriteLine( "" );
            foreach( RelationsList fact in Facts )
            {
                GeneratePFact( fact );
            }
        }

        private void GeneratePFact( RelationsList fact )
        {
            string objName = fact.MainObject.PCodeName;
            pcodeWriter.WriteLine( objName + ":- expr (" + objName + ",_); internal(" + objName + ",_), !." );
            
            string arguments = "";
            foreach ( KBObject obj in fact.Objects )
            {
                if ( obj.ObjectId != fact.MainObject.ObjectId )
                    arguments += obj.PCodeName + ",";
            }

            string pcodeFact = (fact.KeyRelation.SecondObject as Relation ).GetPCodeRelation();
            string keyRel = fact.KeyRelation.Name;
            
            pcodeWriter.WriteLine( objName + " :- p(" + objName + "), " + arguments + " giveval(" + objName + ", " + pcodeFact + "), asserta(hasVal(" + objName + "))." );
        }

        private void GenerateAuxilliaryClauses()
        {
            pcodeWriter.WriteLine( "clauses" );
            pcodeWriter.WriteLine( "pp(X) :- asserta(hasVal(X))." );
            pcodeWriter.WriteLine( "pp(X) :- retract(hasVal(X)), fail." );
            pcodeWriter.WriteLine( "" );
            pcodeWriter.WriteLine( "p(X) :- not(hasVal(X)), pp(X)." );
            pcodeWriter.WriteLine( "" );
            pcodeWriter.WriteLine( "giveVal (X, E) :- asserta(expr(X, E))." );
            pcodeWriter.WriteLine( "giveInternalVal(X, E) :- asserta(internal(X, E))." );
            pcodeWriter.WriteLine( "" );
            pcodeWriter.WriteLine( "given([])." );
            pcodeWriter.WriteLine( "given([val(N,E)|T]) :- giveInternalVal(N,E)," );
            pcodeWriter.WriteLine( "given(T)." );
            pcodeWriter.WriteLine( "" );
            pcodeWriter.WriteLine( "find([])." );
            pcodeWriter.WriteLine( "find([H|T]) :- findVal(H),!,find(T)." );
            pcodeWriter.WriteLine( "" );
            pcodeWriter.WriteLine( "getResult() :- openread(datafile, " + ('"').ToString() + "buffin.txt"+ ('"').ToString() + ")," );
            pcodeWriter.WriteLine( new string( ' ', 8 ) + "readdevice(datafile)," );
            pcodeWriter.WriteLine( new string( ' ', 8 ) + "operateParams([], " + ('"').ToString() + ('"').ToString() + ")." );
            pcodeWriter.WriteLine( "" );
            pcodeWriter.WriteLine( "fullFillInput([])." );
            pcodeWriter.WriteLine( "fullFillInput([H|T]) :- given([val(H, symb(H))])," );
            pcodeWriter.WriteLine( new string( ' ', 8 ) + "fullFillInput(T)." );
            pcodeWriter.WriteLine( "" );
            pcodeWriter.WriteLine( "operateParams(ListIn, StrCurr) :- readchar(Ch)," );
            pcodeWriter.WriteLine( new string( ' ', 8 ) + "operateInpChar(ListIn, StrCurr, Ch)." );			
            pcodeWriter.WriteLine( "" );
            pcodeWriter.WriteLine( "operateInpChar(ListIn, StrCurr, Ch) :- Ch = ';'," );
            pcodeWriter.WriteLine( new string( ' ', 8 ) + "append(ListIn, [StrCurr], ListIn1)," );
            pcodeWriter.WriteLine( new string( ' ', 8 ) + "operateParams(ListIn1, " + ('"').ToString() +  ('"').ToString() +  ")." );
            pcodeWriter.WriteLine( "operateInpChar(ListIn, StrCurr, Ch) :- Ch = '#'," );
            pcodeWriter.WriteLine( new string( ' ', 8 ) + "closefile(datafile)," );
            pcodeWriter.WriteLine( new string( ' ', 8 ) + "fullFillInput(ListIn)," );
            pcodeWriter.WriteLine( new string( ' ', 8 ) + "find([StrCurr])," );
            pcodeWriter.WriteLine( new string( ' ', 8 ) + "openwrite(datafile, " + ('"').ToString() + "buffout.txt" + ('"').ToString() + ")," );
            pcodeWriter.WriteLine( new string( ' ', 8 ) + "writedevice(datafile)," );
            pcodeWriter.WriteLine( new string( ' ', 8 ) + "showTerms(StrCurr)," );
            pcodeWriter.WriteLine( new string( ' ', 8 ) + "writedevice(screen)," );
            pcodeWriter.WriteLine( new string( ' ', 8 ) + "closefile(datafile). " );
            pcodeWriter.WriteLine( "operateInpChar(ListIn, StrCurr, Ch) :- not(Ch = '#')," );
            pcodeWriter.WriteLine( new string( ' ', 8 ) + "not(Ch = ';')," );
            pcodeWriter.WriteLine( new string( ' ', 8 ) + "str_char(Str, Ch)," );
            pcodeWriter.WriteLine( new string( ' ', 8 ) + "concat(StrCurr, Str, StrCurr1)," );
            pcodeWriter.WriteLine( new string( ' ', 8 ) + "operateParams(ListIn, StrCurr1)." );
            pcodeWriter.WriteLine( "" );
            pcodeWriter.WriteLine( "append([],List, List). " );
            pcodeWriter.WriteLine( "append([H|T1],List2,[H|T3]) :- append(T1,List2,T3)." ); 
            pcodeWriter.WriteLine( "" );
            pcodeWriter.WriteLine( "showTerms(X) :- expr(C, Outterm)," );
            pcodeWriter.WriteLine( new string( ' ', 8 ) + "write( C, " + ('"').ToString() + " = " + ('"').ToString() + ", Outterm, " + ('"').ToString() + ";" + ('"').ToString() + " )," );
            pcodeWriter.WriteLine( new string( ' ', 8 ) + "nl," );
            pcodeWriter.WriteLine( new string( ' ', 8 ) + "fail." );
            pcodeWriter.WriteLine( "" );
        }
        
        private void GenerateGoal()
        {
            pcodeWriter.WriteLine( "goal");
            pcodeWriter.WriteLine( "    getResult." );
        }
        
        public bool FilterName( string name )
        {
            if ( name.Length == 0 )
                return ( false );
            if ( name[0] == '+' ||
                name[0] == '-' ||
                name[0] == '*' ||
                name[0] == '/' ||
                name[0] == '\\' ||
                name[0] == '!' ||
                name[0] == '@' ||
                name[0] == '#' ||
                name[0] == '$' ||
                name[0] == '%' ||
                name[0] == '^' ||
                name[0] == '&' ||
                name[0] == '(' ||
                name[0] == ')' ||
                name[0] == '=' ||
                name[0] == '|' ||
                name[0] == '~' ||
                name[0] == ':' ||
                name[0] == ';' ||
                name[0] == '?' ||
                name[0] == '.' ||
                name[0] == ',' ||
                name[0] == '+' ||
                name[0] == '>' ||
                name[0] == '<' )
                return ( true );
            if ( name[0] == name.Substring(0, 1).ToUpper()[0] )
                return ( true );
            try
            {   
                Convert.ToInt32( name[0] );
            }
            catch ( Exception )
            {
                return ( true );
            }
            return ( false );
        }

        #endregion

        #region XML auxilliaries
        public string ValueToString( object value, Type type )
        {
            if ( typeof ( string ) == type )
                return ( (string) value );
            else if ( typeof ( long ) == type )
                return ( (long) value ).ToString( NumberFormatInfo.InvariantInfo );
            else if ( typeof ( float ) == type )
                return ( (float) value ).ToString( NumberFormatInfo.InvariantInfo );
            else if ( typeof ( bool ) == type )
                return ( (bool) value ).ToString();
            else if ( typeof ( RelationArity ) == type )
                return ( (RelationArity) value ).ToString( NumberFormatInfo.InvariantInfo );
            else if ( typeof ( Point ) == type )
            {
                Point point = (Point) value;
                return ( point.X.ToString( NumberFormatInfo.InvariantInfo ) + "; " + point.Y.ToString( NumberFormatInfo.InvariantInfo ) );
            }
            return null;
        }

        public object StringToValue( string s, Type type )
        {
            if ( typeof ( string ) == type )
                return s;
            else if ( typeof ( long ) == type )
                return long.Parse( s, NumberFormatInfo.InvariantInfo );
            else if ( typeof ( float ) == type )
                return float.Parse( s, NumberFormatInfo.InvariantInfo );
            else if ( typeof ( bool ) == type )
                return bool.Parse( s );
            else if ( typeof ( RelationArity ) == type )
                return Enum.Parse( typeof ( RelationArity ), s );
            else if ( typeof ( Point ) == type )
            {
                string[] ss = s.Split( ';' );
                return ( new Point( int.Parse( ss[ 0 ], NumberFormatInfo.InvariantInfo ), int.Parse( ss[ 1 ], NumberFormatInfo.InvariantInfo ) ) );
            }
            return ( null );    
        }
        #endregion
    }
}
