#ImportDLL Microsoft.VisualBasic.dll
#ImportDLL System.Runtime.dll

using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Markup;
using Microsoft.VisualBasic;
using ABACUS.ObjectModel;
using ABACUS.Messaging;
using ABACUS.WPFUtil;

namespace nameChangeSpace
{
	public static class WasChanged{
		public static bool changed = false;
	}
	
	
	public class Form1 : Form
	{
		private ComboBox cb;
		private Button closeButton;
		private Label lbLabel;
		private ListBox lb1;
		private CComponentType [] cTypes;
		private Button changeBtn;
		private CComponentType selectedComp;
		
		public Form1()
		{
			this.InitializeComponent();
		}

		private void InitializeComponent()
		{
			this.Size = new Size(700, 400);
			this.closeButton = new Button();
			this.closeButton.Name = "Close";
			this.closeButton.Text = "Close (Trying to get this to work lol)";
			this.closeButton.Location = new Point(280, 300);
			this.closeButton.Size = new Size(120, 40);
			this.closeButton.Click += new System.EventHandler(closeButton_Click);
			
			
			this.changeBtn = new Button();
			this.changeBtn.Text = "Change";
			this.changeBtn.Location = new Point(280, 260);
			this.changeBtn.Size = new Size(120, 25);
			this.changeBtn.Click += new System.EventHandler(changeBtn_Click);
			
			
			this.lbLabel = new Label();
			this.lbLabel.Text = "Select a component to modify";
			this.lbLabel.Location = new Point(200, 30);
			this.lbLabel.Size = new Size(200, 20);
			
			this.lb1 = new ListBox();
			this.lb1.Size = new Size(300, 200);
			this.lb1.Location = new Point(180, 50);
			
			this.Text = "Change Component Name";
			
			this.Controls.AddRange(new Control[] {closeButton, lb1, lbLabel, changeBtn});
			
		}
		public void loadComboBox(CComponentType[] compTypes)
		{
			this.cTypes = compTypes;
			lb1.BeginUpdate();
			foreach(CComponentType c in cTypes){
				lb1.Items.Add(c);
			}
			lb1.EndUpdate();
			
		}
		public void changeBtn_Click(object sender, System.EventArgs e)
		{
			foreach(CComponentType c in cTypes){
				if(lb1.SelectedItem.ToString() == c.Name.ToString()){
					selectedComp = c;
					//send selected comp to form2
					Form2 f2 = new Form2();
					f2.loadProps(c);
					break;
				}
			}
		}
		
		public CComponentType getSelectedComp()
		{
			return selectedComp;
		}
		 
		public void closeButton_Click(object sender, System.EventArgs e)
		{
			
		}
	}
	
	public class Form2 : Form{
		//this form is to actually drill down into the properties
		private ListBox propsBox = new ListBox();
		private Button select = new Button();
		private Label title = new Label();
		private CComponentType comp;
		private Button closeButton = new Button();
		
		public Form2()
		{
			InitializeComponent();
		}
		
		private void InitializeComponent()
		{
			this.Size = new Size(700, 400);
			this.propsBox.Size = new Size(300, 200);
			this.propsBox.Location = new Point(180, 50);
		
			this.select.Text = "Change";
			this.select.Location = new Point(280, 260);
			this.select.Size = new Size(120, 25);
			//this.select.Click += new System.EventHandler(changeBtn_Click);
			
			this.closeButton.Text = "Close";
			this.closeButton.Location = new Point (280, 300);
			this.closeButton.Size = new Size(120, 25);
		
			
			this.title = new Label();
			this.title.Text = "Select a property to modify";
			this.title.Location = new Point(200, 30);
			this.title.Size = new Size(200, 20);
			
			this.Controls.AddRange(new Control[] {title, closeButton, propsBox, select});
		
		}
		public void loadProps(CComponentType c)
		{
			this.comp = c;
			if(this.comp.Properties.Count > 0){
				propsBox.BeginUpdate();
				for(int i = 0; i < this.comp.Properties.Count; i++){
					propsBox.Items.Add(this.comp.Properties[i]);
				}
				propsBox.EndUpdate();
				this.Show();
			}
			else{
				MessageBox.Show("This component has no properties!");
			}
		}
		
		private void changeBtn_Click()
		{
			
		}
	}

	
	/*public class NameChanger{
		
		public NameChanger(){}

		public CComponentType [] grabComps()
		{
			CObjectModel objModel = ABACUSScriptingModule.ABACUSModel;
			CComponentTypeCollection rootComps = objModel.ComponentTypes;
			CComponentType[] allComps = rootComps.Clone();
			
			return allComps; //allComps just has the component types. each component type has properties. 
		}
				
		public void changeName(CComponentType toChange)
		{
			string oldName = toChange.Name;
			string newName = Interaction.InputBox("Enter in the new component type name", "Change Name", toChange.ToString());
			toChange.Name = newName;
			MessageBox.Show("Component type " + "\"" + oldName + "\"" + " changed to " + "\"" + newName + "\"");
			WasChanged.changed = true;
		
		}
			
			
		public static void checkMatch(string src)
		{
			CObjectModel objModel = ABACUSScriptingModule.ABACUSModel;
			CComponentTypeCollection rootComps = objModel.ComponentTypes;
			CComponentType[] allComps = rootComps.Clone();
			
			bool match = false;
			foreach(CComponentType c in allComps){
			
				if(c.Name == src){
					MessageBox.Show("There's a match!");
					match = true;
					long eeid = c.EEID;
					//changeName(eeid);
					//confirms that the eeid is returned.
					MessageBox.Show(eeid.ToString());
					ComboBox cb = new ComboBox();
		
					cb.Show();
				}
			}
			if(match == false){
				MessageBox.Show("No component of that name found!", "Error");
			}
		}
			
	
	}*/
	
  public class mainClass
	{
		public static void Main() 
		{
			CObjectModel objModel = ABACUSScriptingModule.ABACUSModel;
			CComponentTypeCollection rootComps = objModel.ComponentTypes;
			CComponentType[] allComps = rootComps.Clone();
			Form1 f1 = new Form1();
			f1.loadComboBox(allComps);
			f1.Show();		
		}
	}
}

