//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Diagnostics;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Windows.Forms;

//namespace SentraWinFramework
//{
//public partial class Popup : System.ComponentModel.Component
//{
////Title: Display any usercontrol as a popup menu
////Author: Pascal GANAYE
////Email: pascalcp@ganaye.com
////Environment: VB.NET 2003
////Keywords: Popup, Contextual, Menu, Tooltip
////Level: Beginner
////Description: This class let you show any usercontrol in XP style popup menu.

//public Popup()
//: this(null, null)
//{
//}

//public Popup(Control UserControl)
//: this(UserControl, null)
//{
//InitializeComponent();
//}

//public interface IPopupUserControl
//{
//bool AcceptPopupClosing();
//}

//public enum ePlacement : int
//{
//Left = 1,
//Right = 2,
//Top = 4,
//Bottom = 8,
//TopLeft = Top | Left,
//TopRight = Top | Right,
//BottomLeft = Bottom | Left,
//BottomRight = Bottom | Right
//}
//private bool mResizable = true;
//private Control mUserControl;
//private Control mParent;
//private ePlacement mPlacement = ePlacement.BottomLeft;
//private Color mBorderColor = Color.DarkGray;
//private int mAnimationSpeed = 150;
//private bool mShowShadow = true;

//private PopupForm mForm;

////INSTANT C# NOTE: C# does not support optional parameters. Overloaded method(s) are created above.
////ORIGINAL LINE: Sub new(Optional ByVal UserControl As Control = null, Optional ByVal parent As Control = null)
//public Popup(Control UserControl, Control parent)
//{
//mParent = parent;
//mUserControl = UserControl;
//}

//public void Show()
//{
//// I use a shared variable in PopupForm class level for this ShowShadow
//// because the CreateParams is called from within the form constructor
//// and we need a way to inform the form if a shadow is nescessary or not
//PopupForm.mShowShadow = this.mShowShadow;
//if (mForm != null)
//{
//mForm.DoClose();
//}
//mForm = new PopupForm(this);
//OnDropDown(mParent, new EventArgs());
//}

//// This internal class is a borderless form used to show the popup
//private class PopupForm : Form
//{
//public static bool mShowShadow;
//private bool mClosing;
//private const int BORDER_MARGIN = 1;
//private Timer mTimer;
//private Size mControlSize;
//private Size mWindowSize;
//private Point mNormalPos;
//private Rectangle mCurrentBounds;
//private Popup mPopup;
//private ePlacement mPlacement;
//private System.DateTime mTimerStarted;
//private double mProgress;
//private int mx;
//private int my;
//private bool mResizing;
//internal Panel mResizingPanel;
//private const int CS_DROPSHADOW = 0X20000;
//private static System.Drawing.Image mBackgroundImage;
//public delegate void DropDownEventHandler(object Sender, EventArgs e);
//public event DropDownEventHandler DropDown;
//public delegate void DropDownClosedEventHandler(object Sender, EventArgs e);
//public event DropDownClosedEventHandler DropDownClosed;

//public PopupForm(Popup popup)
//{
//mPopup = popup;
//this.SetStyle(ControlStyles.ResizeRedraw, true);
//FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
//StartPosition = FormStartPosition.Manual;
//this.ShowInTaskbar = false;
//this.DockPadding.All = BORDER_MARGIN;
//mControlSize = mPopup.mUserControl.Size;
//mPopup.mUserControl.Dock = DockStyle.Fill;
//Controls.Add(mPopup.mUserControl);
//mWindowSize.Width = mControlSize.Width + 2 * BORDER_MARGIN;
//mWindowSize.Height = mControlSize.Height + 2 * BORDER_MARGIN;
//Form parentForm = mPopup.mParent.FindForm();
//if (parentForm != null)
//{
//parentForm.AddOwnedForm(this);
//}


