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
	public class Form1 : Form
	{
		private ComboBox cb;
		private Button closeButton;
		private Label lbLabel;
		private ListBox lb1;
		private CComponentType [] cTypes;
		private Button changeBtn;
		
		int[] testArr = new int[] {1,2,3,4};
		
		public Form1(){
			this.InitializeComponent();
		}

		private void InitializeComponent()
		{
			this.cb = new ComboBox();
			this.cb.Location = new Point(8, 248);
			this.cb.Size = new Size(280, 21);
			
			this.closeButton = new Button();
			this.closeButton.Name = "Close";
			this.closeButton.Text = "Close";
			this.closeButton.Location = new Point(70, 220);
			this.closeButton.Size = new Size(120, 20);
			this.closeButton.Click += new System.EventHandler(closeButton_Click);
			
			this.changeBtn = new Button();
			this.changeBtn.Text = "Change";
			this.changeBtn.Location = new Point(70, 170);
			this.changeBtn.Size = new Size(120, 25);
			this.changeBtn.Click += new System.EventHandler(changeBtn_Click);
			
			
			this.lbLabel = new Label();
			this.lbLabel.Text = "Select a component to modify";
			this.lbLabel.Location = new Point(35, 40);
			this.lbLabel.Size = new Size(200, 20);
			
			this.lb1 = new ListBox();
			this.lb1.Size = new Size(200, 100);
			this.lb1.Location = new Point(35, 60);

			this.cb.Items.Add(testArr);
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
			NameChanger nc = new NameChanger();
			string newName;
			foreach(CComponentType c in cTypes){
				if(lb1.SelectedItem.ToString() == c.Name.ToString()){
					nc.changeName(c);
					break;
				}
			}
	
		}
		 
	
		public void closeButton_Click(object sender, System.EventArgs e)
		{
			
		}
	}

	
	public class NameChanger{
		
		public NameChanger(){}

		public CComponentType [] grabComps()
		{
			CObjectModel objModel = ABACUSScriptingModule.ABACUSModel;
			CComponentTypeCollection rootComps = objModel.ComponentTypes;
			CComponentType[] allComps = rootComps.Clone();
			
			return allComps;
		}
				
		public void changeName(CComponentType toChange)
		{
			
			toChange.Name = Interaction.InputBox("Enter in the new component type name", "Change Name", toChange.ToString());
	
			
			//message back how many fields changed.
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
			
	
	}
	
  public class mainClass
	{
		public static void Main() 
		{
			CObjectModel objModel = ABACUSScriptingModule.ABACUSModel;
		
			//MessageBoxWPF.Show(objModel.ComponentTypes.TotalCount().ToString()); //returns 8
			
			//MessageBoxWPF.Show(rootComps.TotalCount().ToString());
			Form1 f1 = new Form1();
			NameChanger nb = new NameChanger();
			
			f1.loadComboBox(nb.grabComps());
			f1.Show();
			
			//else prompt for change
			
			//change
			
			
			//confirm
			
			
			/*foreach(CComponentType c in allComps){
			MessageBoxWPF.Show(c.ToString());
			}*/
			//make input box that accepts a string
			
			//the allComps array contains all of the components in the metamodel 
			//we need to get the service offering comp, and then change the property name.
			
		}
	}
}















