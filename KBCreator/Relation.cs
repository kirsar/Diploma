using System;
using System.Drawing;
using System.Xml;
using System.Collections;

namespace KBCreator
{
    public class Relation : ICloneable
    {
        public Relation( string name, string pCodeName, RelationArity arity, bool commutative, bool mutable, Relation pairRelation, bool primary )
        {
            this.name = name;
            this.pCodeName = pCodeName;
            this.arity = arity;
            this.commutative = commutative;
            this.mutable = mutable;
            this.pairRelation = pairRelation;
            this.primary = primary;

            this.FirstObject = new Object();
            this.SecondObject = new Object();

            this.relationId = formMain.Manager.Uniqueid;
        }

        public Relation ( Relation relation )
        {
            this.name = relation.name;
            this.pCodeName = relation.pCodeName;
            this.arity = relation.arity;
            this.commutative = relation.commutative;
            this.mutable = relation.mutable;
            this.pairRelation = (Relation)relation.pairRelation;
            this.primary = relation.primary;
      
            this.FirstObject = new Object();
            this.SecondObject = new Object();

            this.relationId = formMain.Manager.Uniqueid;
        }

        internal Relation()
        {
        }

        internal Relation( long id )
        {
            this.relationId = id;
        }

        internal Relation( string name )
        {
            this.name = name;
            this.pCodeName = name;
        }

        private string name;
        public string Name { get { return name; } }

        private string pCodeName;
        public string PCodeName { get { return pCodeName; } }
        
        private RelationArity arity;
        public RelationArity Arity { get { return arity; } }
       
        private bool commutative;
        public bool Commutative { get { return commutative; } }

        private bool mutable;
        public bool Mutable { get { return mutable; } }
   
        private Relation pairRelation;
        public Relation PairRelation { get { return pairRelation; } set { pairRelation = value; } } 
        
        private bool primary;
        public bool Primary { get { return primary; } }

        public Object FirstObject;
        public Object SecondObject;

        public Point StartPoint;
        public Point EndPoint;

        public bool Connected = false;

        private long relationId;
        public long RelationId{ get { return relationId; } }

        #region ICloneable Members

        public object Clone()
        {
            Relation rel = new Relation();
            rel = (Relation)this.MemberwiseClone();
            if ( this.FirstObject is KBObject )
                rel.FirstObject = ( this.FirstObject as KBObject ).Clone();
            else if ( this.FirstObject is Relation )
                rel.FirstObject = ( this.FirstObject as Relation ).Clone();
            if ( this.SecondObject is KBObject )
                rel.SecondObject = ( this.SecondObject as KBObject ).Clone();
            else if ( this.SecondObject is Relation )
                rel.SecondObject = ( this.SecondObject as Relation ).Clone();

            if ( this.PairRelation != null )
            {
                rel.PairRelation = this.PairRelation;
            }
            return rel;
        }

        #endregion
 
       
        public static string ArityToString( RelationArity arity )
        {
            return ( arity.ToString() );
        }

        public static RelationArity StringToArity( string strArity )
        {
            if ( strArity == ArityToString( RelationArity.Unary ) )
                return ( RelationArity.Unary );

            if ( strArity == ArityToString( RelationArity.Binary ) )
                return ( RelationArity.Binary );

            return( RelationArity.Unknown );
        }

        public static Relation StringToRelation( string strRelation )
        {
            foreach ( Relation relation in formMain.Manager.RelationsTypes )
                if ( strRelation == relation.Name )
                {
                    return ( relation );
                }
            return( null );
        }

        
        public string GetStringRelation()
        {
            string relation = "(";
            if ( this.Arity == RelationArity.Binary ) 
            {
                if ( this.FirstObject is Relation )
                    relation += (this.FirstObject as Relation).GetStringRelation();
                else if ( this.FirstObject is KBObject )
                    relation += ( this.FirstObject as KBObject ).GetStringObject();
            
                relation += this.Name;
            
                if ( this.SecondObject is Relation )
                    relation += (this.SecondObject as Relation).GetStringRelation();
                else if ( this.SecondObject is KBObject )
                    relation += ( this.SecondObject as KBObject ).GetStringObject();
            }
            else if ( this.Arity == RelationArity.Unary )
            {
                relation += this.Name;
                if ( this.FirstObject is Relation )
                    relation += (this.FirstObject as Relation).GetStringRelation();
                else if ( this.FirstObject is KBObject )
                    relation += "(" + ( this.FirstObject as KBObject ).GetStringObject() + ")";
            }
            
            
            relation += ")";
            return( relation );
        }