//if (mPopup.mResizable)
//{
//mResizingPanel = new Panel();
////TODO: INSTANT C# TODO TASK: Insert the following converted event handlers at the end of the 'InitializeComponent' method for forms, 'Page_Init' for web pages, or into a constructor for other classes:
//mResizingPanel.MouseUp += new MouseEventHandler(mResizingPanel_MouseUp);
//mResizingPanel.MouseMove += new MouseEventHandler(mResizingPanel_MouseMove);
//mResizingPanel.MouseDown += new MouseEventHandler(mResizingPanel_MouseDown);
//if (mBackgroundImage == null)
//{
//System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Popup));
//mBackgroundImage = (System.Drawing.Image)(resources.GetObject("CornerPicture.Image"));
//}
//mResizingPanel.BackgroundImage = mBackgroundImage;
//mResizingPanel.Width = 12;
//mResizingPanel.Height = 12;
//mResizingPanel.BackColor = Color.Red;
//mResizingPanel.Left = mPopup.mUserControl.Width - 15;
//mResizingPanel.Top = mPopup.mUserControl.Height - 15;
//mResizingPanel.Cursor = Cursors.SizeNWSE;
//mResizingPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
//mResizingPanel.Parent = this;
//mResizingPanel.BringToFront();
//}
//mPlacement = mPopup.mPlacement;
//// Try to place the popup at the asked location
//ReLocate();

//// Check if the form is out of the screen
//// And if yes try to adapt the placement
//Rectangle workingArea = Screen.FromControl(mPopup.mParent).WorkingArea;
//if ((mNormalPos.X + this.Width) > workingArea.Right)
//{
//if ((mPlacement & ePlacement.Right) != 0)
//{
//mPlacement = (mPlacement & ~ePlacement.Right) | ePlacement.Left;
//}
//}
//else if (mNormalPos.X < workingArea.Left)
//{
//if ((mPlacement & ePlacement.Left) != 0)
//{
//mPlacement = (mPlacement & ~ePlacement.Left) | ePlacement.Right;
//}
//}
//if (mNormalPos.Y + this.Height > workingArea.Bottom)
//{
//if ((mPlacement & ePlacement.Bottom) != 0)
//{
//mPlacement = (mPlacement & ~ePlacement.Bottom) | ePlacement.Top;
//}
//}
//else if (mNormalPos.Y < workingArea.Top)
//{
//if ((mPlacement & ePlacement.Top) != 0)
//{
//mPlacement = (mPlacement & ~ePlacement.Top) | ePlacement.Bottom;
//}
//}
//if (mPlacement != mPopup.mPlacement)
//{
//ReLocate();
//}

//// Check if the form is still out of the screen
//// If yes just move it back into the screen without changing Placement
//if (mNormalPos.X + mWindowSize.Width > workingArea.Right)
//{
//mNormalPos.X = workingArea.Right - mWindowSize.Width;
//}
//else if (mNormalPos.X < workingArea.Left)
//{
//mNormalPos.X = workingArea.Left;
//}
//if (mNormalPos.Y + mWindowSize.Height > workingArea.Bottom)
//{
//mNormalPos.Y = workingArea.Bottom - mWindowSize.Height;
//}
//else if (mNormalPos.Y < workingArea.Top)
//{
//mNormalPos.Y = workingArea.Top;
//}

//// Initialize the animation
//mProgress = 0;
//if (mPopup.mAnimationSpeed > 0)
//{
//mTimer = new Timer();
//// I always aim 25 images per seconds.. seems to be a good value
//// it looks smooth enough on fast computers and do not drain slower one
////INSTANT C# NOTE: The VB integer division operator \ was replaced 1 time(s) by the regular division operator /
//mTimer.Interval = Convert.ToInt32(System.Math.Floor(1000f / 25f));
//mTimerStarted = System.DateTime.Now;
//mTimer.Tick += new System.EventHandler(Showing);
//mTimer.Start();
//Showing(null, null);
//}
//else
//{
//SetFinalLocation();
//}
//Show();
//mPopup.OnDropDown(mPopup.mParent, new EventArgs());
//}

//public static bool DropShadowSupported()
//{
//OperatingSystem os = Environment.OSVersion;
//return (os.Platform == PlatformID.Win32NT) & os.Version.CompareTo(new Version(5, 1, 0, 0)) >= 0;
//}

//protected override CreateParams CreateParams
//{
//get
//{
//CreateParams parameters = base.CreateParams;
//if (mShowShadow && DropShadowSupported())
//{
//parameters.ClassStyle = parameters.ClassStyle | CS_DROPSHADOW;
//}
//return parameters;
//}
//}

//protected override void Dispose(bool disposing)
//{
//if (disposing)
//{
//if (mTimer != null)
//{
//mTimer.Dispose();
//}
//}
//base.Dispose(disposing);
//}

