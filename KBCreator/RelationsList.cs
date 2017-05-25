using System;
using System.Collections;

namespace KBCreator
{
	public class RelationsList : CollectionBase, ICloneable
	{
		public RelationsList()
		{
     	}

        public string Fact; 
        
        private Relation keyRelation;
        public Relation KeyRelation
        {
            get 
            {
                if ( keyRelation == null )
                    keyRelation = GetKeyRelation();
                return keyRelation;
            }
        }

        private ArrayList objects;
        public ArrayList Objects 
        {
            get
            {
                if ( objects == null )
                    objects = new ArrayList();
                if ( objects.Count == 0 )
                    objects = this.KeyRelation.EnumObjects( objects );
                return ( objects );
            }
        }

        public KBObject MainObject;
        
        
        #region CollectionBase overrides
        public Relation this[ int index ] { get { return ( (Relation) List[ index ] ); } set { List[ index ] = value; } }

        public int Add( Relation value )
        {
            return ( List.Add( value ) );
        }

        public int IndexOf( Relation value )
        {
            return ( List.IndexOf( value ) );
        }

        public void Insert( int index, Relation value )
        {
            List.Insert( index, value );
        }

        public void Remove( Relation value )
        {
            List.Remove( value );
        }

        public bool Contains( Relation value )
        {
            return ( List.Contains( value ) );
        }

        protected override void OnValidate( Object value )
        {
            if ( value.GetType() != typeof ( Relation ) )
                throw new ArgumentException( "value must be of type Relation.", "value" );
        }
        #endregion
	
        public object Clone()
        {
            RelationsList list = new RelationsList();
            foreach( Relation rel in this.List )
            {
                list.Add( rel.Clone() as Relation );
            }
            return list;
        }

        public bool RelationNameExists( string name )
        {
            foreach( Relation rel in List )
                if ( name == rel.Name )
                    return ( true );
            return ( false );
        }

        public Relation CreatePair( string typeName )
        {
            foreach( Relation rel in List )
            {
                if ( rel.Name == typeName )
                    return( rel.Clone() as Relation );
            }
            return null;
        }

        /* public ArrayList EnumObjects( ArrayList objects )
        {
            ArrayList objects = new ArrayList();
            foreach( Relation rel in this.List )
            {
                if ( rel.FirstObject is KBObject )
                    objects.Add( rel.FirstObject );
                if ( rel.SecondObject is KBObject )
                    objects.Add( rel.SecondObject );
                if ( rel.SecondObject is Relation )
            }
            return ( objects );
        }*/

        public string[] EnumRelationNames()
        {
            string[] rels = new string[this.List.Count];
            for( int i=0; i<rels.Length; i++)
                rels[i] = (this.List[i] as Relation).Name;
            return ( rels );
        }

        public Relation GetRelationByName( string relName )
        {
            foreach( Relation rel in this.List )
                if ( rel.Name == relName )
                    return ( rel );
            return ( null );
        }

        public Relation GetKeyRelation()
        {
            foreach( Relation rel in this.List )
                if ( rel.Primary )
                    return rel;
            return ( null );
        }

        public Object DetermineObjectSide( Relation rel )
        {
            if ( rel.FirstObject is KBObject )
                if ( (rel.FirstObject as KBObject).ObjectId == MainObject.ObjectId )
                    return (KBObject)rel.FirstObject;
            
            if ( rel.Arity != RelationArity.Unary )
            {
                if ( rel.SecondObject is KBObject )
                    if ( (rel.SecondObject as KBObject).ObjectId == MainObject.ObjectId )
                        return (KBObject)rel.SecondObject;
            }
            
            if ( rel.FirstObject is Relation )
                if ( null != FindObject( MainObject, (Relation)rel.FirstObject ) )
                    return (Relation)rel.FirstObject;
            
            if ( rel.Arity != RelationArity.Unary )
            {
                if ( rel.SecondObject is Relation )
                    if ( null != FindObject( MainObject, (Relation)rel.SecondObject ) )
                        return (Relation)rel.SecondObject;

            }
            return null;
        }

        public KBObject FindObject( KBObject obj, Relation rel )
        {
            if ( rel.FirstObject is KBObject )
                if ( (rel.FirstObject as KBObject).ObjectId == obj.ObjectId )
                    return (KBObject)rel.FirstObject;
            if ( rel.Arity != RelationArity.Unary )
            {
                if ( rel.SecondObject is KBObject )
                    if ( (rel.SecondObject as KBObject).ObjectId == obj.ObjectId )
                        return (KBObject)rel.SecondObject;
            }
            
            if ( rel.FirstObject is Relation )
            {
                KBObject objFound = FindObject( obj, (Relation)rel.FirstObject );
                if ( null != objFound )
                    return ( objFound );
            }
            
            if ( rel.Arity != RelationArity.Unary )
            {
                if ( rel.SecondObject is Relation )
                {
                    KBObject objFound = FindObject( obj, (Relation)rel.SecondObject );
                    if ( null != objFound )
                        return ( objFound );
                }
            }
            return null;
        }

        public bool IsSimple()
        {
            return ( ( this.KeyRelation.FirstObject is KBObject ) && ( ( this.KeyRelation.FirstObject as KBObject ).ObjectId == MainObject.ObjectId ) ||  
                ( this.KeyRelation.SecondObject is KBObject ) && ( ( this.KeyRelation.SecondObject as KBObject ).ObjectId == MainObject.ObjectId ) );
        }

        public void NormalizeByKeyRelation()
        {
            if ( this.KeyRelation.FirstObject is KBObject )
            {
                 if ( (this.KeyRelation.FirstObject as KBObject).ObjectId == MainObject.ObjectId )
                 {
                     return;
                 }
            }
            else
            {
                Object tmp = this.KeyRelation.FirstObject;
                this.KeyRelation.FirstObject = this.KeyRelation.SecondObject;
                this.KeyRelation.SecondObject = tmp;
            }
            return;
        }
      

        public string GetStringFact()
        {
            string fact = this.KeyRelation.GetStringRelation();
            return( fact );
        }
    }
}