        public string GetPCodeRelation()
        {
            string relation = this.PCodeName + "(";
            if ( this.FirstObject is Relation )
                relation += (this.FirstObject as Relation).GetPCodeRelation();
            else if ( this.FirstObject is KBObject )
                relation += ( this.FirstObject as KBObject ).GetPCodeObject();

            if ( this.Arity != RelationArity.Unary )
            {
                relation += ", ";
            
                if ( this.SecondObject is Relation )
                    relation += (this.SecondObject as Relation).GetPCodeRelation();
                else if ( this.SecondObject is KBObject )
                    relation += ( this.SecondObject as KBObject ).GetPCodeObject();
            }
            relation += ")";
            
            return( relation );
        }

        public ArrayList EnumObjects( ArrayList objects )
        {
            if ( this.FirstObject is KBObject )
                objects.Add( this.FirstObject );
            if ( this.SecondObject is KBObject )
                objects.Add( this.SecondObject );
            if ( this.SecondObject is Relation )
                objects = (this.SecondObject as Relation).EnumObjects( objects );
            if ( this.FirstObject is Relation )
                objects = (this.FirstObject as Relation).EnumObjects( objects );
            return ( objects );
        }
            
        public void Serialize( XmlDocument doc, XmlElement parent )
        {
            XmlElement relation = doc.CreateElement( "relation" ); 
            parent.AppendChild( relation );

            XmlAttribute name = doc.CreateAttribute( "name" );
            name.Value = formMain.Manager.ValueToString( this.name, typeof(string) );
            relation.Attributes.Append( name );

            XmlAttribute pCodeName = doc.CreateAttribute( "pname" );
            pCodeName.Value = formMain.Manager.ValueToString( this.pCodeName, typeof(string) );
            relation.Attributes.Append( pCodeName );

            
            XmlAttribute id = doc.CreateAttribute( "id" );
            id.Value = formMain.Manager.ValueToString( this.relationId, typeof(long) );
            relation.Attributes.Append( id );
            
            XmlAttribute arity = doc.CreateAttribute( "arity" );
            arity.Value = formMain.Manager.ValueToString( this.arity, typeof(RelationArity) );
            relation.Attributes.Append( arity );
            
            XmlAttribute commutative = doc.CreateAttribute( "commutative" );
            commutative.Value = formMain.Manager.ValueToString( this.commutative, typeof(bool) );
            relation.Attributes.Append( commutative );
            
            XmlAttribute mutable = doc.CreateAttribute( "mutable" );
            mutable.Value = formMain.Manager.ValueToString( this.mutable, typeof(bool) );
            relation.Attributes.Append( mutable );
            
            XmlAttribute pair = doc.CreateAttribute( "pair" );
            if ( this.PairRelation != null )
                pair.Value = formMain.Manager.ValueToString( this.pairRelation.Name, typeof(string) );
            else
                pair.Value = formMain.Manager.ValueToString( "null", typeof(string) );
            relation.Attributes.Append( pair );
            
            XmlAttribute primary = doc.CreateAttribute( "primary" );
            primary.Value = formMain.Manager.ValueToString( this.primary, typeof(bool) );
            relation.Attributes.Append( primary );

            XmlAttribute firsttype = doc.CreateAttribute( "firsttype" );
            if ( this.FirstObject is Relation )
            {
                firsttype.Value = formMain.Manager.ValueToString( "relation", typeof(string) );
            
                XmlAttribute firstid = doc.CreateAttribute( "firstid" );
                firstid.Value = formMain.Manager.ValueToString( (this.FirstObject as Relation).RelationId, typeof(long) );
                relation.Attributes.Append( firstid );
            }
            else if ( this.FirstObject is KBObject )
            {
                firsttype.Value = formMain.Manager.ValueToString( "object", typeof(string) );
            
                XmlAttribute firstid = doc.CreateAttribute( "firstid" );
                firstid.Value = formMain.Manager.ValueToString( (this.FirstObject as KBObject).ObjectId, typeof(long) );
                relation.Attributes.Append( firstid );
            }
            else
                firsttype.Value = formMain.Manager.ValueToString( "null", typeof(string) );
            relation.Attributes.Append( firsttype );
          
             
          
            XmlAttribute secondtype = doc.CreateAttribute( "secondtype" );
            if ( this.SecondObject is Relation )
            {
                secondtype.Value = formMain.Manager.ValueToString( "relation", typeof(string) );
            
                XmlAttribute secondid = doc.CreateAttribute( "secondid" );
                secondid.Value = formMain.Manager.ValueToString( (this.SecondObject as Relation).RelationId, typeof(long) );
                relation.Attributes.Append( secondid );
            }
            else if ( this.SecondObject is KBObject )
            {
                secondtype.Value = formMain.Manager.ValueToString( "object", typeof(string) );
    
                XmlAttribute secondid = doc.CreateAttribute( "secondid" );
                secondid.Value = formMain.Manager.ValueToString( (this.SecondObject as KBObject).ObjectId, typeof(long) );
                relation.Attributes.Append( secondid );
            }
            else
                secondtype.Value = formMain.Manager.ValueToString( "null", typeof(string) );
            relation.Attributes.Append( secondtype );


            XmlAttribute startPoint = doc.CreateAttribute( "startpoint" );
            startPoint.Value = formMain.Manager.ValueToString( this.StartPoint, typeof(Point) );
            relation.Attributes.Append( startPoint );

            XmlAttribute endPoint = doc.CreateAttribute( "endpoint" );
            endPoint.Value = formMain.Manager.ValueToString( this.EndPoint, typeof(Point) );
            relation.Attributes.Append( endPoint );

            XmlAttribute connected = doc.CreateAttribute( "connected" );
            connected.Value = formMain.Manager.ValueToString( this.Connected, typeof(bool) );
            relation.Attributes.Append( connected );
        }
    
