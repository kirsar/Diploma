using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.Windows.Forms;

namespace KBCreator
{
    /// <summary>
    /// Summary description for ModelAreaControl.
    /// </summary>
    public class ModelAreaControl : System.Windows.Forms.UserControl
    {
        private System.ComponentModel.Container components = null;
        
        public ModelAreaControl()
        {
            InitializeComponent();
            InitializeDrawing();
        }

        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                if(components != null)
                {
                    components.Dispose();
                    graph.Dispose();
                }
            }
            base.Dispose( disposing );
        }

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // 
            // ModelAreaControl
            // 
            this.AllowDrop = true;
            this.BackColor = System.Drawing.Color.NavajoWhite;
            this.Name = "ModelAreaControl";
            this.Size = new System.Drawing.Size(544, 392);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.DrawingControl_DragEnter);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ModelAreaControl_MouseUp);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.DrawingControl_DragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.DrawingControl_DragOver);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ModelAreaControl_MouseMove);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ModelAreaControl_MouseDown);

        }
        #endregion

        private Graphics graph;
        private Color    backColor;
        
        private Color objectColor;
        private Size objectSize;
        
        private Color objectBorderColor;
        private Brush objectBrush;
        private Pen   objectPen;
        
        private Color objectNonKBBorderColor;
        private Brush objectNonKBBrush;
        private Pen   objectNonKBPen;
        
        private Color objectSelectedBorderColor;
        private Brush objectSelectedBrush;
        private Pen   objectSelectedPen;

        private Font  objectFont;
        private Brush objectTextBrush;
       
 
        private int   relationHalfLength;
        
        private Brush relationBrush;
        private Color relationColor;
        private Pen   relationPen;

        private Color relationConnectedColor;
        private Brush relationConnectedBrush;
        private Pen   relationConnectedPen;


        
        private Color relationGripColor;
        private Size  relationGripSize;
        private Pen   relationGripPen;
        private Brush relationGripBrush;
        private Brush relationSelectedGripBrush;
        private Font  relationFont;
        private Brush relationTextBrush;

        public Object SelectedObject;

        private bool bStartGripDrag = false;
        private bool bEndGripDrag = false;
        private bool bRelationIntersected = false;

       
        public delegate void ObjectSelectedEventHandler( object selectetObject );
        public event ObjectSelectedEventHandler objectSelectedEvent = null;

         
        private void InitializeDrawing()
        {
            graph = this.CreateGraphics();
            this.backColor = Color.NavajoWhite;

            this.objectSize             = new Size( 40, 40 );
            this.objectBorderColor      = Color.Blue;
            this.objectBrush            = new SolidBrush( objectBorderColor );
            this.objectPen              = new Pen( objectBrush, 2 );
            this.objectNonKBBorderColor = Color.Red;
            this.objectNonKBBrush       = new SolidBrush( objectNonKBBorderColor );
            this.objectNonKBPen         = new Pen( objectNonKBBrush, 2 );
            this.objectSelectedBorderColor = Color.Green;
            this.objectSelectedBrush    = new SolidBrush( objectSelectedBorderColor );
            this.objectSelectedPen      = new Pen( objectSelectedBrush, 2 );
            this.objectColor            = Color.Yellow;
            this.objectFont             = new Font( "Arial", 7);
            this.objectTextBrush        = new SolidBrush( Color.Olive );
            
            this.relationHalfLength     = 40;
            this.relationBrush          = new SolidBrush( relationColor );
            this.relationColor          = Color.Red;
            this.relationPen            = new Pen( relationColor, 1 );
            this.relationConnectedBrush = new SolidBrush( relationConnectedColor );
            this.relationConnectedColor = Color.Black;
            this.relationConnectedPen   = new Pen( relationConnectedColor, 1 );
            this.relationGripColor      = Color.White;
            this.relationGripSize       = new Size( 5, 5 );
            this.relationGripPen        = new Pen( Color.Black, 1 );
            this.relationGripBrush      = new SolidBrush( relationGripColor );
            this.relationSelectedGripBrush = new SolidBrush( Color.YellowGreen );
            this.relationFont           = new Font( "Arial", 7);
            this.relationTextBrush      = new SolidBrush( Color.Blue );

            this.Clear();
        }
        
        private void DrawingControl_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            // allow dragging items:
            e.Effect = e.AllowedEffect;
        }

        private void DrawingControl_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            try
            {
                // handle drop tree item effect:
                // Retrieve the client coordinates of the drop location.
                Point targetPoint = this.PointToClient( new Point(e.X, e.Y) );

                // Retrieve the node that was dragged.
                TreeNode draggedNode = ( TreeNode )e.Data.GetData( typeof( TreeNode ) );               

                // obtain pointer to the tree object if any:
                Relation rel = draggedNode.Tag as Relation;
                if ( rel != null )
                {
                    Relation newRelation = new Relation( rel );
                    newRelation.StartPoint = new Point( targetPoint.X, targetPoint.Y - relationHalfLength );
                    newRelation.EndPoint   = new Point( targetPoint.X, targetPoint.Y + relationHalfLength );
                    
                    formMain.Manager.Relations.Add( newRelation );
                    this.Redraw( null );
                }
                else if ( draggedNode.Text == "Object" )
                {
                    KBObject newObj = new KBObject();
                    newObj.LeftTop = targetPoint;
                    formMain.Manager.NonKBObjects.Add( newObj );
                    this.Redraw( null );
                }
            }
            catch( Exception ex )
            {
                MessageBox.Show( ex.Message );
            }
        }
        private void DrawingControl_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
        {
            // Get handle to form.
            /*IntPtr hWnd = new IntPtr();
            hWnd = this.Handle;

            // Create new graphics object using handle to window.
            Graphics grForm = Graphics.FromHwnd( hWnd );
            Point ptCur = this.PointToClient( Cursor.Position );

            // Retrieve the node that was dragged.
            TreeNode draggedNode = ( TreeNode )e.Data.GetData( typeof( TreeNode ) );    
          
            // obtain pointer to the tree object if any:
            ITreeNode node = draggedNode.Tag as ITreeNode;

            // do nothing if the graphical representation already exists:
            if ( null != this.objectManager.FindDrawableEntity( node ) )
            {
                return;
            }

            // create additional bitmap for double-buffering:
            Bitmap imbmp = new Bitmap( drawing.DrawingBitmap );
            Graphics gr = Graphics.FromImage( imbmp );

            // get the type of graph being dragged:
            Type typeGraph = objectManager.Catalogue.GetElementType( node.TypeId );

            // create ghost entity:
            DrawableEntity entGhost = this.drawing.GhostEntity( ptCur, typeGraph );
            ShapeEntity shape = entGhost as ShapeEntity;
            shape.Text.Text = node.Name; 

            RectangleF rectTest = shape.BoundingBox;
            rectTest = DrawableEntity.AlignRectangle( rectTest, this.drawing.GridDensity );
            if ( this.drawing.IntersectsWithObject( rectTest ) )
            {
                shape.Color = Color.FromArgb( 0x4f, 0xfe, 0x4f,  0x00 );
            }

            // place valid DC into context:
            entGhost.Draw( this.drawing.DrawContext.Clone( gr ) );

            // stream bitmap buffer back to screen:
            grForm.DrawImage( imbmp, 0, 0 );

            //entGhost.Dispose();
            gr.Dispose();
            imbmp.Dispose();
            grForm.Dispose();*/
        }

        public void Redraw( MouseEventArgs e )
        {
            this.Clear();
            foreach ( Object obj in formMain.Manager.NonKBObjects )
                RedrawObject( obj as KBObject );
            foreach ( Object obj in formMain.Manager.Relations )
                RedrawRelation( obj as Relation );
            if ( bRelationIntersected )
                graph.DrawRectangle( relationConnectedPen, e.X - 7, e.Y - 7, 15, 15 );
            bRelationIntersected = false;
        }

        private void RedrawObject( KBObject obj )
        {
            Pen currentPen = (obj == SelectedObject) ? objectSelectedPen : (obj.bUnderRelation) ? objectPen : objectNonKBPen;
            graph.DrawRectangle( currentPen, obj.LeftTop.X, obj.LeftTop.Y, objectSize.Width, objectSize.Height );
            Point textPoint = new Point( (int)(obj.LeftTop.X + objectSize.Width / 2 - graph.MeasureString( obj.Name, objectFont ).Width / 2), (int)(obj.LeftTop.Y + objectSize.Height / 2 - graph.MeasureString( obj.Name, objectFont ).Height / 2) );
            graph.DrawString( obj.Name, objectFont, objectTextBrush, textPoint );
        }

        private void RedrawRelation( Relation rel )
        {
            Point[] relPoints = { new Point( rel.StartPoint.X, rel.StartPoint.Y ),
                                    new Point( rel.EndPoint.X, rel.StartPoint.Y ),
                                    new Point( rel.EndPoint.X, rel.EndPoint.Y ) 
                                };
            graph.DrawLines( (rel.Connected) ? relationConnectedPen : relationPen, relPoints );
            if ( Math.Abs( rel.StartPoint.X - rel.EndPoint.X ) < 10 )
            {
                if ( rel.StartPoint.Y > rel.EndPoint.Y )
                    graph.DrawString( rel.Name, relationFont, relationTextBrush, new Point( rel.EndPoint.X - 10, rel.EndPoint.Y + Math.Abs( rel.StartPoint.Y - rel.EndPoint.Y ) / 2 ) );
                if ( rel.StartPoint.Y < rel.EndPoint.Y )
                    graph.DrawString( rel.Name, relationFont, relationTextBrush, new Point( rel.EndPoint.X - 10, rel.StartPoint.Y + Math.Abs( rel.StartPoint.Y - rel.EndPoint.Y ) / 2 ) );
            }
            else if ( Math.Abs( rel.StartPoint.Y - rel.EndPoint.Y ) < 10 )
            {
                if ( rel.StartPoint.X > rel.EndPoint.X )
                    graph.DrawString( rel.Name, relationFont, relationTextBrush, new Point( rel.EndPoint.X + Math.Abs( rel.StartPoint.X - rel.EndPoint.X ) / 2, rel.EndPoint.Y - 15) );
                if ( rel.StartPoint.X < rel.EndPoint.X )
                    graph.DrawString( rel.Name, relationFont, relationTextBrush, new Point( rel.StartPoint.X + Math.Abs( rel.StartPoint.X - rel.EndPoint.X ) / 2, rel.EndPoint.Y - 15) );
            }
            else
                graph.DrawString( rel.Name, relationFont, relationTextBrush, new Point( rel.EndPoint.X - 10, rel.StartPoint.Y - 15 ) );
            graph.DrawString( "S", relationFont, relationTextBrush, rel.StartPoint );
            graph.DrawString( "E", relationFont, relationTextBrush, rel.EndPoint );
            RedrawGrips( rel );

            if ( rel.Connected )
            { 
                if ( rel.FirstObject is KBObject )
                {
                    KBObject startObj = rel.FirstObject as KBObject;
                    graph.DrawRectangle( (startObj == SelectedObject) ? objectSelectedPen : objectPen, startObj.LeftTop.X, startObj.LeftTop.Y, objectSize.Width, objectSize.Height );
                    Point textPoint = new Point( (int)(startObj.LeftTop.X + objectSize.Width / 2 - graph.MeasureString( startObj.Name, objectFont ).Width / 2), (int)(startObj.LeftTop.Y + objectSize.Height / 2 - graph.MeasureString( startObj.Name, objectFont ).Height / 2) );
                    graph.DrawString( startObj.Name, objectFont,  objectTextBrush, textPoint );
                }

                if ( rel.SecondObject is KBObject )
                {
                    KBObject endObj   = rel.SecondObject as KBObject;
                    graph.DrawRectangle( (endObj == SelectedObject) ? objectSelectedPen : objectPen, endObj.LeftTop.X, endObj.LeftTop.Y, objectSize.Width, objectSize.Height );
                    Point textPoint = new Point( (int)(endObj.LeftTop.X + objectSize.Width / 2 - graph.MeasureString( endObj.Name, objectFont ).Width / 2), (int)(endObj.LeftTop.Y + objectSize.Height / 2 - graph.MeasureString( endObj.Name, objectFont ).Height / 2) );
                    graph.DrawString( endObj.Name, objectFont,  objectTextBrush, textPoint );
                }
            }
        }

        private void RedrawGrips( Relation rel )
        {
            if ( Convert.ToInt32(rel.Arity) > 0 )
            {
                Rectangle startGrip = new Rectangle( new Point( rel.StartPoint.X - 2, rel.StartPoint.Y - 2 ), relationGripSize );
                graph.FillRectangle( ( bStartGripDrag && rel == SelectedObject) ? relationSelectedGripBrush : relationGripBrush, startGrip );
                graph.DrawRectangle( relationGripPen, startGrip );
            }
            if ( Convert.ToInt32(rel.Arity) > 1 )
            {
                Rectangle endGrip = new Rectangle( new Point( rel.EndPoint.X - 2, rel.EndPoint.Y - 2 ), relationGripSize );
                graph.FillRectangle( ( bEndGripDrag && rel == SelectedObject) ? relationSelectedGripBrush : relationGripBrush, endGrip );
                graph.DrawRectangle( relationGripPen, endGrip );
            }
        }

        private void ModelAreaControl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Point targetPoint = new Point ( e.X, e.Y );
            KBObject objUnderRelation = null;
            if ( e.Button == MouseButtons.Left )
            {
                if ( SelectedObject is KBObject )
                {
                    KBObject selectedObj = SelectedObject as KBObject;
                    selectedObj.LeftTop = targetPoint;
                }
                if ( SelectedObject is Relation )
                {
                    Relation selectedRel = SelectedObject as Relation;
                    if ( bStartGripDrag )
                    {
                        selectedRel.StartPoint = targetPoint;
                        foreach ( KBObject obj in formMain.Manager.NonKBObjects )
                           if ( new Rectangle( obj.LeftTop, objectSize ).Contains( targetPoint ) )
                           {
                               obj.bUnderRelation = true;
                               objUnderRelation = obj;
                           }
                        foreach ( Relation rel in formMain.Manager.Relations )
                            if ( rel != selectedRel )
                                foreach ( Rectangle rect in ConstructRelRegion( rel ) )
                                    if ( new Rectangle( targetPoint, relationGripSize ).IntersectsWith( rect ) )
                                    {
                                        bRelationIntersected = true;
                                        break;
                                    }
                    
                    }
                    if ( bEndGripDrag )
                    {
                        selectedRel.EndPoint = targetPoint;
                        foreach ( KBObject obj in formMain.Manager.NonKBObjects )
                            if ( new Rectangle( obj.LeftTop, objectSize ).Contains( targetPoint ) )
                            {
                                obj.bUnderRelation = true;
                                objUnderRelation = obj;
                            }
                        foreach ( Relation rel in formMain.Manager.Relations )
                            if ( rel != selectedRel )
                                foreach ( Rectangle rect in ConstructRelRegion( rel ) )
                                    if ( new Rectangle( targetPoint, relationGripSize ).IntersectsWith( rect ) )
                                    {
                                        bRelationIntersected = true;
                                        break;
                                    }
                    }
                }
                Redraw( e );
                if ( objUnderRelation != null )
                {
                    objUnderRelation.bUnderRelation = false;
                }
            }
        }

        private void ModelAreaControl_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Point targetPoint = new Point( e.X, e.Y );
            SelectedObject = FindGripUnderCursor( targetPoint );
            if ( SelectedObject == null )
                SelectedObject = FindObjectUnderCursor( targetPoint );
            if ( objectSelectedEvent != null )
                objectSelectedEvent( SelectedObject );
            Redraw( e );
        }

        private void ModelAreaControl_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Point targetPoint = new Point ( e.X, e.Y );
            Relation selectedRel = null;
            bool bObjectFound = false;
            if ( e.Button == MouseButtons.Left )
            {
                if ( SelectedObject is Relation )
                {
                    selectedRel = SelectedObject as Relation;
                    if ( bStartGripDrag )
                    {
                        foreach ( KBObject obj in formMain.Manager.NonKBObjects )
                            if ( new Rectangle( obj.LeftTop, objectSize ).Contains( targetPoint ) )
                            {
                                selectedRel.FirstObject = obj;
                                bObjectFound = true;
                                formMain.Manager.NonKBObjects.Remove( obj );
                                break;
                            }
                        if ( !bObjectFound )
                            foreach ( Relation rel in formMain.Manager.Relations )
                            {
                                if ( rel != selectedRel )
                                    foreach ( Rectangle rect in ConstructRelRegion( rel ) )
                                        if ( new Rectangle( targetPoint, relationGripSize ).IntersectsWith( rect ) )
                                        {
                                            selectedRel.FirstObject = rel;
                                            bObjectFound = true;
                                            break;
                                        }
                            }
                        if ( !bObjectFound )
                            selectedRel.FirstObject = null;
                  }

                    if ( bEndGripDrag )
                    {
                        foreach ( KBObject obj in formMain.Manager.NonKBObjects )
                            if ( new Rectangle( obj.LeftTop, objectSize ).Contains( targetPoint ) )
                            {
                                selectedRel.SecondObject = obj;
                                bObjectFound = true;
                                formMain.Manager.NonKBObjects.Remove( obj );
                                break;
                            }
                        if ( !bObjectFound )
                            foreach ( Relation rel in formMain.Manager.Relations )
                            {
                                if ( rel != selectedRel )
                                    foreach ( Rectangle rect in ConstructRelRegion( rel ) )
                                        if ( new Rectangle( targetPoint, relationGripSize ).IntersectsWith( rect ) )
                                        {
                                            selectedRel.SecondObject = rel;
                                            bObjectFound = true;
                                            break;
                                        }
                            }
                        if ( !bObjectFound )
                            selectedRel.SecondObject = null;

                    }
                }
            
                if ( selectedRel != null )
                {
                    if ( Convert.ToInt32(selectedRel.Arity) < 2 )
                    {
                        if ( selectedRel.FirstObject is KBObject || selectedRel.FirstObject is Relation )
                            selectedRel.Connected = true;
                    }
                    else if ( Convert.ToInt32(selectedRel.Arity) < 3 )
                    {
                        if ( (selectedRel.FirstObject is KBObject) || (selectedRel.FirstObject is Relation)   
                            && (selectedRel.SecondObject) is KBObject || (selectedRel.SecondObject is Relation) )
                            selectedRel.Connected = true;
                    }
                }
                Redraw( e );
            }
        }
    
       
        private KBObject FindObjectUnderCursor( Point targetPoint )
        {
            foreach( KBObject obj in formMain.Manager.NonKBObjects )
                if ( new Rectangle( obj.LeftTop, (Size)objectSize ).Contains( targetPoint ) )
                {   
                    return obj;
                }
            foreach ( Relation rel in formMain.Manager.Relations )
            {
                if ( rel.FirstObject is KBObject )
                {
                    if ( new Rectangle( (rel.FirstObject as KBObject).LeftTop, (Size)objectSize ).Contains( targetPoint ) )
                    {
                        return rel.FirstObject as KBObject;
                    }
                }
                if ( rel.SecondObject is KBObject )
                {
                    if ( new Rectangle( (rel.SecondObject as KBObject).LeftTop, (Size)objectSize ).Contains( targetPoint ) )
                    {
                        return rel.SecondObject as KBObject;
                    }
                }
            }
            return null;
        }
        private Relation FindGripUnderCursor( Point targetPoint )
        {
            bStartGripDrag = false;
            bEndGripDrag   = false;
            foreach ( Relation rel in formMain.Manager.Relations )
            {
                if ( Convert.ToInt32(rel.Arity) > 0 )
                {    
                    Rectangle startGrip = new Rectangle( new Point( rel.StartPoint.X - 2, rel.StartPoint.Y - 2 ), relationGripSize );
                    if ( startGrip.Contains( targetPoint ) )
                    {
                        bStartGripDrag = true;
                        return rel;
                    }
                }
                if ( Convert.ToInt32(rel.Arity) > 1 )
                {    
                    Rectangle endGrip = new Rectangle( new Point( rel.EndPoint.X - 2, rel.EndPoint.Y - 2 ), relationGripSize );
                    if ( endGrip.Contains( targetPoint ) )
                    {
                        bEndGripDrag = true;
                        return rel;
                    }
                }
            }
            return null;
        }
        private Relation FindRelationUnderCursor( Point targetPoint )
        {
            foreach ( Relation rel in formMain.Manager.Relations )
            {
                if ( rel.StartPoint.X == rel.EndPoint.X )
                {
                    if ( new Rectangle( rel.StartPoint.X - 2, rel.StartPoint.Y, 4, rel.StartPoint.Y - rel.EndPoint.Y ).Contains( targetPoint) )
                    {
                        return rel;
                    }
                }
                else if ( rel.StartPoint.Y == rel.EndPoint.Y )
                { 
                    if ( new Rectangle( rel.StartPoint.X, rel.StartPoint.Y - 2, rel.StartPoint.X - rel.EndPoint.X, 4 ).Contains( targetPoint) )
                    {
                        return rel;
                    }
                }
                else
                {
                    Rectangle firstSegment = new Rectangle( rel.StartPoint.X, rel.StartPoint.Y - 2, rel.StartPoint.X - rel.EndPoint.X, 4 );
                    Rectangle secondSegment = new Rectangle( rel.EndPoint.X - 2, rel.StartPoint.Y, 4, rel.EndPoint.Y - rel.StartPoint.Y );
                    if ( firstSegment.Contains( targetPoint ) || secondSegment.Contains( targetPoint ) )
                    {
                        return rel;
                    }
                }
            }
            return null;
        }

        private Rectangle[] ConstructRelRegion( Relation rel )
        {
            Rectangle[] regions = new Rectangle[2];
            
            if ( rel.StartPoint.X == rel.EndPoint.X )
                if ( rel.StartPoint.Y < rel.EndPoint.Y )
                    regions[0] = new Rectangle( rel.StartPoint.X - 2, rel.StartPoint.Y, 4, Math.Abs( rel.StartPoint.Y - rel.EndPoint.Y ) );
                else
                    regions[0] = new Rectangle( rel.EndPoint.X - 2, rel.EndPoint.Y, 4, Math.Abs( rel.StartPoint.Y - rel.EndPoint.Y ) );
            else if ( rel.StartPoint.Y == rel.EndPoint.Y )
                if ( rel.StartPoint.X < rel.EndPoint.X )
                    regions[0] = new Rectangle( rel.StartPoint.X, rel.StartPoint.Y - 2, Math.Abs( rel.StartPoint.X - rel.EndPoint.X ), 4 );
                else
                    regions[0] = new Rectangle( rel.EndPoint.X, rel.StartPoint.Y - 2, Math.Abs( rel.StartPoint.X - rel.EndPoint.X ), 4 );
            else
            {
                Point[] relPoints = { new Point( rel.StartPoint.X, rel.StartPoint.Y ),
                                        new Point( rel.EndPoint.X, rel.StartPoint.Y ),
                                        new Point( rel.EndPoint.X, rel.EndPoint.Y ) 
                                    };
                if ( rel.StartPoint.X < rel.EndPoint.X )
                    regions[0] = new Rectangle( rel.StartPoint.X, rel.StartPoint.Y - 2, Math.Abs( rel.StartPoint.X - rel.EndPoint.X ), 4 );
                else
                    regions[0] = new Rectangle( rel.EndPoint.X, rel.StartPoint.Y - 2, Math.Abs( rel.StartPoint.X - rel.EndPoint.X ), 4 );
        
                
                if ( rel.StartPoint.Y < rel.EndPoint.Y )
                    regions[1] = new Rectangle( rel.EndPoint.X - 2, rel.StartPoint.Y, 4, Math.Abs( rel.EndPoint.Y - rel.StartPoint.Y ) );
                else
                    regions[1] = new Rectangle( rel.EndPoint.X - 2, rel.EndPoint.Y, 4, Math.Abs( rel.EndPoint.Y - rel.StartPoint.Y ) );
            }
            return regions;
        }

        public void Clear()
        {
            graph.Clear( backColor );
        }
     }
}
