using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;

namespace text1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog();
            fileDialog.Title = "WPF Open ifc File Dialog";
            fileDialog.InitialDirectory = "C:\\Users\\10699\\Desktop";
            fileDialog.Filter = "ifc files(*.ifc)|*.ifc|All files（*.*）|*.* ";
            fileDialog.RestoreDirectory = true;//如果值为true，每次打开这个对话框初始目录不随你的选择而改变，是固定的
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = System.IO.Path.GetFullPath(fileDialog.FileName);//获取文件夹路径
            }
            const string fileName = "Ghasem.ifc";
            using (var model = IfcStore.Open(fileName))
            {
                //显示Site
                var ids = from site in model.Instances.OfType<IIfcSite>() select site.GlobalId;
                foreach (var o in ids)
                {
                    string id = o;
                    var theSite = model.Instances.FirstOrDefault<IIfcSite>(d => d.GlobalId == id);
                    ComboBox2.Items.Add(theSite.Name);
                }
                //显示Wall
                ids = from wall in model.Instances.OfType<IIfcWall>() where wall.HasOpenings.Any() select wall.GlobalId;
                foreach (var o in ids)
                {
                    string id = o;
                    var theWall = model.Instances.FirstOrDefault<IIfcWall>(d => d.GlobalId == id);
                    ComboBox1.Items.Add(theWall.Name);
                }
            }
        }

        private void ComboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)  // get all basic properties of the site
        {
            const string fileName = "Ghasem.ifc";
            using (var model = IfcStore.Open(fileName))
            {
                var id1 = from site in model.Instances.OfType<IIfcSite>() where site.Name == ComboBox2.SelectedItem.ToString() select site.GlobalId;
                foreach (var o in id1)
                {
                    string id = o;
                    List<ifcData> data = new List<ifcData>();
                    var theSite = model.Instances.FirstOrDefault<IIfcSite>(d => d.GlobalId == id);
                    var properties = theSite.IsDefinedBy
                        .Where(r => r.RelatingPropertyDefinition is IIfcPropertySet)
                        .SelectMany(r => ((IIfcPropertySet)r.RelatingPropertyDefinition).HasProperties)
                        .OfType<IIfcPropertySingleValue>();
                    foreach (var property in properties)
                    {
                        ifcData ifcData1 = new ifcData();
                        ifcData1.Property = property.Name;
                        ifcData1.Value = property.NominalValue.ToString();
                        data.Add(ifcData1);
                    }
                    DataGrid2.ItemsSource = data;
                }

            }
        }

        private void ComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)  // get all basic properties of the wall
        { 
            const string fileName = "Ghasem.ifc";
            using (var model = IfcStore.Open(fileName))
            {
                var id1 = from wall in model.Instances.OfType<IIfcWall>() where wall.Name == ComboBox1.SelectedItem.ToString() select wall.GlobalId;
                foreach (var o in id1)
                {
                    string id = o;
                    List<ifcData> data = new List<ifcData>();
                    var theWall = model.Instances.FirstOrDefault<IIfcWall>(d => d.GlobalId == id);                  
                    var properties = theWall.IsDefinedBy
                        .Where(r => r.RelatingPropertyDefinition is IIfcPropertySet)
                        .SelectMany(r => ((IIfcPropertySet)r.RelatingPropertyDefinition).HasProperties)
                        .OfType<IIfcPropertySingleValue>();
                    foreach (var property in properties)
                    {
                        ifcData ifcData1 = new ifcData();
                        ifcData1.Property = property.Name;
                        ifcData1.Value = property.NominalValue.ToString();
                        data.Add(ifcData1);
                    }
                    DataGrid1.ItemsSource = data;
                }
            }
        }

 
    }

    
}