//private void ReLocate()
//{
////int parent = 0;
//int rW = 0;
//int rH = 0;
//rW = mWindowSize.Width;
//rH = mWindowSize.Height;
//mNormalPos = mPopup.mParent.PointToScreen(new Point());
//switch (mPlacement)
//{
//case ePlacement.Top:
//case ePlacement.TopLeft:
//case ePlacement.TopRight:
//mNormalPos.Y -= rH;
//break;
//case ePlacement.Bottom:
//case ePlacement.BottomLeft:
//case ePlacement.BottomRight:
//mNormalPos.Y += mPopup.mParent.Height;
//break;
//case ePlacement.Left:
//case ePlacement.Right:
////INSTANT C# NOTE: The VB integer division operator \ was replaced 1 time(s) by the regular division operator /
//mNormalPos.Y += Convert.ToInt32(System.Math.Floor((mPopup.mParent.Height - rH) / 2f));
//break;
//}
//switch (mPlacement)
//{
//case ePlacement.Left:
//mNormalPos.X -= rW;
//break;
//case ePlacement.TopRight:
//case ePlacement.BottomRight:
//// nothing
//break;
//case ePlacement.Right:
//mNormalPos.X += mPopup.mParent.Width;
//break;
//case ePlacement.TopLeft:
//case ePlacement.BottomLeft:
//mNormalPos.X += mPopup.mParent.Width - rW;
//break;
//case ePlacement.Top:
//case ePlacement.Bottom:
////INSTANT C# NOTE: The VB integer division operator \ was replaced 1 time(s) by the regular division operator /
//mNormalPos.X += Convert.ToInt32(System.Math.Floor((mPopup.mParent.Width - rW) / 2f));
//break;
//}
//}

//private void Showing(object sender, EventArgs e)
//{
//mProgress = System.DateTime.Now.Subtract(mTimerStarted).TotalMilliseconds / mPopup.mAnimationSpeed;
//if (mProgress >= 1)
//{
//mTimer.Stop();
//mTimer.Tick -= new System.EventHandler(Showing);
//AnimateForm(1);
//}
//else
//{
//AnimateForm(mProgress);
//}

//}

//protected override void OnDeactivate(System.EventArgs e)
//{
//base.OnDeactivate(e);
//if (mClosing == false)
//{
//if (this.mPopup.mUserControl is IPopupUserControl)
//{
//mClosing = ((IPopupUserControl)this.mPopup.mUserControl).AcceptPopupClosing();
//}
//else
//{
//mClosing = true;
//}
//if (mClosing)
//{
//DoClose();
//}
//}
//}

//protected override void OnPaintBackground(PaintEventArgs e)
//{
//e.Graphics.DrawRectangle(new Pen(mPopup.mBorderColor), 0, 0, this.Width - 1, this.Height - 1);
//}

//private void SetFinalLocation()
//{
//mProgress = 1;
//AnimateForm(1);
//Invalidate();
//}

//private void AnimateForm(double Progress)
//{
//double x = 0;
//double y = 0;
//double w = 0;
//double h = 0;
//if (Progress <= 0.1)
//{
//Progress = 0.1;
//}
//switch (mPlacement)
//{
//case ePlacement.Top:
//case ePlacement.TopLeft:
//case ePlacement.TopRight:
//y = 1 - Progress;
//h = Progress;
//break;
//case ePlacement.Bottom:
//case ePlacement.BottomLeft:
//case ePlacement.BottomRight:
//y = 0;
//h = Progress;
//break;
//case ePlacement.Left:
//case ePlacement.Right:
//y = 0;
//h = 1;
//break;
//}
//switch (mPlacement)
//{
//case ePlacement.TopRight:
//case ePlacement.BottomRight:
//case ePlacement.Right:
//x = 0;
//w = Progress;
//break;
//case ePlacement.TopLeft:
//case ePlacement.BottomLeft:
//case ePlacement.Left:
//x = 1 - Progress;
//w = Progress;
//break;
//case ePlacement.Top:
//case ePlacement.Bottom:
//x = 0;
//w = 1;
//break;
//}
//mCurrentBounds.X = mNormalPos.X + System.Convert.ToInt32(x * mControlSize.Width);
//mCurrentBounds.Y = mNormalPos.Y + System.Convert.ToInt32(y * mControlSize.Height);
//mCurrentBounds.Width = System.Convert.ToInt32(w * mControlSize.Width) + 2 * BORDER_MARGIN;
//mCurrentBounds.Height = System.Convert.ToInt32(h * mControlSize.Height) + 2 * BORDER_MARGIN;
//this.Bounds = mCurrentBounds;
//}