        public static Relation Deserialize( XmlElement relation )
        {
            Relation retVal = new Relation();
            retVal.name        = (string)formMain.Manager.StringToValue( relation.Attributes["name"].Value, typeof(string) );
            retVal.pCodeName   = (string)formMain.Manager.StringToValue( relation.Attributes["pname"].Value, typeof(string) );
            retVal.relationId  = (long)  formMain.Manager.StringToValue( relation.Attributes["id"].Value, typeof(long) );
            retVal.arity       = (RelationArity)formMain.Manager.StringToValue( relation.Attributes["arity"].Value, typeof(RelationArity) );
            retVal.commutative = (bool)  formMain.Manager.StringToValue( relation.Attributes["commutative"].Value, typeof(bool) );
            retVal.mutable     = (bool)  formMain.Manager.StringToValue( relation.Attributes["mutable"].Value, typeof(bool) );
            retVal.primary     = (bool)  formMain.Manager.StringToValue( relation.Attributes["primary"].Value, typeof(bool) );
            string pair   = (string)formMain.Manager.StringToValue( relation.Attributes["pair"].Value, typeof(string) );
            if ( pair == "null" )
                retVal.pairRelation = null;
            else
            {
                retVal.pairRelation = new Relation( pair );
            }
            string firstType   = (string)formMain.Manager.StringToValue( relation.Attributes["firsttype"].Value, typeof(string) );
            if ( firstType == "object")
            {
                long firstId     = (long)formMain.Manager.StringToValue( relation.Attributes["firstid"].Value, typeof(long) );
                retVal.FirstObject = new KBObject( firstId );
            }
            else if ( firstType == "relation" )
            {
                long firstId     = (long)formMain.Manager.StringToValue( relation.Attributes["firstid"].Value, typeof(long) );
                retVal.FirstObject = new Relation( firstId );
            }
            else
                retVal.FirstObject = null;
            
            string secondType  = (string)formMain.Manager.StringToValue( relation.Attributes["secondtype"].Value, typeof(string) );
            if ( secondType == "object")
            {
                long secondId    = (long)formMain.Manager.StringToValue( relation.Attributes["secondid"].Value, typeof(long) );
                retVal.SecondObject = new KBObject( secondId );
            }
            else if ( secondType == "relation" )
            {
                long secondId    = (long)formMain.Manager.StringToValue( relation.Attributes["secondid"].Value, typeof(long) );
                retVal.SecondObject = new Relation( secondId );
            }
            else
                retVal.SecondObject = null;
            retVal.StartPoint  = (Point) formMain.Manager.StringToValue( relation.Attributes["startpoint"].Value, typeof(Point) );
            retVal.EndPoint    = (Point) formMain.Manager.StringToValue( relation.Attributes["endpoint"].Value, typeof(Point) ); 
            retVal.Connected   = (bool)  formMain.Manager.StringToValue( relation.Attributes["connected"].Value, typeof(bool) ); 
            return ( retVal );
        }
    }

 
    public enum RelationArity
    {
        Unknown = 0,
        Unary = 1,
        Binary = 2
    }

    public enum Side
    {
        Unknown = 0,
        First = 1,
        Second = 2
    }
}
