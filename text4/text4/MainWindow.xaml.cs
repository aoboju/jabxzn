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
using Xbim.Ifc4.SharedBldgElements;
using Xbim.Ifc4.PropertyResource;
using Xbim.Ifc4.Kernel;
using Xbim.Ifc4.MeasureResource;
using Xbim.IO;

namespace text4
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
        System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog();
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            fileDialog.Title = "WPF Open ifc File Dialog";
            fileDialog.InitialDirectory = "C:\\Users\\10699\\Desktop";
            fileDialog.Filter = "ifc files(*.ifc)|*.ifc|All files（*.*）|*.* ";
            fileDialog.RestoreDirectory = true;//如果值为true，每次打开这个对话框初始目录不随你的选择而改变，是固定的
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = System.IO.Path.GetFullPath(fileDialog.FileName);//获取文件夹路径
                MessageBox.Show("打开成功");
                using (var model = IfcStore.Open(fileDialog.FileName))
                {
                    //显示Site
                    var ids = from site in model.Instances.OfType<IIfcSite>() select site.GlobalId;
                    foreach (var o in ids)
                    {
                        string id = o;
                        var theSite = model.Instances.FirstOrDefault<IIfcSite>(d => d.GlobalId == id);
                        ComboBox1.Items.Add(theSite.Name);
                    }
                }
            }
        }

        private void ComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (var model = IfcStore.Open(fileDialog.FileName ))
            {
                var id1 = from site in model.Instances.OfType<IIfcSite>() where site.Name == ComboBox1.SelectedItem.ToString() select site.GlobalId;
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
                    DataGrid1.ItemsSource = data;
                }

            }
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            using (var model = IfcStore.Open(fileDialog.FileName, null, null, null, XbimDBAccess.ReadWrite, -1))
            {
                int i = 0;
                var ifcSite = model.Instances.OfType<IIfcSite>();
                var theSite = ifcSite.First();
                var properties = theSite.IsDefinedBy.Where(r => r.RelatingPropertyDefinition is IIfcPropertySet).SelectMany(r => ((IIfcPropertySet)r.RelatingPropertyDefinition).HasProperties).OfType<IIfcPropertySingleValue>();
                using (var txn = model.BeginTransaction("Site modification"))
                {
                    foreach (var property in properties)
                    {
                        property.Name = new IfcLabel ((DataGrid1.Columns[0].GetCellContent (DataGrid1.Items[i]) as TextBlock ).Text ).ToString()  ;
                        property.NominalValue = new IfcLabel((DataGrid1.Columns[1].GetCellContent(DataGrid1.Items[i]) as TextBlock).Text  );
                        i++;
                    }
                    theSite.Name = theSite.Name + "checked";
                    txn.Commit();
                    try
                    {
                        model.SaveAs(fileDialog.FileName);
                        MessageBox.Show("保存成功");
                    }
                    catch
                    {
                    };
                }

            }
        }
    }
}
