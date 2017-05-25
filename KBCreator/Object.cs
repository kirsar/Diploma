using System;
using System.Drawing;
using System.Xml;

namespace KBCreator
{
	public class KBObject : ICloneable
	{
		public KBObject() 
		{
      	    this.objectId = formMain.Manager.Uniqueid;
        }

        internal KBObject( long id )
        {
            this.objectId = id;
        }

        public string Name = "Object";

        public string PCodeName = "Object";
        
        public Relation Property;
        public Point LeftTop;
        public bool bUnderRelation;
        
        private long objectId;
        public long ObjectId { get { return objectId; } }

        public object Clone()
        {
            KBObject obj = (KBObject)this.MemberwiseClone();
            if ( this.Property != null )
                obj.Property = (Relation)this.Property.Clone();
            return obj;
        }

        public string GetStringObject()
        {
            return (this.Name);
        }

        public string GetPCodeObject()
        {
            return ( "symb(" + this.PCodeName + ")" );
        }

        public void Serialize( XmlDocument doc, XmlElement parent )
        {
            XmlElement kbobj = doc.CreateElement( "object" ); 
            parent.AppendChild( kbobj );

            XmlAttribute name = doc.CreateAttribute( "name" );
            name.Value = formMain.Manager.ValueToString( this.Name, typeof(string) );
            kbobj.Attributes.Append( name );

            XmlAttribute pCodeName = doc.CreateAttribute( "pname" );
            pCodeName.Value = formMain.Manager.ValueToString( this.PCodeName, typeof(string) );
            kbobj.Attributes.Append( pCodeName );

            XmlAttribute propertyname = doc.CreateAttribute( "property" );
            if ( this.Property != null )
                propertyname.Value = formMain.Manager.ValueToString( this.Property.Name, typeof(string) );
            else
                propertyname.Value = formMain.Manager.ValueToString( "null", typeof(string ) );
            kbobj.Attributes.Append( propertyname );
            
            XmlAttribute lefttop = doc.CreateAttribute( "lefttop" );
            lefttop.Value = formMain.Manager.ValueToString( this.LeftTop, typeof(Point) );
            kbobj.Attributes.Append( lefttop );

            XmlAttribute id = doc.CreateAttribute( "id" );
            id.Value = formMain.Manager.ValueToString( this.objectId, typeof(long) );
            kbobj.Attributes.Append( id );
        }

        public static KBObject Deserialize( XmlElement kbObj )
        {
            KBObject retVal = new KBObject( (long)formMain.Manager.StringToValue( kbObj.Attributes["id"].Value, typeof(long) ) );
            retVal.Name = (string)formMain.Manager.StringToValue( kbObj.Attributes["name"].Value, typeof(string) );
            retVal.PCodeName = (string)formMain.Manager.StringToValue( kbObj.Attributes["pname"].Value, typeof(string) );
            retVal.LeftTop = (Point)formMain.Manager.StringToValue( kbObj.Attributes["lefttop"].Value, typeof(Point) );
            string property = (string)formMain.Manager.StringToValue( kbObj.Attributes["property"].Value, typeof(string) );
            if ( property == "null" )
                retVal.Property = null;
            else
                retVal.Property = new Relation( property );
            return ( retVal );
        }
    }
}