//internal void DoClose()
//{
//try
//{
//mPopup.OnDropDownClosed(mPopup.mParent, new EventArgs());
//}
//finally
//{
//mPopup.mUserControl.Parent = null;
//mPopup.mUserControl.Size = mControlSize;
//mPopup.mForm = null;
//Form parentForm = mPopup.mParent.FindForm();
//if (parentForm != null)
//{
//parentForm.RemoveOwnedForm(this);
//}
//parentForm.Focus();
//Close();
//}
//}

//private void mResizingPanel_MouseUp(object sender, MouseEventArgs e)
//{
//mResizing = false;
//Invalidate();
//}

//private void mResizingPanel_MouseMove(object sender, MouseEventArgs e)
//{
//if (mResizing)
//{
//Size s = Size;
//s.Width += (e.X - mx);
//s.Height += (e.Y - my);
//this.Size = s;
//}
//}

//private void mResizingPanel_MouseDown(object sender, MouseEventArgs e)
//{
//if (e.Button == MouseButtons.Left)
//{
//mResizing = true;
//mx = e.X;
//my = e.Y;
//}
//}

//protected override void OnLoad(System.EventArgs e)
//{
//base.OnLoad(e);
//// for some reason setbounds do not work well in the constructor
//this.Bounds = mCurrentBounds;
//}
//}

//protected virtual void OnDropDown(object Sender, EventArgs e)
//{
//if (DropDown != null)
//DropDown(Sender, e);
//}

//protected virtual void OnDropDownClosed(object Sender, EventArgs e)
//{
//if (DropDownClosed != null)
//DropDownClosed(Sender, e);
//}

//#region Public properties and events

//public delegate void DropDownEventHandler(object Sender, EventArgs e);
//public event DropDownEventHandler DropDown;
//public delegate void DropDownClosedEventHandler(object Sender, EventArgs e);
//public event DropDownClosedEventHandler DropDownClosed;

//[DefaultValue(false)]
//public bool Resizable
//{
//get
//{
//return mResizable;
//}
//set
//{
//mResizable = value;
//}
//}

//[Browsable(false)]
//public Control UserControl
//{
//get
//{
//return mUserControl;
//}
//set
//{
//mUserControl = value;
//}
//}

//[Browsable(false)]
//public Control Parent
//{
//get
//{
//return mParent;
//}
//set
//{
//mParent = value;
//}
//}

//[DefaultValue(typeof(ePlacement), "BottomLeft")]
//public ePlacement HorizontalPlacement
//{
//get
//{
//return mPlacement;
//}
//set
//{
//mPlacement = value;
//}
//}

//[DefaultValue(typeof(Color), "DarkGray")]
//public Color BorderColor
//{
//get
//{
//return mBorderColor;
//}
//set
//{
//mBorderColor = value;
//}
//}

//[DefaultValue(true)]
//public bool ShowShadow
//{
//get
//{
//return mShowShadow;
//}
//set
//{
//mShowShadow = value;
//}
//}

//[DefaultValue(150)]
//public int AnimationSpeed
//{
//get
//{
//return mAnimationSpeed;
//}
//set
//{
//mAnimationSpeed = value;
//}
//}
//#endregion

//internal PictureBox CornerPicture;

//}
//}


//namespace Streamline.Windows.CommonControls
//{
//partial class Popup
//{
///// <summary>
///// Required designer variable.
///// </summary>
//private System.ComponentModel.IContainer components = null;

///// <summary>
///// Clean up any resources being used.
///// </summary>
///// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
//protected override void Dispose(bool disposing)
//{
//if (disposing && (components != null))
//{
//components.Dispose();
//}
//base.Dispose(disposing);
//}

//#region Component Designer generated code

///// <summary>
///// Required method for Designer support - do not modify
///// the contents of this method with the code editor.
///// </summary>
//private void InitializeComponent()
//{
//System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Popup));
//this.CornerPicture = new PictureBox();
//((System.ComponentModel.ISupportInitialize)(this.CornerPicture)).BeginInit();
////
//// CornerPicture
////
//this.CornerPicture.Image = ((System.Drawing.Image)(resources.GetObject("CornerPicture.Image")));
//this.CornerPicture.Location = new System.Drawing.Point(17, 17);
//this.CornerPicture.Name = "CornerPicture";
//this.CornerPicture.Size = new System.Drawing.Size(100, 50);
//this.CornerPicture.TabIndex = 0;
//this.CornerPicture.TabStop = false;
//((System.ComponentModel.ISupportInitialize)(this.CornerPicture)).EndInit();

//}

//#endregion
//}
//} 